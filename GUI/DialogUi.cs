using System.Drawing;
using GUI.Ui;
using RHFYP.Interfaces;

namespace GUI
{
    public delegate void DialogPainter(Graphics g);

    internal class DialogUi : SimpleUi
    {
        private readonly DialogPainter _dialogPainter;

        public DialogUi(IGame game, DialogPainter painter) : base(game)
        {
            _dialogPainter = painter;
        }

        /// <inheritdoc />
        public override void Draw(Graphics g, int parentWidth, int parentHeight)
        {
            base.Draw(g, parentWidth, parentHeight);
            _dialogPainter.Invoke(g);
        }
    }
}