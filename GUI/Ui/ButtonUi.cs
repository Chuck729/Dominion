using System;
using System.Drawing;
using System.Threading;
using RHFYP.Interfaces;

namespace GUI.Ui
{
    public enum TextAlignment
    {
        Right,
        Center,
        Left
    }

    public class ButtonUi : SimpleUi
    {
        private Point _mouseLocation = Point.Empty;

        public Font TextFont { get; set; }

        protected readonly SolidBrush TextBrush = new SolidBrush(Color.LightGray);
        protected readonly SolidBrush InactiveTextBrush = new SolidBrush(Color.DimGray);
        protected Pen BorderPen = new Pen(Color.FromArgb(200, 80, 90, 90), 1.5f);
        protected SolidBrush ButtonBrush = new SolidBrush(Color.FromArgb(200, 60, 70, 70));
        protected SolidBrush MousedOverButtonBrush = new SolidBrush(Color.FromArgb(200, 50, 60, 60));
        protected SolidBrush ClickedButtonBrush = new SolidBrush(Color.FromArgb(200, 30, 30, 30));

        public bool Active { get; set; }

        protected bool Clicked;

        private string Text { get; set; }

        public Action Action { private get; set; }

        public TextAlignment TextAlignment { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="game">The game that this button is in.</param>
        /// <param name="text">The text this button should display.</param>
        /// <param name="action">The action this button should invoke when it's clicked.</param>
        /// <param name="width">The initial width of the button.</param>
        /// <param name="height">The initial height of the button.</param>
        public ButtonUi(IGame game, string text, Action action, int width, int height) : base(game)
        {
            TextFont = new Font("Trebuchet MS", 12, FontStyle.Bold);
            Location = new Point(100, 200);
            // Even if you don't draw on this it determines the width/height of this Ui.
            BufferImage = new Bitmap(width, height);
            Text = text;
            Action = action;
            Active = true;

            TextAlignment = TextAlignment.Left;
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

            g.TranslateTransform(Location.X, Location.Y);
            DrawAlignedButtonString(g);
            g.TranslateTransform(-Location.X, -Location.Y);

            if (!Clicked) return;
            Clicked = false;
        }

        /// <summary>
        /// Has all of the different draw code required to align the text.
        /// </summary>
        /// <param name="g">The graphics object to draw onto.  Origin should be at the buttons top left corner.</param>
        private void DrawAlignedButtonString(Graphics g)
        {
            SizeF measure;
            PointF point;

            switch (TextAlignment)
            {
                case TextAlignment.Right:
                    measure = g.MeasureString(Text, TextFont);
                    point = new PointF(Width - measure.Width, (Height - measure.Height) / 2);
                    break;
                case TextAlignment.Center:
                    measure = g.MeasureString(Text, TextFont);
                    point = new PointF((Width - measure.Width) / 2, (Height - measure.Height) / 2);
                    break;
                case TextAlignment.Left:
                    measure = g.MeasureString(Text, TextFont);
                    point = new PointF(0, (Height - measure.Height) / 2);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            g.DrawString(Text, TextFont, Active ? TextBrush : InactiveTextBrush, point);
        }

        public override bool SendMouseLocation(int x, int y)
        {
            _mouseLocation = new Point(x, y);
            return base.SendMouseLocation(x, y);
        }

        protected bool IsMouseOnButton()
        {
            if (_mouseLocation.X < 0 || _mouseLocation.X > Width)
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