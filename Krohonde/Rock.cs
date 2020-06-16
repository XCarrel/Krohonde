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
        /// Shape generation algorithm:
        ///  - Generate an array of random x-coordinates ordered ascending
        ///  - Generate an array of random y-coordinates ordered ascending
        ///  - Apply the random curve obtained in all 4 quadrants around the location
        /// </summary>
        /// <param name="location"></param>
        /// <param name="nbpoints"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Rock(System.Windows.Point location, int nbpoints, int width, int height)
        {
            shape = new List<Point>();

            int[] axs;
            int[] ays;
            axs = getArray(nbpoints, width);
            ays = getArray(nbpoints, height);
            for (int i = 0; i < axs.Length; i++)
            {
                shape.Add(new Point((int)(location.X - width + axs[i]), (int)(location.Y + ays[i])));
            }
            axs = getArray(nbpoints, width);
            ays = getArray(nbpoints, height);
            for (int i = 0; i < axs.Length; i++)
            {
                shape.Add(new Point((int)(location.X + axs[i]), (int)(location.Y + height - ays[i])));
            }
            axs = getArray(nbpoints, width);
            ays = getArray(nbpoints, height);
            for (int i = 0; i < axs.Length; i++)
            {
                shape.Add(new Point((int)(location.X + width - axs[i]), (int)(location.Y - ays[i])));
            }
            axs = getArray(nbpoints, width);
            ays = getArray(nbpoints, height);
            for (int i = 0; i < axs.Length; i++)
            {
                shape.Add(new Point((int)(location.X - axs[i]), (int)(location.Y -height + ays[i])));
            }
        }

        public Point[] Shape { get => shape.ToArray(); }
    }
}
