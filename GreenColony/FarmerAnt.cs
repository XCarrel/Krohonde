using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
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

            if (FoodBag < FOOD_BAG_SIZE * 20)
            {
                //when food is spotted
                if (greenFoods.Count > 0)
                {
                    Food closestFood = greenFoods[0];
                    foreach (Food f in greenFoods)
                    {
                        Logger.WriteLogFile($"GreenFarmer found food at x:{f.Location.X} ; y:{f.Location.Y}");

                        //Check which on is closer
                        if (Helpers.Distance(f.Location, SDLocation) <
                            Helpers.Distance(closestFood.Location, SDLocation))
                        {
                            closestFood = f;
                        }
                    }

                    //go toward the closest food
                    MoveToward(closestFood.Location);

                    //Todo: update function
                    if (Helpers.Distance(closestFood.Location, SDLocation) < PICKUP_REACH)
                    {
                        Pickup(closestFood);
                    }
                }
            }
            else
            {
                MoveToward(new System.Drawing.Point((int) Colony.Location.X, (int) Colony.Location.Y));
            }

            Move();
        }

        private void MoveToward(System.Drawing.Point targetLocation)
        {
            //calculates the distance
            var distanceToX = targetLocation.X - X;
            var distanceToY = targetLocation.Y - Y;
            //makes distances positive values
            if (distanceToX < 0) distanceToX *= -1;
            if (distanceToY < 0) distanceToY *= -1;

            //adjust signs
            if (X <= targetLocation.X)
            {
                Speed.X = distanceToX;
            }
            else
            {
                Speed.X = -distanceToX;
            }

            if (Y <= targetLocation.Y)
            {
                Speed.Y = +distanceToY;
            }
            else
            {
                Speed.Y = -distanceToY;
            }
        }
    }
}