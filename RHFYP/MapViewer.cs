using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using RHFYP.Properties;

namespace RHFYP
{
    internal class MapViewer
    {
        private Bitmap _map;
        private Point _topLeftCoord;
        private IDeck _mapDeck;
        private IDeck _availableDeck;
        private bool _mapNeedsRedraw; 

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

        private void CreateNewBitmapToFitMap()
        {
            var maxX = int.MinValue;
            var minX = int.MaxValue;
            var maxY = int.MinValue;
            var minY = int.MaxValue;

            foreach (var card in MapDeck)
            {
                var screenPoint = TileToScreen(card.Location);
                if (screenPoint.X > maxX) maxX = screenPoint.X;
                if (screenPoint.X < minX) minX = screenPoint.X;
                if (screenPoint.Y > maxY) maxY = screenPoint.Y;
                if (screenPoint.Y < minY) minY = screenPoint.Y;
            }

            _topLeftCoord = new Point(minX, minY);
            var bitmapMapWidth = (maxX - minX) + TileWidth;
            var bitmapMapHeight = (maxY - minY) + TileHeight;
            _map = new Bitmap(bitmapMapWidth, bitmapMapHeight);
        }

        /// <summary>
        /// Converts a tile point to an isometric pixel coordinate
        /// </summary>
        /// <param name="tilePoint">The tile coords of the tile you want the screen point of.</param>
        /// <returns>The pixel coords of where that tile is in an isometric view.</returns>
        private Point TileToScreen(Point tilePoint)
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
                CreateNewBitmapToFitMap();
                var mapGraphics = Graphics.FromImage(_map);

                // TODO: Sort cards so they draw top to bottom.
                foreach (var card in MapDeck)
                {
                    // Translate card over so that all coords are positive
                    var posCardLoc = TileToScreen(card.Location);
                    posCardLoc = new Point(posCardLoc.X - _topLeftCoord.X, posCardLoc.Y - _topLeftCoord.Y);

                    mapGraphics.DrawImage(Resources.grass, posCardLoc.X, posCardLoc.Y, TileWidth, TileHeight * 2);
                }

                _mapNeedsRedraw = false;
            }

            // Actually draw the map onto the given graphics object, with the center of the map appearing at the given center.
            g.DrawImage(_map, centerX - (_map.Width / 2), centerY - (_map.Height / 2));
        }
    }
}