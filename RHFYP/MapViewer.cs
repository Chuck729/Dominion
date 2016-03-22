using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Priority_Queue;
using RHFYP.Properties;

namespace RHFYP
{
    internal class MapViewer
    {
        private Bitmap _map = new Bitmap(1, 1);
        private Point _topLeftCoord;
        private IDeck _mapDeck;
        private IDeck _availableDeck;
        private bool _mapNeedsRedraw;
        private bool _selectPointMode;
        private bool _isMouseOverValidTile;
        private Card _tileMouseIsOver;

        public MapViewer()
        {
           
        }

        public int Width => _map.Width;

        public int Height => _map.Height;

        public bool SelectPointMode
        {
            internal get
            {
                return _selectPointMode;
            }
            set
            {
                _mapNeedsRedraw = true;
                _selectPointMode = value;
            }
        }

        public IDeck MapDeck {
            internal get
            {
                return _mapDeck;
            }
            set
            {
                _mapNeedsRedraw = true;
                _mapDeck = value;
            }
        }

        public IDeck AvailableDeck
        {
            internal get
            {
                return _availableDeck;
            }
            set
            {
                _mapNeedsRedraw = true;
                _availableDeck = value;
            }
        }


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

            foreach (var screenPoint in deck.Select(card => TileToScreen(card.Location)))
            {
                if (screenPoint.X > maxX) maxX = screenPoint.X;
                if (screenPoint.X < minX) minX = screenPoint.X;
                if (screenPoint.Y > maxY) maxY = screenPoint.Y;
                if (screenPoint.Y < minY) minY = screenPoint.Y;
            }

            _topLeftCoord = new Point(minX, minY);
            var bitmapMapWidth = (maxX - minX) + TileWidth;
            var bitmapMapHeight = (maxY - minY) + TileHeight + TileHeight + TileHeightHalf;
            _map = new Bitmap(bitmapMapWidth, bitmapMapHeight);
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
        /// Converts a screen point to the coord of the tile that's at that point.
        /// </summary>
        /// <param name="screenPoint">The pixel coords, within the map viewers bitmap, where you want to probe.</param>
        /// <returns>The tile coords of the tile at that pixel coord</returns>
        /// <remarks>This doesn't check to see is a tile is actually there or not.</remarks>
        private Point ScreenToTile(Point screenPoint)
        {
            var tileX = (screenPoint.X / TileWidthHalf + screenPoint.Y / TileHeightHalf) / 2;
            var tileY = (screenPoint.Y / TileHeightHalf - (screenPoint.X / TileWidthHalf)) / 2;
            return new Point(tileX, tileY);
        }

        public void DrawMap(Graphics g, int centerX, int centerY, int mouseX, int mouseY)
        {
            // Check to see if the decks that this map viewer is watching have changed.
            _mapNeedsRedraw = _mapNeedsRedraw | MapDeck.DeckChanged() | AvailableDeck.DeckChanged();

            if (_mapNeedsRedraw)
            {
                if (MapDeck.CardCount() == 0) return;
                var cardsInDrawOrder = new SimplePriorityQueue<Card>();

                // Load all cards into a priority queue.
                foreach (var card in MapDeck)
                {
                    cardsInDrawOrder.Enqueue(card, TileToScreen(card.Location).Y);
                }

                var borderDeck = new TestDeck(null);
                if (SelectPointMode)
                {
                    var surroundingPoints = new List<Point>();
                    foreach (var card in MapDeck)
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
                        borderDeck.AddCard(new Card { Location = surroundingPoint });
                    }

                    foreach (var card in borderDeck)
                    {
                        cardsInDrawOrder.Enqueue(card, TileToScreen(card.Location).Y);
                    }
                }

                CreateNewBitmapToFitMap(MapDeck.AppendDeck(borderDeck));
                var mapGraphics = Graphics.FromImage(_map);

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
                    if (!IsMouseInTile(posCardLoc, mouseX, mouseY)) continue;
                    if (!SelectPointMode || borderDeck.Contains(card))
                    {
                        _isMouseOverValidTile = true;
                        _tileMouseIsOver = card;
                        mapGraphics.DrawImage(SelectPointMode ? Resources.placeselection : Resources.selection, posCardLoc.X, posCardLoc.Y, TileWidth, TileHeight * 2);
                    }
                }

                _mapNeedsRedraw = false;
            }

            // Actually draw the map onto the given graphics object, with the center of the map appearing at the given center.
            g.DrawImage(_map, centerX - (_map.Width / 2), centerY - (_map.Height / 2));
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
            return !MapDeck.Any(card => card.Location.Equals(point));
        }

        // TODO: Upload maple doc to explain this maybe?
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


        private static Image GetTileImageFromName(string typeString)
        {
            Resources.ResourceManager.IgnoreCase = true;
            return (Image)Resources.ResourceManager.GetObject(typeString);
        }
    }
}