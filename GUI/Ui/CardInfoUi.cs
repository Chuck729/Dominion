using System.Drawing;
using System.Windows.Forms;
using RHFYP;
using RHFYP.Cards;

namespace GUI.Ui
{
    /// <summary>
    ///     A Ui object that will display information about any card given to it.
    /// </summary>
    public class CardInfoUi : SimpleUi
    {
        private const int MarginFromBottomAndLeft = 20;
        private const int ViewerWidth = 200;
        private const int ViewerHeight = 100;

        private const int AnimationFrames = 10;
        private int _animationFrame;

        private int _parentHeight;

        public CardInfoUi(IGame game) : base(game)
        {
            BufferImage = new Bitmap(ViewerWidth, ViewerHeight);
        }

        /// <summary>
        ///     The source of the displayed information.
        /// </summary>
        public ICard Card { get; set; }

        /// <summary>
        ///     Draws this Ui onto the <see cref="Graphics" /> object.
        /// </summary>
        /// <param name="g">The <see cref="Graphics" /> object to draw on.</param>
        public override void Draw(Graphics g)
        {
            base.Draw(g);

            g.FillRectangle(Brushes.Black, new Rectangle(20, _parentHeight - 100 - 20, 200, 100));
        }

        // TODO: This code is repeated in buydeckui.  Might want to add it to SimpleUi or something.
        public void AdjustSizeAndPosition(int parentWidth, int parentHeight)
        {
            Location = new Point(MarginFromBottomAndLeft, parentHeight - Height - MarginFromBottomAndLeft);
            _parentHeight = parentHeight;
        }
    }
}