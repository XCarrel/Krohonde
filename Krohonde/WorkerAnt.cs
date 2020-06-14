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
        private int podometer = 0;
        private readonly int xchange;
        private readonly int ychange;

        public WorkerAnt(Point location, Point speed, MotherNature world) : base (location,speed,world)
        {
            xchange = MyWorld.alea.Next(10, 20);
            ychange = MyWorld.alea.Next(10, 20);
        }

        public override void Live()
        {
            base.Live();
            podometer++;
            if (podometer % xchange == 0) Speed.X = MyWorld.alea.Next(0,9)-4;
            if (podometer % ychange == 0) Speed.Y = MyWorld.alea.Next(0,9)-4;
        }
    }
}
