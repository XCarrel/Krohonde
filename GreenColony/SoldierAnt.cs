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
            foreach (Ant foe in foes) Console.WriteLine(string.Format("{0} sees enemy {1}", this.Fullname, foe.Fullname));

            Move(deltatime);
        }
    }
}
