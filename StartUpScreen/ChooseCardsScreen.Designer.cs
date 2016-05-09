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
            this.SuspendLayout();
            // 
            // cardSelectorLB
            // 
            this.cardSelectorLB.FormattingEnabled = true;
            this.cardSelectorLB.Location = new System.Drawing.Point(0, 0);
            this.cardSelectorLB.Name = "cardSelectorLB";
            this.cardSelectorLB.Size = new System.Drawing.Size(403, 225);
            this.cardSelectorLB.TabIndex = 0;
            this.cardSelectorLB.SelectedIndexChanged += new System.EventHandler(this.cardSelectorLB_SelectedIndexChanged);
            // 
            // ChooseCardsScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 447);
            this.Controls.Add(this.cardSelectorLB);
            this.Name = "ChooseCardsScreen";
            this.Text = "ChooseCardsScreen";
            this.Load += new System.EventHandler(this.ChooseCardsScreen_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox cardSelectorLB;
    }
}