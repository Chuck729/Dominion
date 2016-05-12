namespace StartUpScreen
{
    partial class ChooseCardsScreen
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
            this.cardSelectorLB = new System.Windows.Forms.CheckedListBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SelectAllCards = new System.Windows.Forms.Button();
            this.DeselectAllCards = new System.Windows.Forms.Button();
            this.SubmitButton = new System.Windows.Forms.Button();
            this.numUpDown = new System.Windows.Forms.NumericUpDown();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.SubmitNumRandCards = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // cardSelectorLB
            // 
            this.cardSelectorLB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(40)))), ((int)(((byte)(35)))));
            this.cardSelectorLB.CheckOnClick = true;
            this.cardSelectorLB.ForeColor = System.Drawing.Color.White;
            this.cardSelectorLB.FormattingEnabled = true;
            this.cardSelectorLB.IntegralHeight = false;
            this.cardSelectorLB.Location = new System.Drawing.Point(-1, 38);
            this.cardSelectorLB.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.cardSelectorLB.MultiColumn = true;
            this.cardSelectorLB.Name = "cardSelectorLB";
            this.cardSelectorLB.Size = new System.Drawing.Size(629, 195);
            this.cardSelectorLB.TabIndex = 0;
            this.cardSelectorLB.SelectedIndexChanged += new System.EventHandler(this.cardSelectorLB_SelectedIndexChanged);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(40)))), ((int)(((byte)(35)))));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Trebuchet MS", 12F);
            this.textBox1.ForeColor = System.Drawing.Color.White;
            this.textBox1.Location = new System.Drawing.Point(17, 12);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(525, 26);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "Select all action cards that you want to be in the game.";
            // 
            // SelectAllCards
            // 
            this.SelectAllCards.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.SelectAllCards.FlatAppearance.BorderColor = System.Drawing.Color.Brown;
            this.SelectAllCards.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SelectAllCards.Font = new System.Drawing.Font("Trebuchet MS", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectAllCards.ForeColor = System.Drawing.Color.White;
            this.SelectAllCards.Location = new System.Drawing.Point(12, 318);
            this.SelectAllCards.Name = "SelectAllCards";
            this.SelectAllCards.Size = new System.Drawing.Size(264, 52);
            this.SelectAllCards.TabIndex = 14;
            this.SelectAllCards.Text = "Select All Cards";
            this.SelectAllCards.UseVisualStyleBackColor = false;
            this.SelectAllCards.Click += new System.EventHandler(this.SelectAllCards_Click);
            // 
            // DeselectAllCards
            // 
            this.DeselectAllCards.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.DeselectAllCards.FlatAppearance.BorderColor = System.Drawing.Color.Brown;
            this.DeselectAllCards.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DeselectAllCards.Font = new System.Drawing.Font("Trebuchet MS", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeselectAllCards.ForeColor = System.Drawing.Color.White;
            this.DeselectAllCards.Location = new System.Drawing.Point(282, 318);
            this.DeselectAllCards.Name = "DeselectAllCards";
            this.DeselectAllCards.Size = new System.Drawing.Size(264, 52);
            this.DeselectAllCards.TabIndex = 15;
            this.DeselectAllCards.Text = "Deselect All Cards";
            this.DeselectAllCards.UseVisualStyleBackColor = false;
            this.DeselectAllCards.Click += new System.EventHandler(this.DeselectAllCards_Click);
            // 
            // SubmitButton
            // 
            this.SubmitButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.SubmitButton.FlatAppearance.BorderColor = System.Drawing.Color.Brown;
            this.SubmitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SubmitButton.Font = new System.Drawing.Font("Trebuchet MS", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SubmitButton.ForeColor = System.Drawing.Color.White;
            this.SubmitButton.Location = new System.Drawing.Point(142, 438);
            this.SubmitButton.Name = "SubmitButton";
            this.SubmitButton.Size = new System.Drawing.Size(264, 52);
            this.SubmitButton.TabIndex = 16;
            this.SubmitButton.Text = "Submit";
            this.SubmitButton.UseVisualStyleBackColor = false;
            this.SubmitButton.Click += new System.EventHandler(this.SubmitButton_Click);
            // 
            // numUpDown
            // 
            this.numUpDown.Location = new System.Drawing.Point(85, 243);
            this.numUpDown.Name = "numUpDown";
            this.numUpDown.Size = new System.Drawing.Size(47, 25);
            this.numUpDown.TabIndex = 17;
            this.numUpDown.ValueChanged += new System.EventHandler(this.numUpDown_ValueChanged);
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(40)))), ((int)(((byte)(35)))));
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Font = new System.Drawing.Font("Trebuchet MS", 12F);
            this.textBox2.ForeColor = System.Drawing.Color.White;
            this.textBox2.Location = new System.Drawing.Point(154, 241);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(512, 24);
            this.textBox2.TabIndex = 18;
            this.textBox2.Text = "Number of random cards to select";
            // 
            // SubmitNumRandCards
            // 
            this.SubmitNumRandCards.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.SubmitNumRandCards.FlatAppearance.BorderColor = System.Drawing.Color.Brown;
            this.SubmitNumRandCards.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SubmitNumRandCards.Font = new System.Drawing.Font("Trebuchet MS", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SubmitNumRandCards.ForeColor = System.Drawing.Color.White;
            this.SubmitNumRandCards.Location = new System.Drawing.Point(138, 267);
            this.SubmitNumRandCards.Name = "SubmitNumRandCards";
            this.SubmitNumRandCards.Size = new System.Drawing.Size(294, 45);
            this.SubmitNumRandCards.TabIndex = 19;
            this.SubmitNumRandCards.Text = "Select Random Cards";
            this.SubmitNumRandCards.UseVisualStyleBackColor = false;
            this.SubmitNumRandCards.Click += new System.EventHandler(this.SubmitNumRandCards_Click);
            // 
            // ChooseCardsScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(40)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(554, 676);
            this.Controls.Add(this.SubmitNumRandCards);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.numUpDown);
            this.Controls.Add(this.SubmitButton);
            this.Controls.Add(this.DeselectAllCards);
            this.Controls.Add(this.SelectAllCards);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.cardSelectorLB);
            this.Font = new System.Drawing.Font("Trebuchet MS", 9F);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "ChooseCardsScreen";
            this.Text = "ChooseCardsScreen";
            this.Load += new System.EventHandler(this.ChooseCardsScreen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox cardSelectorLB;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button SelectAllCards;
        private System.Windows.Forms.Button DeselectAllCards;
        private System.Windows.Forms.Button SubmitButton;
        private System.Windows.Forms.NumericUpDown numUpDown;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button SubmitNumRandCards;
    }
}