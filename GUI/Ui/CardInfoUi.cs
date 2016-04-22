using System;
using System.Drawing;
using GUI.Properties;
using RHFYP;
using RHFYP.Cards;

namespace GUI.Ui
{
    /// <summary>
    ///     A Ui object that will display information about any card given to it.
    /// </summary>
    public class CardInfoUi : SimpleUi, IExpandingElement
    {

        private const int MarginFromBottomAndLeft = 20;
        private const int MinimumViewerWidth = 400;
        private const int ViewerHeight = 150;
        private const int MarginFromLeft = 15;
        private const int MarginFromTop = 10;
        private int _parentHeight;

        public CardInfoUi(IGame game) : base(game)
        {
            BackgroundBrush = new SolidBrush(Color.FromArgb(200, 50, 55, 58));
            BorderPen = new Pen(Color.FromArgb(100, 130, 150), 3.5f);
            BufferImage = new Bitmap(MinimumViewerWidth, ViewerHeight);

            CardNameFont = new Font("Trebuchet MS", 20);
            CardDescriptionFont = new Font("Trebuchet MS", 11);
            CostFont = new Font("Trebuchet MS", 9, FontStyle.Bold);
            TextColor = new SolidBrush(Color.LightGray);

            AnimationFrames = 8;
        }

        private Brush BackgroundBrush { get; }
        private Pen BorderPen { get; }

        /// <summary>
        ///     The source of the displayed information.
        /// </summary>
        public ICard Card { private get; set; }

        private Font CostFont { get; }

        private Font CardDescriptionFont { get; }

        private Brush TextColor { get; }

        private Font CardNameFont { get; }

        public int AnimationFrames { get; }

        public int AnimationFrame { get; set; }

        public bool Expanded => AnimationFrame == AnimationFrames;

        public bool Collapsed => AnimationFrame == 0;

        /// <summary>
        ///     Increments the animation frame when a card is being display and decrements it when
        ///     a card is notbeing displayed.
        /// </summary>
        public void AdjustAnimationFrame()
        {
            AnimationFrame += Card == null ? -1 : 1;
            AnimationFrame = Math.Max(0, AnimationFrame);
            AnimationFrame = Math.Min(AnimationFrame, AnimationFrames);
        }

        /// <summary>
        ///     Draws this Ui onto the <see cref="Graphics" /> object.
        /// </summary>
        /// <param name="g">The <see cref="Graphics" /> object to draw on.</param>
        public override void Draw(Graphics g)
        {
            base.Draw(g);
            AdjustAnimationFrame();

            var actualViewerWidth = MinimumViewerWidth;
            if (Card != null)
            {
                actualViewerWidth =
                    (int)
                        Math.Max(actualViewerWidth,
                            g.MeasureString(Card.Description, CardDescriptionFont).Width + 2*MarginFromLeft);
                actualViewerWidth =
                    (int)
                        Math.Max(actualViewerWidth,
                            g.MeasureString(Card.Name, CardNameFont).Width + 3*MarginFromLeft + 64);
            }

            var displayWidth =
                (int) AnimationFunction.EaseInOutCirc(AnimationFrame, 0, actualViewerWidth, AnimationFrames);
            var displayHeight = (int) AnimationFunction.EaseInOutCirc(AnimationFrame, 0, ViewerHeight, AnimationFrames);

            // Translate to the top left corner of the card info box.
            var xTranslation = MarginFromBottomAndLeft + ((actualViewerWidth - displayWidth)/2);
            var yTranslation = (_parentHeight - ViewerHeight - MarginFromBottomAndLeft) +
                               ((ViewerHeight - displayHeight)/2);

            g.TranslateTransform(xTranslation, yTranslation);

            // Draw background and border rectangles.
            g.FillRectangle(BackgroundBrush, new Rectangle(0, 0, displayWidth, displayHeight));
            g.DrawRectangle(BorderPen, new Rectangle(0, 0, displayWidth, displayHeight));

            if (Card != null && Expanded)
            {
                g.DrawImage(FastSafeImageResource.GetTileImageFromName(Card.ResourceName), MarginFromLeft, MarginFromTop,
                    64, 64);
                g.DrawImage(Resources._base, MarginFromLeft, 60 + MarginFromTop - 12, 64, 32);

                g.DrawString(Card.Name, CardNameFont, TextColor, 64 + MarginFromLeft*2, MarginFromTop + 12);
                g.DrawString(Card.Description, CardDescriptionFont, TextColor, MarginFromLeft, MarginFromTop*2 + 64 + 16);

                g.DrawRectangle(BorderPen, new Rectangle(displayWidth - 64, displayHeight - 20, 64, 20));
                g.DrawString("Cost: " + Card.CardCost, CostFont, TextColor, displayWidth - 60, displayHeight - 18);
            }

            g.TranslateTransform(-xTranslation, -yTranslation);
        }

        public void AdjustSizeAndPosition(int parentWidth, int parentHeight)
        {
            Location = new Point(MarginFromBottomAndLeft, parentHeight - Height - MarginFromBottomAndLeft);
            _parentHeight = parentHeight;
        }
    }
}