using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Krohonde.RedColony
{
    public class FarmerAnt : Ant
    {
        private Point position;

        public FarmerAnt(Point location, Point speed, RedColony colony) : base(location, speed, colony)
        {
            
        }

        public override void Live(double deltatime)
        {
           
            double disMin = 5000;

                                                               
            List<Food> foodposition = FoodAroundMe();
            if (foodposition.Count() > 0)
            {
                Food closest = foodposition[0];
                foreach (Food foodproche in foodposition)
                {
                    if (Helpers.Distance (SDLocation, foodproche.Location)< disMin)
                    {
                        closest = foodproche;
                        disMin = Helpers.Distance(SDLocation, foodproche.Location);
                    }
                }
                Speed.X = closest.Location.X - X;
                Speed.Y = closest.Location.Y - Y;
            }
            Move(deltatime);
            


        }
    }
}
