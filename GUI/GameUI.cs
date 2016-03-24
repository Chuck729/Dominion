using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GUI.Properties;
using RHFYP;
using RHFYP.Cards;

namespace GUI
{
    public class GameUi : SimpleUi
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
        
        private Stack<string> _inputEvents = new Stack<string>();

        private bool _isCardItemMousedOver;
        private Card _cardItemMousedOver;
        private Card _cardItemSelected;

        public int XResolution { get; set; }
        public int YResolution { get; set; }


        public MapUi Map  { get; set; }

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

        public GameUi(Form form, Game game, int resX, int resY)
        {
            _game = game;
            XResolution = resX;
            YResolution = resY;
            _form = form;

            Map = new MapUi();

            MapCenter = new Point(resX / 2, resY / 2);

            // TEMP CREATE FAKE MAP
            Map.MapDeck = new TestDeck(new List<Card>
            {
                new TestCard { Location = new Point(10,10) },
                new TestCard { Location = new Point(11,11) },
                new TestCard { Location = new Point(12,11) },
                new TestCard { Location = new Point(11,12) },
                new TestCard { Location = new Point(9, 8) },
                new TestCard { Location = new Point(9,9) },
                new TestCard { Location = new Point(8,9) },
                new TestCard { Location = new Point(10,9) },
            });
            Map.AvailableDeck = new TestDeck(new List<TestCard>()
            {
                new TestCard { Location = new Point(0,0) }
            });

            _bankCardsDeck = new TestDeck(new List<TestCard>
            {
                new TestCard { Location = new Point(10,10) },
                new TestCard { Location = new Point(10,10) },
                new TestCard { Location = new Point(10,10) },
            });

            _buildingsCardsDeck = new TestDeck(new List<TestCard>
            {
                new TestCard { Location = new Point(10,10) },
                new TestCard { Location = new Point(10,10) },
                new TestCard { Location = new Point(10,10) },
                new TestCard { Location = new Point(10,10) },
                new TestCard { Location = new Point(10,10) },
                new TestCard { Location = new Point(10,10) },
                new TestCard { Location = new Point(10,10) },
                new TestCard { Location = new Point(10,10) },
                new TestCard { Location = new Point(10,10) },
            });

            _treasureCardsDeck = new TestDeck(new List<TestCard>
            {
                new TestCard { Location = new Point(10,10) },
                new TestCard { Location = new Point(10,10) },
                new TestCard { Location = new Point(10,10) },
            });

            _victoryCardsDeck = new TestDeck(new List<TestCard>
            {
                new TestCard { Location = new Point(10,10) },
                new TestCard { Location = new Point(10,10) },
                new TestCard { Location = new Point(10,10) },
            });

            SetDefaultStyle();
        }

        /// <summary>
        /// Sets the default game viewer style.  Effects colors and fonts potentially.
        /// </summary>
        private void SetDefaultStyle()
        {
            TextBrush = new SolidBrush(Color.WhiteSmoke);

            PlayerNameTextPosition = new PointF(0.020f * 1920, 0.025f * 1080);
            GoldTextPosition = new PointF(0.025f * 1920, 0.08f * 1080);
            ManagersTextPosition = new PointF(0.025f * 1920, 0.10f * 1080);
            InvestmentsTextPosition = new PointF(0.025f * 1920, 0.12f * 1080);

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

        /// <summary>
        /// If the user clicks a Ui the mouse coords should be sent to each sub Ui.
        /// The Ui should have event handlers to fire when specific things happen.
        /// </summary>
        /// <param name="x">Mouse click X pos</param>
        /// <param name="y">Mouse click Y pos</param>
        public override void SendClick(int x, int y)
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
        }

        /// <summary>
        /// If the user presses a key that key gets passed to all sub Ui's.
        /// </summary>
        /// <param name="e"></param>
        public override void SendKey(KeyEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Draws this Ui onto the <see cref="Graphics"/> object.
        /// </summary>
        /// <param name="g">The <see cref="Graphics"/> object to draw on.</param>
        public override void Draw(Graphics g)
        {
            // Draw the child ui's
            base.Draw(g);

            // Draw background.
            // NOTE: It might be more effecient to use the form to draw the background and just gid rid of the background property.
            g.FillRectangle(BackgroundBrush, 0, 0, XResolution, YResolution);

            // TODO: Draw player details
            g.DrawString("BOBSAVILLIAN", PlayerNameTextFont, TextBrush, PlayerNameTextPosition.X, PlayerNameTextPosition.Y);

            // TODO: Draw gold amount
            g.DrawString("GOLD: \t\t0", ResourcesTextFont, TextBrush, GoldTextPosition.X, GoldTextPosition.Y);

            // TODO: Draw Managers
            g.DrawString("MANAGERS: \t0", ResourcesTextFont, TextBrush, ManagersTextPosition.X, ManagersTextPosition.Y);

            // TODO: Draw Investments
            g.DrawString("INVESTMENTS: \t0", ResourcesTextFont, TextBrush, InvestmentsTextPosition.X, InvestmentsTextPosition.Y);

            // TODO: See if player has changed and if so update mapviewer

            var curserPosInsideMapX = CursurLocation.X - (MapCenter.X - (Map.Width / 2));
            var curserPosInsideMapY = CursurLocation.Y - (MapCenter.Y - (Map.Height / 2));
            Map.DrawMap(g, MapCenter.X, MapCenter.Y, curserPosInsideMapX, curserPosInsideMapY);

            // Draw the buyable cards

            _isCardItemMousedOver = false;
            IDeck[] decksByClass = { _buildingsCardsDeck, _treasureCardsDeck, _victoryCardsDeck };
            for (var cardClass = 0; cardClass < decksByClass.Length; cardClass++)
            {
                var i = 0;
                foreach (Card card in decksByClass[cardClass].Cards())
                {
                    var cardX = (int)(XResolution * (1 - AvailableCardsMarginFromRight) - 32) -
                                XMarginBetweenAvailableCards * cardClass;
                    var cardY = (int)(YResolution * AvailableCardsMarginFromTop) + (i * YMarginBetweenAvailableCards);

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
                    if (!ellipseRect.Contains(CursurLocation)) continue;
                    _isCardItemMousedOver = true;
                    _cardItemMousedOver = card;
                    g.DrawEllipse(BuySelectionPen, ellipseRect);
                }
            }
        }
    }
}
