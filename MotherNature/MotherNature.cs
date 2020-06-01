using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Krohonde.Creatures;

namespace Krohonde.World
{
    public class MotherNature : IMotherNature
    {
        private List<Ant> ants;
        private string[] KnownAntType = { "WorkerAnt", "SoldierAnt" };

        public MotherNature()
        {
            ants = new List<Ant>();
        }

        public void AddAnt(Ant ant)
        {
            ants.Add(ant);
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
