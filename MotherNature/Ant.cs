using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Krohonde.World;

namespace Krohonde.Creatures
{
    public class Ant
    {
        private Point Location;
        protected Vector Speed;

        protected MotherNature MyWorld;

        public Ant(Point location, Vector speed, MotherNature world)
        {
            Location = location;
            Speed = speed;
            MyWorld = world;
        }

        public virtual void Live()
        {
            Location = Vector.Add(Speed, Location);
        }

        public int Heading
        {
            get => ((int)Vector.AngleBetween(new Vector(0, 1), Speed)+180)%360;
        }

        public double X { get => Location.X; }
        public double Y { get => Location.Y; }
    }
}
