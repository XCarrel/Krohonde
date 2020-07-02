﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Krohonde.RedColony
{
    public class WorkerAnt : Ant
    {
        private Point position;
        private Point goToPosition;
        int inc = 0;
        
        

        public WorkerAnt(Point location, Point speed, RedColony colony) : base(location, speed, colony)
        {
            
        }

        public override void Live()
        {
            double distMin = 5000;

            if ( Energy < 28000)
            {

                if(FoodBag < 1)
                {
                    List<Food> foodposition = FoodAroundMe();
                    if (foodposition.Count() > 0)
                    {
                        Food closest = foodposition[0];
                        foreach (Food foodproche in foodposition)   //find zone
                        {
                            if (Helpers.Distance(SDLocation, foodproche.Location) < distMin)
                            {
                                closest = foodproche;
                                distMin = Helpers.Distance(SDLocation, foodproche.Location);
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




            inc++;
            if(inc == 3)
            {
                foreach (Ant enemy in EnemiesAroundMe())
                {
                    SoldierAnt.PointAnEnemy(enemy,3);
                }
                inc = 0;
            }

            if (BlockedBy == this)
            {
                Logger.WriteLogFile("RED Je suis bloquer");
            }

            

            if (BrickBag == 50)
            {
                Speed.X = MyColony.Location.X - X;
                Speed.Y = MyColony.Location.Y - Y;
                Move();
                MyColony.BuildExtension(this);
            }

            

            List<Brick> brickpositions = BricksAroundMe();
            if (brickpositions.Count() > 0)
            {
                Brick closest = brickpositions[0];
                foreach (Brick brickporche in brickpositions)
                {
                    if (Helpers.Distance(SDLocation, brickporche.Location) < distMin)
                    {
                        closest = brickporche;
                        distMin = Helpers.Distance(SDLocation, brickporche.Location);
                    }
                }

                int xPos = closest.Location.X;
                int yPos = closest.Location.Y;
                int xMyPos = Convert.ToInt32(X);
                int yMyPos = Convert.ToInt32(Y);
                float distance = Math.Abs(xPos - xMyPos) + Math.Abs(yPos - yMyPos);

                if(distance < 2.0f)
                {
                    ScoutAnt.DesactivateRessource(closest);
                    Logger.WriteLogFile("RED patate");
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
                Resource procheSelonScouts = ScoutAnt.GoToResource(new Point(X, Y), true, false);

                if (procheSelonScouts != null)
                {
                    goToPosition = new Point(procheSelonScouts.Location.X, procheSelonScouts.Location.Y);
                }


                Speed.X = goToPosition.X - X;
                Speed.Y = goToPosition.Y - Y;
                Move();
            }
            Move();
            
        }
    }
}