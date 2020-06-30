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
            List<Ant> foes = EnemiesAroundMe();
                foreach(Ant foe in foes)
                if (Helpers.Distance(SDLocation,foe.SDLocation) < Ant.HIT_REACH)
                    Hit(foes[0]);
            Move(deltatime);
        }
    }
}
