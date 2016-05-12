using System;
using System.Windows.Forms;

namespace CopyFuzz
{
    /// <summary>
    ///     This interface should be implemented by an application that you want to run CopyFuzz on.
    /// </summary>
    public interface ICopyFuzzifyer
    {

        /// <summary>
        ///     The valid range of Y values that mouse actions such as clicks can be.
        /// </summary>
        int MouseValidYRange { get; }

        /// <summary>
        ///     The valid range of X values that mouse actions such as clicks can be.
        /// </summary>
        int MouseValidXRange { get; }

        /// <summary>
        ///     This is run before simulation begins.
        /// </summary>
        /// <remarks>Use this method to make the application fuzz testable by doing actions like turning animations off.</remarks>
        void PreTest();

        /// <summary>
        ///     Should open the applicationm in the state that you want it be tested in.
        /// </summary>
        void Launch();

        /// <summary>
        ///     Simulates a mouse move input.
        /// </summary>
        /// <param name="e">Mouse event arguments.</param>
        void SimulateMouseMove(MouseEventArgs e);

        /// <summary>
        ///     Simulates a mouse click input.
        /// </summary>
        /// <param name="e">Mouse event arguments.</param>
        void SimulateClickMouse(MouseEventArgs e);

        /// <summary>
        ///     Simulates a mouse down input.
        /// </summary>
        /// <param name="e">Mouse event arguments.</param>
        void SimulateMouseDown(MouseEventArgs e);

        /// <summary>
        ///     Simulates a mouse up input.
        /// </summary>
        /// <param name="e">Mouse event arguments.</param>
        void SimulateMouseUp(MouseEventArgs e);

        /// <summary>
        ///     Simulates a key down input.
        /// </summary>
        /// <param name="e">Key event arguments.</param>
        void SimulateSendKey(KeyEventArgs e);

        /// <summary>
        ///     This event should be triggered every time a key is pressed.
        /// </summary>
        event Action<object, KeyEventArgs> KeyDownEvent;

        /// <summary>
        ///     This event should be triggered every time the mouse is moved.
        /// </summary>
        event Action<object, MouseEventArgs> MouseMoveEvent;

        /// <summary>
        ///     This event should be triggered every time a mouse button released.
        /// </summary>
        event Action<object, MouseEventArgs> MouseUpEvent;

        /// <summary>
        ///     This event should be triggered every time a mouse button is pressed.
        /// </summary>
        event Action<object, MouseEventArgs> MouseDownEvent;

        /// <summary>
        ///     This event should be triggered every time a mouse button is clicked.
        /// </summary>
        event Action<object, MouseEventArgs> MouseClickEvent;
    }
}