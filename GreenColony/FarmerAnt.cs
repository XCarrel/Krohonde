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
        public Boolean Protected { get; set; }

        public FarmerAnt(Point location, Point speed, GreenColony colony) : base(location, speed, colony)
        {
        }

        public override void Live()
        {
            //default speed direction
            //Todo:change default
            Speed.X = -1000;

            //strength increase for speed
            if (FoodBag > 1 && Strength < 10)
            {
                EatFromBag(2, MotherNature.DigestionFor.Strength);
            }

            if (FoodBag >= FOOD_BAG_SIZE * 20)
            {
                System.Drawing.Point closestHill = MyColony.Hill[0];
                //gets closest point to the base
                foreach (var h in MyColony.Hill)
                {
                    if (Helpers.Distance(SDLocation, new System.Drawing.Point(h.X, h.Y)) < Helpers.Distance(SDLocation,
                        new System.Drawing.Point(closestHill.X, closestHill.Y)))
                    {
                        closestHill = h;
                    }
                }

                Logger.WriteLogFile(
                    $"GreenFarmer distance to store{Helpers.Distance(SDLocation, new System.Drawing.Point(closestHill.X, closestHill.Y))}");
                if (Helpers.Distance(SDLocation, new System.Drawing.Point(closestHill.X, closestHill.Y)) < 2)
                {
                    Logger.WriteLogFile("GreenWorker dumped food");
                    Colony.DumpFood(this);
                }

                //returns to the colony when full
                MoveToward(new System.Drawing.Point(closestHill.X, closestHill.Y));
            }
            else
            {
                //Updates food list
                var greenFoods = FoodAroundMe();

                //when food is spotted
                if (greenFoods.Count > 0)
                {
                    Food closestFood = greenFoods[0];
                    foreach (Food f in greenFoods)
                    {
                        //Check which on is closer
                        if (Helpers.Distance(f.Location, SDLocation) <
                            Helpers.Distance(closestFood.Location, SDLocation))
                        {
                            closestFood = f;
                        }
                    }

                    //go toward the closest food
                    MoveToward(closestFood.Location);

                    if (Helpers.Distance(closestFood.Location, SDLocation) < PICKUP_REACH)
                    {
                        Pickup(closestFood);
                    }
                }
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