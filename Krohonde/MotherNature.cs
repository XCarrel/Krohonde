using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Krohonde
{
    public class MotherNature : IMotherNature
    {
        private const int FOOD_CLUSTERS = 5;
        private const int FOOD_CLUSTER_SIZE = 20;
        private const int BRICK_CLUSTERS = 5;
        private const int BRICK_CLUSTER_SIZE = 20;

        public Random alea;

        private List<Colony> colonies;
        private List<FoodCluster> food;
        private List<BrickCluster> bricks;
        private string[] KnownAntType = { "WorkerAnt", "SoldierAnt" };
        private readonly int width;
        private readonly int height;
        private Stopwatch sw;
        private TimeSpan lastThump;

        public MotherNature(int width, int height)
        {
            colonies = new List<Colony>();
            food = new List<FoodCluster>();
            bricks = new List<BrickCluster>();
            alea = new Random();
            this.width = width;
            this.height = height;
            sw = new Stopwatch();
            sw.Start();
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

        public void Live()
        {
            Console.WriteLine(string.Format("Thump {0}",sw.Elapsed-lastThump));
            lastThump = sw.Elapsed;
            foreach (Colony colony in colonies)
            {
                foreach (Ant ant in colony.Population)
                {
                    if (KnownAntType.Contains(ant.GetType().Name)) ant.Live();
                }
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

        void IMotherNature.GoFaster(Ant ant)
        {
            throw new NotImplementedException();
        }

        void IMotherNature.GoSlower(Ant ant)
        {
            throw new NotImplementedException();
        }

        void IMotherNature.Stop(Ant ant)
        {
            throw new NotImplementedException();
        }

        void IMotherNature.TurnRight(Ant ant)
        {
            throw new NotImplementedException();
        }

        void IMotherNature.TurnLeft(Ant ant)
        {
            throw new NotImplementedException();
        }

        void IMotherNature.Eat(Ant ant)
        {
            throw new NotImplementedException();
        }

        void IMotherNature.Build(Ant ant)
        {
            throw new NotImplementedException();
        }
        #endregion


    }
}
