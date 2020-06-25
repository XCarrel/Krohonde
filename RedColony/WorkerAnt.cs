using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Krohonde.RedColony
{
    public class WorkerAnt : Ant
    {
        public WorkerAnt(Point location, Point speed, RedColony colony) : base (location,speed,colony)
        {
        }

        public override void Live(double deltatime)
        {
            if (Selected)
            {
                Console.WriteLine($"Colony size = {MyColony.Size}");
                Build();
                Console.WriteLine($"Colony size = {MyColony.Size}");
                Selected = false;
            }
            Move(deltatime);
        }
    }
}
