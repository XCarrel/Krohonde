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
    }
}
