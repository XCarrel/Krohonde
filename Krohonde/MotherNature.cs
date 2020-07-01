﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections;
using System.Net.Configuration;

namespace Krohonde
{
    public class MotherNature : IMotherNature
    {

        private const int FOOD_CLUSTERS = 8;
        private const int FOOD_CLUSTER_SIZE = 40;
        private const int BRICK_CLUSTERS = 6;
        private const int BRICK_CLUSTER_SIZE = 40;
        private const int NB_ROCKS = 10;

        private const int MAX_ROCK_WIDTH = 100;
        private const int MIN_ROCK_WIDTH = 10;
        private const int MAX_ROCK_HEIGHT = 100;
        private const int MIN_ROCK_HEIGHT = 10;
        private const int CLEAR_ZONE_RADIUS = 200; // Empty zone around anthills

        public static Random alea;
        public const int MAX_ENERGY = 30000; // of an ant 
        public const int PHEROMON_LIFE_DURATION = 120; // seconds
        public const int COST_OF_DROPPING_PHEROMON = 30; // units of energy
        public const int COST_OF_LOOKING_AROUND = 10; // units of energy
        public const int COST_OF_SMELLING_AROUND = 20; // units of energy
        public const int COST_OF_COLLECTING_RESOURCE = 50; // units of energy
        public const int COST_OF_BUILDING = 50; // units of energy
        public const int COST_OF_LAYING_AN_EGG = 500; // units of energy
        public const int COST_OF_EGG_GROWTH = 5; // units of energy
        public const int MAX_QUEEN_BITE_SIZE = 10; // how much food a queen can eat in one action
        public const int MAX_QUEEN_SPEED = 4; // A queen will walk but never run
        public const int INITIAL_FOOD_STORE = 500; 
        public const int FOOD_TO_ENERGY = 100; // how much energy an ant get when it eats food
        public const int FOOD_TO_STRENGTH = 5; // how much strength an ant get when it eats food
        public const int FOOD_TO_TOUGHNESS = 5; // how much touhness an ant get when it eats food
        public const int BRICKS_TO_BUILD = 50; // how much bricks it takes to build an extension

        public enum PheromonTypes { Food, Danger, Build }
        public enum DigestionFor { Energy, Strength, Toughness }
        public enum AntTypes { Queen, FarmerAnt, WorkerAnt, ScoutAnt, SoldierAnt }

        private List<Colony> colonies;
        private List<FoodCluster> food;
        private List<BrickCluster> bricks;
        private List<Rock> rocks;
        private List<Pheromon> pheromons;

        private readonly int width;
        private readonly int height;
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

        public void Initialize()
        {
            AddRocks(); // Rocks
            Seed();     // Food
            Sprinkle(); // Bricks
        }

        /// <summary>
        /// Selects a point at random, free of anthills and rocks. The point is 1/20th of the screen clear of the sides
        /// </summary>
        /// <returns></returns>
        private System.Drawing.Point PickAPoint()
        {
            double distmin;
            System.Drawing.Point res;
            do
            {
                distmin = width;
                res = new System.Drawing.Point(alea.Next(width / 20, 19 * width / 20), alea.Next(height / 20, 19 * height / 20)); // keep 1/20th padding
                foreach (Colony colo in colonies)
                {
                    double dist = Helpers.Distance(res, new System.Drawing.Point((int)colo.Location.X, (int)colo.Location.Y));
                    if (dist < distmin) distmin = dist;
                }
                if (inARock(res)) distmin = 0;
            } while (distmin < CLEAR_ZONE_RADIUS);
            return res;
        }

        /// <summary>
        /// Check if a point is inside a rock
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private bool inARock(System.Drawing.Point p)
        {
            foreach (Rock rock in rocks)
            {
                if (Helpers.IsInPolygon(rock.Shape, p)) return true;
            }
            return false;
        }

