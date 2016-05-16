using System;
using System.Drawing;
using GUI.Properties;
using RHFYP.Cards;
using RHFYP.Interfaces;

namespace GUI.Ui
{
    /// <summary>
    ///     A Ui object that will display information about any card given to it.
    /// </summary>
    public class CardInfoUi : SimpleUi, IExpandingElement
    {

        private const int MarginFromBottomAndLeft = 20;
        private const int MinimumViewerWidth = 400;
        private const int MaximumViewerWidth = 650;
        private const int ViewerHeight = 175;
        private const int MarginFromLeft = 15;
        private const int MarginFromTop = 10;

        public CardInfoUi(IGame game) : base(game)
        {
            BackgroundBrush = new SolidBrush(Color.FromArgb(200, 50, 55, 58));
            BorderPen = new Pen(Color.FromArgb(100, 130, 150), 3.5f);
            BufferImage = new Bitmap(MinimumViewerWidth, ViewerHeight);

            CardNameFont = new Font("Trebuchet MS", 20);
            CardDescriptionFont = new Font("Trebuchet MS", 11);
            CostFont = new Font("Trebuchet MS", 9, FontStyle.Bold);
            TextColor = new SolidBrush(Color.LightGray);

            AnimationFrames = GameUi.AnimationsOn ? 8 : 1;
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

        /// <inheritdoc/>
        public int AnimationFrames { get; }

        /// <inheritdoc/>
        public int AnimationFrame { get; set; }

        /// <inheritdoc/>
        public bool Expanded => AnimationFrame == AnimationFrames;

        /// <inheritdoc/>
        public bool Collapsed => AnimationFrame == 0;

        /// <inheritdoc/>
        public void AdjustAnimationFrame()
        {
            AnimationFrame += Card == null ? -1 : 1;
            AnimationFrame = Math.Max(0, AnimationFrame);
            AnimationFrame = Math.Min(AnimationFrame, AnimationFrames);
        }

        /// <inheritdoc/>
        public override void Draw(Graphics g, int parentWidth, int parentHeight)
        {
            base.Draw(g, parentWidth, parentHeight);
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
                actualViewerWidth = Math.Min(actualViewerWidth, MaximumViewerWidth);
            }

            var displayWidth =
                (int) AnimationFunction.EaseInOutCirc(AnimationFrame, 0, actualViewerWidth, AnimationFrames);
            var displayHeight = (int) AnimationFunction.EaseInOutCirc(AnimationFrame, 0, ViewerHeight, AnimationFrames);

            // Translate to the top left corner of the card info box.
            var xTranslation = MarginFromBottomAndLeft + (actualViewerWidth - displayWidth)/2;
            var yTranslation = parentHeight - ViewerHeight - MarginFromBottomAndLeft +
                               (ViewerHeight - displayHeight)/2;

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
                g.DrawString(Card.Description, CardDescriptionFont, TextColor,
                    new RectangleF(MarginFromLeft, MarginFromTop*2 + 64 + 16, actualViewerWidth, 1000));

                g.DrawRectangle(BorderPen, new Rectangle(displayWidth - 64, displayHeight - 20, 64, 20));
                g.DrawString("Cost: " + Card.CardCost, CostFont, TextColor, displayWidth - 60, displayHeight - 18);

                var type = Card.Type.ToString();
                g.DrawRectangle(BorderPen,
                    new Rectangle(0, displayHeight - 20, (int) (g.MeasureString(type, CostFont).Width + 8), 20));
                g.DrawString(type, CostFont, TextColor, 4, displayHeight - 18);
            }

            g.TranslateTransform(-xTranslation, -yTranslation);
        }
    }
}