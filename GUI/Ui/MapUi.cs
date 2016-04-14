using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using GUI.Properties;
using GUI.Ui.BuyCardUi;
using Priority_Queue;
using RHFYP;
using RHFYP.Cards;

namespace GUI.Ui
{
    public class MapUi : SimpleUi
    {
        private const int TileHeight = 32;
        private const int TileWidth = 64;
        private const int TileHeightHalf = TileHeight/2;
        private const int TileWidthHalf = TileWidth/2;
        private readonly CardInfoUi _cardInfoUi;
        private readonly BuyDeckUi _buyDeckUi;
        private bool _isMouseOverValidTile;

        private Point _mouseLocation = Point.Empty;
        private ICard _tileMouseIsOver;
        private Point _topLeftCoord = Point.Empty;

        private IDeck _borderDeck = new Deck(null);
        public MapUi(IGame game, BuyDeckUi buyDeckUi, CardInfoUi cardInfoUi) : base(game)
        {
            // TEMP, show grass for the test card.
            FastSafeImageResource.RegisterImage("TestCard", Resources.grass);

            _buyDeckUi = buyDeckUi;
            _cardInfoUi = cardInfoUi;

            Location = Point.Empty;
        }

        public MapUi(IGame game, BuyDeckUi buyDeckUi, int x, int y) : base(game)
        {
            // TEMP, show grass for the test card.
            FastSafeImageResource.RegisterImage("TestCard", Resources.grass);

            _buyDeckUi = buyDeckUi;

            Location = new Point(x, y);
        }

        public bool SelectPointMode { get; set; }

        public IDeck DrawDeck => Game.Players[Game.CurrentPlayer].DrawPile;

        public IDeck HandDeck => Game.Players[Game.CurrentPlayer].Hand;

        public IDeck DiscardDeck => Game.Players[Game.CurrentPlayer].DiscardPile;

        private void CreateNewBitmapToFitMap(IDeck deck)
        {
            var maxX = int.MinValue;
            var minX = int.MaxValue;
            var maxY = int.MinValue;
            var minY = int.MaxValue;

            foreach (var screenPoint in deck.Cards().Select(card => TileToScreen(card.Location)))
            {
                if (screenPoint.X > maxX) maxX = screenPoint.X;
                if (screenPoint.X < minX) minX = screenPoint.X;
                if (screenPoint.Y > maxY) maxY = screenPoint.Y;
                if (screenPoint.Y < minY) minY = screenPoint.Y;
            }

            _topLeftCoord = new Point(minX, minY);
            var bitmapMapWidth = (maxX - minX) + TileWidth;
            var bitmapMapHeight = (maxY - minY) + TileHeight + TileHeight + TileHeightHalf;
            BufferImage = new Bitmap(bitmapMapWidth, bitmapMapHeight);
        }

        /// <summary>
        ///     Converts a tile point to an isometric pixel coordinate
        /// </summary>
        /// <param name="tilePoint">The tile coords of the tile you want the screen point of.</param>
        /// <returns>The pixel coords of where that tile is in an isometric view.</returns>
        private static Point TileToScreen(Point tilePoint)
        {
            var screenX = (tilePoint.X - tilePoint.Y)*TileWidthHalf;
            var screenY = (tilePoint.X + tilePoint.Y)*TileHeightHalf;
            return new Point(screenX, screenY);
        }

        /// <summary>
        ///     Gets the tile the mouse is over if it is valid.
        /// </summary>
        /// <returns>The tile the selection box is around, or null if no tile is moused over.</returns>
        public ICard GetTileMouseIsOver()
        {
            return _isMouseOverValidTile ? _tileMouseIsOver : null;
        }


        private bool IsTilePointNotOnTile(Point point, IDeck deck)
        {
            return deck.Cards().All(card => card.Location != point);
        }

        /// <summary>
        ///     Checks to see if the mouse is in the isometric tile graphic by calculating the border functions
        ///     and checking if the mouse is on the right side of each one.
        /// </summary>
        /// <param name="positiveCardLocation">The cards location that were checking.</param>
        /// <param name="mouseX">Mouses X Location inside the <see cref="MapUi" /> _map bitmap.</param>
        /// <param name="mouseY">Mouses Y Location inside the <see cref="MapUi" /> _map bitmap.</param>
        /// <returns></returns>
        private static bool IsMouseInTile(Point positiveCardLocation, int mouseX, int mouseY)
        {
            var buttonXDistR = ((mouseX - positiveCardLocation.X - TileWidth)/2);
            var buttonXDistL = ((mouseX - positiveCardLocation.X)/2);
            var yMidLine = positiveCardLocation.Y + TileHeight + TileHeightHalf;

            if (mouseY >= yMidLine + buttonXDistL) return false;
            if (mouseY >= yMidLine - buttonXDistR) return false;
            if (mouseY <= yMidLine - buttonXDistL) return false;
            return mouseY > yMidLine + buttonXDistR;
        }

