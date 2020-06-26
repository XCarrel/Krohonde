namespace FormsApp
{
    partial class Score
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
            this.components = new System.ComponentModel.Container();
            this.pnlScore = new System.Windows.Forms.Panel();
            this.timerScore = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // pnlScore
            // 
            this.pnlScore.AutoScroll = true;
            this.pnlScore.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlScore.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pnlScore.Location = new System.Drawing.Point(20, 12);
            this.pnlScore.Name = "pnlScore";
            this.pnlScore.Size = new System.Drawing.Size(1036, 951);
            this.pnlScore.TabIndex = 0;
            // 
            // timerScore
            // 
            this.timerScore.Interval = 3000;
            this.timerScore.Tick += new System.EventHandler(this.timerScore_Tick);
            // 
            // Score
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::FormsApp.Properties.Resources.grass;
            this.ClientSize = new System.Drawing.Size(1078, 975);
            this.Controls.Add(this.pnlScore);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Score";
            this.Text = "Score";
            this.Load += new System.EventHandler(this.Score_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlScore;
        private System.Windows.Forms.Timer timerScore;
    }
}