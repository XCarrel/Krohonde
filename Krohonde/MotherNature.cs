using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Point = System.Drawing.Point;

namespace Krohonde
{
    public class MotherNature : IMotherNature
    {
        public Random alea;

        private List<Colony> colonies;
        private List<FoodCluster> food;
        private string[] KnownAntType = { "WorkerAnt", "SoldierAnt" };
        private readonly int width;
        private readonly int height;

        public MotherNature(int width, int height)
        {
            colonies = new List<Colony>();
            food = new List<FoodCluster>();
            alea = new Random();
            this.width = width;
            this.height = height;
        }

        public int Width { get => width; }
        public int Height { get => height; }

        public void Seed()
        {
            for (int c=0; c<3; c++)
            {
                Food seed = new Food(new Point(500, 100 + 150 * c), 10);
                FoodCluster fc = new FoodCluster();
                for (int i = 0; i < 180; i++)
                {
                    fc.Add(seed);
                    seed = new Food(new Point(seed.Location.X + alea.Next(0, 5) - 2, seed.Location.Y + alea.Next(0,5)-2), seed.Value); // clone to have a new object
                }
                food.Add(fc);
            }
        }

        public void Live()
        {
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
