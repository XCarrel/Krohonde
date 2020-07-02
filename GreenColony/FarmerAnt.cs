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
                MoveToward(closestFood.Location);

                //Todo: update function
                if (Helpers.Distance(closestFood.Location, SDLocation) < PICKUP_REACH)
                {
                    Pickup(closestFood);
                }
            }

            Move();
        }

        private void MoveToward(System.Drawing.Point targetLocation)
        {
            //gets the distance 
            //todo:add distance to line function in order to match the 0
            var distanceToX = Helpers.DistanceToLine(SDLocation,
                new System.Drawing.Point(targetLocation.X, targetLocation.Y - 10),
                new System.Drawing.Point(targetLocation.X, targetLocation.Y + 10));
            var distanceToY = Helpers.DistanceToLine(SDLocation,
                new System.Drawing.Point(targetLocation.X - 10, targetLocation.Y),
                new System.Drawing.Point(targetLocation.X + 10, targetLocation.Y));

            //adjust signs
            if (X < targetLocation.X)
            {
                Speed.X = +distanceToX;
            }
            else if (X > targetLocation.X)
            {
                Speed.X = -distanceToX;
            }
            else
            {
                Speed.X = 0;
            }

            if (Y < targetLocation.Y)
            {
                Speed.Y = +distanceToY;
            }
            else if (Y > targetLocation.Y)
            {
                Speed.Y = -distanceToY;
            }
            else
            {
                Speed.Y = 0;
            }
        }
    }
}