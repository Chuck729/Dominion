using System.Drawing;
using RHFYP;
using RHFYP.Cards;

namespace GUI
{
    public class BuyDeckUi : SimpleUi
    {

        private bool _mouseIn;
        private Game _game;
        private bool _forceMinimize;

        private const int AnimationFrames = 30;
        private int _animationFrame = 0;

        private bool _isCardItemMousedOver;
        private Card _cardItemMousedOver;
        private Card _cardItemSelected;

        #region Style Properties

        public int YMarginBetweenAvailableCards { get; set; }

        public int XMarginBetweenAvailableCards { get; set; }

        public float AvailableCardsMarginFromRight { get; set; }

        public float AvailableCardsMarginFromTop { get; set; }

        public int BuyBackgroundEllipseSize { get; set; }

        public Brush BuildingCardBackgroundEllipseBrush { get; set; }

        public Pen BuySelectionPen { get; set; }

        public Brush SelectedBuildingCardBackgroundEllipseBrush { get; set; }

        #endregion

        public BuyDeckUi()
        {

        }

        /// <summary>
        /// Sets the default game viewer style.  Effects colors and fonts potentially.
        /// </summary>
        private void SetDefaultStyle()
        {
            XMarginBetweenAvailableCards = 96;
            YMarginBetweenAvailableCards = 96;
            AvailableCardsMarginFromRight = 0.05f;
            AvailableCardsMarginFromTop = 0.05f;

            BuyBackgroundEllipseSize = 11;
            BuildingCardBackgroundEllipseBrush = new SolidBrush(Color.FromArgb(40, 50, 45));
            SelectedBuildingCardBackgroundEllipseBrush = new SolidBrush(Color.FromArgb(70, 80, 75));
            BuySelectionPen = new Pen(Color.FromArgb(254, 71, 71), 2);
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

            return base.SendMouseLocation(x, y);
        }
    }
}
