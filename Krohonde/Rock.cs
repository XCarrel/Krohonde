using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Krohonde
{
    public class Rock
    {
        private List<Point> shape;

        private int[] getArray(int nb, int max)
        {
            List<int> values = new List<int>();
            for (int i = 0; i < nb; i++) values.Add(MotherNature.alea.Next(0, max));
            values.Sort();
            return values.ToArray();
        }

        /// <summary>
        /// Shape generation algorithm: pick points at random on the ellipse defined by the parameters 
        /// </summary>
        /// <param name="location"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Rock(System.Windows.Point location, int width, int height)
        {
            shape = new List<Point>();
            int deg = 0;
            do
            {
                double angle = deg * Math.PI / 180;
                int noise = MotherNature.alea.Next(0, Math.Min(width,height)/10); // roughen the shape a bit
                shape.Add(new Point((int)(location.X + Math.Cos(angle) * (width / 2 + noise)), (int)(location.Y + Math.Sin(angle) * (height / 2 + noise))));
                deg += MotherNature.alea.Next(10, 30);
            } while (deg < 360);
            // rotate a bit so that it doesn't look too orthogonal
            double rot = MotherNature.alea.Next(0, 90) * Math.PI / 180;
            for (int i=0; i<shape.Count; i++)
            {
                Point p = shape[i];
                double nx = p.X * Math.Cos(rot) - p.Y * Math.Sin(rot);
                double ny = p.X * Math.Sin(rot) + p.Y * Math.Cos(rot);
                p.X = (int)nx;
                p.Y = (int)ny;
            }
        }

        public Point[] Shape { get => shape.ToArray(); }
    }
}
