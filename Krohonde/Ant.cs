﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Krohonde
{
    public abstract class Ant
    {
        static int lastid = 0;
        private readonly int id;

        private readonly Point origin;
        private Point Location;
        protected Point Speed;

        protected Colony MyColony;

        public Ant(Point location, Point speed, Colony colony)
        {
            Location = location;
            Speed = speed;
            MyColony = colony;
            origin = new Point(0, 0);
            id = ++lastid;
        }

        protected void Move()
        {
            Location.X += Speed.X;
            Location.Y += Speed.Y;
        }

        public abstract void Live();

        public int Heading
        {
            get => (int)(Math.Atan2(Speed.Y , Speed.X -1)*180/Math.PI);
        }

        public double X { get => Location.X; }
        public double Y { get => Location.Y; }

        public Colony Colony { get => MyColony; }
    }
}
