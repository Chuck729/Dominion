using System;
using System.Drawing;
using System.Windows.Forms;
using RHFYP;
using RHFYP.Cards;

namespace GUI
{
    public class BuyDeckUi : SimpleUi
    {
        private Game _game;

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
            return true;
        }

        /// <summary>
        /// Draws this Ui onto the <see cref="Graphics"/> object.
        /// </summary>
        /// <param name="g">The <see cref="Graphics"/> object to draw on.</param>
        public override void Draw(Graphics g)
        {
            base.Draw(g);
        }
    }
}
