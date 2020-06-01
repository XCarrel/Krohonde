using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Krohonde.World
{
    class Helpers
    {
        public static Vector Rotate (Vector v, int degrees)
        {
            var ca = Math.Cos((double)degrees * Math.PI / 180);
            var sa = Math.Sin((double)degrees * Math.PI / 180);
            return new Vector(ca * v.X - sa * v.Y, sa * v.X + ca * v.Y);
        }
    }
}
