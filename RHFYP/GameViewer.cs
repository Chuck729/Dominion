using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RHFYP.Properties;

namespace RHFYP
{
    class GameViewer
    {
        private Game _game;

        private Point _mapLocation;
        private readonly int _resX = 0;
        private readonly int _resY = 0;

        private MapViewer _map;

#region Style Properties

        public PointF PlayerNameTextPosition { get; set; }

        public Brush TextBrush { get; set; }

        public Font PlayerNameTextFont { get;  set; }

        public PointF InvestmentsTextPosition { get; set; }

        public PointF ManagersTextPosition { get; set; }

        public PointF GoldTextPosition { get; set; }

        public Font ResourcesTextFont { get; set; }

        public SolidBrush BackgroundBrush { get; set; }

        #endregion

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

            SetDefaultStyle();
        }

        /// <summary>
        /// Sets the default game viewer style.  Effects colors and fonts potentially.
        /// </summary>
        private void SetDefaultStyle()
        {
            TextBrush = new SolidBrush(Color.WhiteSmoke);

            PlayerNameTextPosition = new PointF(0.025f, 0.025f);
            GoldTextPosition = new PointF(0.025f, 0.08f);
            ManagersTextPosition = new PointF(0.025f, 0.10f);
            InvestmentsTextPosition = new PointF(0.025f, 0.12f);

            PlayerNameTextFont = new Font("Trebuchet MS", 16, FontStyle.Bold);
            ResourcesTextFont = new Font("Trebuchet MS", 12, FontStyle.Bold);

            BackgroundBrush = new SolidBrush(Color.FromArgb(30, 40, 35));
        }

        /// <summary>
        /// Draws an updated view of the game onto the passed in <see cref="Graphics"/> object.
        /// </summary>
        /// <param name="g"><see cref="Graphics"/> object on which to draw the game.</param>
        public void DrawGame(Graphics g)
        {
            // Draw background.
            // NOTE: It might be more effecient to use the form to draw the background and just gid rid of the background property.
            g.FillRectangle(BackgroundBrush, 0, 0, _resX, _resY);

            // TODO: Draw player details
            g.DrawString("BOBSAVILLIAN", PlayerNameTextFont, TextBrush, PlayerNameTextPosition.X * _resX, PlayerNameTextPosition.Y * _resY);

            // TODO: Draw gold amount
            g.DrawString("GOLD: \t\t0", ResourcesTextFont, TextBrush, GoldTextPosition.X * _resX, GoldTextPosition.Y * _resY);

            // TODO: Draw Managers
            g.DrawString("MANAGERS: \t0", ResourcesTextFont, TextBrush, ManagersTextPosition.X * _resX, ManagersTextPosition.Y * _resY);

            // TODO: Draw Investments
            g.DrawString("INVESTMENTS: \t0", ResourcesTextFont, TextBrush, InvestmentsTextPosition.X * _resX, InvestmentsTextPosition.Y * _resY);

            // TODO: See if player has changed and if so update mapviewer

            _map.DrawMap(g, _resX / 2, _resY / 2, 0, 0);
        }

    }
}
