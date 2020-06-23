using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krohonde
{
    public interface IMotherNature
    {
        #region Build the world
        void Initialize();
        void AddColony(Colony colo);
        void Live();
        string GetBirthCertificate(string antname);
        double getMaxSpeed(string anttype);
        int width { get; }
        int height { get; }
        Stopwatch universaltime { get; }
        List<Colony> Colonies();
        List<FoodCluster> FoodStock();
        List<BrickCluster> BrickStock();
        List<Pheromon> Pheromons();
        List<Rock> Rocks();
        #endregion
        #region Probing the world
        List<Food> LookForFoodAround(Ant ant);
        List<Brick> LookForBricksAround(Ant ant);
        void LookAroundForEnemies(Ant ant);
        void SmellAround(Ant ant);
        #endregion
        #region General actions
        void Eat(Ant ant);
        void DropPheromon(Ant ant, MotherNature.PheromonTypes pherotype);
        void DropPheromon(Ant ant);

        #endregion
        #region Worker ants
        void Build(Ant ant);
        #endregion
    }
}
