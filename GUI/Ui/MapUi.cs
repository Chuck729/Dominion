using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;
using GUI.Properties;
using GUI.Ui.BuyCardUi;
using Priority_Queue;
using RHFYP;
using RHFYP.Cards;
using RHFYP.Interfaces;

namespace GUI.Ui
{
    public class MapUi : SimpleUi, IExpandingElement
    {
        private const int TileHeight = 32;
        private const int TileWidth = 64;
        private const int TileHeightHalf = TileHeight/2;
        private const int TileWidthHalf = TileWidth/2;

        private const int BounceAnimationOffset = 20;
        private readonly ButtonPanelUi _buttonPanel;
        private readonly BuyDeckUi _buyDeckUi;
        private readonly CardInfoUi _cardInfoUi;


        private readonly ButtonUi _trashButton;
        private string _actionInfoText;
        private Color _actionInfoTextColor;

        private IDeck _borderDeck = new Deck();

        private ICard _currentExpandingTile;

        private int _frameInc = 1;
        private bool _ignoreShading;

        private Point _mouseLocation = Point.Empty;
        private bool _selectPointMode;
        private ICard _tileMouseIsOver;

        private Point _topLeftCoord = Point.Empty;

        private float _transparency;
        private bool _trashMode;
        private float _zoom;

        public MapUi(IGame game, BuyDeckUi buyDeckUi, CardInfoUi cardInfoUi, ButtonPanelUi buttonPanel, Player player,
            float zoom = 1.0f) : base(game)
        {
            _buyDeckUi = buyDeckUi;
            _cardInfoUi = cardInfoUi;
            _buttonPanel = buttonPanel;
            _zoom = zoom;

            Player = player;

            Location = Point.Empty;
            AnimationFrames = GameUi.AnimationsOn ? 5 : 1;
            _transparency = 1;
            ActionInfoTextFont = new Font("Trebuchet MS", 10, FontStyle.Bold);
            ActionInfoTextFont2 = new Font("Trebuchet MS", 10, FontStyle.Bold);

            _trashButton = new DoneTrashingButtonUi(Game, "Done Trashing",
                () => { Game.Players[Game.CurrentPlayer].Nukes = 0; },
                180, 25);
        }

