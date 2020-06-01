using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Krohonde.World;

namespace Krohonde.Creatures
{
    public class Ant
    {
        private Point Location;
        private Point Speed;

        private MotherNature MyWorld;

        public Ant(Point location, Point speed, MotherNature world)
        {
            Location = location;
            Speed = speed;
            MyWorld = world;
        }

        public void Live()
        {
            Location.Offset(Speed);
        }

        public int X { get => Location.X; }
        public int Y { get => Location.Y; }
    }
}
