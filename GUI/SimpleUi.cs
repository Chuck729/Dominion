using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GUI
{
    public abstract class SimpleUi : ISimpleUi
    {
        /// <summary>
        /// A buffer image that can be used to only this Ui if an element of the Ui has changed.
        /// </summary>
        protected Bitmap BufferImage = new Bitmap(1, 1);

        /// <summary>
        /// A list of the sub components that make up this Ui.
        /// </summary>
        public List<SimpleUi> SubUis { get; set; }

        /// <summary>
        /// The <see cref="Point"/> that this Ui component should draw it top corner at.
        /// </summary>
        public Point Location { get; set; }

        /// <summary>
        /// If the user clicks a Ui the mouse coords should be sent to each sub Ui.
        /// The Ui should have event handlers to fire when specific things happen.
        /// </summary>
        /// <param name="x">Mouse click X pos</param>
        /// <param name="y">Mouse click Y pos</param>
        public virtual void SendClick(int x, int y)
        {
            foreach (var simpleUi in SubUis)
            {
                simpleUi.SendClick(x, y);
            }
        }

        /// <summary>
        /// If the user presses a key that key gets passed to all sub Ui's.
        /// </summary>
        /// <param name="e"></param>
        public virtual void SendKey(KeyEventArgs e)
        {
            foreach (var simpleUi in SubUis)
            {
                simpleUi.SendKey(e);
            }
        }

        /// <summary>
        /// Draws this Ui onto the <see cref="Graphics"/> object.
        /// </summary>
        /// <param name="g">The <see cref="Graphics"/> object to draw on.</param>
        public virtual void Draw(Graphics g)
        {
            foreach (var simpleUi in SubUis)
            {
                simpleUi.Draw(g);
            }
        }
    }
}
