using System.Windows.Forms;

namespace GUI
{
    public interface IIoAutomation
    {
        void MoveMouse(MouseEventArgs e);

        void ClickMouse(MouseEventArgs e);

        void SimulateMouseDown(MouseEventArgs e);

        void SimulateMouseUp(MouseEventArgs e);

        void SendKey(KeyEventArgs e);
    }
}
