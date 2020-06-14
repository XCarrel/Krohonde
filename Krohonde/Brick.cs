using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Point = System.Drawing.Point;

namespace Krohonde
{
    public class Brick : Resource
    {
        public Brick(Point loc, int val) : base(loc, val) { } 
    }
}
