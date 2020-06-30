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
        private static Queen lastactionby; // the queen that performed an action (prevent double play)

        private const int INITIAL_MATURITY = 25; // When an egg is laid, it doesn't start from 0 so that it is visible

        private Point Location;
        protected Point Speed;
        protected Colony MyColony;
        private int energy;     // 0 energy means you're dead

        public Queen(Point location, Point speed, Colony colony)
        {
            Location = location;
            Speed = speed;
            MyColony = colony;
            energy = MotherNature.MAX_ENERGY;
            lastactionby = this;
        }

        /// <summary>
        /// Verifies that a queen is not trying to make more than one action in a lifecycle
        /// </summary>
        /// <returns></returns>
        private bool ActionAllowed()
        {
            bool res = (lastactionby != this);
            lastactionby = this; 
            return res;
        }

        protected void DoNothing()
        {
            lastactionby = this; 
        }

        public abstract void Live(double deltatime);

        protected bool Eat(int amount)
        {
            if (!ActionAllowed()) return false; // ignore multiple actions by same ant
            int val = MyColony.GetFoodFromStore(MotherNature.MAX_QUEEN_BITE_SIZE);
            energy += val * MotherNature.FOOD_TO_ENERGY;
            return true;
        }

        protected bool LayEgg(MotherNature.AntTypes typ, System.Drawing.Point loc, int val)
        {
            if (!ActionAllowed()) return false; // already did something 

            if (!Helpers.IsInPolygon(MyColony.Hill, loc)) return false; // can't lay an egg outside the hill

            if (Helpers.Distance(SDLocation, loc) > Colony.CRIB_SIZE) return false; // queen cannot throw an egg !!

            MyColony.StoreEggInNursery(new Egg(typ, loc, this,val));

            energy -= MotherNature.COST_OF_LAYING_AN_EGG;

            return true;
        }

        protected void Move(double deltatime)
        {
            if (!ActionAllowed()) return; // ignore multiple actions by same queen
            // Linear speed
            double linspeed = (new System.Windows.Vector(Speed.X, Speed.Y)).Length;
            if (linspeed > MotherNature.MAX_QUEEN_SPEED) // Too big, let's adjust to max 
            {
                Speed.X /= (int)(linspeed / MotherNature.MAX_QUEEN_SPEED);
                Speed.Y /= (int)(linspeed / MotherNature.MAX_QUEEN_SPEED);
                linspeed = MotherNature.MAX_QUEEN_SPEED;
            }

            Location.X += (int)(Speed.X * deltatime);
            Location.Y += (int)(Speed.Y * deltatime);

            // Energy consumption
            energy -= (int)(linspeed * deltatime);
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
