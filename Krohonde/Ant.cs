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
    /// <summary>
    /// The Ant class defines all ants with
    /// 
    /// - A full (and unique) name
    /// - A location (X,Y)
    /// - A speed (can be higher for Scouts)
    /// - The Colony to which it belongs
    /// - Energy, Strength and Toughness
    /// - A foodbag to hold food (much bigger for Farmers)
    /// - A brickbag to hold bricks (much bigger for Workers)
    /// 
    /// The class also defines things all ants can do:
    /// 
    /// - Move
    /// - Eat
    /// - Fight
    /// - Build (Workers)
    /// - Pick a resource from the ground (Workers and Farmers)
    /// - Drop a pheromon
    /// - Look for Food, Buildin material ('bricks') or enemies around them
    /// - Detect smells (pheromons)
    /// 
    /// </summary>
    public abstract class Ant
    {
        public const int HIT_REACH = 30; // How far ants can hit another ant
        public const int SIGHT_RANGE = 100; // How far ants can see
        public const int SMELL_RANGE = 1000; // How far ants can smell a full-intensity pheromon
        public const int FOOD_BAG_SIZE = 5; // The basic size of the food bag of all ants
        public const int BRICK_BAG_SIZE = 5; // The basic size of the brick bag of all ants
        public const int PICKUP_REACH = 10; // From how far can an ant pickup a resource

        private static int lastid = 0; // id if the last Ant that was instanciated
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

        /// <summary>
        /// Moves the ant according to its speed and the time elapsed since last update
        /// If the speed of the ant is too big, only the max speed of its kind is applied
        /// If the move would put the ant into a rock, the ant stays there and its 'BlockedBy' attribute identifies the rock that prevented the move
        /// If the move would put the ant outside the garden, the ant stays there and its 'BlockedBy' attribute is MotherNature
        /// </summary>
        /// <param name="deltatime"></param>
        protected void Move(double deltatime)
        {
            // Temporary
            roam();

            if (!ActionAllowed()) return; // ignore multiple actions by same ant
            double maxSpeed = this.getMaxSpeed();
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

        /// <summary>
        /// Build an extension to the colony's hill
        /// The ant must be a worker, with enough bricks in its brickbag.
        /// It must be outside the anthill, but not too far (see Colony.CONSTRUCTION_ZONE)
        /// </summary>
        /// <returns>True if the action succeeded</returns>
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

        /// <summary>
        /// Hit an enemy ant
        /// The enemy must be close enough (see HIT_REACH)
        /// The more energy the ant has, the more damage it makes to the enemy
        /// The more strength the ant has, the more damage too
        /// </summary>
        /// <param name="ant"></param>
        /// <returns></returns>
        protected bool Hit(Ant ant)
        {
            if (!ActionAllowed()) return false; // ignore multiple actions by same ant
            if (Helpers.Distance(SDLocation, ant.SDLocation) > HIT_REACH) return false;
            if (ant.Colony == MyColony) return false; // don't hit a friend !!!!

            // Ok, we have a fight...
            int damage = energy / 10;
            ant.energy -= damage;
            // TODO damage take strength into account
            ant.lastHitBy = this;

            return true;
        }

        /// <summary>
        /// Updates the ant's state according to the time it has lived since last update
        /// </summary>
        /// <param name="deltatime"></param>
        public abstract void Live(double deltatime);

        /// <summary>
        /// The angle (in degrees) the speed makes with x-axis
        /// </summary>
        [Browsable(false)]
        public int Heading
        {
            get => (int)(Math.Atan2(Speed.Y , Speed.X -1)*180/Math.PI);
        }

        /// <summary>
        /// The position of the ant's head (which is not the picture's location
        /// </summary>
        [Browsable(false)]
        public Point HeadPosition
        {
            get
            {
                double angle = Math.Atan2(Speed.Y, Speed.X - 1);
                return new Point(Location.X + 12 + 12*Math.Cos(angle), Location.Y + 12 + 12 * Math.Sin(angle));
            }
        }

        /// <summary>
        /// Picks up a resource (food or brick) from the ground and places it in the right bag.
        /// The ant must be clode enough to the resource (see PICKUP_REACH)
        /// </summary>
        /// <param name="resource"></param>
        /// <returns>True if we have been able to collect (some of) the resource</returns>
        protected bool Pickup(Resource resource)
        {
            if (!ActionAllowed()) return false; // ignore multiple actions by same ant

            int max = MyColony.World().BagSize(this,resource); // How much can I carry of that resource ?
            int val = MyColony.World().Collect(this,resource); // pick it up. Can be 0 if we're too far for instance
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

        /// <summary>
        /// Dump the foodbag into the colony's foodstore
        /// </summary>
        public void EmptyFoodBag()
        {
            foodbag = 0;
        }

        /// <summary>
        /// Drop a pheromon at the ant's current location. The type of the pheromon depends on the type of ant:
        /// Food for Farmers and scouts
        /// Build for Workers
        /// Danger for soldiers 
        /// </summary>
        protected void DropPheromon()
        {
            if (!ActionAllowed()) return; // ignore multiple actions by same ant
            MyColony.World().DropPheromon(this);
            energy -= MotherNature.COST_OF_DROPPING_PHEROMON;
        }

        /// <summary>
        /// Drop a specific pheromontype
        /// At this point, it is only useful for scouts to drop other types of pheromons than Food
        /// </summary>
        /// <param name="pherotype"></param>
        protected void DropPheromon(MotherNature.PheromonTypes pherotype)
        {
            if (!ActionAllowed()) return; // ignore multiple actions by same ant
            MyColony.World().DropPheromon(this, pherotype);
            energy -= MotherNature.COST_OF_DROPPING_PHEROMON;
        }

        /// <summary>
        /// Get the list of food chunks in sight (
        /// </summary>
        /// <returns></returns>
        protected List<Food> FoodAroundMe()
        {
            energy -= MotherNature.COST_OF_LOOKING_AROUND;
            return Colony.World().LookForFoodAround(this);
        }

        /// <summary>
        /// Gets the list of bricks in sighr
        /// </summary>
        /// <returns></returns>
        protected List<Brick> BricksAroundMe()
        {
            energy -= MotherNature.COST_OF_LOOKING_AROUND;
            return Colony.World().LookForBricksAround(this);
        }

        /// <summary>
        ///  Gets the list of enemies in sight
        /// </summary>
        /// <returns></returns>
        protected List<Ant> EnemiesAroundMe()
        {
            energy -= MotherNature.COST_OF_LOOKING_AROUND;
            return Colony.World().LookForEnemiesAround(this);
        }

        /// <summary>
        /// Gets the list of pheromons within smelling distance (SMELL_RANGE)
        /// </summary>
        /// <returns></returns>
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

        //For scoreboard
        public String LastHitByName
        {
            get
            {
                if (lastHitBy != null)
                    return lastHitBy.Fullname;
                else
                    return null;
            }
        }
        
        [Browsable(false)]
        public Ant LastHitBy { get => lastHitBy; }

        private double getMaxSpeed()
        {
            switch (this.GetType().Name)
            {
                case "FarmerAnt": return 10;
                case "WorkerAnt": return 10;
                case "ScoutAnt": return 20;
                case "SoldierAnt": return 15;
                default: return 0;
            }
        }
        public double SightRange()
        {
            switch (this.GetType().Name)
            {
                case "FarmerAnt": return SIGHT_RANGE;
                case "WorkerAnt": return SIGHT_RANGE;
                case "ScoutAnt": return SIGHT_RANGE * 2;
                case "SoldierAnt": return SIGHT_RANGE * 1.5;
                default: return 0;
            }
        }


    }
}
