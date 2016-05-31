using System.Drawing;
using RHFYP.Cards;
using RHFYP.Interfaces;

namespace GUI.Ui
{
    public class CardDiscardPutOnDeckDialog : Dialog
    {
        private readonly string _prompt;
        private readonly ICard _cardToDisplay;

        public CardDiscardPutOnDeckDialog(IGame game, string prompt, ICard cardToDisplay) : base(game)
        {
            _prompt = prompt;
            _cardToDisplay = cardToDisplay;

            BufferImage = new Bitmap(480, 200);

            AddDialogButton(UserResponse.Discard);
            AddDialogButton(UserResponse.PutOnDeck, "Put on Deck");
        }

        /// <inheritdoc />
        public override void Draw(Graphics g, int parentWidth, int parentHeight)
        {
            base.Draw(g, parentWidth, parentHeight);

            var measure = g.MeasureString(_prompt, DialogFont);
            g.DrawString(_prompt, DialogFont, DialogTextColor,
                new PointF((Width - measure.Width) / 2 + Left, ButtonDistanceFromBottom + Top));

            if (_cardToDisplay == null) return;

            var image = FastSafeImageResource.GetTileImageFromName(_cardToDisplay.ResourceName);
            g.DrawImage(image, new Point(Left + (Width - image.Width) / 2, Top + (Height - image.Height) / 2));
        }
    }
}