using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Krohonde.BlueColony
{
    public class ScoutAnt : Ant
    {
        public List<Food> food;
        public int time = 50;
        Point destination;
        public ScoutAnt(Point location, Point speed, BlueColony colony) : base(location, speed, colony)
        {
            
        }

        public override void Live(double deltatime)
        {
                Move(deltatime);
        }
    }
}
