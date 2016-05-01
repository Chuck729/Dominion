using System;
using System.Drawing;
using RHFYP;
using RHFYP.Interfaces;

namespace GUI.Ui
{
    public class ButtonUi : SimpleUi
    {
        private Point _mouseLocation = Point.Empty;

        private readonly Font _textFont = new Font("Trebuchet MS", 12, FontStyle.Bold);
        private readonly SolidBrush _textBrush = new SolidBrush(Color.LightGray);
        private readonly SolidBrush _inactiveTextBrush = new SolidBrush(Color.DimGray);
        private readonly Pen _borderPen = new Pen(Color.FromArgb(200, 80, 90, 90), 1.5f);
        readonly SolidBrush _buttonBrush = new SolidBrush(Color.FromArgb(200, 60, 70, 70));
        readonly SolidBrush _mousedOverButtonBrush = new SolidBrush(Color.FromArgb(200, 50, 60, 60));
        readonly SolidBrush _clickedButtonBrush = new SolidBrush(Color.FromArgb(200, 30, 30, 30));

        public bool Active { get; set; }

        private bool _clicked;

        protected string Text { get; set; }

        protected Action Action { get; set; }

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
            var bgBrush = _buttonBrush;
            if (IsMouseOnButton()) bgBrush = _mousedOverButtonBrush;
            if (_clicked) bgBrush = _clickedButtonBrush;

            g.FillRectangle(bgBrush, new Rectangle(Location.X, Location.Y,
                Width, Height));
            g.DrawRectangle(_borderPen, new Rectangle(Location.X, Location.Y,
                Width, Height));

            g.DrawString(Text, _textFont, Active ? _textBrush : _inactiveTextBrush, Location);

            if (!_clicked) return;
            _clicked = false;
        }

        public override bool SendMouseLocation(int x, int y)
        {
            _mouseLocation = new Point(x, y);
            return base.SendMouseLocation(x, y);
        }
        private bool IsMouseOnButton()
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
            Action?.Invoke();
            _clicked = true;
            return false;
        }
    }
}