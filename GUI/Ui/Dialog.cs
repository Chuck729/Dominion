using System;
using System.Drawing;
using GUI.Ui.Buttons;
using RHFYP.Interfaces;

namespace GUI.Ui
{

    /// <summary>
    /// A box that displays on a GameUI as a top level ui element that must be clicked before the game resumes.
    /// Provides a way to add buttons that the user can click and give data back to the game.
    /// </summary>
    /// <seealso cref="GUI.Ui.SimpleUi" />
    public abstract class Dialog : SimpleUi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Dialog"/> class.
        /// </summary>
        /// <param name="game">The game.</param>
        protected Dialog(IGame game) : base(game)
        {
            DialogBackgroundColor = new SolidBrush(Color.FromArgb(50, 60, 52));
            DialogBorderColor = new Pen(Color.FromArgb(40, 50, 60), 1.5f);
            DialogTextColor = new SolidBrush(Color.WhiteSmoke);
            DialogFont = new Font("Trebuchet MS", 14, FontStyle.Bold);
            ButtonDistanceFromBottom = 20;
        }

        /// <summary>
        ///     Gets or sets the top of the dialog box.
        /// </summary>
        /// <value>
        ///     The top of the dilog box.
        /// </value>
        internal int Top { get; set; }

        /// <summary>
        ///     Gets or sets the left of the dialog box.
        /// </summary>
        /// <value>
        ///     The left of the dilog box.
        /// </value>
        internal int Left { get; set; }

        /// <summary>
        /// Gets or sets the color of the dialog background.
        /// </summary>
        /// <value>
        /// The color of the dialog background.
        /// </value>
        internal Brush DialogBackgroundColor { get; set; }

        /// <summary>
        /// Gets or sets the color of the dialog border.
        /// </summary>
        /// <value>
        /// The color of the dialog border.
        /// </value>
        internal Pen DialogBorderColor { get; set; }

        /// <summary>
        /// Gets or sets the color of the dialog text.
        /// </summary>
        /// <value>
        /// The color of the dialog text.
        /// </value>
        internal Brush DialogTextColor { get; set; }

        /// <summary>
        /// Gets or sets the dialog font.
        /// </summary>
        /// <value>
        /// The dialog font.
        /// </value>
        internal Font DialogFont { get; set; }

        /// <summary>
        /// Gets or sets the button distance from bottom.
        /// </summary>
        /// <value>
        /// The button distance from bottom.
        /// </value>
        internal int ButtonDistanceFromBottom { get; set; }

        /// <inheritdoc />
        public override void Draw(Graphics g, int parentWidth, int parentHeight)
        {
            Top = (parentHeight - Height)/2;
            Left = (parentWidth - Width)/2;
            g.FillRectangle(DialogBackgroundColor, Left, Top, Width, Height);
            g.DrawRectangle(DialogBorderColor, Left, Top, Width, Height);
            for (var i = 0.0f; i < SubUis.Count; i++)
            {
                SubUis[(int)i].Location = new Point((int)((BufferImage.Width/(float)SubUis.Count)*(i + 0.5f)) + Left - (SubUis[0].Width / 2),
                    Top + Height - SubUis[(int)i].Height - ButtonDistanceFromBottom);
            }
            base.Draw(g, parentWidth, parentHeight);
        }

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

        /// <inheritdoc />
        public override bool SendClick(int x, int y)
        {
            base.SendClick(x, y);
            return false;
        }
    }
}