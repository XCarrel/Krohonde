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
        private MotherNature myWorld;

        public World()
        {
            InitializeComponent();
            myWorld = new MotherNature(pctWorld.ClientSize.Width, pctWorld.ClientSize.Height);
            myWorld.Seed(); // put food in the world
            myWorld.Sprinkle(); // put construction material in the world
            myWorld.AddRocks(); // put rocks in the world
            RedColony rcolo = new RedColony(new System.Windows.Point(400, 200), myWorld);
            rcolo.Spawn(20);
            myWorld.AddColony(rcolo);
            GreenColony gcolo = new GreenColony(new System.Windows.Point(1200, 600), myWorld);
            gcolo.Spawn(50);
            myWorld.AddColony(gcolo);
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
            gfx.DrawImage(img, 0,0,img.Width,img.Height);

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
            foreach (Colony colony in myWorld.Colonies)
            {
                foreach (Ant ant in colony.Population)
                {
                    Image sourceImage = global::FormsApp.Properties.Resources.ant;
                    sourceImage = RotateImage(sourceImage, ant.Heading+90);
                    if (showOrigin) graphics.DrawLine(new Pen(colony.Color, 6), new System.Drawing.Point((int)ant.X, (int)ant.Y), new System.Drawing.Point((int)(ant.X + 24 * ant.Energy / MotherNature.MAX_ENERGY), (int)ant.Y));
                    graphics.DrawImage(sourceImage, (int)ant.X, (int)ant.Y, sourceImage.Width, sourceImage.Height);
                }
                graphics.FillClosedCurve(new TextureBrush(Properties.Resources.anthill), colony.Hill);
                graphics.FillEllipse(new SolidBrush(colony.Color), (float)colony.Location.X-15, (float)colony.Location.Y-15, 30, 30); // Colony "flag"
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

            // Rocks
            foreach (Rock r in myWorld.Rocks)
            {
                graphics.FillPolygon(new TextureBrush(Properties.Resources.rock), r.Shape);
                graphics.DrawPolygon(new Pen(Color.Black,2), r.Shape);
            }

            chkRenderOnce.Checked = false; // clear that flag for next loop
        }

    }
}
