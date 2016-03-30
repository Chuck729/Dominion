using System.Drawing;
using System.Windows.Forms;

namespace GUI
{
    public interface ISimpleUi
    {
        /// <summary>
        /// Gets the location of this Ui inside its parent Ui.
        /// </summary>
        /// <returns>The location.</returns>
        Point Location { get; set; }

        ISimpleUi ParentUi { get; set; }

        int Width { get; }

        int Height { get; }


        /// <summary>
        /// Adds a <see cref="ISimpleUi"/> as a child of this <see cref="ISimpleUi"/>.  
        /// Also properly sets the parent of the <paramref name="childUi"/> to this.
        /// </summary>
        /// <param name="childUi">The Ui you want to be displayed within this Ui.</param>
        void AddChildUi(ISimpleUi childUi);


        /// <summary>
        /// The parent Ui is the Ui that this child is contained within.
        /// </summary>
        /// <returns>The parent Ui or null if there is no parent.</returns>
        ISimpleUi GetParentUi();


        /// <summary>
        /// If the user clicks a Ui the mouse coords should be sent to each sub Ui.
        /// The Ui should have event handlers to fire when specific things happen.
        /// </summary>
        /// <param name="x">Mouse click X pos</param>
        /// <param name="y">Mouse click Y pos</param>
        /// <returns>False if the click event should be consitered 'swallowed'.</returns>
        bool SendClick(int x, int y);


        bool SendMouseLocation(int x, int y);


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
