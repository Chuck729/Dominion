using System.Drawing;
using GUI.Ui.BuyCardUi;
using RHFYP;

namespace GUI.Ui
{

    public class GameUi : SimpleUi
    {

        private const int PlayerPanelXOffset = 25;

        public GameUi(IGame game) : base(game)
        {
            XResolution = 4000;
            YResolution = 4000;

            Location = Point.Empty;

            CardInfo = new CardInfoUi(game);
            BuyDeck = new BuyDeckUi(game, CardInfo);
            Map = new MapUi(game, BuyDeck, CardInfo);
            ButtonBuyAllTreasuresButton = new ButtonUi(game, "Play all treasures", () =>
            {
                ButtonBuyAllTreasuresButton.Active = false;
            }, 180, 25);
            NextTurnButton = new ButtonUi(game, "Next Turn", game.NextTurn, 180, 25);

            AddChildUi(Map);
            AddChildUi(BuyDeck);
            AddChildUi(CardInfo);
            AddChildUi(ButtonBuyAllTreasuresButton, PlayerPanelXOffset, 160);
            AddChildUi(NextTurnButton, PlayerPanelXOffset, 190);

            SetDefaultStyle();
        }

        public int XResolution { private get; set; }
        public int YResolution { private get; set; }


        private MapUi Map { get; }
        private BuyDeckUi BuyDeck { get; }
        public CardInfoUi CardInfo { get; }
        private ButtonUi ButtonBuyAllTreasuresButton { get; }
        private ButtonUi NextTurnButton { get; }

        public Point MouseLocation { get; set; }


        /// <summary>
        ///     Sets the default Game viewer style.  Effects colors and fonts potentially.
        /// </summary>
        private void SetDefaultStyle()
        {
            TextBrush = new SolidBrush(Color.LightGray);

            PlayerNameTextPosition = new PointF(PlayerPanelXOffset, 0.025f*1080);
            GoldTextPosition = new PointF(PlayerPanelXOffset, 0.08f*1080);
            ManagersTextPosition = new PointF(PlayerPanelXOffset, 0.10f*1080);
            InvestmentsTextPosition = new PointF(PlayerPanelXOffset, 0.12f*1080);

            PlayerNameTextFont = new Font("Trebuchet MS", 16, FontStyle.Bold);
            ResourcesTextFont = new Font("Trebuchet MS", 12, FontStyle.Bold);

            BackgroundBrush = new SolidBrush(Color.FromArgb(30, 40, 35));
        }

        public void MoveMap(int dx, int dy)
        {
            Map.Location = new Point(Map.Location.X + dx, Map.Location.Y + dy);
        }

        /// <summary>
        ///     Draws this Ui onto the <see cref="Graphics" /> object.
        /// </summary>
        /// <param name="g">The <see cref="Graphics" /> object to draw on.</param>
        public override void Draw(Graphics g)
        {
            // NOTE: It might be more effecient to use the form to draw the background and just gid rid of the background property.
            g.FillRectangle(BackgroundBrush, 0, 0, XResolution, YResolution);
            // Draw the child ui's
            base.Draw(g);

            if (Game.Players.Count <= 0 || Game.CurrentPlayer < 0 || Game.CurrentPlayer >= Game.Players.Count) return;

            IPlayer player = Game.Players[Game.CurrentPlayer];
            g.DrawString(player.Name,
                PlayerNameTextFont,
                TextBrush,
                PlayerNameTextPosition.X,
                PlayerNameTextPosition.Y);

            g.DrawString("GOLD: \t\t" + player.Gold,
                ResourcesTextFont,
                TextBrush,
                GoldTextPosition.X,
                GoldTextPosition.Y);

            g.DrawString("MANAGERS: \t" + player.Managers,
                ResourcesTextFont,
                TextBrush,
                ManagersTextPosition.X,
                ManagersTextPosition.Y);

            g.DrawString("INVESTMENTS: \t" + player.Investments,
                ResourcesTextFont,
                TextBrush,
                InvestmentsTextPosition.X,
                InvestmentsTextPosition.Y);
        }


        public void CenterMap(int width, int height)
        {
            Map.Location = new Point(((width - BufferImage.Width - Map.Width)/2),
                (height - BufferImage.Height - Map.Height)/2);
        }

        public void AdjustSidebar(int width, int height)
        {
            BuyDeck.AdjustSizeAndPosition(width, height);
        }

        public void DisplayCardInfo(ICard card)
        {
            if (CardInfo != null)
            {
                CardInfo.Card = card;
            }
        }

        public void ClearCardInfo()
        {
            if (CardInfo != null)
            {
                CardInfo.Card = null;
            }
        }

        #region Style Properties

        public PointF PlayerNameTextPosition { get; set; }

        public Brush TextBrush { get; set; }

        public Font PlayerNameTextFont { get; set; }

        public PointF InvestmentsTextPosition { get; set; }

        public PointF ManagersTextPosition { get; set; }

        public PointF GoldTextPosition { get; set; }

        public Font ResourcesTextFont { get; set; }

        public SolidBrush BackgroundBrush { get; set; }

        #endregion
    }
}