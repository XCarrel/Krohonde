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
            if (MotherNature.alea.Next(0, 5) == 0)
                if (MotherNature.alea.Next(0, 2) == 0)
                    Speed.X = MotherNature.alea.Next(0, 9) - 4;
                else
                    Speed.Y = MotherNature.alea.Next(0, 9) - 4;
            Move(deltatime);
        }
    }
}
