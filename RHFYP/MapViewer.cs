using System.Drawing;

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

        private void CreateNewBitmapToFitMap()
        {
            var maxX = int.MinValue;
            var minX = int.MaxValue;
            var maxY = int.MinValue;
            var minY = int.MaxValue;

            foreach (var card in MapDeck)
            {
                if (card.Location.X > maxX) maxX = card.Location.X;
                else if (card.Location.X < minX) minX = card.Location.X;
                if (card.Location.Y > maxY) maxY = card.Location.Y;
                else if (card.Location.Y < minY) minY = card.Location.Y;
            }

            _topLeftCoord = new Point(minX, minY);
            var xCoordDiff = maxX - minX;
            var yCoordDiff = maxY - minY;
            var bitmapMapWidth = TileWidth + ((xCoordDiff - 1) * (TileWidth / 2));
            var bitmapMapHeight = TileHeight + ((yCoordDiff - 1)*(TileHeight / 2));
            _map = new Bitmap(bitmapMapWidth, bitmapMapHeight);
        }

        public void DrawMap(Graphics g, int centerX, int centerY, int mouseX, int mouseY)
        {
            // Check to see if the decks that this map viewer is watching have changed.
            _mapNeedsRedraw = _mapNeedsRedraw | MapDeck.DeckChanged() | AvailableDeck.DeckChanged();

            if (_mapNeedsRedraw)
            {
                CreateNewBitmapToFitMap();
                var mapGraphics = Graphics.FromImage(_map);

                foreach (var card in MapDeck)
                {
                    // Translate card over so that all coords are positive
                    var posCardLoc = new Point(card.Location.X - _topLeftCoord.X, card.Location.Y - _topLeftCoord.Y);

                    var pixelX = (posCardLoc.X - posCardLoc.Y) * (TileWidth / 2);
                    var pixelY = (posCardLoc.X + posCardLoc.Y) * (TileHeight / 2);

                    
                    mapGraphics.DrawRectangle(Pens.Black, pixelX, pixelY, TileWidth, TileHeight);
                }

                _mapNeedsRedraw = false;
            }

            // Actually draw the map onto the given graphics object, with the center of the map appearing at the given center.
            g.DrawImage(_map, centerX - (_map.Width / 2), centerY - (_map.Height / 2));
        }
    }
}