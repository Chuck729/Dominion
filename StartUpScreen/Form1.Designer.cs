﻿namespace StartUpScreen
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.titleLabel = new System.Windows.Forms.Label();
            this.check2Players = new System.Windows.Forms.CheckBox();
            this.check3Players = new System.Windows.Forms.CheckBox();
            this.check4Players = new System.Windows.Forms.CheckBox();
            this.startButton = new System.Windows.Forms.Button();
            this.player1Name = new System.Windows.Forms.TextBox();
            this.player4Name = new System.Windows.Forms.TextBox();
            this.player3Name = new System.Windows.Forms.TextBox();
            this.player2Name = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.BackColor = System.Drawing.Color.Black;
            this.titleLabel.Font = new System.Drawing.Font("Bauhaus 93", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.ForeColor = System.Drawing.Color.White;
            this.titleLabel.Location = new System.Drawing.Point(80, 21);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(435, 38);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "Rose-Hulman\'s Five Year Plan";
            // 
            // check2Players
            // 
            this.check2Players.AutoSize = true;
            this.check2Players.Location = new System.Drawing.Point(53, 146);
            this.check2Players.Name = "check2Players";
            this.check2Players.Size = new System.Drawing.Size(38, 21);
            this.check2Players.TabIndex = 1;
            this.check2Players.Text = "2";
            this.check2Players.UseVisualStyleBackColor = true;
            this.check2Players.CheckedChanged += new System.EventHandler(this.check2Players_CheckedChanged);
            // 
            // check3Players
            // 
            this.check3Players.AutoSize = true;
            this.check3Players.Location = new System.Drawing.Point(53, 173);
            this.check3Players.Name = "check3Players";
            this.check3Players.Size = new System.Drawing.Size(38, 21);
            this.check3Players.TabIndex = 2;
            this.check3Players.Text = "3";
            this.check3Players.UseVisualStyleBackColor = true;
            this.check3Players.CheckedChanged += new System.EventHandler(this.check3Players_CheckChanged);
            // 
            // check4Players
            // 
            this.check4Players.AutoSize = true;
            this.check4Players.Location = new System.Drawing.Point(53, 200);
            this.check4Players.Name = "check4Players";
            this.check4Players.Size = new System.Drawing.Size(38, 21);
            this.check4Players.TabIndex = 3;
            this.check4Players.Text = "4";
            this.check4Players.UseVisualStyleBackColor = true;
            this.check4Players.CheckedChanged += new System.EventHandler(this.check4Players_CheckedChanged);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(226, 348);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(171, 62);
            this.startButton.TabIndex = 4;
            this.startButton.Text = "Start!";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // player1Name
            // 
            this.player1Name.Location = new System.Drawing.Point(315, 144);
            this.player1Name.Name = "player1Name";
            this.player1Name.Size = new System.Drawing.Size(197, 22);
            this.player1Name.TabIndex = 5;
            this.player1Name.Text = "Player 1";
            this.player1Name.TextChanged += new System.EventHandler(this.player1Name_TextChanged);
            // 
            // player4Name
            // 
            this.player4Name.Location = new System.Drawing.Point(315, 229);
            this.player4Name.Name = "player4Name";
            this.player4Name.Size = new System.Drawing.Size(197, 22);
            this.player4Name.TabIndex = 6;
            this.player4Name.Text = "Player 4";
            this.player4Name.TextChanged += new System.EventHandler(this.player4Name_TextChanged);
            // 
            // player3Name
            // 
            this.player3Name.Location = new System.Drawing.Point(315, 201);
            this.player3Name.Name = "player3Name";
            this.player3Name.Size = new System.Drawing.Size(197, 22);
            this.player3Name.TabIndex = 7;
            this.player3Name.Text = "Player 3";
            this.player3Name.TextChanged += new System.EventHandler(this.player3Name_TextChanged);
            // 
            // player2Name
            // 
            this.player2Name.Location = new System.Drawing.Point(315, 173);
            this.player2Name.Name = "player2Name";
            this.player2Name.Size = new System.Drawing.Size(197, 22);
            this.player2Name.TabIndex = 8;
            this.player2Name.Text = "Player 2";
            this.player2Name.TextChanged += new System.EventHandler(this.player2Name_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(619, 458);
            this.Controls.Add(this.player2Name);
            this.Controls.Add(this.player3Name);
            this.Controls.Add(this.player4Name);
            this.Controls.Add(this.player1Name);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.check4Players);
            this.Controls.Add(this.check3Players);
            this.Controls.Add(this.check2Players);
            this.Controls.Add(this.titleLabel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.CheckBox check2Players;
        private System.Windows.Forms.CheckBox check3Players;
        private System.Windows.Forms.CheckBox check4Players;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.TextBox player1Name;
        private System.Windows.Forms.TextBox player4Name;
        private System.Windows.Forms.TextBox player3Name;
        private System.Windows.Forms.TextBox player2Name;
    }
}
