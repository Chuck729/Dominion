using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using RHFYP;
using RHFYP.Cards;

namespace GUI.Ui.BuyCardUi
{
    public class BuyDeckUi : SimpleUi
    {
        private const int AnimationFrames = 10;

        private readonly List<BuyCardViewer> _buyCardViewers = new List<BuyCardViewer>();
        private int _animationFrame;
        private IDeck _buyDeck;


        /// <summary>
        ///     Returns what it thinks the lowest displayed card value was (+ the width of the last card)
        ///     Should reset to 0 when SetBuyDeck() is called.
        /// </summary>
        private int _lazyBiggestY;

        private bool _mouseIn;

        private Point _mouseLocation = Point.Empty;

        public BuyCardViewer CardViewerMousedOver;

        public BuyDeckUi(IGame game, IDeck buyDeck) : base(game)
        {
            if (buyDeck == null) throw new ArgumentException("The buy deck Ui can not observe a null deck.");
            _buyDeck = buyDeck;
            SetBuyDeck(buyDeck);
        }

        /// <summary>
        ///     This is the <see cref="BuyCardViewer" /> in the top right corner.
        /// </summary>
        public BuyCardViewer SelectedCardViewer { get; set; }

        /// <summary>
        ///     Is the buy menu fully collapsed.
        /// </summary>
        private bool Collapsed => _animationFrame <= 0;

        /// <summary>
        ///     Is the buy menu fully expanded.
        /// </summary>
        private bool Expanded => _animationFrame >= AnimationFrames;

        public override bool SendClick(int x, int y)
        {
            base.SendClick(x, y);
            if (CardViewerMousedOver != null && SelectedCardViewer != CardViewerMousedOver)
            {
                SelectedCardViewer.TrackedCard = CardViewerMousedOver.TrackedCard;


                // Force a collapse
                _mouseIn = false;
                return true;
            }
            SelectedCardViewer.TrackedCard = null;
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

            ChangeAnimationFrame();
            CardViewerMousedOver = null;

            foreach (var cardViewer in _buyCardViewers.Reverse<BuyCardViewer>())
            {
                CalculatePixelLocationForAnimation(cardViewer);

                _lazyBiggestY = Math.Max(cardViewer.PixelLocation.Y, _lazyBiggestY);
                if (
                    new Rectangle(cardViewer.PixelLocation,
                        new Size(BuyCardViewer.CirclesDiameter, BuyCardViewer.CirclesDiameter)).Contains(_mouseLocation))
                {
                    CardViewerMousedOver = cardViewer;
                }

                // Only draw the viewer if its selected or if the viewers are expanded
                if ((Collapsed) && SelectedCardViewer != cardViewer && cardViewer.TrackedCard != null) continue;

                var showCardAsSelected = SelectedCardViewer == cardViewer;
                showCardAsSelected = showCardAsSelected || cardViewer.TrackedCard == SelectedCardViewer.TrackedCard;
                showCardAsSelected = showCardAsSelected && SelectedCardViewer.TrackedCard != null;
                cardViewer.DrawCardViewer(bufferGraphics, true, CardViewerMousedOver == cardViewer, showCardAsSelected);
            }

            // Draw the buffered image onto the main graphics object.
            g.DrawImage(BufferImage, Location);
            base.Draw(g);
        }

        public void CalculatePixelLocationForAnimation(BuyCardViewer bcv)
        {
            const int widthAndMargin = BuyCardViewer.CirclesDiameter + BuyCardViewer.MarginBetweenCircles;
            var xMin = Width - widthAndMargin;
            var xMax = Width - (bcv.GridLocation.X + 1)*widthAndMargin;

            var yMax = (bcv.GridLocation.Y*widthAndMargin) + BuyCardViewer.MarginBetweenCircles;
            var yMin = BuyCardViewer.MarginBetweenCircles;

            if (!Collapsed && _lazyBiggestY > Height)
            {
                var adjustedMouseY = _mouseLocation.Y - BuyCardViewer.MarginBetweenCircles -
                                     (BuyCardViewer.CirclesDiameter/2);
                var adjustedHeight = Height - 2*BuyCardViewer.MarginBetweenCircles - BuyCardViewer.CirclesDiameter;

                var yOverflow = (_lazyBiggestY + widthAndMargin) - Height;
                var adjustedMouseYPrecent = (float) adjustedMouseY/adjustedHeight;
                var offset = (int) (adjustedMouseYPrecent*yOverflow);

                yMin -= offset;
                yMax -= offset;
            }

            var pixelX = AnimationFunction.EaseInOutCirc(_animationFrame, xMin, xMax - xMin, AnimationFrames);
            var pixelY = AnimationFunction.EaseInOutCirc(_animationFrame, yMin, yMax - yMin, AnimationFrames);

            bcv.PixelLocation = new Point((int) pixelX, (int) pixelY);
        }

        private void ChangeAnimationFrame()
        {
            if (!_mouseIn)
            {
                if (_animationFrame > 0)
                {
                    _animationFrame--;
                }
            }
            else
            {
                if (_animationFrame < AnimationFrames - 1)
                {
                    _animationFrame++;
                }
            }
        }

        /// <summary>
        ///     Checks to see if the mouse is within the buy deck ui to know whether it should expand or not.
        /// </summary>
        /// <param name="x">Mouse x location.</param>
        /// <param name="y">Mouse y location.</param>
        /// <returns>True is the mouse event is consitered "swallowed"</returns>
        public override bool SendMouseLocation(int x, int y)
        {
            if (x >= (Width - BuyCardViewer.CirclesDiameter - (BuyCardViewer.MarginBetweenCircles*2)) &&
                x <= BufferImage.Width)
            {
                if (y >= 0 && y <= BuyCardViewer.CirclesDiameter + (BuyCardViewer.MarginBetweenCircles*2))
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
        private void SetBuyDeck(IDeck buyDeck)
        {
            SelectedCardViewer = new BuyCardViewer(null, buyDeck, 0, 0);
            _buyCardViewers.Clear();
            _lazyBiggestY = 0;
            const int gridSizeX = 3;
            var counts = new int[gridSizeX];

            // Filters the deck of cards into a list of only one card of each name.
            var setOfCardNames = new List<ICard>();
            foreach (var card in buyDeck.Cards().Where(card => setOfCardNames.All(x => x.Name != card.Name)))
            {
                setOfCardNames.Add(card);
            }

            // Creates the special card viewer that displays the selected card.
            SelectedCardViewer = new BuyCardViewer(null, buyDeck, 0, 0);
            counts[0]++;
            _buyCardViewers.Add(SelectedCardViewer);

            foreach (var card in setOfCardNames)
            {
                int x;
                if (card.Type.Equals("victory"))
                {
                    x = 2;
                }
                else if (card.Type.Equals("treasure"))
                {
                    x = 1;
                }
                else
                {
                    x = 0;
                }
                _buyCardViewers.Add(new BuyCardViewer(card, buyDeck, x, counts[x]));
                counts[x]++;
            }

            const int bitmapWidth =
                gridSizeX*(BuyCardViewer.CirclesDiameter + BuyCardViewer.MarginBetweenCircles) +
                BuyCardViewer.MarginBetweenCircles;

            BufferImage = new Bitmap(bitmapWidth, 1);
        }

        public void AdjustSizeAndPosition(int parentWidth, int parentHeight)
        {
            BufferImage = new Bitmap(BufferImage.Width, parentHeight);
            Location = new Point(parentWidth - BufferImage.Width, 0);
        }
    }
}