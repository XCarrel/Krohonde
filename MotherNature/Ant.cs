using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Krohonde.World;
using Point = System.Drawing.Point;

namespace Krohonde.Creatures
{
    public class BasicAnt
    {
        private Point Location;
        protected Vector Speed;

        protected MotherNature MyWorld;

        public BasicAnt(Point location, Vector speed, MotherNature world)
        {
            Location = location;
            Speed = speed;
            MyWorld = world;
        }

        public virtual void Live()
        {
            Location.X += (int)Speed.X;
            Location.Y += (int)Speed.Y;
        }

        public int Heading
        {
            get => ((int)Vector.AngleBetween(new Vector(0, 1), Speed)+180)%360;
        }

        public void turn(int degrees)
        {
            double radians = degrees * Math.PI / 180;
            double ca = Math.Cos(radians);
            double sa = Math.Sin(radians);
            Speed.X = ca * Speed.X - sa * Speed.Y;
            Speed.Y = sa * Speed.X + ca * Speed.Y;
        }

        public double X { get => Location.X; }
        public double Y { get => Location.Y; }
    }
}
