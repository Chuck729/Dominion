using System.Drawing;

namespace GUI
{
    class BuyCardViewer
    {
        // TODO: Add change flags so you only redraw the image when some state has changed.

        public Rectangle CircleRectangle { get; set; }
        public Pen CircleColor { get; set; }
        public Pen CircleMouseOverColor { get; set; }
        public Pen CircleSelectedColor { get; set; }
        public Pen CircleUnavalibleColor { get; set; }

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
            CircleRectangle = new Rectangle(0, 0, 30, 30);
            CircleColor = new Pen(Color.WhiteSmoke);
            CircleMouseOverColor = new Pen(Color.WhiteSmoke);
            CircleSelectedColor = new Pen(Color.WhiteSmoke);
            CircleUnavalibleColor = new Pen(Color.WhiteSmoke);
        }

        public void DrawCardViewer(Graphics g)
        {
            g.DrawEllipse(CircleColor, CircleRectangle);
        }
    }
}
