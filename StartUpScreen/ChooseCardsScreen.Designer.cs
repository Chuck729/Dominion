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
            this.ToggleSelectAllButton = new System.Windows.Forms.Button();
            this.SubmitButton = new System.Windows.Forms.Button();
            this.SubmitNumRandCards = new System.Windows.Forms.Button();
            this.NumRandomCardsDownButton = new System.Windows.Forms.Button();
            this.NumRandomCardsUpButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cardSelectorLB
            // 
            this.cardSelectorLB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(60)))), ((int)(((byte)(52)))));
            this.cardSelectorLB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.cardSelectorLB.CheckOnClick = true;
            this.cardSelectorLB.ForeColor = System.Drawing.Color.White;
            this.cardSelectorLB.FormattingEnabled = true;
            this.cardSelectorLB.Location = new System.Drawing.Point(219, 111);
            this.cardSelectorLB.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.cardSelectorLB.MultiColumn = true;
            this.cardSelectorLB.Name = "cardSelectorLB";
            this.cardSelectorLB.Size = new System.Drawing.Size(363, 128);
            this.cardSelectorLB.TabIndex = 0;
            this.cardSelectorLB.SelectedIndexChanged += new System.EventHandler(this.cardSelectorLB_SelectedIndexChanged);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(60)))), ((int)(((byte)(52)))));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Trebuchet MS", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.Color.White;
            this.textBox1.Location = new System.Drawing.Point(282, 76);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(211, 28);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "Select Action Cards";
            // 
            // ToggleSelectAllButton
            // 
            this.ToggleSelectAllButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.ToggleSelectAllButton.FlatAppearance.BorderColor = System.Drawing.Color.Brown;
            this.ToggleSelectAllButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ToggleSelectAllButton.Font = new System.Drawing.Font("Trebuchet MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ToggleSelectAllButton.ForeColor = System.Drawing.Color.White;
            this.ToggleSelectAllButton.Location = new System.Drawing.Point(207, 331);
            this.ToggleSelectAllButton.Name = "ToggleSelectAllButton";
            this.ToggleSelectAllButton.Size = new System.Drawing.Size(133, 34);
            this.ToggleSelectAllButton.TabIndex = 14;
            this.ToggleSelectAllButton.Text = "Select All";
            this.ToggleSelectAllButton.UseVisualStyleBackColor = false;
            this.ToggleSelectAllButton.Click += new System.EventHandler(this.SelectAllCards_Click);
            // 
            // SubmitButton
            // 
            this.SubmitButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.SubmitButton.FlatAppearance.BorderColor = System.Drawing.Color.Brown;
            this.SubmitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SubmitButton.Font = new System.Drawing.Font("Trebuchet MS", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SubmitButton.ForeColor = System.Drawing.Color.White;
            this.SubmitButton.Location = new System.Drawing.Point(346, 330);
            this.SubmitButton.Name = "SubmitButton";
            this.SubmitButton.Size = new System.Drawing.Size(222, 34);
            this.SubmitButton.TabIndex = 16;
            this.SubmitButton.Text = "Submit";
            this.SubmitButton.UseVisualStyleBackColor = false;
            this.SubmitButton.Click += new System.EventHandler(this.SubmitButton_Click);
            // 
            // SubmitNumRandCards
            // 
            this.SubmitNumRandCards.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.SubmitNumRandCards.FlatAppearance.BorderColor = System.Drawing.Color.Brown;
            this.SubmitNumRandCards.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SubmitNumRandCards.Font = new System.Drawing.Font("Trebuchet MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SubmitNumRandCards.ForeColor = System.Drawing.Color.White;
            this.SubmitNumRandCards.Location = new System.Drawing.Point(207, 291);
            this.SubmitNumRandCards.Name = "SubmitNumRandCards";
            this.SubmitNumRandCards.Size = new System.Drawing.Size(311, 34);
            this.SubmitNumRandCards.TabIndex = 19;
            this.SubmitNumRandCards.Text = "Generate 10 Random Cards";
            this.SubmitNumRandCards.UseVisualStyleBackColor = false;
            this.SubmitNumRandCards.Click += new System.EventHandler(this.SubmitNumRandCards_Click);
            // 
            // NumRandomCardsDownButton
            // 
            this.NumRandomCardsDownButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.NumRandomCardsDownButton.FlatAppearance.BorderColor = System.Drawing.Color.Brown;
            this.NumRandomCardsDownButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.NumRandomCardsDownButton.Font = new System.Drawing.Font("Trebuchet MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NumRandomCardsDownButton.ForeColor = System.Drawing.Color.White;
            this.NumRandomCardsDownButton.Location = new System.Drawing.Point(524, 291);
            this.NumRandomCardsDownButton.Name = "NumRandomCardsDownButton";
            this.NumRandomCardsDownButton.Size = new System.Drawing.Size(19, 34);
            this.NumRandomCardsDownButton.TabIndex = 20;
            this.NumRandomCardsDownButton.Text = "↓";
            this.NumRandomCardsDownButton.UseVisualStyleBackColor = false;
            this.NumRandomCardsDownButton.Click += new System.EventHandler(this.NumRandomCardsDownButton_Click);
            // 
            // NumRandomCardsUpButton
            // 
            this.NumRandomCardsUpButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.NumRandomCardsUpButton.FlatAppearance.BorderColor = System.Drawing.Color.Brown;
            this.NumRandomCardsUpButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.NumRandomCardsUpButton.Font = new System.Drawing.Font("Trebuchet MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NumRandomCardsUpButton.ForeColor = System.Drawing.Color.White;
            this.NumRandomCardsUpButton.Location = new System.Drawing.Point(549, 291);
            this.NumRandomCardsUpButton.Name = "NumRandomCardsUpButton";
            this.NumRandomCardsUpButton.Size = new System.Drawing.Size(19, 34);
            this.NumRandomCardsUpButton.TabIndex = 21;
            this.NumRandomCardsUpButton.Text = "↑\t";
            this.NumRandomCardsUpButton.UseVisualStyleBackColor = false;
            this.NumRandomCardsUpButton.Click += new System.EventHandler(this.NumRandomCardsUpButton_Click);
            // 
            // ChooseCardsScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(60)))), ((int)(((byte)(52)))));
            this.ClientSize = new System.Drawing.Size(879, 591);
            this.Controls.Add(this.NumRandomCardsUpButton);
            this.Controls.Add(this.NumRandomCardsDownButton);
            this.Controls.Add(this.SubmitNumRandCards);
            this.Controls.Add(this.SubmitButton);
            this.Controls.Add(this.ToggleSelectAllButton);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.cardSelectorLB);
            this.Font = new System.Drawing.Font("Trebuchet MS", 9F);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "ChooseCardsScreen";
            this.Text = "Rose-Hulman\'s Five Year Plan";
            this.Load += new System.EventHandler(this.ChooseCardsScreen_Load);
            this.SizeChanged += new System.EventHandler(this.ChooseCardsScreen_SizeChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox cardSelectorLB;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button ToggleSelectAllButton;
        private System.Windows.Forms.Button SubmitButton;
        private System.Windows.Forms.Button SubmitNumRandCards;
        private System.Windows.Forms.Button NumRandomCardsDownButton;
        private System.Windows.Forms.Button NumRandomCardsUpButton;
    }
}