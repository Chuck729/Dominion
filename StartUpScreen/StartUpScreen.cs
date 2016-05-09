using GUI;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace StartUpScreen
{
    public partial class StartUpScreen : Form
    {
        public StartUpScreen()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            check2Players.Checked = true;
            Size = new Size(950, 650);
            CenterToScreen();
        }

        /// <summary>
        /// Action handler for check2Players
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void check2Players_CheckedChanged(object sender, EventArgs e)
        {
            if (!check2Players.Checked && !check3Players.Checked && !check4Players.Checked)
            {
                check2Players.Checked = true;
            }
            else if(check2Players.Checked)
            {
                check3Players.Checked = false;
                check4Players.Checked = false;
                player3Name.Visible = false;
                player4Name.Visible = false;
            } 
        }

        /// <summary>
        /// Action handler for check3Players
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void check3Players_CheckChanged(object sender, EventArgs e)
        {
            if(!check2Players.Checked && !check3Players.Checked && !check4Players.Checked)
            {
                check3Players.Checked = true;
            } else if (check3Players.Checked)
            {
                check2Players.Checked = false;
                check4Players.Checked = false;
                player3Name.Visible = true;
                player4Name.Visible = false;
            }
        }

        /// <summary>
        /// Action handler for check4Players
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void check4Players_CheckedChanged(object sender, EventArgs e)
        {
            if (!check2Players.Checked && !check3Players.Checked && !check4Players.Checked)
            {
                check4Players.Checked = true;
            }
            else if(check4Players.Checked)
            {
                check2Players.Checked = false;
                check3Players.Checked = false;
                player3Name.Visible = true;
                player4Name.Visible = true;
            }
        }

        /// <summary>
        /// Action handler for player1Name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void player1Name_TextChanged(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Action handler for player2Name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void player2Name_TextChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Action handler for player3Name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void player3Name_TextChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Action handler for player4Name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void player4Name_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void player1Name_Click(object sender, EventArgs e)
        {
            player1Name.Text = "";
        }

        private void player2Name_Click(object sender, EventArgs e)
        {
            player2Name.Text = "";
        }

        private void player3Name_Click(object sender, EventArgs e)
        {
            player3Name.Text = "";
        }

        private void player4Name_Click(object sender, EventArgs e)
        {
            player4Name.Text = "";
        }

        private void player1Name_LostFocus(object sender, EventArgs e)
        {
            if(player1Name.Text.Equals(""))
            {
                player1Name.Text = @"Player 1";
            }
        }

        private void player2Name_LostFocus(object sender, EventArgs e)
        {
            if (player2Name.Text.Equals(""))
            {
                player2Name.Text = @"Player 2";
            }
        }

        private void player3Name_LostFocus(object sender, EventArgs e)
        {
            if (player3Name.Text.Equals(""))
            {
                player3Name.Text = @"Player 3";
            }
        }

        private void player4Name_LostFocus(object sender, EventArgs e)
        {
            if (player4Name.Text.Equals(""))
            {
                player4Name.Text = @"Player 4";
            }
        }

        /// <summary>
        /// Action handler for startButton
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startButton_Click(object sender, EventArgs e)
        {
            MainForm frm;

            if (check2Players.Checked)
            {
                if (player1Name.Text.Equals("") || player2Name.Text.Equals(""))
                {
                    errorMessage.Text = @"Error: Please name all players";
                    return;
                }

                frm = new MainForm(player1Name.Text, player2Name.Text, null, null, new Random().Next());
            } else if(check3Players.Checked)
            {
                if (player1Name.Text.Equals("") || player2Name.Text.Equals("") || player3Name.Text.Equals(""))
                {
                    errorMessage.Text = @"Error: Please name all players";
                    return;
                }

                frm = new MainForm(player1Name.Text, player2Name.Text, player3Name.Text, null, new Random().Next());
            } else
            {
                if (player1Name.Text.Equals("") || player2Name.Text.Equals("") || player3Name.Text.Equals("")
                        || player4Name.Text.Equals(""))
                {
                    errorMessage.Text = @"Error: Please name all players";
                    return;
                }

                frm = new MainForm(player1Name.Text, player2Name.Text, player3Name.Text, player4Name.Text, new Random().Next());
            }

            frm.Location = Location;
            frm.StartPosition = FormStartPosition.Manual;
            frm.FormClosing += delegate { Close(); };
            frm.Show();
            Hide();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            titleLabel.Location = new Point((ClientSize.Width - titleLabel.Width) / 2, titleLabel.Location.Y);
            playerCountLabel.Location = new Point((ClientSize.Width - playerCountLabel.Width) / 2, playerCountLabel.Location.Y);
            playerNameLabel.Location = new Point((ClientSize.Width - playerNameLabel.Width) / 2, playerNameLabel.Location.Y);
            newGameLabel.Location = new Point((ClientSize.Width - newGameLabel.Width) / 2, newGameLabel.Location.Y);

            player1Name.Location = new Point((ClientSize.Width - player1Name.Width) / 2, player1Name.Location.Y);
            player2Name.Location = new Point((ClientSize.Width - player2Name.Width) / 2, player1Name.Location.Y + 25);
            player3Name.Location = new Point((ClientSize.Width - player3Name.Width) / 2, player2Name.Location.Y + 25);
            player4Name.Location = new Point((ClientSize.Width - player4Name.Width) / 2, player3Name.Location.Y + 25);

            startButton.Location = new Point((ClientSize.Width - startButton.Width) / 2, startButton.Location.Y);

            check2Players.Location = new Point(((ClientSize.Width - check2Players.Width)/2) - check3Players.Width, check2Players.Location.Y);
            check3Players.Location = new Point((ClientSize.Width - check3Players.Width)/2, check3Players.Location.Y);
            check4Players.Location = new Point(((ClientSize.Width - check4Players.Width)/2) + check3Players.Width, check4Players.Location.Y);
        }

        private void StartAndChooseButton_Click(object sender, EventArgs e)
        {
            MainForm frm;

            var chooseCardScreen = new ChooseCardsScreen();
            chooseCardScreen.ShowDialog();
            var cardList =  chooseCardScreen.GetCardList();

            if (check2Players.Checked)
            {
                if (player1Name.Text.Equals("") || player2Name.Text.Equals(""))
                {
                    errorMessage.Text = @"Error: Please name all players";
                    return;
                }

                frm = new MainForm(player1Name.Text, player2Name.Text, null, null, new Random().Next(), cardList);
            }
            else if (check3Players.Checked)
            {
                if (player1Name.Text.Equals("") || player2Name.Text.Equals("") || player3Name.Text.Equals(""))
                {
                    errorMessage.Text = @"Error: Please name all players";
                    return;
                }

                frm = new MainForm(player1Name.Text, player2Name.Text, player3Name.Text, null, new Random().Next(), cardList);
            }
            else
            {
                if (player1Name.Text.Equals("") || player2Name.Text.Equals("") || player3Name.Text.Equals("")
                        || player4Name.Text.Equals(""))
                {
                    errorMessage.Text = @"Error: Please name all players";
                    return;
                }

                frm = new MainForm(player1Name.Text, player2Name.Text, player3Name.Text, player4Name.Text, new Random().Next(), cardList);
            }

            frm.Location = Location;
            frm.StartPosition = FormStartPosition.Manual;
            frm.FormClosing += delegate { Close(); };
            frm.Show();
            Hide();
        }
    }
}
