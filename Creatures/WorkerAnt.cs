using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Krohonde.World;
using Krohonde.Creatures;
using System.Windows;

namespace Creatures
{
    public class WorkerAnt : Ant
    {
        public WorkerAnt(Point location, Vector speed, MotherNature world) : base (location,speed,world)
        { }

        public override void Live()
        {
            base.Live();
            int rangeStartX = (int)(X / MyWorld.Width * 10);
            int rangeStartY = (int)(Y / MyWorld.Height * 10);
            Speed.X += MyWorld.alea.Next(-rangeStartX, 10 - rangeStartX);
            Speed.Y += MyWorld.alea.Next(-rangeStartY, 10 - rangeStartY);
        }
    }
}
