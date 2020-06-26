using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Configuration;
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

        private int foodbag;
        private int brickbag;

        private Point Location;
        protected Point Speed;
        protected Object BlockedBy; // if defined: the object that prevented the ant from moving
        protected Ant lastHitBy; // if defined: the ant that just hit me

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
            brickbag = 500;
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

        /// <summary>
        /// Just for testing
        /// </summary>
        private void roam()
        {
            if (MotherNature.alea.Next(0, 5) == 0)
                if (MotherNature.alea.Next(0, 2) == 0)
                    Speed.X = MotherNature.alea.Next(0, 30) - 14;
                else
                    Speed.Y = MotherNature.alea.Next(0, 30) - 14;
        }

        protected void Move(double deltatime)
        {
            // Temporary
            roam();

            if (!ActionAllowed()) return; // ignore multiple actions by same ant
            double maxSpeed = MyColony.World().getMaxSpeed(this);
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
            Logger.WriteLogFile($"{fullname} moves to {Location}");
        }

        /// <summary>
        /// Eat some food from my own bag, to build up energy, strength or toughness
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        protected bool EatFromBag(int amount, MotherNature.DigestionFor purpose)
        {
            if (!ActionAllowed()) return false; // ignore multiple actions by same ant
            int val = Math.Min(Math.Min(amount,foodbag), MotherNature.MAX_BITE_SIZE);
            switch (purpose)
            {
                case MotherNature.DigestionFor.Energy:
                    energy += val * MotherNature.FOOD_TO_ENERGY;
                    break;
                case MotherNature.DigestionFor.Strength:
                    strength += val * MotherNature.FOOD_TO_STRENGTH;
                    break;
                case MotherNature.DigestionFor.Toughness:
                    toughness += val * MotherNature.FOOD_TO_TOUGHNESS;
                    break;
            }
            foodbag -= val;
            return true;
        }

        protected bool Build()
        {
            if (!ActionAllowed()) return false; // ignore multiple actions by same ant
            if (MyColony.BuildExtension(this))
            {
                energy -= MotherNature.COST_OF_BUILDING;
                brickbag -= MotherNature.BRICKS_TO_BUILD;
                return true;
            }
            return false;
        }

        protected bool Hit(Ant ant)
        {
            if (!ActionAllowed()) return false; // ignore multiple actions by same ant
            if (Helpers.Distance(SDLocation, ant.SDLocation) > MotherNature.ANT_HIT_REACH) return false;
            if (ant.Colony == MyColony) return false; // don't hit a friend !!!!

            // Ok, we have a fight...
            int damage = energy / 10;
            ant.energy -= damage;
            ant.lastHitBy = this;

            return true;
        }

        /// <summary>
        /// Updates the ant's state according to the time it has lived since last update
        /// </summary>
        /// <param name="deltatime"></param>
        public abstract void Live(double deltatime);

        [Browsable(false)]
        public int Heading
        {
            get => (int)(Math.Atan2(Speed.Y , Speed.X -1)*180/Math.PI);
        }

        [Browsable(false)]
        public Point HeadPosition
        {
            get
            {
                double angle = Math.Atan2(Speed.Y, Speed.X - 1);
                return new Point(Location.X + 12 + 12*Math.Cos(angle), Location.Y + 12 + 12 * Math.Sin(angle));
            }
        }

        protected bool Pickup(Resource resource)
        {
            if (!ActionAllowed()) return false; // ignore multiple actions by same ant

            int max = MyColony.World().BagSize(this,resource); // How much can I carry of that resource ?
            int val = MyColony.World().Collect(this,resource); // pick it up
            energy -= MotherNature.COST_OF_COLLECTING_RESOURCE;
            if (val == 0) return false; // waste of energy !!!!

            if (resource.GetType() == typeof(Food))
            {
                foodbag = Math.Min(max, foodbag + val); // store it without exceeding max
            }
            else if (resource.GetType() == typeof(Brick))
            {
                brickbag = Math.Min(max, brickbag + val); // store it without exceeding max
            }
            return true;
        }

        public void EmptyFoodBag()
        {
            foodbag = 0;
        }

        protected void DropPheromon()
        {
            if (!ActionAllowed()) return; // ignore multiple actions by same ant
            MyColony.World().DropPheromon(this);
            energy -= MotherNature.COST_OF_DROPPING_PHEROMON;
        }

        protected void DropPheromon(MotherNature.PheromonTypes pherotype)
        {
            if (!ActionAllowed()) return; // ignore multiple actions by same ant
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

        protected List<Ant> EnemiesAroundMe()
        {
            energy -= MotherNature.COST_OF_LOOKING_AROUND;
            return Colony.World().LookForEnemiesAround(this);
        }

        protected List<Pheromon> SmellsAroundMe()
        {
            energy -= MotherNature.COST_OF_SMELLING_AROUND;
            return Colony.World().SmellAround(this);
        }

        [Browsable(false)]
        public double X { get => Location.X; }

        [Browsable(false)]
        public double Y { get => Location.Y; }


        [Browsable(false)]
        public Colony Colony { get => MyColony; }
        
        public string Fullname { get => fullname; }

        [Browsable(false)]
        public string Certificate { get => certificate; }

        [Browsable(false)]
        public System.Drawing.Point SDLocation { get => new System.Drawing.Point((int)X,(int)Y); }

        public int Energy { get => energy; }
        
        public int Strength { get => strength; }

        public int Toughness { get => toughness; }

        [Browsable(false)]
        public Object BlockedByScore { get => BlockedBy; }

        public string Blocked
        {
            get
            {
                if (BlockedBy != null)
                {
                    if (BlockedByScore is Krohonde.MotherNature)
                        return "oui, par mère nature";
                    else if (BlockedByScore is Krohonde.Rock)
                        return "oui, par un rocher";
                    else
                        return BlockedByScore.GetType().Name;
                }
                else
                {
                    return "non";
                }
            }
        }

        [Browsable(false)]
        public int FoodBag { get => foodbag; }

        [Browsable(false)]
        public int BrickBag { get => brickbag; }

        [Browsable(false)]
        public bool Selected { get; set; }

        [Browsable(false)]
        public Ant LastHitBy { get => lastHitBy; }
    }
}
