using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using RHFYP;
using RHFYP.Cards;

namespace GUI.Ui.BuyCardUi
{
    public class ButtonUi : SimpleUi
    {
        private readonly List<BuyCardViewer> _buyCardViewers = new List<BuyCardViewer>();

        /// <summary>
        ///     Returns what it thinks the lowest displayed card value was (+ the width of the last card)
        ///     Should reset to 0 when SetBuyDeck() is called.
        /// </summary>
        private int _lazyBiggestY;

        private bool _mouseIn;

        private Point _mouseLocation = Point.Empty;

        private readonly CardInfoUi _cardInfoUi;
        public BuyCardViewer CardViewerMousedOver;

        /// <summary>
        /// Creates a Ui element that views a buy deck. 
        /// </summary>
        /// <param name="game"></param>
        /// <param name="cardInfoUi">can be null.  A card info Ui if you want to display information about the moused over card.</param>
        public ButtonUi(IGame game) : base(game)
        {
 
        }

        /// <summary>
        ///     This is the <see cref="BuyCardViewer" /> in the top right corner.
        /// </summary>
        public void PlayAllTreasuresButton()
        {

        }
            

        public override bool SendClick(int x, int y)
        {
            base.SendClick(x, y);

            

            // Force a collapse
            _mouseIn = false;
            return false;
        }

        /// <summary>
        ///     Draws this Ui onto the <see cref="Graphics" /> object.
        /// </summary>
        /// <param name="g">The <see cref="Graphics" /> object to draw on.</param>
        public override void Draw(Graphics g)
        {
            // Create buffer graphics, set quality, and draw background.
            var bufferGraphics = Graphics.FromImage(BufferImage);
            bufferGraphics.SmoothingMode = SmoothingMode.HighQuality;
            bufferGraphics.Clear(Color.FromArgb(0, 0, 0, 0));


            // Draw the buffered image onto the main graphics object.
            g.DrawImage(BufferImage, Location);
            base.Draw(g);
        }

       

        /// <summary>
        ///     Checks to see if the mouse is within the buy deck ui to know whether it should expand or not.
        /// </summary>
        /// <param name="x">Mouse x location.</param>
        /// <param name="y">Mouse y location.</param>
        /// <returns>True is the mouse event is consitered "swallowed"</returns>
        public override bool SendMouseLocation(int x, int y)
        {
            if (x >= (Width - BuyCardViewer.CirclesDiameter - (BuyCardViewer.MarginBetweenCircles * 2)) &&
                x <= BufferImage.Width)
            {
                if (y >= 0 && y <= BuyCardViewer.CirclesDiameter + (BuyCardViewer.MarginBetweenCircles * 2))
                {
                    _mouseIn = true;
                }
            }

            if (!(x >= 0 && x <= BufferImage.Width && y >= 0 && y <= BufferImage.Height))
            {
                _mouseIn = false;
            }

            _mouseLocation = new Point(x, y);

            return base.SendMouseLocation(x, y);
        }

        /// <summary>
        ///     This method needs to be called at the start of the game to set the deck of cards
        ///     the game will be using.  Passing in this deck gives the UI what it needs to draw the
        ///     animated side bar.
        /// </summary>
        /// <param name="buyDeck"></param>
   
    }
}