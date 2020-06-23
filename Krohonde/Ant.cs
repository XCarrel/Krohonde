using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
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
        private readonly string certificate;

        private int energy;     // 0 energy means you're dead
        private int strength;   // With more strength, Farmer and Worker can carry more, Soldier hit harder, Scouts go faster. All get tired more slowly.
        private int toughness;  // Resistance to enemy's hits

        private Point Location;
        protected Point Speed;
        protected Object BlockedBy; // if defined: the object that prevented the ant from moving

        protected Colony MyColony;

        public Ant(Point location, Point speed, Colony colony)
        {
            Location = location;
            Speed = speed;
            MyColony = colony;
            id = ++lastid;
            fullname = colony.GetType().Name+this.GetType().Name+id;
            certificate = colony.World().GetBirthCertificate(fullname);
            energy = MotherNature.MAX_ENERGY;
            strength = 0;
            toughness = 0;
        }

        /// <summary>
        /// Verifies that an ant is not trying to make more than one action in a lifecycle
        /// </summary>
        /// <returns></returns>
        private bool ActionAllowed()
        {
            bool res = (lastactionby != id);
            lastactionby = id; // static variable that remembers the last ant who did something
            return res;
        }

        protected void Move(double deltatime)
        {
            if (!ActionAllowed()) return; // ignore multiple actions by same ant
            double maxSpeed = MyColony.World().getMaxSpeed(this.GetType().Name);
            // Linear speed
            double linspeed = (new Vector(Speed.X, Speed.Y)).Length;
            if (linspeed > maxSpeed) // Too big, let's adjust to max 
            {
                Speed.X /= (linspeed / maxSpeed);
                Speed.Y /= (linspeed / maxSpeed);
                linspeed = maxSpeed;
            }

            // Check if move is OK
            double nextX = HeadPosition.X + Speed.X * deltatime;
            double nextY = HeadPosition.Y + Speed.Y * deltatime;
            if (nextX < 0 || nextY < 0 || nextX > this.Colony.World().width || nextY > this.Colony.World().height)
            {
                BlockedBy = this.Colony.World();
                return; // no escape from the world !!!
            }
            foreach (Rock rock in this.Colony.World().Rocks())
                if (Helpers.IsInPolygon(rock.Shape, new System.Drawing.Point((int)nextX, (int)nextY)))
                {
                    BlockedBy = rock;
                    return; // can't go through a rock
                }

            Location.X += Speed.X * deltatime;
            Location.Y += Speed.Y * deltatime;

            // Energy consumption
            energy -= (int)(linspeed * deltatime);
        }

        /// <summary>
        /// Updates the ant's state according to the time it has lived since last update
        /// </summary>
        /// <param name="deltatime"></param>
        public abstract void Live(double deltatime);

        public int Heading
        {
            get => (int)(Math.Atan2(Speed.Y , Speed.X -1)*180/Math.PI);
        }

        public Point HeadPosition
        {
            get
            {
                double angle = Math.Atan2(Speed.Y, Speed.X - 1);
                return new Point(Location.X + 12 + 12*Math.Cos(angle), Location.Y + 12 + 12 * Math.Sin(angle));
            }
        }

        protected void DropPheromon()
        {
            MyColony.World().DropPheromon(this);
            energy -= MotherNature.COST_OF_DROPPING_PHEROMON;
        }

        protected void DropPheromon(MotherNature.PheromonTypes pherotype)
        {
            MyColony.World().DropPheromon(this, pherotype);
            energy -= MotherNature.COST_OF_DROPPING_PHEROMON;
        }

        protected List<Food> FoodAroundMe()
        {
            energy -= MotherNature.COST_OF_LOOKING_AROUND;
            return Colony.World().LookForFoodAround(this);
        }

        protected List<Brick> BricksAroundMe()
        {
            energy -= MotherNature.COST_OF_LOOKING_AROUND;
            return Colony.World().LookForBricksAround(this);
        }

        public double X { get => Location.X; }
        public double Y { get => Location.Y; }

        public Colony Colony { get => MyColony; }
        
        public string Fullname { get => fullname; }

        public string Certificate { get => certificate; }

        public int Energy { get => energy; }
    }
}
