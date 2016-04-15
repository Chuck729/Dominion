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
        SolidBrush ButtonBrush = new SolidBrush(Color.Gold);
        SolidBrush ClickedButtonBrush = new SolidBrush(Color.DarkGoldenrod);
        private int PlayAllTreasureWidth = 180;
        private int PlayAllTreasureHeight = 30;
        private bool clicked = false;
       
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
            g.FillRectangle(ButtonBrush, new Rectangle(Location.X, Location.Y,
                PlayAllTreasureWidth, PlayAllTreasureHeight));
           
            g.DrawString("Play All Treasures", TextFont, TextBrush, Location);
           
            if (clicked)
            {
                ButtonBrush = ClickedButtonBrush;
            }
        }

        public override bool SendMouseLocation(int x, int y)
        {
            _mouseLocation = new Point(x, y);
            return base.SendMouseLocation(x - Location.X, y - Location.X);
        }
        private bool IsMouseOnButton()
        {
            if (_mouseLocation.X < 0 || _mouseLocation.X >  PlayAllTreasureWidth)
                return false;
            else if (_mouseLocation.Y < 0 || _mouseLocation.Y > PlayAllTreasureHeight)
                return false;
            else
                return true;
        }
        public override bool SendClick(int x, int y)
        {
            if(IsMouseOnButton())
            {
                //call function to play all treasures
                clicked = true;
                return false;
            }
            return true;
        }
    }
}