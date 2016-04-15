using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using RHFYP;
using RHFYP.Cards;
using System.Windows.Forms;

namespace GUI.Ui.BuyCardUi
{
    public class ButtonUi : SimpleUi
    {
        private Point _mouseLocation = Point.Empty;

        private Font TextFont = new Font("Trebuchet MS", 12, FontStyle.Bold);

        private SolidBrush TextBrush = new SolidBrush(Color.Black);
        private int PlayAllTreasureWidth = 180;
        private int PlayAllTreasureHeight = 30;
       
        /// <summary>
        /// Creates a Ui element that views all buttons 
        /// </summary>
        /// <param name="game"></param>
        public ButtonUi(IGame game) : base(game)
        {
            Location = new Point(100, 200);
        }


        /// <summary>
        ///     Draws this Ui onto the <see cref="Graphics" /> object.
        /// </summary>
        /// <param name="g">The <see cref="Graphics" /> object to draw on.</param>
        public override void Draw(Graphics g)
        {
            
            
            
            var ButtonBrush = new SolidBrush(Color.Gold);
            g.FillRectangle(ButtonBrush, new Rectangle(Location.X, Location.Y,
                PlayAllTreasureWidth, PlayAllTreasureHeight));
            
            if (IsMouseOnButton(Location, PlayAllTreasureWidth, PlayAllTreasureHeight,
                _mouseLocation.X, _mouseLocation.Y))
            {
                g.DrawString("Play All Treasures", TextFont, TextBrush, Location);
            }
            
        }

        public override bool SendMouseLocation(int x, int y)
        {
            _mouseLocation = new Point(x, y);
            return base.SendMouseLocation(x - Location.X, y - Location.X);
        }
        private bool IsMouseOnButton(Point location, int width, int height, int mouseX, int mouseY)
        {
            if (mouseX < 0 || mouseX >  width)
                return false;
            else if (mouseY < 0 || mouseY > height)
                return false;
            else
                return true;
        }
    }
}