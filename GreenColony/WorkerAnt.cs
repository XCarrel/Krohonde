using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
/// 
namespace Krohonde.GreenColony
{
    public class WorkerAnt : Ant
    {
        public WorkerAnt(Point location, Point speed, GreenColony colony) : base(location, speed, colony)
        {
        }

        public override void Live(double deltatime)
        {
            Logger.WriteLogFile($"deltatime: {deltatime}");
            List<Food> ListFood = FoodAroundMe();
            foreach (Food OneFood in ListFood)
            {
                Helpers.Distance(new System.Drawing.Point((int)X, (int) Y), OneFood.Location);
                GoFood(OneFood.Location.X, OneFood.Location.Y);
            }
            Move(deltatime);

        }

        public void SudOuest()
        {
            Speed.X = -500;
            Speed.Y = 500;
        }
        public void NordOuest()
        {
            Speed.X = -500;
            Speed.Y = -500;
        }
        public void SudEst()
        {
            Speed.X = 500;
            Speed.Y = 500;
        }
        public void NordEst()
        {
            Speed.X = 500;
            Speed.Y = -500;
        }
        public void Sud()
        {
            Speed.X = 0;
            Speed.Y = 500;
        }
        public void Nord()
        {
            Speed.X = 0;
            Speed.Y = -500;
        }
        public void Est()
        {
            Speed.X = 500;
            Speed.Y = 0;
        }
        public void Ouest()
        {
            Speed.X = -500;
            Speed.Y = 0;
        }

        public void GoFood(int X, int Y)
        {
            Speed.X = X;
            Speed.Y = Y;
        }

    }

}