namespace StartUpScreen
{
    partial class StartUpScreen
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
            this.playerCountLabel = new System.Windows.Forms.Label();
            this.playerNameLabel = new System.Windows.Forms.Label();
            this.errorMessage = new System.Windows.Forms.Label();
            this.newGameLabel = new System.Windows.Forms.Label();
            this.StartAndChooseButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.titleLabel.AutoSize = true;
            this.titleLabel.BackColor = System.Drawing.Color.Transparent;
            this.titleLabel.Font = new System.Drawing.Font("Trebuchet MS", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.ForeColor = System.Drawing.Color.White;
            this.titleLabel.Location = new System.Drawing.Point(170, 142);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(674, 59);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "Rose-Hulman\'s Five Year Plan";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // check2Players
            // 
            this.check2Players.AutoSize = true;
            this.check2Players.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.check2Players.Location = new System.Drawing.Point(386, 295);
            this.check2Players.Name = "check2Players";
            this.check2Players.Size = new System.Drawing.Size(41, 27);
            this.check2Players.TabIndex = 1;
            this.check2Players.Text = "2";
            this.check2Players.UseVisualStyleBackColor = true;
            this.check2Players.CheckedChanged += new System.EventHandler(this.check2Players_CheckedChanged);
            // 
            // check3Players
            // 
            this.check3Players.AutoSize = true;
            this.check3Players.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.check3Players.Location = new System.Drawing.Point(426, 295);
            this.check3Players.Name = "check3Players";
            this.check3Players.Size = new System.Drawing.Size(41, 27);
            this.check3Players.TabIndex = 2;
            this.check3Players.Text = "3";
            this.check3Players.UseVisualStyleBackColor = true;
            this.check3Players.CheckedChanged += new System.EventHandler(this.check3Players_CheckChanged);
            // 
            // check4Players
            // 
            this.check4Players.AutoSize = true;
            this.check4Players.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.check4Players.Location = new System.Drawing.Point(466, 295);
            this.check4Players.Name = "check4Players";
            this.check4Players.Size = new System.Drawing.Size(41, 27);
            this.check4Players.TabIndex = 3;
            this.check4Players.Text = "4";
            this.check4Players.UseVisualStyleBackColor = true;
            this.check4Players.CheckedChanged += new System.EventHandler(this.check4Players_CheckedChanged);
            // 
            // startButton
            // 
            this.startButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.startButton.FlatAppearance.BorderColor = System.Drawing.Color.Brown;
            this.startButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.startButton.Font = new System.Drawing.Font("Trebuchet MS", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startButton.ForeColor = System.Drawing.Color.White;
            this.startButton.Location = new System.Drawing.Point(44, 513);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(264, 52);
            this.startButton.TabIndex = 4;
            this.startButton.Text = "Start!";
            this.startButton.UseVisualStyleBackColor = false;
            this.startButton.Click += new System.EventHandler(this.StartButtonClick);
            // 
            // player1Name
            // 
            this.player1Name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.player1Name.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.player1Name.Location = new System.Drawing.Point(369, 362);
            this.player1Name.Name = "player1Name";
            this.player1Name.Size = new System.Drawing.Size(135, 26);
            this.player1Name.TabIndex = 5;
            this.player1Name.Text = "Player 1";
            this.player1Name.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.player1Name.Click += new System.EventHandler(this.Player1NameClick);
            this.player1Name.TextChanged += new System.EventHandler(this.player1Name_TextChanged);
            this.player1Name.LostFocus += new System.EventHandler(this.player1Name_LostFocus);
            // 
            // player4Name
            // 
            this.player4Name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.player4Name.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.player4Name.Location = new System.Drawing.Point(369, 457);
            this.player4Name.Name = "player4Name";
            this.player4Name.Size = new System.Drawing.Size(135, 26);
            this.player4Name.TabIndex = 6;
            this.player4Name.Text = "Player 4";
            this.player4Name.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.player4Name.Click += new System.EventHandler(this.player4Name_Click);
            this.player4Name.TextChanged += new System.EventHandler(this.Player4NameTextChanged);
            this.player4Name.LostFocus += new System.EventHandler(this.player4Name_LostFocus);
            // 
            // player3Name
            // 
            this.player3Name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.player3Name.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.player3Name.Location = new System.Drawing.Point(369, 425);
            this.player3Name.Name = "player3Name";
            this.player3Name.Size = new System.Drawing.Size(135, 26);
            this.player3Name.TabIndex = 7;
            this.player3Name.Text = "Player 3";
            this.player3Name.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.player3Name.Click += new System.EventHandler(this.player3Name_Click);
            this.player3Name.TextChanged += new System.EventHandler(this.Player3NameTextChanged);
            this.player3Name.LostFocus += new System.EventHandler(this.player3Name_LostFocus);
            // 
            // player2Name
            // 
            this.player2Name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.player2Name.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.player2Name.Location = new System.Drawing.Point(369, 394);
            this.player2Name.Name = "player2Name";
            this.player2Name.Size = new System.Drawing.Size(135, 26);
            this.player2Name.TabIndex = 8;
            this.player2Name.Text = "Player 2";
            this.player2Name.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.player2Name.Click += new System.EventHandler(this.Player2NameClick);
            this.player2Name.TextChanged += new System.EventHandler(this.Player2NameTextChanged);
            this.player2Name.LostFocus += new System.EventHandler(this.player2Name_LostFocus);
            // 
            // playerCountLabel
            // 
            this.playerCountLabel.AutoSize = true;
            this.playerCountLabel.Font = new System.Drawing.Font("Trebuchet MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playerCountLabel.Location = new System.Drawing.Point(382, 267);
            this.playerCountLabel.Name = "playerCountLabel";
            this.playerCountLabel.Size = new System.Drawing.Size(166, 24);
            this.playerCountLabel.TabIndex = 9;
            this.playerCountLabel.Text = "Number of Players";
            // 
            // playerNameLabel
            // 
            this.playerNameLabel.AutoSize = true;
            this.playerNameLabel.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playerNameLabel.Location = new System.Drawing.Point(408, 330);
            this.playerNameLabel.Name = "playerNameLabel";
            this.playerNameLabel.Size = new System.Drawing.Size(72, 26);
            this.playerNameLabel.TabIndex = 10;
            this.playerNameLabel.Text = "Names";
            // 
            // errorMessage
            // 
            this.errorMessage.AutoSize = true;
            this.errorMessage.Location = new System.Drawing.Point(12, 432);
            this.errorMessage.Name = "errorMessage";
            this.errorMessage.Size = new System.Drawing.Size(0, 17);
            this.errorMessage.TabIndex = 11;
            // 
            // newGameLabel
            // 
            this.newGameLabel.AutoSize = true;
            this.newGameLabel.Font = new System.Drawing.Font("Trebuchet MS", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newGameLabel.Location = new System.Drawing.Point(394, 216);
            this.newGameLabel.Name = "newGameLabel";
            this.newGameLabel.Size = new System.Drawing.Size(183, 43);
            this.newGameLabel.TabIndex = 12;
            this.newGameLabel.Text = "New Game";
            // 
            // StartAndChooseButton
            // 
            this.StartAndChooseButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.StartAndChooseButton.FlatAppearance.BorderColor = System.Drawing.Color.Brown;
            this.StartAndChooseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StartAndChooseButton.Font = new System.Drawing.Font("Trebuchet MS", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartAndChooseButton.ForeColor = System.Drawing.Color.White;
            this.StartAndChooseButton.Location = new System.Drawing.Point(466, 513);
            this.StartAndChooseButton.Name = "StartAndChooseButton";
            this.StartAndChooseButton.Size = new System.Drawing.Size(264, 52);
            this.StartAndChooseButton.TabIndex = 13;
            this.StartAndChooseButton.Text = "Choose Cards";
            this.StartAndChooseButton.UseVisualStyleBackColor = false;
            this.StartAndChooseButton.Click += new System.EventHandler(this.StartAndChooseButton_Click);
            // 
            // StartUpScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(40)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(1071, 637);
            this.Controls.Add(this.StartAndChooseButton);
            this.Controls.Add(this.newGameLabel);
            this.Controls.Add(this.errorMessage);
            this.Controls.Add(this.playerNameLabel);
            this.Controls.Add(this.playerCountLabel);
            this.Controls.Add(this.player2Name);
            this.Controls.Add(this.player3Name);
            this.Controls.Add(this.player4Name);
            this.Controls.Add(this.player1Name);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.check4Players);
            this.Controls.Add(this.check3Players);
            this.Controls.Add(this.check2Players);
            this.Controls.Add(this.titleLabel);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "StartUpScreen";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
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
        private System.Windows.Forms.Label playerCountLabel;
        private System.Windows.Forms.Label playerNameLabel;
        private System.Windows.Forms.Label errorMessage;
        private System.Windows.Forms.Label newGameLabel;
        private System.Windows.Forms.Button StartAndChooseButton;
    }
}

