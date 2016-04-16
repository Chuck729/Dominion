using System;
using System.Drawing;
using RHFYP;

namespace GUI.Ui
{
    public class ButtonUi : SimpleUi
    {
        private Point _mouseLocation = Point.Empty;

        private readonly Font _textFont = new Font("Trebuchet MS", 12, FontStyle.Bold);
        private readonly SolidBrush _textBrush = new SolidBrush(Color.Black);
        SolidBrush _buttonBrush = new SolidBrush(Color.Gold);
        readonly SolidBrush _clickedButtonBrush = new SolidBrush(Color.DarkGoldenrod);

        private bool _clicked;
        private string Text { get; }

        private Action Action { get; }

        /// <summary>
        /// Creates a Ui element that views all buttons 
        /// </summary>
        /// <param name="game">The game because all Ui elements have access to game.</param>
        /// <param name="text">The text that the button should display.</param>
        /// <param name="action">The <see cref="Action"/> you want to be invoked when this button is clicked.</param>
        public ButtonUi(IGame game, string text, Action action) : base(game)
        {
            Location = new Point(100, 200);
            // Even if you don't draw on this it determines the width/height of this Ui.
            BufferImage = new Bitmap(180, 30);
            Text = text;
            Action = action;
        }


        /// <summary>
        /// Draws this Ui onto the <see cref="Graphics" /> object.
        /// </summary>
        /// <param name="g">The <see cref="Graphics" /> object to draw on.</param>
        public override void Draw(Graphics g)
        {
            g.FillRectangle(_buttonBrush, new Rectangle(Location.X, Location.Y,
                Width, Height));
           
            g.DrawString(Text, _textFont, _textBrush, Location);
           
            if (_clicked)
            {
                _buttonBrush = _clickedButtonBrush;
            }
        }

        public override bool SendMouseLocation(int x, int y)
        {
            _mouseLocation = new Point(x, y);
            return base.SendMouseLocation(x - Location.X, y - Location.X);
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