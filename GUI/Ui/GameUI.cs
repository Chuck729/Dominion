using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using GUI.Ui.Buttons;
using GUI.Ui.BuyCardUi;
using RHFYP.Interfaces;

namespace GUI.Ui
{
    public sealed class GameUi : SimpleUi
    {
        private const int PlayerPanelXOffset = 25;
        public static bool AnimationsOn = true;

        private readonly Action _uiCloseAction;

        private int _goldAnimationFrame;
        private int _investmentsAnimationFrame;

        private int _lastCurrentPlayer;
        private GameState _lastGameState;

        private int _lastGold;
        private int _lastInvestments;
        private int _lastManagers;
        private int _managersAnimationFrame;

        private bool _dialogOpen;

        public GameUi(IGame game, Control mf, Action uiCloseAction) : base(game)
        {
            XResolution = 4000;
            YResolution = 4000;

            _uiCloseAction = uiCloseAction;

            Location = Point.Empty;

            ButtonPanel = new ButtonPanelUi(game);
            CardInfo = new CardInfoUi(game);
            BuyDeck = new BuyDeckUi(game, CardInfo, ButtonPanel);
            Map = new MapUi(game, BuyDeck, CardInfo, ButtonPanel, Game.Players[0], 1.5f);

            // EndActionButton
            EndActionsButton = new ButtonUi(game, "End actions", game.Players[game.CurrentPlayer].EndActions, 180, 25);

            // First turn no players will have action cards in their hand.
            game.Players[game.CurrentPlayer].EndActions();

            // PlayAllTreasuresButton
            PlayAllTreasuresButton = new ButtonUi(game, "Play all treasures",
                game.Players[game.CurrentPlayer].PlayAllTreasures, 180, 25);

            // First turn player WILL have treasure cards.

            // NextTurnButton
            NextTurnButton = new ButtonUi(game, "End Turn", () =>
            {
                game.NextTurn();
                CenterMap(mf.Width, mf.Height);
            }, 180, 25) {Active = true};

            // First turn player will be in buy state already so end turn is available.


            ButtonPanel.AddChildUi(PlayAllTreasuresButton);
            ButtonPanel.AddChildUi(EndActionsButton);
            ButtonPanel.AddChildUi(NextTurnButton);

            AddChildUi(Map);
            AddChildUi(BuyDeck);
            AddChildUi(CardInfo);
            AddChildUi(ButtonPanel, 20, 160);
            SetDefaultStyle();
        }

        public int XResolution { private get; set; }
        public int YResolution { private get; set; }


        private MapUi Map { get; }
        private BuyDeckUi BuyDeck { get; }
        private CardInfoUi CardInfo { get; }
        private ButtonUi EndActionsButton { get; }
        private ButtonUi PlayAllTreasuresButton { get; }
        private ButtonUi NextTurnButton { get; }
        private ButtonPanelUi ButtonPanel { get; }

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

        /// <summary>
        /// Moves the mapUi element if it exists.
        /// </summary>
        /// <param name="dx">The amount the move the map in the X direction.</param>
        /// <param name="dy">The amount the move the map in the Y direction.</param>
        public void MoveMap(int dx, int dy)
        {
            if (Map == null) throw new InvalidOperationException("You need to hook up a MapUi to this GameUi before calling this method.");

            // only allow the map to move when a dialog box is not 'blocking' focus.
            if (!_dialogOpen) Map.Location = new Point(Map.Location.X + dx, Map.Location.Y + dy);
        }

        /// <inheritdoc/>
        public override void Draw(Graphics g, int parentWidth, int parentHeight)
        {
            _dialogOpen = CheckForUserInputRequest();

            // NOTE: It might be more effecient to use the form to draw the background and just gid rid of the background property.
            g.FillRectangle(BackgroundBrush, 0, 0, XResolution, YResolution);
            // Draw the child ui's
            base.Draw(g, parentWidth, parentHeight);

            foreach (var ui in SubUis)
            {
                if (ui is Dialog) continue;
                ui.Draw(g, parentWidth, parentHeight);
            }

            CheckStates();

            if (Game.Players.Count <= 0 || Game.CurrentPlayer < 0 || Game.CurrentPlayer >= Game.Players.Count ||
                Game.GameState == GameState.Ended) return;

            IPlayer player = Game.Players[Game.CurrentPlayer];
            g.DrawString(player.Name,
                PlayerNameTextFont,
                TextBrush,
                PlayerNameTextPosition.X,
                PlayerNameTextPosition.Y);

            if (player.Gold != _lastGold)
            {
                _goldAnimationFrame = 4;
                _lastGold = player.Gold;
            }

            if (player.Investments != _lastInvestments)
            {
                _investmentsAnimationFrame = 4;
                _lastInvestments = player.Investments;
            }

            if (player.Managers != _lastManagers)
            {
                _managersAnimationFrame = 4;
                _lastManagers = player.Managers;
            }

            _goldAnimationFrame = Math.Max(0, _goldAnimationFrame - 1);
            _investmentsAnimationFrame = Math.Max(0, _investmentsAnimationFrame - 1);
            _managersAnimationFrame = Math.Max(0, _managersAnimationFrame - 1);

            g.DrawString("GOLD: \t\t" + AddSpaces(_goldAnimationFrame, player.Gold.ToString()),
                ResourcesTextFont,
                TextBrush,
                GoldTextPosition.X,
                GoldTextPosition.Y);

            g.DrawString("MANAGERS: \t" + AddSpaces(_managersAnimationFrame, player.Managers.ToString()),
                ResourcesTextFont,
                TextBrush,
                ManagersTextPosition.X,
                ManagersTextPosition.Y);

            g.DrawString("INVESTMENTS: \t" + AddSpaces(_investmentsAnimationFrame, player.Investments.ToString()),
                ResourcesTextFont,
                TextBrush,
                InvestmentsTextPosition.X,
                InvestmentsTextPosition.Y);

            EndActionsButton.Active = player.ActionCardsInHand && player.Investments != 0;

            // draw a shade over all of the game if theres a dialog box
            if (_dialogOpen) g.FillRectangle(new SolidBrush(Color.FromArgb(100, 0, 0, 0)), 0, 0, XResolution, YResolution);

            foreach (var ui in SubUis)
            {
                if (!(ui is Dialog)) continue;
                ui.Draw(g, parentWidth, parentHeight);
                CardInfo.Card = Game.PublicCardForUiUserInput;
            }
        }

