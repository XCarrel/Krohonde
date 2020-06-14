using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Krohonde
{
    public class WorkerAnt : Ant
    {
        public WorkerAnt(Point location, Point speed, MotherNature world) : base (location,speed,world)
        {
        }

        public override void Live()
        {
            base.Live();
            if (MyWorld.alea.Next(0, 10) == 0)
                if (MyWorld.alea.Next(0, 2) == 0)
                    Speed.X = MyWorld.alea.Next(0, 9) - 4;
                else
                    Speed.Y = MyWorld.alea.Next(0, 9) - 4;
        }
    }
}
