using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Point = System.Drawing.Point;

namespace Krohonde
{
    class Helpers
    {
        public static Point Rotate (Point v, int degrees)
        {
            var ca = Math.Cos((double)degrees * Math.PI / 180);
            var sa = Math.Sin((double)degrees * Math.PI / 180);
            return new Point((int)(ca * v.X - sa * v.Y), (int)(sa * v.X + ca * v.Y));
        }

        // Thanks to https://stackoverflow.com/questions/4243042/c-sharp-point-in-polygon
        public static bool IsInPolygon(Point[] poly, Point point)
        {
            var coef = poly.Skip(1).Select((p, i) =>
                                            (point.Y - poly[i].Y) * (p.X - poly[i].X)
                                          - (point.X - poly[i].X) * (p.Y - poly[i].Y))
                                    .ToList();

            if (coef.Any(p => p == 0))
                return true;

            for (int i = 1; i < coef.Count(); i++)
            {
                if (coef[i] * coef[i - 1] < 0)
                    return false;
            }
            return true;
        }
    }
}
