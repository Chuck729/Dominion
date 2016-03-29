using System.Drawing;
using RHFYP;
using RHFYP.Cards;

namespace GUI
{
    public class BuyDeckUi : SimpleUi
    {
        public int CircleDiameter { get; set; }
        public Color CircleColor { get; set; }
        public Color CircleMouseOverColor { get; set; }
        public Color CircleSelectedColor { get; set; }

        private Point _mouseLocation = Point.Empty;
        private bool _mouseIn;
        private Game _game;
        private bool _forceMinimize;

        private const int AnimationFrames = 30;
        private int _animationFrame = 0;

        private bool _isCardItemMousedOver;
        private Card _cardItemMousedOver;
        private Card _cardItemSelected;


        public BuyDeckUi()
        {
            CircleDiameter = 30;
        }

        public override bool SendClick(int x, int y)
        {
            base.SendClick(x, y);
            if (_isCardItemMousedOver)
            {
                _cardItemSelected = _cardItemMousedOver;
                _forceMinimize = true;
                return true;
            }
            _cardItemSelected = null;
            return false;
        }

        /// <summary>
        /// Draws this Ui onto the <see cref="Graphics"/> object.
        /// </summary>
        /// <param name="g">The <see cref="Graphics"/> object to draw on.</param>
        public override void Draw(Graphics g)
        {
            if (_animationFrame <= 0)
            {
                _animationFrame = 0;
                _forceMinimize = false;
            }

            if (_forceMinimize || !_mouseIn)
            {
                if (_animationFrame > 0)
                {
                    _animationFrame--;
                }
            }
            else
            {
                if (_animationFrame < AnimationFrames - 1)
                {
                    _animationFrame++;
                }
            }

            base.Draw(g);
        }

        /// <summary>
        /// Checks to see if the mouse is within the buy deck ui to know whether it should expand or not.
        /// </summary>
        /// <param name="x">Mouse x location.</param>
        /// <param name="y">Mouse y location.</param>
        /// <returns>True is the mouse event is consitered "swallowed"</returns>
        public override bool SendMouseLocation(int x, int y)
        {
            if (x >= 0 && x <= BufferImage.Width && y >= 0 && y <= BufferImage.Height)
            {
                _mouseIn = true;
            }
            else
            {
                _mouseIn = false;
            }

            _mouseLocation = new Point(x, y);

            return base.SendMouseLocation(x, y);
        }

        private Point CalculateCirclePosition(int circleX, int circleY)
        {
            
        }
    }
}
