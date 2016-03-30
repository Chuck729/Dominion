using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using RHFYP;
using RHFYP.Cards;

namespace GUI
{
    public class BuyDeckUi : SimpleUi
    {
        private const int AnimationFrames = 45;

        private List<BuyCardViewer> _buyCardViewers = new List<BuyCardViewer>();

        private Point _mouseLocation = Point.Empty;
        private bool _mouseIn;
        private Game _game;

        private int _animationFrame;

        /// <summary>
        /// Returns what it thinks the lowest displayed card value was (+ the width of the last card)
        /// Should reset to 0 when SetBuyDeck() is called.
        /// </summary>
        private int _lazyBiggestY;

        private bool _isCardItemMousedOver;
        private Card _cardItemMousedOver;
        private Card _cardItemSelected;

        /// <summary>
        /// Is the buy menu fully collapsed.
        /// </summary>
        private bool Collapsed => _animationFrame <= 0;

        /// <summary>
        /// Is the buy menu fully expanded.
        /// </summary>
        private bool Expanded => _animationFrame >= AnimationFrames;

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
            bufferGraphics.SmoothingMode = SmoothingMode.HighQuality;

            bufferGraphics.Clear(Color.FromArgb(0, 0, 0, 0));

            // Background and top corner line for testing.
            //            bufferGraphics.FillRectangle(Brushes.Black, 0, 0, Width, Height);
            //            bufferGraphics.DrawLine(Pens.Green, Point.Empty, new Point(40, 40));

            foreach (var cardViewer in _buyCardViewers)
            {
                CalculatePixelLocationForAnimation(cardViewer);
                cardViewer.DrawCardViewer(bufferGraphics);
                _lazyBiggestY = Math.Max(cardViewer.PixelLocation.Y, _lazyBiggestY);
                if (_animationFrame == 0) break;
            }

            // Draw the buffered image onto the main graphics object.
            g.DrawImage(BufferImage, Location);
            base.Draw(g);
        }

        public void CalculatePixelLocationForAnimation(BuyCardViewer bcv)
        {
            var widthAndMargin = BuyCardViewer.CircleRectangle.Width + BuyCardViewer.MarginBetweenCircles;
            var xMin = Width - widthAndMargin;
            var xMax = Width - (bcv.GridLocation.X + 1) * widthAndMargin;

            var yMax = (bcv.GridLocation.Y * widthAndMargin) + BuyCardViewer.MarginBetweenCircles;
            var yMin = BuyCardViewer.MarginBetweenCircles;

            if (!Collapsed && _lazyBiggestY > Height)
            {
                var adjustedMouseY = _mouseLocation.Y - BuyCardViewer.MarginBetweenCircles;
                var adjustedHeight = Height - 2* BuyCardViewer.MarginBetweenCircles;

                var yOverflow = _lazyBiggestY - Height;
                var adjustedMouseYPrecent = (float)adjustedMouseY / adjustedHeight;
                var offset = (int)(adjustedMouseYPrecent * yOverflow);

                yMin -= offset;
                yMax -= offset;
            }

            var pixelX = EaseOutQuint(_animationFrame, xMin, xMax - xMin, AnimationFrames);
            var pixelY = EaseOutQuint(_animationFrame, yMin, yMax - yMin, AnimationFrames);
            bcv.PixelLocation = new Point((int)pixelX, (int)pixelY);
        }

        /// <summary>
        /// Formula aquired from http://gizma.com/easing/#quint2
        /// </summary>
        /// <param name="time">The current time or the current frame.</param>
        /// <param name="start">The start point of the animation.</param>
        /// <param name="change">The total distance that should be animated across.</param>
        /// <param name="duration">The duration of the animation or the number of frames.</param>
        /// <returns>The position along the animation.</returns>
        private float EaseOutQuint(float time, float start, float change, float duration)
        {
            time /= duration;
            time--;
            return change * (time * time * time * time * time + 1) + start;
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
            if (x >= (Width - BuyCardViewer.CircleRectangle.Width - (BuyCardViewer.MarginBetweenCircles * 2)) && x <= BufferImage.Width)
            {
                if (y >= 0 && y <= BuyCardViewer.CircleRectangle.Width + (BuyCardViewer.MarginBetweenCircles * 2))
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

        /// <summary>
        /// This method needs to be called at the start of the game to set the deck of cards
        /// the game will be using.  Passing in this deck gives the UI what it needs to draw the
        /// animated side bar.
        /// </summary>
        /// <param name="buyDeck"></param>
        public void SetBuyDeck(IDeck buyDeck)
        {
            _lazyBiggestY = 0;
            var gridSizeX = 2;
            var gridSizeY = 2;

            // TODO: Unfinished
            var i = 0;
            foreach (var card in buyDeck.Cards())
            {
                _buyCardViewers.Add(new BuyCardViewer(0, i));
                i++;
            }
            i = 0;
            foreach (var card in buyDeck.Cards())
            {
                _buyCardViewers.Add(new BuyCardViewer(1, i));
                i++;
            }


            var bitmapWidth = gridSizeX * (BuyCardViewer.CircleRectangle.Width + BuyCardViewer.MarginBetweenCircles);

            BufferImage = new Bitmap(bitmapWidth, ParentUi.Height);

            if (ParentUi == null)
            {
                throw new InvalidOperationException("A BuyDeckUi has to be a child of another ISimple Ui.");
            }
            Location = new Point(ParentUi.Width - bitmapWidth, 0);
        }

        public void AdjustSizeAndPosition(int parentWidth, int parentHeight)
        {
            BufferImage = new Bitmap(BufferImage.Width, parentHeight);
            Location = new Point(parentWidth - BufferImage.Width, 0);
        }
    }
}
