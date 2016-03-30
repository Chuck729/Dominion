using System;
using System.Drawing;
using RHFYP;

namespace GUI
{
    /// <summary>
    /// Responsible for showing relavent buy information about the given card type
    /// in the given buy deck.
    /// </summary>
    public class BuyCardViewer
    {
        // TODO: Add change flags so you only redraw the image when some state has changed.

        public const int MarginBetweenCircles = 20;
        public const int CirclesDiameter = 70;

        public static Rectangle CircleRectangle { get; set; }
        public Pen CircleBorderColor { get; set; }
        public Brush CircleColor { get; set; }
        public Brush CircleMouseOverColor { get; set; }
        public Brush CircleSelectedColor { get; set; }
        public Brush CircleUnavalibleColor { get; set; }

        /// <summary>
        /// The grid location of a card viewer is the position of the card in a grid where
        /// a unit is the size of the card veiwer image.
        /// </summary>
        public Point GridLocation { get; set; }

        /// <summary>
        /// The location of the top left pixel in this <see cref="BuyCardViewer"/> within its parent.
        /// </summary>
        public Point PixelLocation { get; set; }

        public BuyCardViewer(int x, int y)
        {
            // Set defaults
            CircleRectangle = new Rectangle(0, 0, CirclesDiameter, CirclesDiameter);
            CircleBorderColor = new Pen(Color.FromArgb(96, 110, 105));
            CircleColor = new SolidBrush(Color.FromArgb(96, 110, 105));
            CircleMouseOverColor = new SolidBrush(Color.FromArgb(30, 40, 35));
            CircleSelectedColor = new SolidBrush(Color.FromArgb(30, 40, 35));
            CircleUnavalibleColor = new SolidBrush(Color.FromArgb(30, 40, 35));

            GridLocation = new Point(x, y);
        }

        public void DrawCardViewer(Graphics g)
        {
            g.TranslateTransform(PixelLocation.X, PixelLocation.Y);

            g.FillEllipse(CircleColor, CircleRectangle);

            g.TranslateTransform(-PixelLocation.X, -PixelLocation.Y);
        }
    }
}
