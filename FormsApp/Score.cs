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

        private const int SCORE_GRP_X = 30;
        private const int SCORE_GRP_Y = 30;

        private int SCORE_GRP_WIDTH;
        private const int SCORE_GRP_HEIGHT = 450;

        private World world;

        private List<ScoreGroupBox> scoreGroupBoxes;

        public Score(World w)
        {
            world = w;
            
            InitializeComponent();
            //SCORE_GRP_WIDTH = this.Width - 100;
            SCORE_GRP_WIDTH = 550;

            scoreGroupBoxes = new List<ScoreGroupBox>();
        }

        
        private void Score_Load(object sender, EventArgs e)
        {
            Logger.WriteLogFile("ouverture score");
            int nbColony = 0;

            timerScore.Start();

            this.SuspendLayout();
            this.WindowState = FormWindowState.Maximized;

            foreach (Colony colony in world.myWorld.Colonies())
            {
                ScoreGroupBox sgb = new ScoreGroupBox(colony, SCORE_GRP_WIDTH);

                if (nbColony > 0)
                {
                    sgb.Location = new System.Drawing.Point(SCORE_GRP_X + nbColony * (SCORE_GRP_WIDTH + 10), SCORE_GRP_Y);
                }
                else
                {
                    sgb.Location = new System.Drawing.Point(SCORE_GRP_X, SCORE_GRP_Y);
                }


                sgb.Name = "grpColonie" + nbColony;
                sgb.Size = new System.Drawing.Size(SCORE_GRP_WIDTH, SCORE_GRP_HEIGHT);
                sgb.TabIndex = 1;
                sgb.TabStop = false;
                sgb.Text = "Colonie " + colony.Color.ToKnownColor().ToString();

                sgb.Display();

                nbColony++;

                scoreGroupBoxes.Add(sgb);

                pnlScore.Controls.Add(sgb);
            }
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        public void RefreshData()
        {
            foreach (ScoreGroupBox sgb in scoreGroupBoxes)
            {
                sgb.RefreshData();
            }
        }

        private void timerScore_Tick(object sender, EventArgs e)
        {
            RefreshData();
        }
    }
}
