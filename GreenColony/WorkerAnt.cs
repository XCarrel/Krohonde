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

        private int phase = 1;
        Brick closestBrick;  //the closest brick object
        bool brickPeriod = true;
        int numLive = 0;
        private Brick brickFound;

        public WorkerAnt(Point location, Point speed, GreenColony colony) : base(location, speed, colony)
        {
        }

        public override void Live()
        {
            //Logger.WriteLogFile($"deltatime: {MotherNature.LastFrameDuration}");
            List<Brick> ListBricks;
            LogEnergy();
            numLive++;
            if (numLive == 1)
            {
                SudOuest(); //default start direction.
            }

            //Default behavior of movement:
            if (Blocked.Contains("oui") == true)
            {
                RandomDirection();
            }

            switch (phase)
            {
                case 1: //Go to first brick that you find
                    closestBrick = getClosestBrick();
                    if (closestBrick != null)
                    {
                        MoveToward(new System.Drawing.Point((int)closestBrick.Location.X, (int)closestBrick.Location.Y));
                    }

                    NextMove(); //Move to the next position
                    if (closestBrick != null)
                    {
                        if (Helpers.Distance(closestBrick.Location, SDLocation) < PICKUP_REACH)
                        {
                            phase = 2;
                        }
                    }
                    break;
                case 2: //Found a brick, just pick up the brick several times
                    closestBrick = getClosestBrick();

                    if (Helpers.Distance(closestBrick.Location, SDLocation) < PICKUP_REACH)
                    {
                        Pickup(closestBrick);
                    }
                    else
                    {
                        MoveToward(new System.Drawing.Point((int)closestBrick.Location.X, (int)closestBrick.Location.Y));
                        NextMove();
                    }

                    Logger.WriteLogFile($"GreenWorker {Fullname} found brick and picked up in BrickBag: {BrickBag}");
                    if (BrickBag == BRICK_BAG_SIZE * 20)
                    {
                        phase = 3;
                    }
                    break;
                case 3: //BrickBag is full, return back to the base
                    Logger.WriteLogFile($"GreenWorker {Fullname}: Distance à la colonie{Helpers.Distance(new System.Drawing.Point((int)Colony.Location.X, (int)Colony.Location.Y), new System.Drawing.Point((int)this.SDLocation.X, (int)this.SDLocation.Y))}");
                    MoveToward(new System.Drawing.Point((int)Colony.Location.X, (int)Colony.Location.Y));

                    NextMove();

                    if (Helpers.Distance(new System.Drawing.Point((int)Colony.Location.X, (int)Colony.Location.Y), new System.Drawing.Point((int)this.SDLocation.X, (int)this.SDLocation.Y)) < 5)
                    {
                        phase = 4;
                    }
                    break;
                case 4: //Build the base with bricks
                    if (brickPeriod == true)
                    {
                        Build();
                        Logger.WriteLogFile($"GreenWorker {Fullname} pick down a brick in the colony. BrickBag: {BrickBag}");
                        brickPeriod = false;
                    }
                    else
                    {
                        //move a bit to exit the base
                        RandomDirection();
                        NextMove();
                        brickPeriod = true;
                    }
                    if (BrickBag == 0)
                    {
                        phase = 1;
                    }
                    break;
                default:
                    break;
            }

        }
        /// <summary>
        /// Get the closest Brick around the ant
        /// </summary>
        /// <returns></returns>
        public Brick getClosestBrick()
        {
            List<Brick> ListBricks;
            ListBricks = BricksAroundMe();
            if (ListBricks.Count() != 0)    //if there are bricks around me:
            {
                closestBrick = ListBricks[0];    //first initialize

                //search the closest brick
                foreach (Brick OneBrick in ListBricks)
                {
                    //Check which on is closer
                    if (Helpers.Distance(OneBrick.Location, this.SDLocation) <
                        Helpers.Distance(closestBrick.Location, this.SDLocation))
                    {
                        brickFound = OneBrick;
                    }

                }
                return brickFound;
            }
            return null;    //if not found, return null
        }

        public void LogEnergy()
        {
            Logger.WriteLogFile($"GreenWorker Energy: {Energy}");
        }
        public void RandomDirection()
        {
            int rand = 0;
            Random gen = new Random();
            rand = gen.Next(1, 8);
            switch (rand)
            {
                case 1:
                    SudOuest();
                    break;
                case 2:
                    NordOuest();
                    break;
                case 3:
                    SudEst();
                    break;
                case 4:
                    NordEst();
                    break;
                case 5:
                    Sud();
                    break;
                case 6:
                    Nord();
                    break;
                case 7:
                    Est();
                    break;
                case 8:
                    Ouest();
                    break;
            }
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

        public void NextMove()
        {
            if (Blocked.Contains("oui") == true)
            {
                RandomDirection();
            }
            Move();
        }

        public void GoFood(int X, int Y)
        {
            Speed.X = X;
            Speed.Y = Y;
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