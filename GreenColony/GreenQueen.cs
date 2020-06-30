using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Krohonde.GreenColony
{
    public class GreenQueen: Queen
    {
        public GreenQueen(Point location, Point speed, Colony colony) : base(location, speed, colony)
        { }
        public override void Live(double deltatime)
        {
        }

    }
}
