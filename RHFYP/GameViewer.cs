using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHFYP
{
    class GameViewer
    {
        private Game _game;

        private Point _mapLocation;
        private readonly int _resX = 0;
        private readonly int _resY = 0;

        private MapViewer _map;

        public GameViewer(Game game, int resX, int resY)
        {
            _game = game;
            _mapLocation = new Point(resX / 2, resY / 2);
            _resX = resX;
            _resY = resY;

            _map = new MapViewer();

            // TEMP CREATE FAKE MAP
            _map.MapDeck = new TestDeck(new List<Card>
            {
                new Card { Location = new Point(0,0) },
                new Card { Location = new Point(1,0) },
                new Card { Location = new Point(1,1) },
                new Card { Location = new Point(0,1) },
                new Card { Location = new Point(-4,1) },
                new Card { Location = new Point(-4,2) },
                new Card { Location = new Point(2,2) },
                new Card { Location = new Point(2,3) },
            });
            _map.AvailableDeck = new TestDeck(new List<Card>()
            {
                new Card { Location = new Point(0,0) }
            });
        }

        /// <summary>
        /// Draws an updated view of the game onto the passed in <see cref="Graphics"/> object.
        /// </summary>
        /// <param name="g"><see cref="Graphics"/> object on which to draw the game.</param>
        public void DrawGame(Graphics g)
        {
            // TODO: Draw player details

            // TODO: See if player has changed and if so update mapviewer
            _map.DrawMap(g, _resX / 2, _resY / 2, 0, 0);
        }
    }
}
