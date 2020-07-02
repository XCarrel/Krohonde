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
        private Point goToPosition;
        double startSpawnX = 0;
        double startSpawnY = 0;
        bool canDumpFood = false;

        public FarmerAnt(Point location, Point speed, RedColony colony) : base(location, speed, colony)
        {
            startSpawnX = X;
            startSpawnY = Y;
        }

        public override void Live()
        {

            if (FoodBag >48) {
                if (X != startSpawnX && Y != startSpawnY)
                {
                    Speed.X = startSpawnX - X;
                    Speed.Y = startSpawnY - Y;
                    Move();

                    
                }
                else {
                    canDumpFood = true;
                }

            }
            if (canDumpFood)
            {
                MyColony.DumpFood(this);
                if (FoodBag == 0)
                {
                    canDumpFood = false;
                }
            }
            else
            {




                double disMin = 5000;



                List<Food> foodposition = FoodAroundMe();
                if (foodposition.Count() > 0)
                {
                    Food closest = foodposition[0];
                    foreach (Food foodproche in foodposition)
                    {
                        if (Helpers.Distance(SDLocation, foodproche.Location) < disMin)
                        {
                            closest = foodproche;
                            disMin = Helpers.Distance(SDLocation, foodproche.Location);
                        }
                    }
                    int xPos = closest.Location.X;
                    int yPos = closest.Location.Y;
                    int xMyPos = Convert.ToInt32(X);
                    int yMyPos = Convert.ToInt32(Y);
                    float distance = Math.Abs(xPos - xMyPos) + Math.Abs(yPos - yMyPos);

                    if (distance < 2.0f)
                    {
                        ScoutAnt.DesactivateRessource(closest);
                        Pickup(closest);
                    }
                    else
                    {
                        Speed.X = closest.Location.X - X;
                        Speed.Y = closest.Location.Y - Y;
                        Move();
                    }
                }
                else
                {
                    Resource procheSelonScouts = ScoutAnt.GoToResource(new Point(X, Y), true, true);

                    if (procheSelonScouts != null)
                    {
                        goToPosition = new Point(procheSelonScouts.Location.X, procheSelonScouts.Location.Y);
                    }


                    Speed.X = goToPosition.X - X;
                    Speed.Y = goToPosition.Y - Y;
                    Move();
                }

            }
            
            


        }
    }
}
