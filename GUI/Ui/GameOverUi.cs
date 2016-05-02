using System;
using System.Drawing;
using RHFYP.Interfaces;

namespace GUI.Ui
{
    public sealed class GameOverUi : SimpleUi, IExpandingElement
    {
        private const int FrameDelay = 10;

        private readonly MapUi[] _maps;
        private int _currentFrameDelay;
        private int _currentPlayer = -1;
        private bool _doneAnimating;
        private int _winningPlayer;

        public GameOverUi(IGame game, CardInfoUi cardInfoUi, int width, int height) : base(game)
        {
            AnimationFrames = 50;
            AnimationFrame = 45;
            GameOverFont = new Font("Consolas", 36, FontStyle.Bold);
            GameOverFontBrush = new SolidBrush(Color.WhiteSmoke);

            _maps = new MapUi[game.Players.Count];
            for (var i = 0; i < game.Players.Count; i++)
            {
                _maps[i] = new MapUi(game, null, cardInfoUi, null, Game.Players[i]);
                _maps[i].IgnoreShading();
            }

            BufferImage = new Bitmap(width, height);          
        }

        private readonly float MapYPrecent = 0.20f;

        private Brush GameOverFontBrush { get; set; }

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
                var xCenter = (int) (Width/(float)Game.Players.Count*(_currentPlayer + 0.5f));
                _maps[_currentPlayer].Draw(Graphics.FromImage(new Bitmap(1, 1)));
                var xCorner = xCenter - _maps[_currentPlayer].Width/2;
                AddChildUi(_maps[_currentPlayer], xCorner, (int)(MapYPrecent * ParentHeight));
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
            g.DrawString("Game Over", GameOverFont, GameOverFontBrush, (ParentWidth - gameOverTextMeasure.Width) / 2, 50);

            // Go through all the players up to the player thats currently being animated.
            for (var i = 0; i < (_doneAnimating ? Game.Players.Count : _currentPlayer); i++)
            {

            }

            if (!_doneAnimating)
            {
            }

            base.Draw(g);
        }

        public void DrawPlayerStats(IPlayer player)
        {
            //player.
        }
    }
}