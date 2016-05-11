using System;
using System.Windows.Forms;

namespace CopyFuzz
{
    /// <summary>
    /// This interface should be implemented by an application that you want to run CopyFuzz on.
    /// </summary>
    public interface ICopyFuzzifyer
    {
        /// <summary>
        /// This is run before simulation begins.
        /// </summary>
        /// <remarks>Use this method to make the application fuzz testable by doing actions like turning animations off.</remarks>
        void PreTest();

        /// <summary>
        /// Should open the applicationm in the state that you want it be tested in.
        /// </summary>
        void Launch();

        /// <summary>
        /// Simulates a mouse move input.
        /// </summary>
        /// <param name="e">Mouse event arguments.</param>
        void SimulateMouseMove(MouseEventArgs e);

        /// <summary>
        /// Simulates a mouse click input.
        /// </summary>
        /// <param name="e">Mouse event arguments.</param>
        void SimulateClickMouse(MouseEventArgs e);

        /// <summary>
        /// Simulates a mouse down input.
        /// </summary>
        /// <param name="e">Mouse event arguments.</param>
        void SimulateMouseDown(MouseEventArgs e);

        /// <summary>
        /// Simulates a mouse up input.
        /// </summary>
        /// <param name="e">Mouse event arguments.</param>
        void SimulateMouseUp(MouseEventArgs e);

        /// <summary>
        /// Simulates a key down input.
        /// </summary>
        /// <param name="e">Key event arguments.</param>
        void SimulateSendKey(KeyEventArgs e);

        /// <summary>
        /// The valid range of Y values that mouse actions such as clicks can be.
        /// </summary>
        int MouseValidYRange { get; }

        /// <summary>
        /// The valid range of X values that mouse actions such as clicks can be.
        /// </summary>
        int MouseValidXRange { get; }

        event Action<object, KeyEventArgs> KeyDownEvent;
        event Action<object, MouseEventArgs> MouseMoveEvent;
        event Action<object, MouseEventArgs> MouseUpEvent;
        event Action<object, MouseEventArgs> MouseDownEvent;
        event Action<object, MouseEventArgs> MouseClickEvent;
    }
}
