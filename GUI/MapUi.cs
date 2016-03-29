using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using GUI.Properties;
using Priority_Queue;
using RHFYP;
using RHFYP.Cards;

namespace GUI
{
    public class MapUi : SimpleUi
    {
        private readonly Dictionary<string, Image> _registeredImages = new Dictionary<string, Image>();

        private Point _mouseLocation = Point.Empty;

        private Point _topLeftCoord;
    
        private bool _isMouseOverValidTile;
        private Card _tileMouseIsOver;

        public MapUi()
        {
            // TEMP, show grass for the test card.
            _registeredImages.Add("TestCard", Resources.grass);

            Location = Point.Empty;
        }

        public MapUi(int x, int y)
        {
            // TEMP, show grass for the test card.
            _registeredImages.Add("TestCard", Resources.grass);

            Location = new Point(x, y);
        }

        public bool SelectPointMode { get; set; }

        public IDeck MapDeck { get; set; }

        public IDeck AvailableDeck { get; set; }


        private const int TileHeight = 32;
        private const int TileWidth = 64;
        private const int TileHeightHalf = TileHeight / 2;
        private const int TileWidthHalf = TileWidth / 2;
            
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
        /// Converts a tile point to an isometric pixel coordinate
        /// </summary>
        /// <param name="tilePoint">The tile coords of the tile you want the screen point of.</param>
        /// <returns>The pixel coords of where that tile is in an isometric view.</returns>
        private static Point TileToScreen(Point tilePoint)
        {
            var screenX = (tilePoint.X - tilePoint.Y) * TileWidthHalf;
            var screenY = (tilePoint.X + tilePoint.Y) * TileHeightHalf;
            return new Point(screenX, screenY);
        }

        /// <summary>
        /// Gets the tile the mouse is over if it is valid.
        /// </summary>
        /// <returns>The tile the selection box is around, or null if no tile is moused over.</returns>
        public Card GetTileMouseIsOver()
        {
            return _isMouseOverValidTile ? _tileMouseIsOver : null;
        }


        private bool IsTilePointNotOnTile(Point point)
        {
            return MapDeck.Cards().All(card => card.Location != point);
        }

        /// <summary>
        /// Checks to see if the mouse is in the isometric tile graphic by calculating the border functions
        /// and checking if the mouse is on the right side of each one.
        /// </summary>
        /// <param name="positiveCardLocation">The cards location that were checking.</param>
        /// <param name="mouseX">Mouses X Location inside the <see cref="MapUi"/> _map bitmap.</param>
        /// <param name="mouseY">Mouses Y Location inside the <see cref="MapUi"/> _map bitmap.</param>
        /// <returns></returns>
        private static bool IsMouseInTile(Point positiveCardLocation, int mouseX, int mouseY)
        {
            var buttonXDistR = ((mouseX - positiveCardLocation.X - TileWidth) / 2);
            var buttonXDistL = ((mouseX - positiveCardLocation.X) / 2);
            var yMidLine = positiveCardLocation.Y + TileHeight + TileHeightHalf;

            if (mouseY >= yMidLine + buttonXDistL) return false;
            if (mouseY >= yMidLine - buttonXDistR) return false;
            if (mouseY <= yMidLine - buttonXDistL) return false;
            return mouseY > yMidLine + buttonXDistR;
        }

        // TODO: Add docs
        private Image GetTileImageFromName(string cardName)
        {
            if (_registeredImages.ContainsKey(cardName)) return _registeredImages[cardName];

            try
            {
                var img = (Image) Resources.ResourceManager.GetObject(cardName);
                _registeredImages.Add(cardName, img ?? Resources.error);
            }
            catch (Exception)
            {
                _registeredImages.Add(cardName, Resources.error);
            }

            return _registeredImages[cardName];
        }

        /// <summary>
        /// If the user presses a key that key gets passed to all sub Ui's.
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
        /// Draws this Ui onto the <see cref="Graphics"/> object.
        /// </summary>
        /// <param name="g">The <see cref="Graphics"/> object to draw on.</param>
        public override void Draw(Graphics g)
        {
            base.Draw(g);

                var cardsInDrawOrder = new SimplePriorityQueue<Card>();

            // Load all cards into a priority queue.
            foreach (var card in MapDeck.Cards())
            {
                cardsInDrawOrder.Enqueue(card, TileToScreen(card.Location).Y);
            }

            var borderDeck = new TestDeck(null);
            if (SelectPointMode)
            {
                var surroundingPoints = new List<Point>();
                foreach (var card in MapDeck.Cards())
                {
                    var p = new Point(card.Location.X + 1, card.Location.Y);
                    if (IsTilePointNotOnTile(p)) surroundingPoints.Add(p);
                    p = new Point(card.Location.X - 1, card.Location.Y);
                    if (IsTilePointNotOnTile(p)) surroundingPoints.Add(p);
                    p = new Point(card.Location.X, card.Location.Y + 1);
                    if (IsTilePointNotOnTile(p)) surroundingPoints.Add(p);
                    p = new Point(card.Location.X, card.Location.Y - 1);
                    if (IsTilePointNotOnTile(p)) surroundingPoints.Add(p);
                }
                surroundingPoints = surroundingPoints.Distinct().ToList();

                foreach (var surroundingPoint in surroundingPoints)
                {
                    // TODO: Change this to a real card.
                    borderDeck.AddCard(new TestCard { Location = surroundingPoint });
                }

                foreach (var card in borderDeck.Cards())
                {
                    cardsInDrawOrder.Enqueue(card, TileToScreen(card.Location).Y);
                }
            }

            CreateNewBitmapToFitMap(MapDeck.AppendDeck(borderDeck));
            var mapGraphics = Graphics.FromImage(BufferImage);

            _isMouseOverValidTile = false;
            // Draw the cards in the correct order (low Y first) by removing them from the priority queue;
            while (cardsInDrawOrder.Count > 0)
            {
                var card = cardsInDrawOrder.Dequeue();

                var posCardLoc = TileToScreen(card.Location);
                // Translate card over so that all coords are positive
                posCardLoc = new Point(posCardLoc.X - _topLeftCoord.X, posCardLoc.Y - _topLeftCoord.Y);

                mapGraphics.DrawImage(GetTileImageFromName(card.Name), posCardLoc.X, posCardLoc.Y, TileWidth, TileHeight * 2);
                mapGraphics.DrawImage(Resources._base, posCardLoc.X, posCardLoc.Y + TileHeight + TileHeightHalf, TileWidth, TileHeight);

                // Draw selection box over tile
                if (!IsMouseInTile(posCardLoc, _mouseLocation.X, _mouseLocation.Y)) continue;
                if (!SelectPointMode || borderDeck.Cards().Contains(card))
                {
                    _isMouseOverValidTile = true;
                    _tileMouseIsOver = card;
                    mapGraphics.DrawImage(SelectPointMode ? Resources.placeselection : Resources.selection, posCardLoc.X, posCardLoc.Y, TileWidth, TileHeight * 2);
                }
            }      

            // Actually draw the map onto the given graphics object, with the center of the map appearing at the given center.
            g.DrawImage(BufferImage, Location.X, Location.Y);
        }

        public override bool SendMouseLocation(int x, int y)
        {
            _mouseLocation = new Point(x,y);
            return base.SendMouseLocation(x - Location.X, y - Location.X);
        }

        public void CenterMap(int inWidth, int inHeight)
        {
            Location = new Point((inWidth - Width) / 2, (inHeight - Height) / 2);
        }
    }
}