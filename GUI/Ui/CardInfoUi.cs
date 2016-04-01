using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RHFYP;
using RHFYP.Cards;

namespace GUI.Ui
{
    /// <summary>
    /// A Ui object that will display information about any card given to it.
    /// </summary>
    public class CardInfoUi : SimpleUi
    {
        /// <summary>
        /// The source of the displayed information.
        /// </summary>
        public ICard Card { get; set; }


        private const int MarginFromBottomAndLeft = 20;

        private const int AnimationFrames = 10;
        private int _animationFrame;

        private int _parentHeight;

        /// <summary>
        /// Draws this Ui onto the <see cref="Graphics"/> object.
        /// </summary>
        /// <param name="g">The <see cref="Graphics"/> object to draw on.</param>
        public override void Draw(Graphics g)
        {
            base.Draw(g);

            g.FillRectangle(Brushes.Black, new Rectangle(20, _parentHeight - 100 - 20, 200, 100));
        }

        // TODO: This code is repeated in buydeckui.  Might want to add it to SimpleUi or something.
        public void AdjustSizeAndPosition(int parentWidth, int parentHeight)
        {
            BufferImage = new Bitmap(BufferImage.Width, parentHeight);
            Location = new Point(MarginFromBottomAndLeft, parentHeight - MarginFromBottomAndLeft - 100);
            _parentHeight = parentHeight;
        }

        public CardInfoUi(IGame game) : base(game)
        {
        }
    }
}
