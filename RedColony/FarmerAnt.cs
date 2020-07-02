using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Krohonde.RedColony
    /*
     * Auteur : Méline Juillet
     * version : 3.6 
     */

{
    public class FarmerAnt : Ant
    {
        private Point position;
        private Point goToPosition;
        bool canDumpFood = false;
        int inc = 0;
        int nouriture = 0;
        int unblock = 0;
        double disMin = 5000;


        public FarmerAnt(Point location, Point speed, RedColony colony) : base(location, speed, colony)
        {
            
        }

        public override void Live()
        {
            inc++;
            if(inc == 3)
            {
                foreach (Ant enemy in EnemiesAroundMe())
                {
                    SoldierAnt.PointAnEnemy(enemy, 4);     // defense
                }
                inc = 0;
            }

            if (Energy < 20000)
            {
                nouriture = 1;
            }

            if (nouriture >0)
            {
                EatFromBag(2, MotherNature.DigestionFor.Energy);
                nouriture++;
                if(nouriture == 10)
                {
                    nouriture = 0;
                }
            }
            if (BlockedBy != null && unblock < 15)
            {
                Move();
                unblock++;
            }
            else
            {
                unblock = 0;

                if (FoodBag > 48)
                {
                    if (X != MyColony.Location.X && Y != MyColony.Location.Y)
                    {
                        Speed.X = MyColony.Location.X - X;
                        Speed.Y = MyColony.Location.Y - Y;
                        Move();


                    }
                    else
                    {
                        canDumpFood = true;
                    }

                }
                if (canDumpFood == true)
                {
                    MyColony.DumpFood(this);        //drop food
                    if (FoodBag <4)
                    {
                        canDumpFood = false;

                    }
                }
                else
                {




                    



                    List<Food> foodposition = FoodAroundMe();
                    if (foodposition.Count() > 0)
                    {
                        Food closest = foodposition[0];
                        foreach (Food foodproche in foodposition)   //find zone
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
}
