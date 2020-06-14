using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Point = System.Drawing.Point;

namespace Krohonde
{
    public class Resource
    {
        private Point location; // Where the food is
        private int value;      // Nutritive value

        public Resource(Point loc, int val)
        {
            location = loc;
            value = val;
        }

        public Point Location { get => location; }
        public int Value { get => value; }

    }
}
