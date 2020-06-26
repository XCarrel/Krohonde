using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Windows;
using Krohonde;
using Krohonde.RedColony;
using Krohonde.GreenColony;

namespace FormsApp
{
    public partial class World : Form
    {
        public IMotherNature myWorld;
        private Score score;
        public World()
        {
            InitializeComponent();
            myWorld = new MotherNature(pctWorld.ClientSize.Width, pctWorld.ClientSize.Height);

            RedColony rcolo = new RedColony(new System.Windows.Point(400, 200), myWorld);
            rcolo.Spawn(40);
            myWorld.AddColony(rcolo);
            GreenColony gcolo = new GreenColony(new System.Windows.Point(400, 500), myWorld);
            gcolo.Spawn(40);
            myWorld.AddColony(gcolo);
            myWorld.Initialize();
            score = new Score(this);
            score.Show();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            myWorld.Live(); // update
            if (chkRender.Checked || chkRenderOnce.Checked) pctWorld.Invalidate(); // render

        }
        public static Image RotateImage(Image img, float rotationAngle)
        {
            //create an empty Bitmap image
            Bitmap bmp = new Bitmap(img.Width, img.Height);

            //turn the Bitmap into a Graphics object
            Graphics gfx = Graphics.FromImage(bmp);

            //now we set the rotation point to the center of our image
            gfx.TranslateTransform((float)bmp.Width / 2, (float)bmp.Height / 2);

            //now rotate the image
            gfx.RotateTransform(rotationAngle);

            gfx.TranslateTransform(-(float)bmp.Width / 2, -(float)bmp.Height / 2);

            //now draw our new image onto the graphics object
            gfx.DrawImage(img, 0, 0, img.Width, img.Height);

            //dispose of our Graphics object
            gfx.Dispose();

            //return the image
            return bmp;
        }

        private void pctWorld_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            bool showOrigin = chkShowColonies.Checked;

            // Colonies
            foreach (Colony colony in myWorld.Colonies())
            {
                foreach (Ant ant in colony.Population)
                {
                    Image sourceImage;
                    switch (ant.GetType().Name)
                    {
                        case "WorkerAnt":
                            sourceImage = global::FormsApp.Properties.Resources.worker;
                            break;
                        case "FarmerAnt":
                            sourceImage = global::FormsApp.Properties.Resources.farmer;
                            break;
                        case "SoldierAnt":
                            sourceImage = global::FormsApp.Properties.Resources.soldier;
                            break;
                        case "ScoutAnt":
                            sourceImage = global::FormsApp.Properties.Resources.scout;
                            break;
                        default:
                            sourceImage = global::FormsApp.Properties.Resources.worker;
                            break;
                    }
                    sourceImage = RotateImage(sourceImage, ant.Heading + 90);
                    graphics.DrawImage(sourceImage, (int)ant.X, (int)ant.Y, sourceImage.Width, sourceImage.Height);
                    
                    // Origin
                    if (showOrigin) graphics.DrawLine(new Pen(colony.Color, 6), new System.Drawing.Point((int)ant.X, (int)ant.Y), new System.Drawing.Point((int)(ant.X + 24 * ant.Energy / MotherNature.MAX_ENERGY), (int)ant.Y));
                    
                    // Selection
                    if (ant.Selected) graphics.DrawEllipse(new Pen(colony.Color, 4), ant.SDLocation.X, ant.SDLocation.Y, 24, 24);

                    /*/ Show fights
                    if (ant.HitBy != null) graphics.DrawLine(new Pen(Color.OrangeRed, 10), new System.Drawing.Point((int)(ant.X+12), (int)(ant.Y+12)), new System.Drawing.Point((int)(ant.HitBy.X + 12), (int)(ant.HitBy.Y + 12)));
                    //*/
                }
                graphics.FillClosedCurve(new TextureBrush(Properties.Resources.anthill), colony.Hill);
                graphics.FillEllipse(new SolidBrush(colony.Color), (float)colony.Location.X - 15, (float)colony.Location.Y - 15, 30, 30); // Colony "flag"
                graphics.DrawPolygon(new Pen(Color.Black), colony.Hill);
            }

            // Food
            foreach (FoodCluster fc in myWorld.FoodStock())
            {
                graphics.DrawCurve(new Pen(new TextureBrush(Properties.Resources.pollen), 5), fc.Content.Select(x => x.Location).ToArray());
            }
            // Bricks
            foreach (BrickCluster bc in myWorld.BrickStock())
            {
                graphics.DrawCurve(new Pen(new TextureBrush(Properties.Resources.brick), 5), bc.Content.Select(x => x.Location).ToArray());
            }

            // Rocks
            foreach (Rock r in myWorld.Rocks())
            {
                graphics.FillPolygon(new TextureBrush(Properties.Resources.rock), r.Shape);
                graphics.DrawPolygon(new Pen(Color.Black, 2), r.Shape);
            }

            // Pheromons
            foreach (Pheromon phero in myWorld.Pheromons())
            {
                Image img;
                switch (phero.PheromonType)
                {
                    case MotherNature.PheromonTypes.Build: img = global::FormsApp.Properties.Resources.pherobuild; break;
                    case MotherNature.PheromonTypes.Food: img = global::FormsApp.Properties.Resources.pherofood; break;
                    case MotherNature.PheromonTypes.Danger: img = global::FormsApp.Properties.Resources.pherodanger; break;
                    default: img = global::FormsApp.Properties.Resources.pherodanger; break;
                }
                graphics.DrawImage(img, (int)phero.Location.X, (int)phero.Location.Y, img.Width / 2, img.Height / 2);
                if (showOrigin) graphics.DrawLine(new Pen(phero.Colony.Color, 6), new System.Drawing.Point((int)phero.Location.X, (int)phero.Location.Y), new System.Drawing.Point((int)(phero.Location.X + 24 * phero.Intensity), (int)phero.Location.Y));
            }

            chkRenderOnce.Checked = false; // clear that flag for next loop
        }

        private void pctWorld_Click(object sender, EventArgs e)
        {
            int distmin = Width;
            Ant chosen = null;
            System.Drawing.Point mouse = new System.Drawing.Point(MousePosition.X, MousePosition.Y);
            foreach (Colony colo in myWorld.Colonies())
                foreach (Ant ant in colo.Population)
                    if (Helpers.Distance(new System.Drawing.Point((int)ant.HeadPosition.X, (int)ant.HeadPosition.Y), mouse) < distmin)
                    {
                        distmin = (int)Helpers.Distance(ant.SDLocation, mouse);
                        chosen = ant;
                    }
            chosen.Selected = !chosen.Selected;
        }
    }
}
