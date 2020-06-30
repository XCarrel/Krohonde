using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Krohonde
{
    public abstract class Queen
    {
        private Point Location;
        private Point Speed;
        protected Colony MyColony;
        private int energy;     // 0 energy means you're dead

        public Queen(Point location, Point speed, Colony colony)
        {
            Location = location;
            Speed = speed;
            MyColony = colony;
            energy = MotherNature.MAX_ENERGY;
        }

        public abstract void Live(double deltatime);

        public Colony Colony { get => MyColony; }

        [Browsable(false)]
        public double X { get => Location.X; }

        [Browsable(false)]
        public double Y { get => Location.Y; }
    }
}
