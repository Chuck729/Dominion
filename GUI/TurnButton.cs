using System;
using System.Drawing;
using GUI.Ui;
using RHFYP.Interfaces;

namespace GUI
{
    enum TurnButtonMode
    {
        Inactive,
        EndActions,
        EndTurn
    }

    class TurnButton : ButtonUi
    {
        private TurnButtonMode _mode;

        private TurnButtonMode Mode
        {
            get { return _mode; }
            set
            {
                switch (Mode)
                {
                    case TurnButtonMode.Inactive:
                        Text = "End Turn";
                        Active = false;
                        Action = () => { };
                        break;
                    case TurnButtonMode.EndActions:
                        Text = "End Turn";
                        Active = true;
                        Action = Game.NextTurn;
                        break;
                    case TurnButtonMode.EndTurn:
                        Text = "End Turn";
                        Active = true;
                        Action = Game.NextTurn;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                _mode = value;
            }
        }

        /// <summary>
        /// Creates a Ui element that views all buttons 
        /// </summary>
        /// <param name="game">The game because all Ui elements have access to game.</param>
        /// <param name="text">The text that the button should display.</param>
        /// <param name=CardType.Action>The <see cref=CardType.Action/> you want to be invoked when this button is clicked.</param>
        public TurnButton(IGame game, int width, int height) : base(game, "", () => { }, width, height)
        {
            Text = "End Actions";
        }

        /// <summary>
        /// Draws this Ui onto the <see cref="Graphics" /> object.
        /// </summary>
        /// <param name="g">The <see cref="Graphics" /> object to draw on.</param>
        public override void Draw(Graphics g)
        {
            if (Game.Players[Game.CurrentPlayer].Investments == 0)
            {
            }
            base.Draw(g);
        }

        private void EndActionsAction()
        {
            Game.Players[Game.CurrentPlayer].EndActions();
        }
    }
}
