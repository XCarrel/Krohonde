using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Krohonde.World;
using Krohonde.Creatures;
using System.Windows;
using Point = System.Drawing.Point;

namespace Creatures
{
    public class FarmerAnt : Ant
    {
        public FarmerAnt(Point location, Vector speed, MotherNature world) : base(location, speed, world)
        { }
    }
}
