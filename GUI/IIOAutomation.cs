using System.Windows.Forms;

namespace GUI
{
    public interface IIoAutomation
    {
        void SimulateMouseMove(MouseEventArgs e);

        void SimulateClickMouse(MouseEventArgs e);

        void SimulateMouseDown(MouseEventArgs e);

        void SimulateMouseUp(MouseEventArgs e);

        void SimulateSendKey(KeyEventArgs e);
    }
}
