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
using Point = System.Windows.Point;

namespace FormsApp
{
    public partial class Form1 : Form
    {
        private MotherNature myWorld;

        public Form1()
        {
            InitializeComponent();
            myWorld = new MotherNature();
            myWorld.AddAnt(new WorkerAnt(new Point(0, this.ClientSize.Height), new Vector(2, -15), myWorld));
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            myWorld.Live();
            this.Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            foreach (Ant ant in myWorld.Ants)
            {
                Image sourceImage = global::FormsApp.Properties.Resources.WorkerAnt;
                sourceImage = RotateImage(sourceImage, ant.Heading);
                graphics.DrawImage(sourceImage, (int)ant.X, (int)ant.Y,sourceImage.Width,sourceImage.Height);
            }
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
    }
}
