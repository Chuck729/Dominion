using GUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StartUpScreen
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            check2Players.Checked = true;
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
                    errorMessage.Text = "Error: Please name all players";
                    return;
                }

                frm = new MainForm(player1Name.Text, player2Name.Text, null, null);
            } else if(check3Players.Checked)
            {
                if (player1Name.Text.Equals("") || player2Name.Text.Equals("") || player3Name.Text.Equals(""))
                {
                    errorMessage.Text = "Error: Please name all players";
                    return;
                }

                frm = new MainForm(player1Name.Text, player2Name.Text, player3Name.Text, null);
            } else
            {
                if (player1Name.Text.Equals("") || player2Name.Text.Equals("") || player3Name.Text.Equals("")
                        || player4Name.Text.Equals(""))
                {
                    errorMessage.Text = "Error: Please name all players";
                    return;
                }

                frm = new MainForm(player1Name.Text, player2Name.Text, player3Name.Text, player4Name.Text);
            }

            frm.Location = this.Location;
            frm.StartPosition = FormStartPosition.Manual;
            frm.FormClosing += delegate { this.Show(); };
            frm.Show();
            this.Hide();
        }

        private void errorMessage_Click(object sender, EventArgs e)
        {

        }
    }
}
