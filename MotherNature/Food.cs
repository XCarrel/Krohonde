using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Point = System.Drawing.Point;

namespace Krohonde.World
{
    public class Food
    {
        private Point location; // Where the food is
        private int value;      // Nutritive value

        public Food (Point loc, int val)
        {
            location = loc;
            value = val;
        }

        // Clone constructor
        public Food (Food f)
        {
            location = f.Location;
            value = f.Value;
        }

        public Point Location { get => location; }
        public int Value { get => value; }
    }
}
