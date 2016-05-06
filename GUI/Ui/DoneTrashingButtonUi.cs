﻿using System;
using System.Drawing;
using RHFYP.Interfaces;

namespace GUI.Ui
{
    internal class DoneTrashingButtonUi : ButtonUi
    {
        /// <summary>
        ///     Creates a Ui element that views all buttons
        /// </summary>
        /// <param name="game">The game because all Ui elements have access to game.</param>
        /// <param name="text">The text that the button should display.</param>
        /// <param name= CardType.Action>The <see cref= CardType.Action /> you want to be invoked when this button is clicked.</param>
        public DoneTrashingButtonUi(IGame game, string text, Action action, int width, int height)
            : base(game, text, action, width, height)
        {
            BorderPen = new Pen(Color.FromArgb(200, 80, 90, 90), 1.5f);
            ButtonBrush = new SolidBrush(Color.FromArgb(200, 120, 70, 70));
            MousedOverButtonBrush = new SolidBrush(Color.FromArgb(120, 150, 60, 60));
            ClickedButtonBrush = new SolidBrush(Color.FromArgb(120, 150, 30, 30));
        }

        /// <summary>
        ///     Draws this Ui onto the <see cref="Graphics" /> object.
        /// </summary>
        /// <param name="g">The <see cref="Graphics" /> object to draw on.</param>
        public override void Draw(Graphics g)
        {
            var bgBrush = ButtonBrush;
            if (IsMouseOnButton()) bgBrush = MousedOverButtonBrush;
            if (Clicked) bgBrush = ClickedButtonBrush;

            g.FillRectangle(bgBrush, new Rectangle(Location.X, Location.Y,
                Width, Height));
            g.DrawRectangle(BorderPen, new Rectangle(Location.X, Location.Y,
                Width, Height));

            g.DrawString("Done Trashing (" + Game.Players[Game.CurrentPlayer].Nukes + ")", TextFont, Active ? TextBrush : InactiveTextBrush, Location);

            if (!Clicked) return;
            Clicked = false;
        }
    }
}