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

        private void player1Name_TextChanged(object sender, EventArgs e)
        {

        }

        private void player2Name_TextChanged(object sender, EventArgs e)
        {

        }

        private void player3Name_TextChanged(object sender, EventArgs e)
        {

        }

        private void player4Name_TextChanged(object sender, EventArgs e)
        {

        }

        private void startButton_Click(object sender, EventArgs e)
        {
            var frm = new MainForm();
            frm.Location = this.Location;
            frm.StartPosition = FormStartPosition.Manual;
            frm.FormClosing += delegate { this.Show(); };
            frm.Show();
            this.Hide();
        }
    }
}
