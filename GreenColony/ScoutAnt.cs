using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Krohonde.GreenColony
{
    public class ScoutAnt : Ant
    {
        public ScoutAnt(Point location, Point speed, GreenColony colony) : base(location, speed, colony)
        {
        }

        public override void Live(double deltatime)
        {
            if (MotherNature.alea.Next(0, 20) == 0) DropPheromon();
            Move(deltatime);
        }
    }
}
