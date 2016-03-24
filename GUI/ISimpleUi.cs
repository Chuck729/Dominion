using System.Drawing;
using System.Windows.Forms;

namespace GUI
{
    public interface ISimpleUi
    {
        /// <summary>
        /// If the user clicks a Ui the mouse coords should be sent to each sub Ui.
        /// The Ui should have event handlers to fire when specific things happen.
        /// </summary>
        /// <param name="x">Mouse click X pos</param>
        /// <param name="y">Mouse click Y pos</param>
        /// <returns>False if the click event should be consitered 'swallowed'.</returns>
        bool SendClick(int x, int y);

        /// <summary>
        /// If the user presses a key that key gets passed to all sub Ui's.
        /// </summary>
        /// <param name="e"></param>
        /// <returns>False if the click event should be consitered 'swallowed'.</returns>
        bool SendKey(KeyEventArgs e);

        /// <summary>
        /// Draws this Ui onto the <see cref="Graphics"/> object.
        /// </summary>
        /// <param name="g">The <see cref="Graphics"/> object to draw on.</param>
        void Draw(Graphics g);
    }
}
