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
            int drot = MotherNature.alea.Next(0, 90);
            double rot = drot * Math.PI / 180; // rotation of the whole rock
            do
            {
                double angle = deg * Math.PI / 180;
                int noise = MotherNature.alea.Next(0, Math.Min(width,height)/5); // roughen the shape a bit
                Point np = new Point((int)(location.X + Math.Cos(angle) * (width / 2 + noise)), (int)(location.Y + Math.Sin(angle) * (height / 2 + noise)));
                // rotate around the center point
                double nx = location.X + (np.X - location.X) * Math.Cos(rot) - (np.Y - location.Y) * Math.Sin(rot);
                double ny = location.Y + (np.X - location.X) * Math.Sin(rot) + (np.Y - location.Y) * Math.Cos(rot);
                np.X = (int)nx;
                np.Y = (int)ny;
                shape.Add(np);
                deg += MotherNature.alea.Next(10, 30);
            } while (deg < 360);
        }

        public Point[] Shape { get => shape.ToArray(); }
    }
}
