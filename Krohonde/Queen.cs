using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krohonde
{
    public class Queen : Ant
    {
        public Queen(Point location, Point speed, Colony colony): base (location,speed,colony)
        { }
        public override void Live(double deltatime)
        {
            throw new NotImplementedException();
        }
    }
}
