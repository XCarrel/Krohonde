using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Point = System.Drawing.Point;

namespace Krohonde
{
    public class Helpers
    {
        public static Point Rotate(Point v, int degrees)
        {
            var ca = Math.Cos((double)degrees * Math.PI / 180);
            var sa = Math.Sin((double)degrees * Math.PI / 180);
            return new Point((int)(ca * v.X - sa * v.Y), (int)(sa * v.X + ca * v.Y));
        }

        // Thanks to https://stackoverflow.com/questions/217578/how-can-i-determine-whether-a-2d-point-is-within-a-polygon
        // using the ray casting method
        public static bool IsInPolygon(Point[] poly, Point point)
        {
            Point origin = new Point(0, 0); // a reference used for heading calculation
            int nbIntersections = 0;
            for (int i = 1; i < poly.Count(); i++)
            {
                if (areIntersecting(0,0,point.X,point.Y,poly[i-1].X,poly[i-1].Y,poly[i].X,poly[i].Y)) nbIntersections++;
            }
            if (areIntersecting(0, 0, point.X, point.Y, poly[poly.Count()-1].X, poly[poly.Count() - 1].Y, poly[0].X, poly[0].Y)) nbIntersections++;
            return nbIntersections % 2 == 1;
        }

        // Thanks to https://stackoverflow.com/questions/217578/how-can-i-determine-whether-a-2d-point-is-within-a-polygon
        public static bool areIntersecting(
            float v1x1, float v1y1, float v1x2, float v1y2,
            float v2x1, float v2y1, float v2x2, float v2y2)
        {
            float d1, d2;
            float a1, a2, b1, b2, c1, c2;

            // Convert vector 1 to a line (line 1) of infinite length.
            // We want the line in linear equation standard form: A*x + B*y + C = 0
            // See: http://en.wikipedia.org/wiki/Linear_equation
            a1 = v1y2 - v1y1;
            b1 = v1x1 - v1x2;
            c1 = (v1x2 * v1y1) - (v1x1 * v1y2);

            // Every point (x,y), that solves the equation above, is on the line,
            // every point that does not solve it, is not. The equation will have a
            // positive result if it is on one side of the line and a negative one 
            // if is on the other side of it. We insert (x1,y1) and (x2,y2) of vector
            // 2 into the equation above.
            d1 = (a1 * v2x1) + (b1 * v2y1) + c1;
            d2 = (a1 * v2x2) + (b1 * v2y2) + c1;

            // If d1 and d2 both have the same sign, they are both on the same side
            // of our line 1 and in that case no intersection is possible. Careful, 
            // 0 is a special case, that's why we don't test ">=" and "<=", 
            // but "<" and ">".
            if (d1 > 0 && d2 > 0) return false;
            if (d1 < 0 && d2 < 0) return false;

            // The fact that vector 2 intersected the infinite line 1 above doesn't 
            // mean it also intersects the vector 1. Vector 1 is only a subset of that
            // infinite line 1, so it may have intersected that line before the vector
            // started or after it ended. To know for sure, we have to repeat the
            // the same test the other way round. We start by calculating the 
            // infinite line 2 in linear equation standard form.
            a2 = v2y2 - v2y1;
            b2 = v2x1 - v2x2;
            c2 = (v2x2 * v2y1) - (v2x1 * v2y2);

            // Calculate d1 and d2 again, this time using points of vector 1.
            d1 = (a2 * v1x1) + (b2 * v1y1) + c2;
            d2 = (a2 * v1x2) + (b2 * v1y2) + c2;

            // Again, if both have the same sign (and neither one is 0),
            // no intersection is possible.
            if (d1 > 0 && d2 > 0) return false;
            if (d1 < 0 && d2 < 0) return false;

            // If we get here, only two possibilities are left. Either the two
            // vectors intersect in exactly one point or they are collinear, which
            // means they intersect in any number of points from zero to infinite.
            if ((a1 * b2) - (a2 * b1) == 0.0f) return false;

            // If they are not collinear, they must intersect in exactly one point.
            return true;
        }

        public static double Distance(Point a, Point b)
        {
            return new Vector(b.X - a.X, b.Y - a.Y).Length;
        }
    }
}
