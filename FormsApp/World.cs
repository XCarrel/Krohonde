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

namespace FormsApp
{
    public partial class World : Form
    {
        private MotherNature myWorld;

        public World()
        {
            InitializeComponent();
            myWorld = new MotherNature(pctWorld.ClientSize.Width, pctWorld.ClientSize.Height);
            myWorld.Seed();
            myWorld.Quake();
            Colony colo = new Colony(new System.Windows.Point(400, 200), myWorld);
            colo.Spawn(300);
            myWorld.AddColony(colo);
            colo = new Colony(new System.Windows.Point(1200, 600), myWorld);
            colo.Spawn(500);
            myWorld.AddColony(colo);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            myWorld.Live();
            pctWorld.Invalidate();
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
            gfx.DrawImage(img, 0,0,img.Width,img.Height);

            //dispose of our Graphics object
            gfx.Dispose();

            //return the image
            return bmp;
        }

        private void pctWorld_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            // All ants
            foreach (Colony colony in myWorld.Colonies)
            {
                foreach (Ant ant in colony.Population)
                {
                    Image sourceImage = global::FormsApp.Properties.Resources.ant;
                    sourceImage = RotateImage(sourceImage, ant.Heading+90);
                    graphics.DrawImage(sourceImage, (int)ant.X, (int)ant.Y, sourceImage.Width, sourceImage.Height);
                }
                graphics.FillClosedCurve(new TextureBrush(Properties.Resources.anthill), colony.Hill);
                graphics.DrawPolygon(new Pen(Color.Black), colony.Hill);
            }

            // Food
            foreach (FoodCluster fc in myWorld.FoodStock)
            {
                graphics.DrawCurve(new Pen(new TextureBrush(Properties.Resources.pollen), 5), fc.Content.Select(x => x.Location).ToArray());
            }
            // Bricks
            foreach (BrickCluster bc in myWorld.BrickStock)
            {
                graphics.DrawCurve(new Pen(new TextureBrush(Properties.Resources.brick), 5), bc.Content.Select(x => x.Location).ToArray());
            }
        }
    }
}
