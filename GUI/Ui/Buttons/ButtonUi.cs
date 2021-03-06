﻿using System;
using System.Drawing;
using RHFYP.Interfaces;

namespace GUI.Ui.Buttons
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

        public Font TextFont { protected get; set; }

        protected readonly SolidBrush TextBrush = new SolidBrush(Color.LightGray);
        protected readonly SolidBrush InactiveTextBrush = new SolidBrush(Color.DimGray);
        protected Pen BorderPen = new Pen(Color.FromArgb(200, 80, 90, 90), 1.5f);
        protected SolidBrush ButtonBrush = new SolidBrush(Color.FromArgb(200, 60, 70, 70));
        protected SolidBrush MousedOverButtonBrush = new SolidBrush(Color.FromArgb(200, 50, 60, 60));
        protected SolidBrush ClickedButtonBrush = new SolidBrush(Color.FromArgb(200, 30, 30, 30));

        public bool Active { protected get; set; }

        protected bool Clicked;

        private string Text { get; }
        private string MousedOverText { get; set; }

        public Action Action { private get; set; }

        public TextAlignment TextAlignment { private get; set; }

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
            MousedOverText = text;
            Action = action;
            Active = true;

            TextAlignment = TextAlignment.Left;
        }

        /// <inheritDoc/>
        public override void Draw(Graphics g, int parentWidth, int parentHeight)
        {
            var bgBrush = ButtonBrush;
            var text = Text;
            if (IsMouseOnButton())
            {
                bgBrush = MousedOverButtonBrush;
                text = MousedOverText;
            }
            if (Clicked) bgBrush = ClickedButtonBrush;

            g.FillRectangle(bgBrush, new Rectangle(Location.X, Location.Y,
                Width, Height));
            g.DrawRectangle(BorderPen, new Rectangle(Location.X, Location.Y,
                Width, Height));

            g.TranslateTransform(Location.X, Location.Y);
            DrawAlignedButtonString(g, text);
            g.TranslateTransform(-Location.X, -Location.Y);

            if (!Clicked) return;
            Clicked = false;
        }

        /// <summary>
        /// Has all of the different draw code required to align the text.
        /// </summary>
        /// <param name="g">The graphics object to draw onto.  Origin should be at the buttons top left corner.</param>
        /// <param name="str">The string you want to be drawn on the button.</param>
        private void DrawAlignedButtonString(Graphics g, string str)
        {
            SizeF measure;
            PointF point;

            switch (TextAlignment)
            {
                case TextAlignment.Right:
                    measure = g.MeasureString(str, TextFont);
                    point = new PointF(Width - measure.Width, (Height - measure.Height) / 2);
                    break;
                case TextAlignment.Center:
                    measure = g.MeasureString(str, TextFont);
                    point = new PointF((Width - measure.Width) / 2, (Height - measure.Height) / 2);
                    break;
                case TextAlignment.Left:
                    measure = g.MeasureString(str, TextFont);
                    point = new PointF(0, (Height - measure.Height) / 2);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            g.DrawString(str, TextFont, Active ? TextBrush : InactiveTextBrush, point);
        }

        /// <inheritDoc/>
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

        /// <inheritDoc/>
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