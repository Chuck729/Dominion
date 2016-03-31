using System.Drawing;
using System.Windows.Forms.VisualStyles;
using RHFYP;
using RHFYP.Cards;

namespace GUI.Ui.BuyCardUi
{
    /// <summary>
    /// Responsible for showing relavent buy information about the given card type
    /// in the given buy deck.
    /// </summary>
    public class BuyCardViewer
    {
        public const int MarginBetweenCircles = 20;
        public const int CirclesDiameter = 80;

        public static Rectangle CircleRectangle { get; set; }
        public Pen CircleBorderColor { get; set; }
        public Brush CircleColor { get; set; }
        public Brush CircleMouseOverColor { get; set; }
        public Brush CircleSelectedColor { get; set; }
        public Brush CircleUnavailableColor { get; set; }

        public bool Available { get; set; }
        public Brush MousedOver { get; set; }


        /// <summary>
        /// Used to get the type information of the card it should be looking for as it looks
        /// though the deck.  Also is used to get the image of the card.
        /// </summary>
        public ICard TrackedCard { get; }

        /// <summary>
        /// The deck it looks through to count howmany cards are left.
        /// </summary>
        /// <remarks>
        /// This could conveiently be the returned deck from a filter operation.
        /// </remarks>
        public IDeck TrackedDeck { get; }

        /// <summary>
        /// The grid location of a card viewer is the position of the card in a grid where
        /// a unit is the size of the card veiwer image.
        /// </summary>
        public Point GridLocation { get; set; }

        /// <summary>
        /// The location of the top left pixel in this <see cref="BuyCardViewer"/> within its parent.
        /// </summary>
        public Point PixelLocation { get; set; }

        public BuyCardViewer(ICard trackedCard, IDeck trackedDeck, int x, int y)
        {
            TrackedCard = trackedCard;
            TrackedDeck = trackedDeck;

            // Set defaults
            CircleRectangle = new Rectangle(0, 0, CirclesDiameter, CirclesDiameter);
            CircleBorderColor = new Pen(Color.FromArgb(128, 140, 133), 3);
            CircleColor = new SolidBrush(Color.FromArgb(85, 95, 90));
            CircleMouseOverColor = new SolidBrush(Color.FromArgb(90, 110, 110));
            CircleSelectedColor = new SolidBrush(Color.FromArgb(85, 75, 100));
            CircleUnavailableColor = new SolidBrush(Color.FromArgb(30, 40, 35));

            GridLocation = new Point(x, y);
        }


        // Defined here so that the object doesn't have to keep getting created.
        private readonly Point _tileGrpahicPointOffset = new Point(8, 6);
        public void DrawCardViewer(Graphics g, bool available, bool mousedOver, bool selected)
        {
            g.TranslateTransform(PixelLocation.X, PixelLocation.Y);

            var circleBgBrush = CircleColor;
            if (mousedOver) circleBgBrush = CircleMouseOverColor;
            if (selected) circleBgBrush = CircleSelectedColor;
            if (!available) circleBgBrush = CircleUnavailableColor;

            g.FillEllipse(circleBgBrush, CircleRectangle);
            g.DrawEllipse(CircleBorderColor, CircleRectangle);
            g.DrawImage(FastSafeImageResource.GetTileImageFromName(TrackedCard.Name), _tileGrpahicPointOffset);

            g.TranslateTransform(-PixelLocation.X, -PixelLocation.Y);
        }
    }
}