        /// <summary>
        /// Checks the game to see if it has any user input requests.
        /// </summary>
        /// <returns>True if there is a pending request (an open dialog box).</returns>
        private bool CheckForUserInputRequest()
        {
            if (Game.NeedUserInput && !SubUis.Exists(x => x is Dialog) && !Map.SelectCardInHandMode)
            {
                switch (Game.PossibleUserResponses.Count)
                {
                    case 1:
                        if (Game.PossibleUserResponses.Contains(UserResponse.CardInHand))
                        {
                            Map.SelectCardInHandMode = true;
                        }
                        break;
                    case 2:
                        if (Game.PossibleUserResponses.Contains(UserResponse.Yes) && Game.PossibleUserResponses.Contains(UserResponse.No))
                        {
                            var dialog = new CardYesNoDialog(Game, Game.UserInputPrompt, Game.PublicCardForUiUserInput);
                            AddChildUi(dialog);
                        }
                        if (Game.PossibleUserResponses.Contains(UserResponse.Discard) && Game.PossibleUserResponses.Contains(UserResponse.PutOnDeck))
                        {
                            var dialog = new CardDiscardPutOnDeckDialog(Game, Game.UserInputPrompt, Game.PublicCardForUiUserInput);
                            AddChildUi(dialog);
                        }
                        if (Game.PossibleUserResponses.Contains(UserResponse.Trash) && Game.PossibleUserResponses.Contains(UserResponse.Steal))
                        {
                            var dialog = new CardTrashStealDialog(Game, Game.UserInputPrompt, Game.PublicCardForUiUserInput);
                            AddChildUi(dialog);
                        }
                        break;
                }
            }
            else if (!Game.NeedUserInput && SubUis.Exists(x => x is Dialog))
            {
                RemoveChildUiWhere(x => x is Dialog);
            }

            return SubUis.Exists(x => x is Dialog);
        }

        private static string AddSpaces(int numSpaces, string str)
        {
            var spaces = "";
            for (var i = 0; i < numSpaces; i++)
            {
                spaces += " ";
            }
            return spaces + str;
        }


        public void CenterMap(int width, int height)
        {
            Map.Location = new Point((width - Map.Width)/2,
                (height - Map.Height)/2);
            Map.Zoom = 1.0f;
        }

        private bool CheckEndActionsActive()
        {
            if (Game.Players[Game.CurrentPlayer].PlayerState != PlayerState.Action) return false;
            if (Game.Players[Game.CurrentPlayer].ActionCardsInHand) return true;
            Game.Players[Game.CurrentPlayer].EndActions();
            return false;
        }

        private void CheckStates()
        {
            if (Game.GameState == GameState.Ended)
            {
                if (_lastGameState != GameState.Ended)
                {
                    ClearChildUis();
                    AddChildUi(new GameOverUi(Game, CardInfo, Width, Height, _uiCloseAction));
                }
            }

            _lastGameState = Game.GameState;

            EndActionsButton.Active = CheckEndActionsActive();
            PlayAllTreasuresButton.Active = Game.Players[Game.CurrentPlayer].TreasureCardsInHand;

            if (_lastCurrentPlayer == Game.CurrentPlayer) return;
            PlayAllTreasuresButton.Action = Game.Players[Game.CurrentPlayer].PlayAllTreasures;
            EndActionsButton.Action = Game.Players[Game.CurrentPlayer].EndActions;
            Map.Player = Game.Players[Game.CurrentPlayer];
            _lastCurrentPlayer = Game.CurrentPlayer;
        }

        /// <inheritdoc />
        public override bool SendClick(int x, int y)
        {
            // Only allow input to go to dialog box if there is one.
            return !_dialogOpen ? base.SendClick(x, y) : SubUis.OfType<Dialog>().Select(ui => ui.SendClick(x, y)).FirstOrDefault();
        }

        /// <inheritdoc />
        public override bool SendMouseLocation(int x, int y)
        {
            // Only allow input to go to dialog box if there is one.
            return !_dialogOpen ? base.SendMouseLocation(x, y) : SubUis.OfType<Dialog>().Select(ui => ui.SendMouseLocation(x, y)).FirstOrDefault();
        }

        /// <inheritdoc />
        public override bool SendKey(KeyEventArgs e)
        {
            // Only allow input to go to dialog box if there is one.
            return !_dialogOpen ? base.SendKey(e) : SubUis.OfType<Dialog>().Select(ui => ui.SendKey(e)).FirstOrDefault();
        }

        #region Style Properties

        private PointF PlayerNameTextPosition { get; set; }

        private Brush TextBrush { get; set; }

        private Font PlayerNameTextFont { get; set; }

        private PointF InvestmentsTextPosition { get; set; }

        private PointF ManagersTextPosition { get; set; }

        private PointF GoldTextPosition { get; set; }

        private Font ResourcesTextFont { get; set; }

        private SolidBrush BackgroundBrush { get; set; }

        #endregion
    }
}