        /// <summary>
        /// Place food at random in the world
        /// </summary>
        private void Seed()
        {
            for (int c = 0; c < FOOD_CLUSTERS; c++)
            {
                Food seed = new Food(PickAPoint(), 10);
                FoodCluster fc = new FoodCluster();
                for (int i = 0; i < FOOD_CLUSTER_SIZE; i++)
                {
                    fc.Add(seed);
                    System.Drawing.Point delta = Helpers.Rotate(new System.Drawing.Point(i + 5, i + 5), i * 130);
                    seed = new Food(new System.Drawing.Point(seed.Location.X + delta.X, seed.Location.Y + delta.Y), seed.Value); // re-instantiate to have a new object
                    if (inARock(seed.Location)) break; // don't put food under a rock: it won't be reachable
                }
                food.Add(fc);
            }
        }

        /// <summary>
        /// Place construction material at random in the world
        /// </summary>
        private void Sprinkle()
        {
            for (int c = 0; c < BRICK_CLUSTERS; c++)
            {
                Brick seed = new Brick(PickAPoint(), 10);
                BrickCluster bc = new BrickCluster();
                for (int i = 0; i < BRICK_CLUSTER_SIZE; i++)
                {
                    bc.Add(seed);
                    System.Drawing.Point delta = Helpers.Rotate(new System.Drawing.Point(i + 5, i + 5), i * 150);
                    seed = new Brick(new System.Drawing.Point(seed.Location.X + delta.X, seed.Location.Y + delta.Y), seed.Value); // re-instantiate to have a new object
                    if (inARock(seed.Location)) break; // don't put bricks under a rock: it won't be reachable
                }
                bricks.Add(bc);
            }
        }

        /// <summary>
        /// Place rocks in the landscape
        /// </summary>
        private void AddRocks()
        {
            for (int r = 0; r < NB_ROCKS; r++)
            {
                System.Drawing.Point loc = PickAPoint();
                int w = MotherNature.alea.Next(MIN_ROCK_WIDTH, MAX_ROCK_WIDTH);
                int h = MotherNature.alea.Next(MIN_ROCK_HEIGHT, MAX_ROCK_HEIGHT);
                rocks.Add(new Rock(loc, w, h));
            }
        }

