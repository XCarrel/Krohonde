using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Krohonde.BlueColony
{
    public class BlueQueen: Queen
    {
        public BlueQueen(Point location, Point speed, Colony colony) : base(location, speed, colony)
        { }
        public override void Live(double deltatime)
        {
            DoNothing(); // The queen MUST either do something (Move, Eat, Lay an egg) or announce that she does nothing
        }

    }
}
