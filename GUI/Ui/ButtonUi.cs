using System;
using System.Drawing;
using RHFYP.Interfaces;

namespace GUI.Ui
{
    public class ButtonUi : SimpleUi
    {
        private Point _mouseLocation = Point.Empty;

        protected Font TextFont = new Font("Trebuchet MS", 12, FontStyle.Bold);
        protected SolidBrush TextBrush = new SolidBrush(Color.LightGray);
        protected SolidBrush InactiveTextBrush = new SolidBrush(Color.DimGray);
        protected Pen BorderPen = new Pen(Color.FromArgb(200, 80, 90, 90), 1.5f);
        protected SolidBrush ButtonBrush = new SolidBrush(Color.FromArgb(200, 60, 70, 70));
        protected SolidBrush MousedOverButtonBrush = new SolidBrush(Color.FromArgb(200, 50, 60, 60));
        protected SolidBrush ClickedButtonBrush = new SolidBrush(Color.FromArgb(200, 30, 30, 30));

        public bool Active { get; set; }

        protected bool Clicked;

        protected string Text { get; set; }

        public Action Action { get; set; }

        /// <summary>
        /// Creates a Ui element that views all buttons 
        /// </summary>
        /// <param name="game">The game because all Ui elements have access to game.</param>
        /// <param name="text">The text that the button should display.</param>
        /// <param name=CardType.Action>The <see cref=CardType.Action/> you want to be invoked when this button is clicked.</param>
        public ButtonUi(IGame game, string text, Action action, int width, int height) : base(game)
        {
            Location = new Point(100, 200);
            // Even if you don't draw on this it determines the width/height of this Ui.
            BufferImage = new Bitmap(width, height);
            Text = text;
            Action = action;
            Active = true;
        }

            
        /// <summary>
        /// Draws this Ui onto the <see cref="Graphics" /> object.
        /// </summary>
        /// <param name="g">The <see cref="Graphics" /> object to draw on.</param>
        public override void Draw(Graphics g)
        {
            var bgBrush = ButtonBrush;
            if (IsMouseOnButton()) bgBrush = MousedOverButtonBrush;
            if (Clicked) bgBrush = ClickedButtonBrush;

            g.FillRectangle(bgBrush, new Rectangle(Location.X, Location.Y,
                Width, Height));
            g.DrawRectangle(BorderPen, new Rectangle(Location.X, Location.Y,
                Width, Height));

            g.DrawString(Text, TextFont, Active ? TextBrush : InactiveTextBrush, Location);

            if (!Clicked) return;
            Clicked = false;
        }

        public override bool SendMouseLocation(int x, int y)
        {
            _mouseLocation = new Point(x, y);
            return base.SendMouseLocation(x, y);
        }
        protected bool IsMouseOnButton()
        {
            if (_mouseLocation.X < 0 || _mouseLocation.X >  Width)
                return false;
            return _mouseLocation.Y >= 0 && _mouseLocation.Y <= Height;
        }

        /// <summary>
        /// Checks to see if the mouse is over the button and invokes this buttons action if the mouse is over it.
        /// </summary>
        /// <param name="x">Mouse x at time of click.</param>
        /// <param name="y">Mouse y at time of click.</param>
        /// <returns>True if the mouse click is 'swallowed'</returns>
        public override bool SendClick(int x, int y)
        {
            if (!IsMouseOnButton()) return false;
            if (!Active) return false;
            Action?.Invoke();
            Clicked = true;
            return false;
        }
    }
}