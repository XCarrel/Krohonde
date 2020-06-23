using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections;

namespace Krohonde
{
    public class MotherNature : IMotherNature
    {

        private const int FOOD_CLUSTERS = 5;
        private const int FOOD_CLUSTER_SIZE = 20;
        private const int BRICK_CLUSTERS = 5;
        private const int BRICK_CLUSTER_SIZE = 20;
        private const int NB_ROCKS = 10;

        private const int MAX_ROCK_WIDTH = 100;
        private const int MIN_ROCK_WIDTH = 10;
        private const int MAX_ROCK_HEIGHT = 100;
        private const int MIN_ROCK_HEIGHT = 10;

        public static Random alea;
        public const int MAX_ENERGY = 1800; // of an ant 
        public const int PHEROMON_LIFE_DURATION = 30; // seconds
        public const int PHEROMON_DROPPING_COST = 30; // units of energy

        public enum PheromonTypes { Food, Danger, Build }

        private List<Colony> colonies;
        private List<FoodCluster> food;
        private List<BrickCluster> bricks;
        private List<Rock> rocks;
        private List<Pheromon> pheromons;

        private readonly int width;
        private readonly int height;
        private Stopwatch universaltime;
        private TimeSpan lastupdate;
        Hashtable birthCertificates;
        Hashtable eggCertificates;


        public MotherNature(int width, int height)
        {
            colonies = new List<Colony>();
            food = new List<FoodCluster>();
            bricks = new List<BrickCluster>();
            rocks = new List<Rock>();
            pheromons = new List<Pheromon>();
            alea = new Random();
            this.width = width;
            this.height = height;
            universaltime = new Stopwatch();
            universaltime.Start();
            birthCertificates = new Hashtable();
            eggCertificates = new Hashtable();
        }

        public int Width { get => width; }
        public int Height { get => height; }

        /// <summary>
        /// Place food at random in the world
        /// </summary>
        public void Seed()
        {
            for (int c = 0; c < FOOD_CLUSTERS; c++)
            {
                Food seed = new Food(new System.Drawing.Point(alea.Next(width / 20, 19 * width / 20), alea.Next(height / 20, 19 * height / 20)), 10);
                FoodCluster fc = new FoodCluster();
                for (int i = 0; i < FOOD_CLUSTER_SIZE; i++)
                {
                    fc.Add(seed);
                    seed = new Food(new System.Drawing.Point(seed.Location.X + alea.Next(0, 5) - 2, seed.Location.Y + alea.Next(0, 5) - 2), seed.Value); // re-instantiate to have a new object
                }
                food.Add(fc);
            }
        }

        /// <summary>
        /// Place construction material at random in the world
        /// </summary>
        public void Sprinkle()
        {
            for (int c = 0; c < BRICK_CLUSTERS; c++)
            {
                Brick seed = new Brick(new System.Drawing.Point(alea.Next(width / 20, 19 * width / 20), alea.Next(height / 20, 19 * height / 20)), 10);
                BrickCluster bc = new BrickCluster();
                for (int i = 0; i < BRICK_CLUSTER_SIZE; i++)
                {
                    bc.Add(seed);
                    seed = new Brick(new System.Drawing.Point(seed.Location.X + alea.Next(0, 5) - 2, seed.Location.Y + alea.Next(0, 5) - 2), seed.Value); // re-instantiate to have a new object
                }
                bricks.Add(bc);
            }
        }

        /// <summary>
        /// Place rocks in the landscape
        /// </summary>
        public void AddRocks()
        {
            for (int r = 0; r < NB_ROCKS; r++)
            {
                Point loc = new Point(alea.Next(width / 20, 19 * width / 20), alea.Next(height / 20, 19 * height / 20));
                int w = MotherNature.alea.Next(MIN_ROCK_WIDTH, MAX_ROCK_WIDTH);
                int h = MotherNature.alea.Next(MIN_ROCK_HEIGHT, MAX_ROCK_HEIGHT);
                rocks.Add(new Rock(loc, w, h));
            }
        }

