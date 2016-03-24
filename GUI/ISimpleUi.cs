using System.Drawing;

namespace GUI
{
    public interface ISimpleUi
    {
        /// <summary>
        /// If the user clicks a Ui the mouse coords should be sent to each sub Ui.
        /// Results from the click (if any events happen) should be added to an output stack in the Ui.
        /// </summary>
        /// <param name="x">Mouse click X pos</param>
        /// <param name="y">Mouse click Y pos</param>
        void SendClick(int x, int y);

        /// <summary>
        /// Draws this Ui onto the <see cref="Graphics"/> object.
        /// </summary>
        /// <param name="g">The <see cref="Graphics"/> object to draw on.</param>
        void Draw(Graphics g);
    }
}
