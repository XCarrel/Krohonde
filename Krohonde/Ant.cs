using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Krohonde
{
    public abstract class Ant
    {
        private static int lastid = 0;
        private readonly int id;
        private static int lastactionby; // the id of the last ant that performed an action (prevent double play)
        private readonly string fullname;

        private int energy;     // 0 energy means you're dead
        private int strength;   // With more strength, Farmer and Worker can carry more, Soldier hit harder, Scouts go faster. All get tired more slowly.
        private int toughness;  // Resistance to enemy's hits

        private readonly Point origin; // a reference used for heading calculation
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
            fullname = colony.GetType().Name+this.GetType().Name+id;
        }

        /// <summary>
        /// Verifies that an ant is not trying to make more than one action in a lifecycle
        /// </summary>
        /// <returns></returns>
        private bool ActionAllowed()
        {
            bool res = (lastactionby == id);
            lastactionby = id;
            return res;
        }

        protected void Move()
        {
            if (!ActionAllowed()) return; // ignore multiple actions by same ant
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
