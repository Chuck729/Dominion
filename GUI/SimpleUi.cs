using System.Drawing;
using System.Windows.Forms;

namespace GUI
{
    abstract class SimpleUi : ISimpleUi
    {
        protected Bitmap BufferImage = new Bitmap(1, 1);

        public Point Location { get; set; }

        /// <summary>
        /// If the user clicks a Ui the mouse coords should be sent to each sub Ui.
        /// The Ui should have event handlers to fire when specific things happen.
        /// </summary>
        /// <param name="x">Mouse click X pos</param>
        /// <param name="y">Mouse click Y pos</param>
        public abstract void SendClick(int x, int y);

        /// <summary>
        /// If the user presses a key that key gets passed to all sub Ui's.
        /// </summary>
        /// <param name="e"></param>
        public abstract void SendKey(KeyEventArgs e);

        /// <summary>
        /// Draws this Ui onto the <see cref="Graphics"/> object.
        /// </summary>
        /// <param name="g">The <see cref="Graphics"/> object to draw on.</param>
        public abstract void Draw(Graphics g);
    }
}