        /// <summary>
        /// The pump of it all
        /// </summary>
        public void Live()
        {
            double deltatime = (universaltime.Elapsed - lastupdate).TotalSeconds;
            foreach (Colony colony in colonies)
            {
                if (colony.IsAlive)
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

                    List<Egg> ripeones = new List<Egg>(); // those that are ready for birth
                    foreach (Egg egg in colony.Nursery)
                    {
                        if (eggCertificates[egg.Name].Equals(egg.Certificate))
                        {
                            egg.Grow(deltatime);
                            if (egg.Maturity >= 100)
                            {
                                eggCertificates.Remove(egg.Name);
                                ripeones.Add(egg);
                            }
                        }
                    }
                    // Remove the ripe ones
                    foreach (Egg egg in ripeones)
                    {
                        colony.Hatch(egg); // welcome a new baby
                    }

                    // Long live the Queen !!!!
                    colony.Queen.Live(deltatime);
                    colony.Queen.Sleep(); // get some rest
                } else
                {
                    colony.DoomsDay();
                }
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

        /// <summary>
        /// Get a birth certificate for a specific egg name
        /// </summary>
        /// <param name="eggname"></param>
        /// <returns></returns>
        public string GetEggCertificate(string eggname)
        {
            // TODO Give the certificate only to a certified queen
            try
            {
                string GuidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                eggCertificates.Add(eggname, GuidString);
                return GuidString;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "";
            }
        }
        public void AddColony(Colony colo)
        {
            colonies.Add(colo);
        }

        public List<Colony> Colonies()
        {
            return colonies;
        }

        public List<FoodCluster> FoodStock()
        {
            return food;
        }
        public List<BrickCluster> BrickStock()
        {
            return bricks;
        }

        public List<Rock> Rocks()
        {
            return rocks;
        }

        public List<Pheromon> Pheromons()
        {
            return pheromons;
        }

        public Stopwatch universaltime { get; }

        int IMotherNature.width => width;

        int IMotherNature.height => height;

        List<Food> IMotherNature.LookForFoodAround(Ant ant)
        {
            List<Food> res = new List<Food>();
            foreach (FoodCluster cluster in food)
                res.AddRange(cluster.Content.Where(f => new Vector(f.Location.X - ant.X, f.Location.Y - ant.Y).Length < ant.SightRange()));
            return res;
        }

        List<Brick> IMotherNature.LookForBricksAround(Ant ant)
        {
            List<Brick> res = new List<Brick>();
            foreach (BrickCluster cluster in bricks)
                res.AddRange(cluster.Content.Where(b => new Vector(b.Location.X - ant.X, b.Location.Y - ant.Y).Length < ant.SightRange()));
            return res;
        }

        List<Ant> IMotherNature.LookForEnemiesAround(Ant ant)
        {
            List<Ant> res = new List<Ant>();
            foreach (Colony colo in colonies)
                if (colo != ant.Colony)
                    res.AddRange(colo.Population.Where(b => new Vector(b.X - ant.X, b.Y - ant.Y).Length < ant.SightRange()));
            return res;
        }

        List<Pheromon> IMotherNature.SmellAround(Ant ant)
        {
            return pheromons.Where(phero => new Vector(phero.Location.X - ant.X, phero.Location.Y - ant.Y).Length < Ant.SMELL_RANGE * phero.Intensity && ant.Colony == phero.Colony).ToList();
        }

        int IMotherNature.Collect(Ant ant, Resource resource)
        {
            if (Helpers.Distance(ant.SDLocation, resource.Location) > Ant.PICKUP_REACH) return 0; // resource is too far

            bool gotcha = false;
            if (resource.GetType() == typeof(Food))
            {
                List<FoodCluster> emptyfood = new List<FoodCluster>();
                foreach (FoodCluster fc in food)
                    if (fc.Content.Remove((Food)resource))
                    {
                        gotcha = true;
                        if (fc.Content.Count() < 2) emptyfood.Add(fc);
                    }
                foreach (FoodCluster empty in emptyfood) food.Remove(empty); // Housekeeping: delete empty clusters
            }
            else if (resource.GetType() == typeof(Brick))
            {
                List<BrickCluster> emptybrick = new List<BrickCluster>();
                foreach (BrickCluster bc in bricks)
                    if (bc.Content.Remove((Brick)resource))
                    {
                        gotcha = true;
                        if (bc.Content.Count() < 2) emptybrick.Add(bc);
                    }
                foreach (BrickCluster empty in emptybrick) bricks.Remove(empty);
            }
            if (gotcha)
                return resource.Value;
            else
                return 0;
        }

        int IMotherNature.BagSize(Ant ant, Resource resource)
        {
            int res = 0;
            if (resource.GetType() == typeof(Food))
            {
                res = Ant.FOOD_BAG_SIZE;
                if (ant.GetType().Name == "FarmerAnt") res *= 20; // farmers can carry 20 times more food
            }
            else if (resource.GetType() == typeof(Brick))
            {
                res = Ant.BRICK_BAG_SIZE;
                if (ant.GetType().Name == "WorkerAnt") res *= 20; // workers can carry 20 times more bricks
            }
            res += (int)(ant.Strength / 10.0); // strength allow ants to carry more
            return res;
        }
        /// <summary>
        /// Explicit pheromone dropping
        /// The pheromone will NOT be dropped if it is not allowed for the ant type
        /// </summary>
        /// <param name="ant"></param>
        /// <param name="pherotype"></param>
        void IMotherNature.DropPheromon(Ant ant, MotherNature.PheromonTypes pherotype)
        {
            switch (ant.GetType().Name) // Must use name and not full type, because Ants have the same class names in different colonies
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
            pheromons.Add(new Pheromon(ant.SDLocation, pherotype, ant.Colony));
        }

    }
}
