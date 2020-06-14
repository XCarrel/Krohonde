using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Krohonde.Creatures;
using Point = System.Drawing.Point;

namespace Krohonde.World
{
    public class MotherNature : IMotherNature
    {
        public Random alea;

        private List<Ant> ants;
        private List<Food> food;
        private string[] KnownAntType = { "WorkerAnt", "SoldierAnt" };
        private readonly int width;
        private readonly int height;

        public MotherNature(int width, int height)
        {
            ants = new List<Ant>();
            food = new List<Food>();
            alea = new Random();
            this.width = width;
            this.height = height;
        }

        public int Width { get => width; }
        public int Height { get => height; }

        public void AddAnt(Ant ant)
        {
            ants.Add(ant);
        }

        public void Seed()
        {
            Food seed = new Food(new Point(500,100), 10);
            for (int i=0; i<80; i++)
            {
                food.Add(seed);
                seed = new Food(new Point(seed.Location.X+alea.Next(-4,4),seed.Location.Y + alea.Next(-4, 4)),seed.Value); // clone to have a new object
            }
        }

        public void Live()
        {
            foreach (Ant ant in ants)
            {
                if (KnownAntType.Contains(ant.GetType().Name)) ant.Live();
            }
        }

        public List<Ant> Ants
        {
            get => ants;
        }

        public List<Food> FoodStock
        {
            get => food;
        }
        #region IMotherNature methods
        public void Build(Ant ant)
        {
            throw new NotImplementedException();
        }

        public void Eat(Ant ant)
        {
            throw new NotImplementedException();
        }

        public void GoFaster(Ant ant)
        {
            throw new NotImplementedException();
        }

        public void GoSlower(Ant ant)
        {
            throw new NotImplementedException();
        }

        public void LookAroundForEnemies(Ant ant)
        {
            throw new NotImplementedException();
        }

        public void LookAroundForFood(Ant ant)
        {
            throw new NotImplementedException();
        }

        public void LookAroundForObstacles(Ant ant)
        {
            throw new NotImplementedException();
        }

        public void SmellAround(Ant ant)
        {
            throw new NotImplementedException();
        }

        public void Stop(Ant ant)
        {
            throw new NotImplementedException();
        }

        public void TurnLeft(Ant ant)
        {
            throw new NotImplementedException();
        }

        public void TurnRight(Ant ant)
        {
            throw new NotImplementedException();
        }
        #endregion


    }
}
