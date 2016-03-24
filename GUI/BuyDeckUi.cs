using System;
using System.Drawing;
using System.Windows.Forms;
using RHFYP;
using RHFYP.Cards;

namespace GUI
{
    public class BuyDeckUi : SimpleUi
    {
        private readonly GameUi _parentUi;
        private readonly Deck _buyDeck;

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

        public BuyDeckUi(GameUi parentUi)
        {
            if (parentUi == null) throw new ArgumentNullException(nameof(parentUi));
            _parentUi = parentUi;
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

        /// <summary>
        /// If the user clicks a Ui the mouse coords should be sent to each sub Ui.
        /// The Ui should have event handlers to fire when specific things happen.
        /// </summary>
        /// <param name="x">Mouse click X pos</param>
        /// <param name="y">Mouse click Y pos</param>
        public override void SendClick(int x, int y)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// If the user presses a key that key gets passed to all sub Ui's.
        /// </summary>
        /// <param name="e"></param>
        public override void SendKey(KeyEventArgs e)
        {
            throw new NotImplementedException();
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
