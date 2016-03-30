using System.Collections.Generic;
using System.Drawing;
using RHFYP;
using RHFYP.Cards;

namespace GUI
{
    public class BuyDeckUi : SimpleUi
    {
        private const int AnimationFrames = 30;

        private List<BuyCardViewer> _buyCardViewers = new List<BuyCardViewer>(); 

        private Point _mouseLocation = Point.Empty;
        private bool _mouseIn;
        private Game _game;

        private int _animationFrame;

        private bool _isCardItemMousedOver;
        private Card _cardItemMousedOver;
        private Card _cardItemSelected;

        public override bool SendClick(int x, int y)
        {
            base.SendClick(x, y);
            if (_isCardItemMousedOver)
            {
                _cardItemSelected = _cardItemMousedOver;
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
            ChangeAnimationFrame();
            var bufferGraphics = Graphics.FromImage(BufferImage);

            foreach (var cardViewer in _buyCardViewers)
            {
                cardViewer.DrawCardViewer(bufferGraphics);
            }

            // Draw the buffered image onto the main graphics object.
            g.DrawImage(BufferImage, Point.Empty);
            base.Draw(g);
        }

        private void ChangeAnimationFrame()
        {
            if (!_mouseIn)
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
        }

        /// <summary>
        /// Checks to see if the mouse is within the buy deck ui to know whether it should expand or not.
        /// </summary>
        /// <param name="x">Mouse x location.</param>
        /// <param name="y">Mouse y location.</param>
        /// <returns>True is the mouse event is consitered "swallowed"</returns>
        public override bool SendMouseLocation(int x, int y)
        {
            if (x >= (Width - BuyCardViewer.CircleRectangle.Width - (MarginBetweenCircles * 2)) && x <= BufferImage.Width)
            {
                if (y >= 0 && y <= BuyCardViewer.CircleRectangle.Width + (MarginBetweenCircles*2))
                {
                    _mouseIn = true;
                }
            }
            if (!(x >= 0 && x <= BufferImage.Width && y >= 0 && y <= BufferImage.Height))
            {
                _mouseIn = false;
            }

            _mouseLocation = new Point(x, y);

            return base.SendMouseLocation(x, y);
        }

        private Point CalculateCirclePosition(int circleX, int circleY)
        {
            return Point.Empty;
        }

        public void SetBuyDeck(Deck buyDeck)
        {
            
        }
    }
}
