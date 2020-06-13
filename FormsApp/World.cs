using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Krohonde.Creatures;
using Krohonde.World;
using Creatures;
using System.Collections;
using System.Drawing.Drawing2D;
using System.Windows;
using Point = System.Drawing.Point;

namespace FormsApp
{
    public partial class World : Form
    {
        private MotherNature myWorld;

        public World()
        {
            InitializeComponent();
            myWorld = new MotherNature(this.ClientSize.Width, this.ClientSize.Height);
            Random alea = new Random();
            for (int i=0; i<20;i++)
            {
                myWorld.AddAnt(new WorkerAnt(new Point(this.ClientSize.Width / 2 + alea.Next(-50, 50), this.ClientSize.Height / 2 + alea.Next(-50, 50)), new Vector(alea.Next(-10,10), alea.Next(-10, 10)), myWorld));
            }
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

            //set the InterpolationMode to HighQualityBicubic so to ensure a high
            //quality image once it is transformed to the specified size
            gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;

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
            foreach (Ant ant in myWorld.Ants)
            {
                Image sourceImage = global::FormsApp.Properties.Resources.ant;
                sourceImage = RotateImage(sourceImage, ant.Heading);
                graphics.DrawImage(sourceImage, (int)ant.X, (int)ant.Y, sourceImage.Width, sourceImage.Height);
            }

            // Anthill
            Point[] ahill = new Point[] {
                new Point { X = 100, Y = 100 },
                new Point { X = 150, Y = 70 },
                new Point { X = 200, Y = 110 },
                new Point { X = 150, Y = 150 },
                new Point { X = 200, Y = 250 },
                new Point { X = 100, Y = 200 }
            };
            graphics.FillClosedCurve(new TextureBrush(Properties.Resources.anthill), ahill);
            graphics.DrawPolygon(new Pen(Color.Black), ahill);
        }
    }
}
