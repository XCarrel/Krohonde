using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Krohonde;
using Krohonde.RedColony;
using Krohonde.GreenColony;
using System.Diagnostics;

namespace FormsApp
{
    public partial class Score : Form
    {
        private const int SCORE_GRP_X = 35;
        private const int SCORE_GRP_Y = 80;
        private const int SCORE_GRP_WIDTH = 200;
        private const int SCORE_GRP_HEIGHT = 75;

        private World world;
        private List<Colony> colonies;
        private Stopwatch stopWatch;
        private System.Windows.Forms.GroupBox grpColonie;
        private System.Windows.Forms.Label lblAntNb;

        public List<Colony> Colonies
        {
            get
            {
                return colonies;
            }
            set
            {
                colonies = value;
            }
        }

        public Stopwatch StopWatch
        {
            get
            {
                return stopWatch;
            }
            set
            {
                stopWatch = value;
            }
        }

        public Score(World w)
        {
            world = w;
            InitializeComponent();
        }

         
        private void Score_Load(object sender, EventArgs e)
        {
            int nbColony = 0;

            TimeSpan ts = world.myWorld.universaltime.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = "";
            bool hourEmpty = true;

            if (ts.Hours > 0)
            {
                elapsedTime += String.Format("{0:00} h", ts.Hours);
                hourEmpty = false;
            }
            if (ts.Minutes > 0)
            {
                if (!hourEmpty)
                    elapsedTime += " ";
                elapsedTime += String.Format("{0:00} min", ts.Minutes);
                hourEmpty = false;
            }
            if (ts.Seconds > 0)
            {
                if (!hourEmpty)
                    elapsedTime += " ";
                elapsedTime += String.Format("{0:00} s", ts.Seconds);
            }

            this.Height = 0;

            lblElapsedTime.Text = lblElapsedTime.Text + " : " + elapsedTime;

            foreach (Colony colony in world.myWorld.Colonies())
            {
                this.grpColonie = new System.Windows.Forms.GroupBox();
                this.lblAntNb = new System.Windows.Forms.Label();
                this.grpColonie.SuspendLayout();
                // 
                // grpColonie
                // 
                this.grpColonie.Controls.Add(this.lblAntNb);
                if (nbColony > 0)
                {
                    this.grpColonie.Location = new System.Drawing.Point(SCORE_GRP_X, SCORE_GRP_Y + nbColony * (SCORE_GRP_HEIGHT + 10));
                } else
                {
                    this.grpColonie.Location = new System.Drawing.Point(SCORE_GRP_X, SCORE_GRP_Y);
                }
                this.grpColonie.Name = "grpColonie";
                this.grpColonie.Size = new System.Drawing.Size(SCORE_GRP_WIDTH, SCORE_GRP_HEIGHT);
                this.grpColonie.TabIndex = 1;
                this.grpColonie.TabStop = false;
                this.grpColonie.Text = "Colonie " + colony.Color.ToKnownColor().ToString();
                // 
                // lblAntNb
                // 
                this.lblAntNb.AutoSize = true;
                this.lblAntNb.Location = new System.Drawing.Point(25, 35);
                this.lblAntNb.Name = "lblAntNb";
                this.lblAntNb.Size = new System.Drawing.Size(128, 17);
                this.lblAntNb.TabIndex = 0;
                this.lblAntNb.Text = "Nombre de fourmis : " + colony.Population.Count;

                this.Controls.Add(this.grpColonie);
                this.grpColonie.ResumeLayout(false);
                this.grpColonie.PerformLayout();

                nbColony++;

                int taille = 50 + SCORE_GRP_Y + (nbColony * (SCORE_GRP_HEIGHT + 10));

                this.Height = taille;
                
                
            }
        }
    }
}