        public void Live()
        {
            double deltatime = (universaltime.Elapsed - lastupdate).TotalSeconds;
            foreach (Colony colony in colonies)
            {
                List<Ant> deadones = new List<Ant>();
                foreach (Ant ant in colony.Population)
                {
                    if (birthCertificates[ant.Fullname].Equals(ant.Certificate))
                    {
                        ant.Live(deltatime);
                        if (ant.Energy < 0)
                        {
                            birthCertificates.Remove(ant.Fullname);
                            deadones.Add(ant);
                        }
                    }
                }
                // Remove the dead ones
                foreach (Ant ant in deadones) colony.Dispose(ant);
            }

            // Handle stale pheromons
            List<Pheromon> staleOnes = new List<Pheromon>();
            foreach (Pheromon phero in pheromons) if (phero.Intensity <= 0) staleOnes.Add(phero);
            foreach (Pheromon stale in staleOnes) pheromons.Remove(stale);

            lastupdate = universaltime.Elapsed;
        }

        /// <summary>
        /// Get a birth certificate for a specific ant name
        /// </summary>
        /// <param name="antname"></param>
        /// <returns></returns>
        public string GetBirthCertificate(string antname)
        {
            // TODO Give the certificate only in exchange to a certified egg
            try
            {
                string GuidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                birthCertificates.Add(antname, GuidString);
                return GuidString;
            }
            catch (Exception e)
            {
                return "";
            }
        }

        public void AddColony(Colony colo)
        {
            colonies.Add(colo);
        }

        public List<Colony> Colonies
        {
            get => colonies;
        }

        public List<FoodCluster> FoodStock
        {
            get => food;
        }
        public List<BrickCluster> BrickStock
        {
            get => bricks;
        }

        public List<Rock> Rocks
        {
            get => rocks;
        }

        public List<Pheromon> Pheromons
        {
            get => pheromons;
        }

        public Stopwatch UniversalTime { get => universaltime; }

        public double getMaxSpeed(string anttype)
        {
            switch (anttype)
            {
                case "FarmerAnt": return 10;
                case "WorkerAnt": return 10;
                case "ScoutAnt": return 20;
                case "SoldierAnt": return 15;
                default: return 0;
            }
        }
        #region IMotherNature methods

        void IMotherNature.LookAroundForFood(Ant ant)
        {
            throw new NotImplementedException();
        }

        void IMotherNature.LookAroundForObstacles(Ant ant)
        {
            throw new NotImplementedException();
        }

        void IMotherNature.LookAroundForEnemies(Ant ant)
        {
            throw new NotImplementedException();
        }

        void IMotherNature.SmellAround(Ant ant)
        {
            throw new NotImplementedException();
        }

        void IMotherNature.Eat(Ant ant)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Explicit pheromone dropping
        /// The pheromone will NOT be dropped if it is not allowed for the ant type
        /// </summary>
        /// <param name="ant"></param>
        /// <param name="pherotype"></param>
        void IMotherNature.DropPheromon(Ant ant, MotherNature.PheromonTypes pherotype)
        {
            switch (ant.GetType().Name)
            {
                case "FarmerAnt":
                    if (pherotype != MotherNature.PheromonTypes.Food) return;
                    break;
                case "WorkerAnt":
                    if (pherotype != MotherNature.PheromonTypes.Build) return;
                    break;
                case "ScoutAnt":
                    if (pherotype != MotherNature.PheromonTypes.Food && pherotype != MotherNature.PheromonTypes.Danger) return;
                    break;
                case "SoldierAnt":
                    if (pherotype != MotherNature.PheromonTypes.Danger) return;
                    break;
            }
            // if we make it there: we passed the test
            pheromons.Add(new Pheromon(new System.Drawing.Point((int)ant.X, (int)ant.Y), pherotype, ant.Colony));
        }

        /// <summary>
        /// Implicit pheromone dropping
        /// The type of pheromone depends on the type of ant
        /// </summary>
        /// <param name="ant"></param>
        void IMotherNature.DropPheromon(Ant ant)
        {
            MotherNature.PheromonTypes pherotype;
            switch (ant.GetType().Name)
            {
                case "FarmerAnt":
                    pherotype = MotherNature.PheromonTypes.Food;
                    break;
                case "WorkerAnt":
                    pherotype = MotherNature.PheromonTypes.Build;
                    break;
                case "ScoutAnt":
                    pherotype = MotherNature.PheromonTypes.Food; // had to choose a default type ....
                    break;
                case "SoldierAnt":
                    pherotype = MotherNature.PheromonTypes.Danger;
                    break;
                default:
                    return;
            }
            pheromons.Add(new Pheromon(new System.Drawing.Point((int)ant.X, (int)ant.Y), pherotype, ant.Colony));
        }

        void IMotherNature.Build(Ant ant)
        {
            throw new NotImplementedException();
        }
        #endregion


    }
}
