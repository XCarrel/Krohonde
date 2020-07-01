using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Krohonde.GreenColony
{
    public class SoldierAnt : Ant
    {
        public SoldierAnt(Point location, Point speed, GreenColony colony) : base(location, speed, colony)
        {
        }

        public override void Live(double deltatime)
        {
            Speed.X = 10;
            Speed.Y = -10;
            Move(deltatime);

            /*while(MotherNature.PheromonTypes.Danger))
            {

            }
            */
        }
    }
}
