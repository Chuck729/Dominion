using System.Windows.Forms;

namespace GUI
{
    public interface IIoAutomation
    {
        void MoveMouse(MouseEventArgs e);

        void ClickMouse(MouseEventArgs e);

        void SendKey(KeyEventArgs e);
    }
}
