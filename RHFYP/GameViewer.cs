﻿using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using RHFYP.Properties;

namespace RHFYP
{
    class GameViewer
    {
        private Game _game;
        private Form _form;

        /// <summary>
        /// TODO: This should probably just be grabbed from _game
        /// </summary>
        private IDeck _bankCardsDeck;
        private IDeck _gameCardsDeck;

        private Point _mapLocation;
        private readonly int _resX = 0;
        private readonly int _resY = 0;

        public MapViewer Map
        { get; set; }

        public Point CursurLocation { get; set; }
        public Point MapCenter { get; set; }

#region Style Properties

        public PointF PlayerNameTextPosition { get; set; }

        public Brush TextBrush { get; set; }

        public Font PlayerNameTextFont { get;  set; }

        public PointF InvestmentsTextPosition { get; set; }

        public PointF ManagersTextPosition { get; set; }

        public PointF GoldTextPosition { get; set; }

        public Font ResourcesTextFont { get; set; }

        public SolidBrush BackgroundBrush { get; set; }

        public float PrecentYMarginBetweenAvailableCards { get; set; }

        public float AvailableCardsMarginFromRight { get; set; }

        #endregion

        public GameViewer(Form form, Game game, int resX, int resY)
        {
            _game = game;
            _mapLocation = new Point(resX / 2, resY / 2);
            _resX = resX;
            _resY = resY;
            _form = form;
            

            Map = new MapViewer();

            MapCenter = new Point(resX / 2, resY / 2);

            // TEMP CREATE FAKE MAP
            Map.MapDeck = new TestDeck(new List<Card>
            {
                new Card { Location = new Point(10,10) },
                new Card { Location = new Point(11,11) },
                new Card { Location = new Point(12,11) },
                new Card { Location = new Point(11,12) },
                new Card { Location = new Point(9, 8) },
                new Card { Location = new Point(9,9) },
                new Card { Location = new Point(8,9) },
                new Card { Location = new Point(10,9) },
            });
            Map.AvailableDeck = new TestDeck(new List<Card>()
            {
                new Card { Location = new Point(0,0) }
            });

            _bankCardsDeck = new TestDeck(new List<Card>
            {
                new Card { Location = new Point(10,10) },
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

            PrecentYMarginBetweenAvailableCards = 0.01f;
            AvailableCardsMarginFromRight = 0.25f;
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

            var curserPosInsideMapX = CursurLocation.X - (MapCenter.X - (Map.Width / 2));
            var curserPosInsideMapY = CursurLocation.Y - (MapCenter.Y - (Map.Height / 2));
            Map.DrawMap(g, MapCenter.X, MapCenter.Y, curserPosInsideMapX, curserPosInsideMapY);

            // Draw the available cards
            int i = 0;
//            foreach (var card in _gameCardsDeck)
//            {
//                //g.DrawImage(Resources.grass, posCardLoc.X, posCardLoc.Y, TileWidth, TileHeight * 2);
//                //g.DrawImage(Resources._base, posCardLoc.X, posCardLoc.Y + TileHeight + TileHeightHalf, TileWidth, TileHeight);
//            }
        }

    }
}
