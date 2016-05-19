using System;
using System.Drawing;
using System.Linq;
using RHFYP.Cards;
using RHFYP.Interfaces;

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

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="trackedCard">The <see cref="ICard" /> that this <see cref="BuyCardViewer" /> should display and represent.</param>
        /// <param name="trackedDeck">
        ///     The <see cref="IDeck" /> that this buy card viewer should look through to count how many
        ///     cards of it's type are left.
        /// </param>
        /// <param name="x">The x grid location of this viewer in a <see cref="BuyDeckUi" />.</param>
        /// <param name="y">The y grid location of this viewer in a <see cref="BuyDeckUi" />.</param>
        public BuyCardViewer(ICard trackedCard, IDeck trackedDeck, int x, int y)
        {
            TrackedCard = trackedCard;
            TrackedDeck = trackedDeck;

            // Set defaults
            CircleRectangle = new Rectangle(0, 0, CirclesDiameter, CirclesDiameter);
            CircleBorderColor = new Pen(Color.FromArgb(120, 132, 125), 3);
            CircleBorderCardsLeftColor = new Pen(Color.FromArgb(145, 155, 200), 3);
            CircleColor = new SolidBrush(Color.FromArgb(200, 85, 95, 90));
            CircleMouseOverColor = new SolidBrush(Color.FromArgb(200, 90, 110, 110));
            CircleSelectedColor = new SolidBrush(Color.FromArgb(200, 85, 75, 100));
            CircleUnavailableColor = new SolidBrush(Color.FromArgb(200, 30, 40, 35));
            CircleCouponColor = new SolidBrush(Color.FromArgb(200, 30, 80, 35));
            CircleCouponMousedOverColor = new SolidBrush(Color.FromArgb(200, 30, 100, 35));


            GridLocation = new Point(x, y);
        }

        private static Rectangle CircleRectangle { get; set; }
        private Pen CircleBorderColor { get; }
        private Pen CircleBorderCardsLeftColor { get; }
        private Brush CircleColor { get; }
        private Brush CircleMouseOverColor { get; }
        private Brush CircleSelectedColor { get; }
        private Brush CircleUnavailableColor { get; }
        private Brush CircleCouponColor { get; set; }
        private Brush CircleCouponMousedOverColor { get; set; }

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

        /// <summary>
        ///     Looks through the given <see cref="IDeck" /> and counds the number of cards in it that
        ///     are the same type as the tracked card.
        /// </summary>
        private void CountTrackedCards()
        {
            if (TrackedCard == null) return;
            _cardCount = TrackedDeck.Cards().Count(card => card.Name == TrackedCard.Name);
            _mostSeenCards = Math.Max(_mostSeenCards, _cardCount);
        }

        /// <summary>
        ///     Paints this <see cref="BuyCardViewer" /> onto the given <see cref="Graphics" /> object.
        /// </summary>
        /// <param name="g">What to paint the <see cref="BuyCardViewer" /> onto.</param>
        /// <param name="available">Should this card display as avalible.</param>
        /// <param name="mousedOver">Should this card display as moused over.</param>
        /// <param name="coupons">How many coupons the player has.</param>
        public void DrawCardViewer(Graphics g, bool available, bool mousedOver, int coupons)
        {
            // Update card count before drawing
            CountTrackedCards();

            g.TranslateTransform(PixelLocation.X, PixelLocation.Y);

            Brush circleBgBrush;
            if (TrackedCard != null && TrackedCard.CardCost <= coupons)
            {
                circleBgBrush = CircleCouponColor;
                if (mousedOver) circleBgBrush = CircleCouponMousedOverColor;
            }
            else
            {
                circleBgBrush = CircleColor;
                if (mousedOver) circleBgBrush = CircleMouseOverColor;
            }
            
            if (!available) circleBgBrush = CircleUnavailableColor;

            g.FillEllipse(circleBgBrush, CircleRectangle);

            // Draw border and number of cards left (Indicated by the border changing color)
            g.DrawEllipse(CircleBorderColor, CircleRectangle);
            g.DrawArc(CircleBorderCardsLeftColor, CircleRectangle, -90.0f, 360.0f*_cardCount/Math.Max(1, _mostSeenCards));

            // If the tracked card is null then show a buy symbol instead.
            var image = TrackedCard == null
                ? FastSafeImageResource.GetTileImageFromName("buysymbol")
                : FastSafeImageResource.GetTileImageFromName(TrackedCard.ResourceName);

            g.DrawImage(image, _tileGraphicPointOffset.X, _tileGraphicPointOffset.Y, 64, 64);

            g.TranslateTransform(-PixelLocation.X, -PixelLocation.Y);
        }
    }
}