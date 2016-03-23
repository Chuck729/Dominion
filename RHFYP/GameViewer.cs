using System.Collections.Generic;
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
        private readonly IDeck _bankCardsDeck;

        // TODO: All of these will change to calls of GetCardsOfClass on a deck.
        private readonly IDeck _treasureCardsDeck;
        private readonly IDeck _victoryCardsDeck;
        private readonly IDeck _buildingsCardsDeck;

        private readonly int _resX;
        private readonly int _resY;

        private bool _isCardItemMousedOver;
        private Card _cardItemMousedOver;
        private Card _cardItemSelected;
        
        private Stack<string> _inputEvents = new Stack<string>(); 


        public MapViewer Map  { get; set; }

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

        public int YMarginBetweenAvailableCards { get; set; }

        public int XMarginBetweenAvailableCards { get; set; }

        public float AvailableCardsMarginFromRight { get; set; }

        public float AvailableCardsMarginFromTop { get; set; }

        public int BuyBackgroundEllipseSize { get; set; }

        public Brush BuildingCardBackgroundEllipseBrush { get; set; }

        public Pen BuySelectionPen { get; set; }

        public Brush SelectedBuildingCardBackgroundEllipseBrush { get; set; }

        #endregion

        public GameViewer(Form form, Game game, int resX, int resY)
        {
            _game = game;
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
                new Card { Location = new Point(10,10) },
                new Card { Location = new Point(10,10) },
            });

            _buildingsCardsDeck = new TestDeck(new List<Card>
            {
                new Card { Location = new Point(10,10) },
                new Card { Location = new Point(10,10) },
                new Card { Location = new Point(10,10) },
                new Card { Location = new Point(10,10) },
                new Card { Location = new Point(10,10) },
                new Card { Location = new Point(10,10) },
                new Card { Location = new Point(10,10) },
                new Card { Location = new Point(10,10) },
                new Card { Location = new Point(10,10) },
            });

            _treasureCardsDeck = new TestDeck(new List<Card>
            {
                new Card { Location = new Point(10,10) },
                new Card { Location = new Point(10,10) },
                new Card { Location = new Point(10,10) },
            });

            _victoryCardsDeck = new TestDeck(new List<Card>
            {
                new Card { Location = new Point(10,10) },
                new Card { Location = new Point(10,10) },
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

            XMarginBetweenAvailableCards = 96;
            YMarginBetweenAvailableCards = 96;
            AvailableCardsMarginFromRight = 0.05f;
            AvailableCardsMarginFromTop = 0.05f;

            BuyBackgroundEllipseSize = 11;
            BuildingCardBackgroundEllipseBrush = new SolidBrush(Color.FromArgb(40, 50, 45));
            SelectedBuildingCardBackgroundEllipseBrush = new SolidBrush(Color.FromArgb(70, 80, 75));
            BuySelectionPen = new Pen(Color.FromArgb(254, 71, 71), 2);
        }

        public string SendMouseClick()
        {
            // Select a item to buy if the mouse is over them.
            _cardItemSelected = _isCardItemMousedOver ? _cardItemMousedOver : null;

            if (_cardItemSelected != null && !Map.SelectPointMode)
            {
                Map.SelectPointMode = true;
            }
            else if (_cardItemSelected == null)
            {
                Map.SelectPointMode = false;
            }

            return "";
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

            _isCardItemMousedOver = false;
            IDeck[] decksByClass = {_buildingsCardsDeck, _treasureCardsDeck, _victoryCardsDeck};
            for (var cardClass = 0; cardClass < decksByClass.Length; cardClass++)
            {
                var i = 0;
                foreach (var card in decksByClass[cardClass])
                {
                    var cardX = (int) (_resX*(1 - AvailableCardsMarginFromRight) - 32) -
                                XMarginBetweenAvailableCards*cardClass;
                    var cardY = (int) (_resY * AvailableCardsMarginFromTop) + (i * YMarginBetweenAvailableCards);

                    var ellipseRect = new Rectangle(cardX - BuyBackgroundEllipseSize,
                        cardY - BuyBackgroundEllipseSize, 64 + BuyBackgroundEllipseSize * 2,
                        64 + BuyBackgroundEllipseSize * 2);

                    // Select what color to draw the background based on whether or not the item is selected.
                    var ellipseBackgroundBrush = BuildingCardBackgroundEllipseBrush;
                    if (_cardItemSelected != null && (card == _cardItemSelected))
                    {
                        ellipseBackgroundBrush = SelectedBuildingCardBackgroundEllipseBrush;
                    }

                    g.FillEllipse(ellipseBackgroundBrush, ellipseRect);
                    g.DrawImage(Resources.grass, cardX, cardY - 15, 64, 64);
                    g.DrawImage(Resources._base, cardX, 48 + cardY - 15, 64, 32);
                    i++;

                    // Highlight moused over item
                    if (ellipseRect.Contains(CursurLocation))
                    {
                        _isCardItemMousedOver = true;
                        _cardItemMousedOver = card;
                        g.DrawEllipse(BuySelectionPen, ellipseRect);
                    }
                }
            }
        }

    }
}