        public float Transparency
        {
            get { return _transparency; }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));
                if (value > 1) throw new ArgumentOutOfRangeException(nameof(value));
                _transparency = value;
            }
        }

        public float Zoom
        {
            get { return _zoom; }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));
                if (value > 1) throw new ArgumentOutOfRangeException(nameof(value));
                _zoom = value;
            }
        }


        public IPlayer Player { private get; set; }

        private ICard TileMouseIsOver
        {
            get { return _tileMouseIsOver; }
            set
            {
                if (_cardInfoUi != null)
                {
                    _cardInfoUi.Card = value;
                }
                _tileMouseIsOver = value;
            }
        }

        private Font ActionInfoTextFont2 { get; }

        private bool SelectPointMode
        {
            get { return _selectPointMode; }
            set
            {
                if (value)
                {
                    _actionInfoText = "Buy : " + _buyDeckUi.SelectedCardViewer.TrackedCard.CardCost;
                    _actionInfoTextColor = Color.LightGray;
                }
                else
                {
                    _actionInfoText = "Play";
                    _actionInfoTextColor = Color.LightGray;
                }
                _selectPointMode = value;
            }
        }

        private bool TrashMode
        {
            get { return _trashMode; }
            set
            {
                if (value)
                {
                    _actionInfoText = "Trash";
                    _actionInfoTextColor = Color.Tomato;
                    if (_buttonPanel != null && !_buttonPanel.Buttons.Contains(_trashButton))
                    {
                        _buttonPanel.AddChildUi(_trashButton);
                    }
                }
                else
                {
                    _actionInfoText = "Play";
                    _actionInfoTextColor = Color.LightGray;
                    if (_buttonPanel != null && _buttonPanel.Buttons.Contains(_trashButton))
                    {
                        if (_buttonPanel.Buttons.Remove(_trashButton))
                        {
                        }
                    }
                }
                _trashMode = value;
            }
        }

        private Font ActionInfoTextFont { get; }

        /// <summary>
        ///     Number of frames.
        /// </summary>
        public int AnimationFrames { get; }

        /// <summary>
        ///     The current frame.
        /// </summary>
        public int AnimationFrame { get; set; }

        /// <summary>
        ///     True if the current frame equals the number of frames.
        /// </summary>
        public bool Expanded => AnimationFrame == AnimationFrames;

        /// <summary>
        ///     True if the current frame equals 0.
        /// </summary>
        public bool Collapsed => AnimationFrame == 0;

        /// <summary>
        ///     Decides how the current animation frame should be adjusted.
        /// </summary>
        public void AdjustAnimationFrame()
        {
            if (_currentExpandingTile == null) return;
            AnimationFrame += _frameInc;
            if (!Expanded && !Collapsed) return;
            _frameInc *= -1;
            if (Collapsed) _currentExpandingTile = null;
        }

        public void IgnoreShading()
        {
            _ignoreShading = true;
        }

        /// <summary>
        ///     Creates a new bitmap that is just big enough to fit the drawn map.
        /// </summary>
        /// <param name="deck">The <see cref="IDeck" /> of cards that should be drawn.</param>
        private void CreateNewBitmapToFitMap(IDeck deck)
        {
            var maxX = int.MinValue;
            var minX = int.MaxValue;
            var maxY = int.MinValue;
            var minY = int.MaxValue;

            foreach (var screenPoint in deck.Cards().Select(card => TileToScreen(card.Location)))
            {
                if (screenPoint.X > maxX) maxX = screenPoint.X;
                if (screenPoint.X < minX) minX = screenPoint.X;
                if (screenPoint.Y > maxY) maxY = screenPoint.Y;
                if (screenPoint.Y < minY) minY = screenPoint.Y;
            }

            _topLeftCoord = new Point(minX, minY);
            var bitmapMapWidth = maxX - minX + TileWidth;
            var bitmapMapHeight = maxY - minY + 2*TileHeight + TileHeightHalf;
            BufferImage = new Bitmap(bitmapMapWidth, bitmapMapHeight + BounceAnimationOffset);
        }

        /// <summary>
        ///     Converts a tile point to an isometric pixel coordinate
        /// </summary>
        /// <param name="tilePoint">The tile coords of the tile you want the screen point of.</param>
        /// <returns>The pixel coords of where that tile is in an isometric view.</returns>
        private static Point TileToScreen(Point tilePoint)
        {
            var screenX = (tilePoint.X - tilePoint.Y)*TileWidthHalf;
            var screenY = (tilePoint.X + tilePoint.Y)*TileHeightHalf;
            return new Point(screenX, screenY);
        }

        private static bool IsTilePointNotOnTile(Point point, IDeck deck)
        {
            return deck.Cards().All(card => card.Location != point);
        }

        /// <summary>
        ///     Checks to see if the mouse is in the isometric tile graphic by calculating the border functions
        ///     and checking if the mouse is on the right side of each one.
        /// </summary>
        /// <param name="positiveCardLocation">The cards location that were checking.</param>
        /// <param name="mouseX">Mouses X Location inside the <see cref="MapUi" /> _map bitmap.</param>
        /// <param name="mouseY">Mouses Y Location inside the <see cref="MapUi" /> _map bitmap.</param>
        /// <returns></returns>
        private bool IsMouseInTile(Point positiveCardLocation, int mouseX, int mouseY)
        {
            mouseX = (int) (mouseX/_zoom);
            mouseY = (int) (mouseY/_zoom);

            mouseY -= BounceAnimationOffset;
            var buttonXDistR = (mouseX - positiveCardLocation.X - TileWidth)/2;
            var buttonXDistL = (mouseX - positiveCardLocation.X)/2;
            var yMidLine = positiveCardLocation.Y + TileHeight + TileHeightHalf;

            if (mouseY >= yMidLine + buttonXDistL) return false;
            if (mouseY >= yMidLine - buttonXDistR) return false;
            if (mouseY <= yMidLine - buttonXDistL) return false;
            return mouseY > yMidLine + buttonXDistR;
        }

        /// <summary>
        ///     If the user presses a key that key gets passed to all sub Ui's.
        /// </summary>
        /// <param name="e"></param>
        /// <returns>False if the click event should be consitered 'swallowed'.</returns>
        public override bool SendKey(KeyEventArgs e)
        {
            const int moveAmount = 20;
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (e.KeyCode)
            {
                case Keys.Left:
                    Location = new Point(Location.X - moveAmount, Location.Y);
                    break;
                case Keys.Right:
                    Location = new Point(Location.X + moveAmount, Location.Y);
                    break;
                case Keys.Up:
                    Location = new Point(Location.X, Location.Y - moveAmount);
                    break;
                case Keys.Down:
                    Location = new Point(Location.X, Location.Y + moveAmount);
                    break;
            }

            return base.SendKey(e);
        }

        /// <summary>
        ///     Draws this Ui onto the <see cref="Graphics" /> object.
        /// </summary>
        /// <param name="g">The <see cref="Graphics" /> object to draw on.</param>
        public override void Draw(Graphics g)
        {
            base.Draw(g);

            var cardsInDrawOrder = PopulateDecks();
            AdjustAnimationFrame();

            var mapGraphics = Graphics.FromImage(BufferImage);
            // Extra height for bounce animation
            mapGraphics.TranslateTransform(0, BounceAnimationOffset);
            mapGraphics.SmoothingMode = SmoothingMode.HighQuality;

            TileMouseIsOver = null;

            // Draw the cards in the correct order (low Y first) by removing them from the priority queue;
            while (cardsInDrawOrder.Count > 0)
            {
                var card = cardsInDrawOrder.Dequeue();

                var cardDrawPos = CardDrawPoint(card);
                var imageName = card.ResourceName;
                var imageMod = "";

                if (IsMouseInTile(cardDrawPos, _mouseLocation.X, _mouseLocation.Y))
                {
                    // Appending bright to the image name so is displays a bright "moused over" image.
                    TileMouseIsOver = card;
                    imageMod = TrashMode ? "-red" : "-bright";

                    if (_borderDeck.CardList.Contains(card) && _buyDeckUi?.SelectedCardViewer?.TrackedCard != null)
                    {
                        imageName = _buyDeckUi.SelectedCardViewer.TrackedCard.ResourceName;
                        imageMod = "-superbright";
                    }

                    if (!_ignoreShading && (SelectPointMode || Player.Hand.CardList.Contains(card)))
                        DrawActionInfoText(mapGraphics, cardDrawPos);
                }

                if (!Player.Hand.CardList.Contains(card) && !_borderDeck.CardList.Contains(card))
                {
                    if (_buyDeckUi?.SelectedCardViewer?.TrackedCard != card)
                        imageMod = "-dim";
                }

                cardDrawPos = new Point(Math.Min(0, cardDrawPos.X), Math.Min(0, cardDrawPos.Y));
                DrawTileGraphics(mapGraphics, imageName + (_ignoreShading ? "" : imageMod), cardDrawPos);
            }

            g.SmoothingMode = SmoothingMode.HighQuality;

            var cm = new ColorMatrix {Matrix33 = _transparency};
            var ia = new ImageAttributes();
            ia.SetColorMatrix(cm);
            g.DrawImage(BufferImage,
                new Rectangle(Location.X, Location.Y, (int) (BufferImage.Width*_zoom), (int) (BufferImage.Height*_zoom)),
                0, 0, BufferImage.Width, BufferImage.Height, GraphicsUnit.Pixel, ia);

            //g.DrawImage(BufferImage, Location.X, Location.Y, BufferImage.Width*_zoom, BufferImage.Height*_zoom);
        }

        private void DrawActionInfoText(Graphics g, Point tileDrawPoint)
        {
            var measure = g.MeasureString(_actionInfoText, ActionInfoTextFont);
            var xOffset = 32 - measure.Width/2;
            const int yOffset = -13;
            var drawPoint = new Point((int) (tileDrawPoint.X + xOffset), tileDrawPoint.Y + yOffset);
            g.DrawString(_actionInfoText, ActionInfoTextFont2, Brushes.Black, drawPoint);
            g.DrawString(_actionInfoText, ActionInfoTextFont, new SolidBrush(_actionInfoTextColor),
                new Point(drawPoint.X + 1, drawPoint.Y - 1));
        }

        private Point CardDrawPoint(ICard card)
        {
            var posCardLoc = TileToScreen(card.Location);

            float yMod = 0;
            if (card == _currentExpandingTile)
            {
                yMod = AnimationFunction.EaseInOutCirc(AnimationFrame, 0, BounceAnimationOffset, AnimationFrames);
            }

            return new Point(posCardLoc.X - _topLeftCoord.X, (int) Math.Max(0, posCardLoc.Y - _topLeftCoord.Y - yMod));
        }

        private static IDeck CalculateBorderDeck(IDeck allCardsDeck)
        {
            var borderDeck = new Deck();
            var surroundingPoints = new List<Point>();
            foreach (var card in allCardsDeck.Cards())
            {
                var p = new Point(card.Location.X + 1, card.Location.Y);
                if (IsTilePointNotOnTile(p, allCardsDeck)) surroundingPoints.Add(p);
                p = new Point(card.Location.X - 1, card.Location.Y);
                if (IsTilePointNotOnTile(p, allCardsDeck)) surroundingPoints.Add(p);
                p = new Point(card.Location.X, card.Location.Y + 1);
                if (IsTilePointNotOnTile(p, allCardsDeck)) surroundingPoints.Add(p);
                p = new Point(card.Location.X, card.Location.Y - 1);
                if (IsTilePointNotOnTile(p, allCardsDeck)) surroundingPoints.Add(p);
            }
            surroundingPoints = surroundingPoints.Distinct().ToList();

            foreach (var surroundingPoint in surroundingPoints)
            {
                borderDeck.AddCard(new BorderCard {Location = surroundingPoint});
            }

            return borderDeck;
        }

        private static void DrawTileGraphics(Graphics g, string tileName, Point location)
        {
            g.DrawImage(FastSafeImageResource.GetTileImageFromName(tileName), location.X, location.Y, TileWidth,
                TileHeight*2);
            g.DrawImage(Resources._base, location.X, location.Y + TileHeight + TileHeightHalf, TileWidth, TileHeight);
        }

        private SimplePriorityQueue<ICard> PopulateDecks()
        {
            var priorSpm = SelectPointMode;
            SelectPointMode = _buyDeckUi?.SelectedCardViewer?.TrackedCard != null;
            if (priorSpm && !SelectPointMode) Location = new Point(Location.X + TileWidth/2, Location.Y + TileHeight/2);
            if (!priorSpm && SelectPointMode) Location = new Point(Location.X - TileWidth/2, Location.Y - TileHeight/2);

            TrashMode = Game.Players[Game.CurrentPlayer].Nukes > 0;

            var cardsInDrawOrder = new SimplePriorityQueue<ICard>();

            var allCardsDeck = Player.DrawPile.AppendDeck(Player.Hand.AppendDeck(Player.DiscardPile));


            if (SelectPointMode)
            {
                _borderDeck = CalculateBorderDeck(allCardsDeck);

                allCardsDeck = allCardsDeck.AppendDeck(_borderDeck);
            }

            // Load all cards into a priority queue.
            foreach (var card in allCardsDeck.Cards())
            {
                cardsInDrawOrder.Enqueue(card, TileToScreen(card.Location).Y);
            }
            CreateNewBitmapToFitMap(allCardsDeck);
            return cardsInDrawOrder;
        }

        public override bool SendMouseLocation(int x, int y)
        {
            _mouseLocation = new Point(x, y);
            return base.SendMouseLocation(x, y);
        }

        /// <summary>
        ///     If the user clicks a Ui the mouse coords should be sent to each sub Ui.
        ///     The Ui should have event handlers to fire when specific things happen.
        /// </summary>
        /// <param name="x">Mouse click X pos</param>
        /// <param name="y">Mouse click Y pos</param>
        /// <returns>False if the click event should be consitered 'swallowed'.</returns>
        public override bool SendClick(int x, int y)
        {
            if (TileMouseIsOver == null) return true;
            if (SelectPointMode && TileMouseIsOver.Name.Equals("Border Card"))
            {
                if (_buyDeckUi?.SelectedCardViewer?.TrackedCard != null)
                {
                    return
                        !Game.BuyCard(_buyDeckUi.SelectedCardViewer.TrackedCard.Name, Game.Players[Game.CurrentPlayer],
                            TileMouseIsOver.Location.X, TileMouseIsOver.Location.Y);
                }
            }
            else if (TrashMode && Player.Hand.InDeck(TileMouseIsOver))
            {
                Game.Players[Game.CurrentPlayer].TrashCard(TileMouseIsOver);
            }
            else
            {
                if (!Game.Players[Game.CurrentPlayer].PlayCard(TileMouseIsOver)) return false;
                // Set up play animation.
                _currentExpandingTile = TileMouseIsOver;
                AnimationFrame = 0;
                return false;
            }
            return true;
        }
    }
}