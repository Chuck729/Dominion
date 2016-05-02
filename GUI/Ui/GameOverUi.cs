using System;
using System.Drawing;
using System.Runtime.InteropServices;
using RHFYP.Interfaces;

namespace GUI.Ui
{
    public sealed class GameOverUi : SimpleUi, IExpandingElement
    {
        private int _currentPlayer;
        private int _currentFrameDelay;
        private int _frameDelay = 0;
        private int _winningPlayer;
        private bool _doneAnimating;

        private readonly MapUi[] _maps;

        public GameOverUi(IGame game, CardInfoUi cardInfoUi, int width, int height) : base(game)
        {
            AnimationFrames = 10;
            GameOverFont = new Font("Consolas", 12, FontStyle.Bold);
            GameOverFontBrush = new SolidBrush(Color.WhiteSmoke);

            _maps = new MapUi[game.Players.Count];
            for (var i = 0; i < game.Players.Count; i++)
            {
                _maps[i] = new MapUi(game, null, cardInfoUi, null, Game.Players[i]);
            }
            AddChildUi(_maps[0]);

            BufferImage = new Bitmap(width, height);
        }

        /// <summary>
        /// Number of frames.
        /// </summary>
        public int AnimationFrames { get; }

        /// <summary>
        /// The current frame.
        /// </summary>
        public int AnimationFrame { get; set; }

        /// <summary>
        /// True if the current frame equals the number of frames.
        /// </summary>
        public bool Expanded => AnimationFrame == AnimationFrames;

        /// <summary>
        /// True if the current frame equals 0.
        /// </summary>
        public bool Collapsed => AnimationFrame == 0;

        /// <summary>
        /// Decides how the current animation frame should be adjusted.
        /// </summary>
        public void AdjustAnimationFrame()
        {
            if (_doneAnimating) return;

            AnimationFrame = Math.Min(AnimationFrames, AnimationFrame + 1);

            if (!Expanded) return;

            _currentFrameDelay++;

            if (_currentFrameDelay != _frameDelay) return;

            if (Game.Players[_currentPlayer].Winner) _winningPlayer = _currentPlayer;
            AnimationFrame = 0;
            _currentFrameDelay = 0;
            _currentPlayer++;
            AddChildUi(_maps[_currentPlayer], (Width / Game.Players.Count) * _currentPlayer, 0);

            if (_currentPlayer != Game.Players.Count) return;

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
            g.DrawString("Game Over", GameOverFont, GameOverFontBrush, 0, 0);

            // Go through all the players up to the player thats currently being animated.
            for (var i = 0; i < (_doneAnimating ? Game.Players.Count : _currentPlayer); i++)
            {
                
            }

            if (!_doneAnimating)
            {

            }

            base.Draw(g);
        }

        public Brush GameOverFontBrush { get; set; }

        public Font GameOverFont { get; set; }

    }
}