        /// <summary>
        ///     If the user presses a key that key gets passed to all sub Ui's.
        /// </summary>
        /// <param name="e"></param>
        /// <returns>False if the click event should be consitered 'swallowed'.</returns>
        public override bool SendKey(KeyEventArgs e)
        {
            const int moveAmount = 20;
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (e.KeyCode)
            {
                case Keys.Left:
                    Location = new Point(Location.X - moveAmount, Location.Y);
                    break;
                case Keys.Right:
                    Location = new Point(Location.X + moveAmount, Location.Y);
                    break;
                case Keys.Up:
                    Location = new Point(Location.X, Location.Y - moveAmount);
                    break;
                case Keys.Down:
                    Location = new Point(Location.X, Location.Y + moveAmount);
                    break;
            }

            return base.SendKey(e);
        }

        /// <summary>
        ///     Draws this Ui onto the <see cref="Graphics" /> object.
        /// </summary>
        /// <param name="g">The <see cref="Graphics" /> object to draw on.</param>
        public override void Draw(Graphics g)
        {
            base.Draw(g);


            var cardsInDrawOrder = PopulateDecks();

          
            var mapGraphics = Graphics.FromImage(BufferImage);
            mapGraphics.SmoothingMode = SmoothingMode.HighQuality;

            var wasMouseInValidTile = _isMouseOverValidTile;
            _isMouseOverValidTile = false;

            // Draw the cards in the correct order (low Y first) by removing them from the priority queue;
            while (cardsInDrawOrder.Count > 0)
            {
                var card = cardsInDrawOrder.Dequeue();

                var posCardLoc = TileToScreen(card.Location);
                // Translate card over so that all coords are positive
                posCardLoc = new Point(posCardLoc.X - _topLeftCoord.X, posCardLoc.Y - _topLeftCoord.Y);

                var imageName = card.ResourceName;
                if (IsMouseInTile(posCardLoc, _mouseLocation.X, _mouseLocation.Y))
                {
                    // Appending bright to the image name so is displays a bright "moused over" image.
                    _isMouseOverValidTile = true;
                    _tileMouseIsOver = card;
                    imageName += "-bright";

                    if (SelectPointMode && _borderDeck.Cards().Contains(card))
                    {
                        if (_buyDeckUi?.SelectedCardViewer?.TrackedCard != null)
                            imageName = _buyDeckUi.SelectedCardViewer.TrackedCard.ResourceName + "-superbright";
                    }

                    // Show the card on the info Ui if it's provided.
                    if (_cardInfoUi != null)
                    {
                        _cardInfoUi.Card = card;
                    }
                }

                mapGraphics.DrawImage(FastSafeImageResource.GetTileImageFromName(imageName), posCardLoc.X, posCardLoc.Y,
                    TileWidth, TileHeight*2);
                mapGraphics.DrawImage(Resources._base, posCardLoc.X, posCardLoc.Y + TileHeight + TileHeightHalf,
                    TileWidth, TileHeight);
            }

            if (wasMouseInValidTile && !_isMouseOverValidTile && _cardInfoUi != null)
            {
                _cardInfoUi.Card = null;
            }

            // Actually draw the map onto the given graphics object, with the center of the map appearing at the given center.
            g.DrawImage(BufferImage, Location.X, Location.Y);
        }

        private IDeck CalculateBorderDeck(IDeck allCardsDeck)
        {
            var borderDeck = new Deck();
            var surroundingPoints = new List<Point>();
            foreach (var card in allCardsDeck.Cards())
            {
                var p = new Point(card.Location.X + 1, card.Location.Y);
                if (IsTilePointNotOnTile(p, allCardsDeck)) surroundingPoints.Add(p);
                p = new Point(card.Location.X - 1, card.Location.Y);
                if (IsTilePointNotOnTile(p, allCardsDeck)) surroundingPoints.Add(p);
                p = new Point(card.Location.X, card.Location.Y + 1);
                if (IsTilePointNotOnTile(p, allCardsDeck)) surroundingPoints.Add(p);
                p = new Point(card.Location.X, card.Location.Y - 1);
                if (IsTilePointNotOnTile(p, allCardsDeck)) surroundingPoints.Add(p);
            }
            surroundingPoints = surroundingPoints.Distinct().ToList();

            foreach (var surroundingPoint in surroundingPoints)
            {
                // TODO: Change this to a real card.
                borderDeck.AddCard(new TestCard { Location = surroundingPoint });
            }

           
            return borderDeck;
        }

        public SimplePriorityQueue<ICard> PopulateDecks()
        {
            SelectPointMode = _buyDeckUi.SelectedCardViewer.TrackedCard != null;

            var cardsInDrawOrder = new SimplePriorityQueue<ICard>();

            var allCardsDeck = DrawDeck.AppendDeck(HandDeck.AppendDeck(DiscardDeck));


            if (SelectPointMode)
            {
                _borderDeck = CalculateBorderDeck(allCardsDeck);

                allCardsDeck = allCardsDeck.AppendDeck(_borderDeck);
            }

            // Load all cards into a priority queue.
            foreach (var card in allCardsDeck.Cards())
            {
                cardsInDrawOrder.Enqueue(card, TileToScreen(card.Location).Y);
            }
            CreateNewBitmapToFitMap(allCardsDeck);
            return cardsInDrawOrder;
        } 

        public override bool SendMouseLocation(int x, int y)
        {
            _mouseLocation = new Point(x, y);
            return base.SendMouseLocation(x - Location.X, y - Location.X);
        }
    }
}