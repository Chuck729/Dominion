﻿using System;
using System.Drawing;
using RHFYP.Interfaces;

namespace GUI.Ui
{
    public sealed class GameOverUi : SimpleUi, IExpandingElement
    {
        private const int FrameDelay = 2;

        private readonly MapUi[] _maps;
        private int _currentFrameDelay;
        private int _currentPlayer = -1;
        private bool _doneAnimating;
        private int _winningPlayer;

        public GameOverUi(IGame game, CardInfoUi cardInfoUi, int width, int height, Action uiCloseAction) : base(game)
        {
            AnimationFrames = 15;
            AnimationFrame = 5;
            GameOverFont = new Font("Trebuchet MS", 36, FontStyle.Bold);
            PlayerNameFont = new Font("Trebuchet MS", 20, FontStyle.Bold);
            VictoryPointsFont = new Font("Trebuchet MS", 12, FontStyle.Bold);
            FontBrush = new SolidBrush(Color.WhiteSmoke);
            WinnerFontBrush = new SolidBrush(Color.IndianRed);

            _maps = new MapUi[game.Players.Count];
            for (var i = 0; i < game.Players.Count; i++)
            {
                _maps[i] = new MapUi(game, null, cardInfoUi, null, Game.Players[i]);
                _maps[i].IgnoreShading();
            }

            BufferImage = new Bitmap(width, height);

            var exitGameButton = new ButtonUi(game, "Exit", uiCloseAction, 200, 50); 
            AddChildUi(exitGameButton, (width - 200) / 2, Height - 70);      
        }

        private const float MapYPrecent = 0.5f;
        private const float PlayerNameYPrecent = 0.20f;
        private const float VictoryPointsYPrecent = 0.70f;

        private Brush FontBrush { get; set; }
        private Brush WinnerFontBrush { get; set; }

        private Font GameOverFont { get; set; }

        /// <summary>
        ///     Number of frames.
        /// </summary>
        public int AnimationFrames { get; }

        /// <summary>
        ///     The current frame.
        /// </summary>
        public int AnimationFrame { get; set; }

        /// <summary>
        ///     True if the current frame equals the number of frames.
        /// </summary>
        public bool Expanded => AnimationFrame == AnimationFrames;

        /// <summary>
        ///     True if the current frame equals 0.
        /// </summary>
        public bool Collapsed => AnimationFrame == 0;

        /// <summary>
        ///     Decides how the current animation frame should be adjusted.
        /// </summary>
        public void AdjustAnimationFrame()
        {
            if (_doneAnimating) return;

            AnimationFrame = Math.Min(AnimationFrames, AnimationFrame + 1);

            if (!Expanded) return;

            _currentFrameDelay++;

            if (_currentFrameDelay <= FrameDelay) return;

            if (Game.Players[Math.Max(0, _currentPlayer)].Winner) _winningPlayer = _currentPlayer;
            AnimationFrame = 0;
            _currentFrameDelay = 0;
            _currentPlayer++;

            if (_currentPlayer < Game.Players.Count)
            {
                _maps[_currentPlayer].Draw(Graphics.FromImage(new Bitmap(1, 1)));
                var xCorner = XCenter(_currentPlayer) - _maps[_currentPlayer].Width/2;
                
                var yCorner = (MapYPrecent * ParentHeight) - _maps[_currentPlayer].Height / 2.0;

                AddChildUi(_maps[_currentPlayer], xCorner, (int) yCorner);
                return;
            }

            _currentPlayer = _winningPlayer;
            _doneAnimating = true;
        }

        /// <summary>
        ///     Draws this Ui onto the <see cref="Graphics" /> object.
        /// </summary>
        /// <param name="g">The <see cref="Graphics" /> object to draw on.</param>
        public override void Draw(Graphics g)
        {
            AdjustAnimationFrame();
            var gameOverTextMeasure = g.MeasureString("Game Over", GameOverFont);
            g.DrawString("Game Over", GameOverFont, FontBrush, (ParentWidth - gameOverTextMeasure.Width) / 2, 50);

            // Go through all the players up to the player thats currently being animated.
            for (var i = 0; i < (_doneAnimating ? Game.Players.Count : _currentPlayer); i++)
            {
                var playerStringMeasure= g.MeasureString(Game.Players[i].Name, PlayerNameFont);
                var xCorner = XCenter(i) - playerStringMeasure.Width/2;
                var winner = Game.Players[Game.CurrentPlayer].Winner;
                var brush = winner ? WinnerFontBrush : FontBrush;
                g.DrawString(Game.Players[i].Name, PlayerNameFont, brush, xCorner, PlayerNameYPrecent * ParentHeight);

                var vpString = "Victory Points: " + Game.Players[i].VictoryPoints;
                var victoryPointsStringMeasure = g.MeasureString(vpString, VictoryPointsFont);
                xCorner = XCenter(i) - victoryPointsStringMeasure.Width / 2;
                g.DrawString(vpString, VictoryPointsFont, brush, xCorner, VictoryPointsYPrecent * ParentHeight);
            }

            if (!_doneAnimating && _currentPlayer >= 0)
            {
                var playerStringMeasure = g.MeasureString(Game.Players[_currentPlayer].Name, PlayerNameFont);
                var xCorner = XCenter(_currentPlayer) - playerStringMeasure.Width / 2;
                var yGoal = PlayerNameYPrecent*ParentHeight;

                const int yChange = 500;

                var y = AnimationFunction.EaseOutCirc(AnimationFrame, yGoal - yChange, yChange, AnimationFrames);
                var winner = Game.Players[Game.CurrentPlayer].Winner;
                var brush = winner ? WinnerFontBrush : FontBrush;
                g.DrawString(Game.Players[_currentPlayer].Name, PlayerNameFont, brush, xCorner, y);

                var vpString = "Victory Points: " + AnimationFunction.Linear(AnimationFrame, 0, Game.Players[_currentPlayer].VictoryPoints, AnimationFrame);
                var victoryPointsStringMeasure = g.MeasureString(vpString, VictoryPointsFont);
                xCorner = XCenter(_currentPlayer) - victoryPointsStringMeasure.Width / 2;
                g.DrawString(vpString, VictoryPointsFont, brush, xCorner, VictoryPointsYPrecent * ParentHeight);
            }

            base.Draw(g);
        }

        private Font VictoryPointsFont { get; set; }

        private int XCenter(int player)
        {
            return (int) (Width/(float) Game.Players.Count*(player + 0.5f));
        }

        private Font PlayerNameFont { get; set; }
    }
}