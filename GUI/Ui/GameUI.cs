﻿using System;
using System.Drawing;
using System.Windows.Forms;
using GUI.Ui.BuyCardUi;
using RHFYP.Cards;
using RHFYP.Interfaces;

namespace GUI.Ui
{

    public sealed class GameUi : SimpleUi
    {

        private const int PlayerPanelXOffset = 25;

        private int _lastGold;
        private int _lastInvestments;
        private int _lastManagers;

        private int _goldAnimationFrame;
        private int _investmentsAnimationFrame;
        private int _managersAnimationFrame;

        private int _lastCurrentPlayer;
        private GameState _lastGameState;

        public GameUi(IGame game, Control mf) : base(game)
        {
            XResolution = 4000;
            YResolution = 4000;

            Location = Point.Empty;

            ButtonPanel = new ButtonPanelUi(game);
            CardInfo = new CardInfoUi(game);
            BuyDeck = new BuyDeckUi(game, CardInfo);
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

            CheckStates();

            if (Game.Players.Count <= 0 || Game.CurrentPlayer < 0 || Game.CurrentPlayer >= Game.Players.Count || Game.GameState == GameState.Ended) return;

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

            if (EndActionsButton.Active)
                EndActionsButton.Active = !player.ActionCardsInHand || player.Investments == 0;
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
            Map.Location = new Point(((width - Map.Width)/2),
                (height - Map.Height)/2);
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
                    AddChildUi(new GameOverUi(Game, CardInfo, Width, Height));
                }
            }

            _lastGameState = Game.GameState;

            EndActionsButton.Active = CheckEndActionsActive();
            PlayAllTreasuresButton.Active = Game.Players[Game.CurrentPlayer].TreasureCardsInHand;

            if (_lastCurrentPlayer != Game.CurrentPlayer)
            {
                PlayAllTreasuresButton.Action = Game.Players[Game.CurrentPlayer].PlayAllTreasures;
                EndActionsButton.Action = Game.Players[Game.CurrentPlayer].EndActions;
                Map.Player = Game.Players[Game.CurrentPlayer];
                _lastCurrentPlayer = Game.CurrentPlayer;
            }
        }

        /// <summary>
        /// Gets called when the size of the parent might have been updated.
        /// </summary>
        /// <param name="parentWidth">The new width of the parent.</param>
        /// <param name="parentHeight">The new height of the parent.</param>
        public override void ParentSizeChanged(int parentWidth, int parentHeight)
        {
            BufferImage = new Bitmap(Math.Max(1, ParentWidth), Math.Max(1, ParentHeight));
            base.ParentSizeChanged(parentWidth, parentHeight);
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