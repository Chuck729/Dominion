using System;
using System.Drawing;
using GUI.Ui.Buttons;
using RHFYP.Interfaces;

namespace GUI.Ui
{
    public abstract class Dialog : SimpleUi
    {
        protected Dialog(IGame game) : base(game)
        {
        }

        /// <inheritdoc />
        public override void Draw(Graphics g, int parentWidth, int parentHeight)
        {
            g.FillRectangle(DialogBackgroundColor, (parentWidth - Width) / 2, (parentHeight - Height) / 2, Width, Height);
            g.DrawRectangle(DialogBorderColor, (parentWidth - Width) / 2, (parentHeight - Height) / 2, Width, Height);
            for (var i = 0; i < SubUis.Count; i++)
            {
                SubUis[i].Location = new Point((int) ((float) BufferImage.Width/SubUis.Count*(i + 1.5f)),
                    y: Height - SubUis[i].Height - ButtonDistanceFromBottom);
            }
        }

        internal Brush DialogBackgroundColor { get; set; }

        internal Pen DialogBorderColor { get; set; }
        internal Brush DialogTextColor { get; set; }

        internal Font DialogFont { get; set; }

        internal int ButtonDistanceFromBottom { get; set; }

        internal void AddDialogButton(UserResponse buttonResult, string text)
        {
            Action action = () =>
            {
                Game.UserResponse = buttonResult;
                Game.NeedUserInput = false;
            };

            var button = new ButtonUi(Game, text == "" ? buttonResult.ToString() : text, action, 100, 25);

            AddChildUi(button);
        }

        internal void AddDialogButton(UserResponse buttonResult)
        {
            AddDialogButton(buttonResult, buttonResult.ToString());
        }
    }
}