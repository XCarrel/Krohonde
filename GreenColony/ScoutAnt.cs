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
namespace Krohonde.GreenColony
{
    public class ScoutAnt : Ant
    {
        public ScoutAnt(Point location, Point speed, GreenColony colony) : base(location, speed, colony)
        {
        }

        public override void Live(double deltatime)
        {
            List<Food> FAM = FoodAroundMe();
            foreach(Food FAM2 in FAM)
            {
                Logger.WriteLogFile($"GreenScout {FAM2.Value.ToString()}");
            }
            //Updates food list
            //Todo:update interval for energy efficiency
            List<Food> greenFoods = FoodAroundMe();
            //default speed direction
            //Todo:change default
            Speed.X = -20;

            //when food is spotted
            if (greenFoods.Count > 0)
            {
                Food closestFood = greenFoods[0];
                foreach (Food f in greenFoods)
                {
                    Logger.WriteLogFile($"GreenFarmer found food at x:{f.Location.X} ; y:{f.Location.Y}");

                    //Check which on is closer
                    if (Helpers.Distance(f.Location, this.SDLocation) <
                        Helpers.Distance(closestFood.Location, this.SDLocation))
                    {
                        closestFood = f;
                    }
                }

                //go toward the closes food
                if (X < closestFood.Location.X)
                {
                    Speed.X = +20;
                }
                else if (X > closestFood.Location.X)
                {
                    Speed.X = -20;
                }
                else
                {
                    Speed.X = 0;
                }

                if (Y < closestFood.Location.Y)
                {
                    Speed.Y = +20;
                }
                else if (Y > closestFood.Location.Y)
                {
                    Speed.Y = -20;
                }
                else
                {
                    Speed.Y = 0;
                }
            }

            Move(deltatime * 3);
            Move(deltatime);
        }
    }
}
