using System.Collections.Generic;
using System.Drawing;
using RHFYP;
using RHFYP.Cards;

namespace GUI
{
    public class GameUi : SimpleUi
    {

        private Game _game;

        /// <summary>
        /// TODO: This should probably just be grabbed from _game
        /// </summary>
        private readonly IDeck _bankCardsDeck;

        // TODO: All of these will change to calls of GetCardsOfClass on a deck.
        private readonly IDeck _treasureCardsDeck;
        private readonly IDeck _victoryCardsDeck;
        private readonly IDeck _buildingsCardsDeck;
        
        private Stack<string> _inputEvents = new Stack<string>();

        public int XResolution { get; set; }
        public int YResolution { get; set; }


        public MapUi Map  { get; set; }
        public BuyDeckUi BuyDeck { get; set; }

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


        #endregion

        public GameUi(Game game)
        {
            XResolution = 4000;
            YResolution = 4000;
            _game = game;
            Location = Point.Empty;

            Map = new MapUi();
            BuyDeck = new BuyDeckUi();
            SubUis.Add(Map);
            SubUis.Add(BuyDeck);

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
        }

        /// <summary>
        /// Draws this Ui onto the <see cref="Graphics"/> object.
        /// </summary>
        /// <param name="g">The <see cref="Graphics"/> object to draw on.</param>
        public override void Draw(Graphics g)
        {
            // NOTE: It might be more effecient to use the form to draw the background and just gid rid of the background property.
            g.FillRectangle(BackgroundBrush, 0, 0, XResolution, YResolution);
            // Draw the child ui's
            base.Draw(g);

            // TODO: Draw player details
            g.DrawString("BOBSAVILLIAN", PlayerNameTextFont, TextBrush, PlayerNameTextPosition.X, PlayerNameTextPosition.Y);

            // TODO: Draw gold amount
            g.DrawString("GOLD: \t\t0", ResourcesTextFont, TextBrush, GoldTextPosition.X, GoldTextPosition.Y);

            // TODO: Draw Managers
            g.DrawString("MANAGERS: \t0", ResourcesTextFont, TextBrush, ManagersTextPosition.X, ManagersTextPosition.Y);

            // TODO: Draw Investments
            g.DrawString("INVESTMENTS: \t0", ResourcesTextFont, TextBrush, InvestmentsTextPosition.X, InvestmentsTextPosition.Y);

            // TODO: See if player has changed and if so update mapviewer
        }
    }
}
