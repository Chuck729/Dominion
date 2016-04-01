using System;
using System.Drawing;
using System.Linq;
using RHFYP;
using RHFYP.Cards;

namespace GUI.Ui.BuyCardUi
{
    /// <summary>
    ///     Responsible for showing relavent buy information about the given card type
    ///     in the given buy deck.
    /// </summary>
    public class BuyCardViewer
    {
        public const int MarginBetweenCircles = 20;
        public const int CirclesDiameter = 80;

        // Defined here so that the object doesn't have to keep getting created.
        private readonly Point _tileGraphicPointOffset = new Point(8, 6);
        private int _cardCount;
        private int _mostSeenCards;

        private ICard _trackedCard;

        public BuyCardViewer(ICard trackedCard, IDeck trackedDeck, int x, int y)
        {
            TrackedCard = trackedCard;
            TrackedDeck = trackedDeck;

            // Set defaults
            CircleRectangle = new Rectangle(0, 0, CirclesDiameter, CirclesDiameter);
            CircleBorderColor = new Pen(Color.FromArgb(120, 132, 125), 3);
            CircleBorderCardsLeftColor = new Pen(Color.FromArgb(145, 155, 200), 3);
            CircleColor = new SolidBrush(Color.FromArgb(85, 95, 90));
            CircleMouseOverColor = new SolidBrush(Color.FromArgb(90, 110, 110));
            CircleSelectedColor = new SolidBrush(Color.FromArgb(85, 75, 100));
            CircleUnavailableColor = new SolidBrush(Color.FromArgb(30, 40, 35));

            GridLocation = new Point(x, y);
        }

        public static Rectangle CircleRectangle { get; set; }
        public Pen CircleBorderColor { get; set; }
        public Pen CircleBorderCardsLeftColor { get; set; }
        public Brush CircleColor { get; set; }
        public Brush CircleMouseOverColor { get; set; }
        public Brush CircleSelectedColor { get; set; }
        public Brush CircleUnavailableColor { get; set; }

        /// <summary>
        ///     Used to get the type information of the card it should be looking for as it looks
        ///     though the deck.  Also is used to get the image of the card.
        /// </summary>
        public ICard TrackedCard
        {
            get { return _trackedCard; }
            set
            {
                _trackedCard = value;
                _mostSeenCards = 0;
            }
        }

        /// <summary>
        ///     The deck it looks through to count howmany cards are left.
        /// </summary>
        /// <remarks>
        ///     This could conveiently be the returned deck from a filter operation.
        /// </remarks>
        public IDeck TrackedDeck { get; }

        /// <summary>
        ///     The grid location of a card viewer is the position of the card in a grid where
        ///     a unit is the size of the card veiwer image.
        /// </summary>
        public Point GridLocation { get; set; }

        /// <summary>
        ///     The location of the top left pixel in this <see cref="BuyCardViewer" /> within its parent.
        /// </summary>
        public Point PixelLocation { get; set; }

        public void CountTrackedCards()
        {
            if (TrackedCard == null) return;
            _cardCount = TrackedDeck.Cards().Count(card => card.Name == TrackedCard.Name);
            _mostSeenCards = Math.Max(_mostSeenCards, _cardCount);
        }

        public void DrawCardViewer(Graphics g, bool available, bool mousedOver, bool selected)
        {
            // Update card count before drawing
            CountTrackedCards();

            g.TranslateTransform(PixelLocation.X, PixelLocation.Y);

            var circleBgBrush = CircleColor;
            if (mousedOver) circleBgBrush = CircleMouseOverColor;
            if (selected) circleBgBrush = CircleSelectedColor;
            if (!available) circleBgBrush = CircleUnavailableColor;

            g.FillEllipse(circleBgBrush, CircleRectangle);

            // Draw border and number of cards left (Indicated by the border changing color)
            g.DrawEllipse(CircleBorderColor, CircleRectangle);
            g.DrawArc(CircleBorderCardsLeftColor, CircleRectangle, -90.0f, (360.0f*_cardCount)/_mostSeenCards);

            // If the tracked card is null then show a buy symbol instead.
            var image = TrackedCard == null
                ? FastSafeImageResource.GetTileImageFromName("buysymbol")
                : FastSafeImageResource.GetTileImageFromName(TrackedCard.Name);

            g.DrawImage(image, _tileGraphicPointOffset);

            g.TranslateTransform(-PixelLocation.X, -PixelLocation.Y);
        }
    }
}