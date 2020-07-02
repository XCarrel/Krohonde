using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Krohonde.GreenColony
{
    public class FarmerAnt : Ant
    {
        public FarmerAnt(Point location, Point speed, GreenColony colony) : base(location, speed, colony)
        {
        }

        public override void Live()
        {
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
                //todo:add distance to line function in order to match the 0
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
                
                //Todo: update function
                if (Helpers.Distance(closestFood.Location, SDLocation) < PICKUP_REACH)
                {
                    Pickup(closestFood);
                }
            }

            Move();
        }
    }
}