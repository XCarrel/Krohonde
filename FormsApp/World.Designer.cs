namespace FormsApp
{
    partial class World
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
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.pctWorld = new System.Windows.Forms.PictureBox();
            this.chkShowColonies = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pctWorld)).BeginInit();
            this.SuspendLayout();
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 50;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // pctWorld
            // 
            this.pctWorld.BackColor = System.Drawing.Color.White;
            this.pctWorld.BackgroundImage = global::FormsApp.Properties.Resources.grass;
            this.pctWorld.Location = new System.Drawing.Point(0, 0);
            this.pctWorld.Name = "pctWorld";
            this.pctWorld.Size = new System.Drawing.Size(5000, 5000);
            this.pctWorld.TabIndex = 0;
            this.pctWorld.TabStop = false;
            this.pctWorld.Paint += new System.Windows.Forms.PaintEventHandler(this.pctWorld_Paint);
            // 
            // chkShowColonies
            // 
            this.chkShowColonies.AutoSize = true;
            this.chkShowColonies.Location = new System.Drawing.Point(8, 8);
            this.chkShowColonies.Name = "chkShowColonies";
            this.chkShowColonies.Size = new System.Drawing.Size(135, 17);
            this.chkShowColonies.TabIndex = 1;
            this.chkShowColonies.Text = "Montrer l\'appartenance";
            this.chkShowColonies.UseVisualStyleBackColor = true;
            // 
            // World
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1201, 578);
            this.Controls.Add(this.chkShowColonies);
            this.Controls.Add(this.pctWorld);
            this.Name = "World";
            this.Text = "Krohonde";
            ((System.ComponentModel.ISupportInitialize)(this.pctWorld)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.PictureBox pctWorld;
        private System.Windows.Forms.CheckBox chkShowColonies;
    }
}

