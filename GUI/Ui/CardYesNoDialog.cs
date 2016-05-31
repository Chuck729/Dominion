using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RHFYP.Cards;
using RHFYP.Interfaces;

namespace GUI.Ui
{
    public class CardYesNoDialog : Dialog
    {
        private readonly string _prompt;

        public CardYesNoDialog(IGame game, string prompt, ICard cardToDisplay) : base(game)
        {
            _prompt = prompt;

            BufferImage = new Bitmap(300, 200);

            AddDialogButton(UserResponse.Yes);
            AddDialogButton(UserResponse.No);
        }

        /// <inheritdoc />
        public override void Draw(Graphics g, int parentWidth, int parentHeight)
        {
            base.Draw(g, parentWidth, parentHeight);

            var measure = g.MeasureString(_prompt, DialogFont);
            g.DrawString(_prompt, DialogFont, DialogTextColor,
                new PointF((Width - measure.Width)/2, ButtonDistanceFromBottom));
        }
    }
}
