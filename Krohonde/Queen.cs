using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Drawing;

namespace Krohonde
{
    public abstract class Queen
    {
        private const int INITIAL_MATURITY = 25; // When an egg is laid, it doesn't start from 0 so that it is visible

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

        public bool LayEgg(MotherNature.AntTypes typ, System.Drawing.Point loc, int val)
        {
            if (!Helpers.IsInPolygon(MyColony.Hill, loc)) return false; // can't lay an egg outside the hill

            if (Helpers.Distance(SDLocation, loc) > Colony.CRIB_SIZE) return false; // queen cannot throw an egg !!

            MyColony.StoreEggInNursery(new Egg(typ, loc, this,val));

            energy -= MotherNature.COST_OF_LAYING_AN_EGG;

            return true;
        }
        public bool LayEgg(MotherNature.AntTypes typ, System.Drawing.Point loc, Queen queen)
        {
            return LayEgg(typ, loc, INITIAL_MATURITY);
        }

        public Colony Colony { get => MyColony; }

        [Browsable(false)]
        public double X { get => Location.X; }

        [Browsable(false)]
        public double Y { get => Location.Y; }

        [Browsable(false)]
        public System.Drawing.Point SDLocation { get => new System.Drawing.Point((int)X, (int)Y); }

    }
}
