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
    public class MapUi : SimpleUi, IExpandingElement
    {
        private const int TileHeight = 32;
        private const int TileWidth = 64;
        private const int TileHeightHalf = TileHeight/2;
        private const int TileWidthHalf = TileWidth/2;

        private const int BounceAnimationOffset = 20;
        private readonly BuyDeckUi _buyDeckUi;
        private readonly CardInfoUi _cardInfoUi;

        private IDeck _borderDeck = new Deck(null);

        private ICard _currentExpandingTile;

        private int _frameInc = 1;

        private Point _mouseLocation = Point.Empty;

        private ICard TileMouseIsOver
        {
            get { return _tileMouseIsOver; }
            set
            {
                if (_cardInfoUi != null)
                {
                    _cardInfoUi.Card = value;
                }
                _tileMouseIsOver = value;
            }
        }

        private Point _topLeftCoord = Point.Empty;
        private ICard _tileMouseIsOver;

        public MapUi(IGame game, BuyDeckUi buyDeckUi, CardInfoUi cardInfoUi) : base(game)
        {
            // TEMP, show grass for the test card.
            FastSafeImageResource.RegisterImage("TestCard", Resources.grass);

            _buyDeckUi = buyDeckUi;
            _cardInfoUi = cardInfoUi;

            Location = Point.Empty;
            AnimationFrames = 5;

            TrashMode = true;
        }

        private bool SelectPointMode { get; set; }
        private bool TrashMode { get; set; }

        private IDeck DrawDeck => Game.Players[Game.CurrentPlayer].DrawPile;

        private IDeck HandDeck => Game.Players[Game.CurrentPlayer].Hand;

        private IDeck DiscardDeck => Game.Players[Game.CurrentPlayer].DiscardPile;

        /// <summary>
        ///     Number of frames.
        /// </summary>
        public int AnimationFrames { get; }

        /// <summary>
        ///     The current frame.
        /// </summary>
        public int AnimationFrame { get; set; }

        /// <summary>
        ///     True if the current frame equals the number of frames.
        /// </summary>
        public bool Expanded => AnimationFrame == AnimationFrames;

        /// <summary>
        ///     True if the current frame equals 0.
        /// </summary>
        public bool Collapsed => AnimationFrame == 0;

        /// <summary>
        ///     Decides how the current animation frame should be adjusted.
        /// </summary>
        public void AdjustAnimationFrame()
        {
            if (_currentExpandingTile == null) return;
            AnimationFrame += _frameInc;
            if (Expanded || Collapsed)
            {
                _frameInc *= -1;
                if (Collapsed) _currentExpandingTile = null;
            }
        }

        /// <summary>
        ///     Creates a new bitmap that is just big enough to fit the drawn map.
        /// </summary>
        /// <param name="deck">The <see cref="IDeck" /> of cards that should be drawn.</param>
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
            BufferImage = new Bitmap(bitmapMapWidth, bitmapMapHeight + BounceAnimationOffset);
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

        private static bool IsTilePointNotOnTile(Point point, IDeck deck)
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
            mouseY -= BounceAnimationOffset;
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
            AdjustAnimationFrame();

            var mapGraphics = Graphics.FromImage(BufferImage);
            // Extra height for bounce animation
            mapGraphics.TranslateTransform(0, BounceAnimationOffset);
            mapGraphics.SmoothingMode = SmoothingMode.HighQuality;

            TileMouseIsOver = null;

            // Draw the cards in the correct order (low Y first) by removing them from the priority queue;
            while (cardsInDrawOrder.Count > 0)
            {
                var card = cardsInDrawOrder.Dequeue();

                var cardDrawPos = CardDrawPoint(card);
                var imageName = card.ResourceName;
                var imageMod = "";

                if (IsMouseInTile(cardDrawPos, _mouseLocation.X, _mouseLocation.Y))
                {
                    // Appending bright to the image name so is displays a bright "moused over" image.
                    TileMouseIsOver = card;
                    imageMod = TrashMode ? "-red" : "-bright";

                    if (_borderDeck.CardList.Contains(card) && _buyDeckUi?.SelectedCardViewer?.TrackedCard != null)
                    {
                        imageName = _buyDeckUi.SelectedCardViewer.TrackedCard.ResourceName;
                        imageMod = "-superbright";
                    }
                }

                if (!HandDeck.CardList.Contains(card) && !_borderDeck.CardList.Contains(card))
                {
                    if (_buyDeckUi?.SelectedCardViewer?.TrackedCard != card) imageMod = "-dim";
                }

                DrawTileGraphics(mapGraphics, imageName + imageMod, cardDrawPos);
            }

            g.DrawImage(BufferImage, Location.X, Location.Y);
        }

        private Point CardDrawPoint(ICard card)
        {
            var posCardLoc = TileToScreen(card.Location);

            float yMod = 0;
            if (card == _currentExpandingTile)
            {
                yMod = AnimationFunction.EaseInOutCirc(AnimationFrame, 0, BounceAnimationOffset, AnimationFrames);
            }

            return new Point(posCardLoc.X - _topLeftCoord.X, (int) (posCardLoc.Y - _topLeftCoord.Y - yMod));
        }

        private static IDeck CalculateBorderDeck(IDeck allCardsDeck)
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
                borderDeck.AddCard(new BorderCard {Location = surroundingPoint});
            }

            return borderDeck;
        }

        private static void DrawTileGraphics(Graphics g, string tileName, Point location)
        {
            g.DrawImage(FastSafeImageResource.GetTileImageFromName(tileName), location.X, location.Y, TileWidth, TileHeight * 2);
            g.DrawImage(Resources._base, location.X, location.Y + TileHeight + TileHeightHalf, TileWidth, TileHeight);
        }

        private SimplePriorityQueue<ICard> PopulateDecks()
        {
            var priorSpm = SelectPointMode;
            SelectPointMode = _buyDeckUi.SelectedCardViewer.TrackedCard != null;
            if (priorSpm && !SelectPointMode) Location = new Point(Location.X + TileWidth / 2, Location.Y + TileHeight / 2);
            if (!priorSpm && SelectPointMode) Location = new Point(Location.X - TileWidth / 2, Location.Y - TileHeight / 2);

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

        /// <summary>
        ///     If the user clicks a Ui the mouse coords should be sent to each sub Ui.
        ///     The Ui should have event handlers to fire when specific things happen.
        /// </summary>
        /// <param name="x">Mouse click X pos</param>
        /// <param name="y">Mouse click Y pos</param>
        /// <returns>False if the click event should be consitered 'swallowed'.</returns>
        public override bool SendClick(int x, int y)
        {
            if (TileMouseIsOver == null) return true;
            if (SelectPointMode && TileMouseIsOver.Name.Equals("Border Card"))
            {
                if (_buyDeckUi?.SelectedCardViewer?.TrackedCard != null)
                {
                    return
                        !Game.BuyCard(_buyDeckUi.SelectedCardViewer.TrackedCard.Name, Game.Players[Game.CurrentPlayer],
                            TileMouseIsOver.Location.X, TileMouseIsOver.Location.Y);
                }
            }
            else
            {
                if (!Game.Players[Game.CurrentPlayer].PlayCard(TileMouseIsOver)) return false;
                // Set up play animation.
                _currentExpandingTile = TileMouseIsOver;
                AnimationFrame = 0;
                return false;
            }
            return true;
        }
    }
}