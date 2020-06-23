using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Krohonde.GreenColony
{
    public class WorkerAnt : Ant
    {
        public WorkerAnt(Point location, Point speed, GreenColony colony) : base (location,speed,colony)
        {
        }

        public override void Live(double deltatime)
        {
            List<Brick> stuff = BricksAroundMe();
            if (stuff.Count() > 0) Console.WriteLine(string.Format("{0} sees {1} bricks", this.Fullname, stuff.Count()));
            Move(deltatime);
        }
    }
}
