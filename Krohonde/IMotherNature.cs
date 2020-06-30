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
        string GetEggCertificate(string eggname);
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
        List<Ant> LookForEnemiesAround(Ant ant);
        List<Pheromon> SmellAround(Ant ant);
        #endregion
        #region General actions
        /// <summary>
        /// Pick a resource from the world
        /// </summary>
        /// <param name="resource"></param>
        /// <returns>The value of the resource</returns>
        int Collect(Ant ant, Resource resource);


        /// <summary>
        /// Returns the size of the bag of a specific type of ant for a specific type of resource
        /// Example: How much food can a SoldierAnt carry ?
        /// </summary>
        /// <param name="ant"></param>
        /// <param name="resource"></param>
        /// <returns></returns>
        int BagSize(Ant ant, Resource resource);
        void DropPheromon(Ant ant, MotherNature.PheromonTypes pherotype);
        void DropPheromon(Ant ant);

        #endregion
    }
}
