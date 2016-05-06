using System;
using System.Collections.Generic;
using System.Drawing;
using RHFYP.Interfaces;

namespace GUI.Ui
{
    public class ButtonPanelUi : SimpleUi
    {
        private const int MarginBetweenButtons = 10;
        private int _height;
        private int _width;

        public ButtonPanelUi(IGame game) : base(game)
        {
            Buttons = new List<ButtonUi>();
        }

        public ICollection<ButtonUi> Buttons { get; }

        /// <summary>
        ///     Adds a <see cref="ISimpleUi" /> as a child of this <see cref="ISimpleUi" />.
        ///     Also properly sets the parent of the <paramref name="childUi" /> to this.
        /// </summary>
        /// <param name="childUi">The Ui you want to be displayed within this Ui.</param>
        public override void AddChildUi(ISimpleUi childUi)
        {
            var button = childUi as ButtonUi;
            if (button == null) throw new ArgumentException();
            Buttons.Add(button);
            base.AddChildUi(childUi);
        }

        /// <summary>
        ///     Draws this Ui onto the <see cref="Graphics" /> object.
        /// </summary>
        /// <param name="g">The <see cref="Graphics" /> object to draw on.</param>
        public override void Draw(Graphics g)
        {
            _width = 0;
            _height = 0;

            g.TranslateTransform(Location.X, Location.Y);

            foreach (var button in Buttons)
            {
                button.Location = new Point(0, _height);
                button.Draw(g);
                _height += MarginBetweenButtons + button.Height;
                _width = Math.Max(_width, button.Width);
            }

            BufferImage = new Bitmap(_width, _height);

            g.TranslateTransform(-Location.X, -Location.Y);
        }
    }
}