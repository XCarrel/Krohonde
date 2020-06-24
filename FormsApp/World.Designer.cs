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
            this.chkRenderOnce = new System.Windows.Forms.CheckBox();
            this.chkRender = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pctWorld)).BeginInit();
            this.SuspendLayout();
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 10;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // pctWorld
            // 
            this.pctWorld.BackColor = System.Drawing.Color.White;
            this.pctWorld.BackgroundImage = global::FormsApp.Properties.Resources.grass;
            this.pctWorld.Location = new System.Drawing.Point(0, 0);
            this.pctWorld.Name = "pctWorld";
            this.pctWorld.Size = new System.Drawing.Size(1800, 1000);
            this.pctWorld.TabIndex = 0;
            this.pctWorld.TabStop = false;
            this.pctWorld.Paint += new System.Windows.Forms.PaintEventHandler(this.pctWorld_Paint);
            // 
            // chkShowColonies
            // 
            this.chkShowColonies.AutoSize = true;
            this.chkShowColonies.BackColor = System.Drawing.Color.Transparent;
            this.chkShowColonies.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkShowColonies.Location = new System.Drawing.Point(8, 8);
            this.chkShowColonies.Name = "chkShowColonies";
            this.chkShowColonies.Size = new System.Drawing.Size(157, 17);
            this.chkShowColonies.TabIndex = 1;
            this.chkShowColonies.Text = "Montrer l\'appartenance";
            this.chkShowColonies.UseVisualStyleBackColor = false;
            // 
            // chkRenderOnce
            // 
            this.chkRenderOnce.AutoSize = true;
            this.chkRenderOnce.BackColor = System.Drawing.Color.Transparent;
            this.chkRenderOnce.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkRenderOnce.Location = new System.Drawing.Point(8, 54);
            this.chkRenderOnce.Name = "chkRenderOnce";
            this.chkRenderOnce.Size = new System.Drawing.Size(147, 17);
            this.chkRenderOnce.TabIndex = 2;
            this.chkRenderOnce.Text = "Mettre à jour une fois";
            this.chkRenderOnce.UseVisualStyleBackColor = false;
            // 
            // chkRender
            // 
            this.chkRender.AutoSize = true;
            this.chkRender.BackColor = System.Drawing.Color.Transparent;
            this.chkRender.Checked = true;
            this.chkRender.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRender.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkRender.Location = new System.Drawing.Point(8, 31);
            this.chkRender.Name = "chkRender";
            this.chkRender.Size = new System.Drawing.Size(162, 17);
            this.chkRender.TabIndex = 3;
            this.chkRender.Text = "Mettre à jour en continu";
            this.chkRender.UseVisualStyleBackColor = false;
            // 
            // World
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1218, 595);
            this.Controls.Add(this.chkRender);
            this.Controls.Add(this.chkRenderOnce);
            this.Controls.Add(this.chkShowColonies);
            this.Controls.Add(this.pctWorld);
            this.Name = "World";
            this.Text = "Krohonde";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.pctWorld)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.PictureBox pctWorld;
        private System.Windows.Forms.CheckBox chkShowColonies;
        private System.Windows.Forms.CheckBox chkRenderOnce;
        private System.Windows.Forms.CheckBox chkRender;
    }
}

