﻿namespace StartUpScreen
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
            this.SuspendLayout();
            // 
            // cardSelectorLB
            // 
            this.cardSelectorLB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(40)))), ((int)(((byte)(35)))));
            this.cardSelectorLB.CheckOnClick = true;
            this.cardSelectorLB.ForeColor = System.Drawing.Color.White;
            this.cardSelectorLB.FormattingEnabled = true;
            this.cardSelectorLB.IntegralHeight = false;
            this.cardSelectorLB.Location = new System.Drawing.Point(0, 0);
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
            this.textBox1.Location = new System.Drawing.Point(12, 232);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(529, 100);
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
            // ChooseCardsScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(40)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(554, 676);
            this.Controls.Add(this.DeselectAllCards);
            this.Controls.Add(this.SelectAllCards);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.cardSelectorLB);
            this.Font = new System.Drawing.Font("Trebuchet MS", 9F);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "ChooseCardsScreen";
            this.Text = "ChooseCardsScreen";
            this.Load += new System.EventHandler(this.ChooseCardsScreen_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox cardSelectorLB;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button SelectAllCards;
        private System.Windows.Forms.Button DeselectAllCards;
    }
}