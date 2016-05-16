using System;
using System.Drawing;
using GUI.Ui.Buttons;
using RHFYP.Interfaces;

namespace GUI.Ui
{
    public enum DialogResult
    {
        Yes,
        No,
    }

    public delegate void DialogPainter(Graphics g);

    internal class DialogUi : SimpleUi
    {
        public bool HaveResult { get; private set; }
        public DialogResult Result { get; private set; }

        private readonly DialogPainter _dialogPainter;


        public DialogUi(IGame game, DialogPainter painter, int width, int height) : base(game)
        {
            _dialogPainter = painter;
            BufferImage = new Bitmap(width, height);
        }

        /// <inheritdoc />
        public override void Draw(Graphics g, int parentWidth, int parentHeight)
        {
            _dialogPainter.Invoke(g);
            base.Draw(g, parentWidth, parentHeight);
        }

        public void AddDialogButton(DialogResult buttonResult, string text)
        {
            Action action = () =>
            {
                Result = buttonResult;
                HaveResult = true;
            };

            var button = new ButtonUi(Game, text == "" ? buttonResult.ToString() : text, action, 100, 25);

            AddChildUi(button);
        }

        public void AddDialogButton(DialogResult buttonResult)
        {
            AddDialogButton(buttonResult, string.Empty);
        }
    }
}