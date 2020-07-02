using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Krohonde.BlueColony
{
    public class ScoutAnt : Ant
    {
        public List<Food> food = new List<Food>();
        public List<Brick> mat = new List<Brick>();
        public List<Ant> colo = new List<Ant>();

        public int time = 50;
        Point destination;
        Point unblockDestination;
        BlueColony bcolony;
        public ScoutAnt(Point location, Point speed, BlueColony colony) : base(location, speed, colony)
        {
            this.bcolony = colony;
            randomPos();
        }
        public override void Live()
        {
            Point location = new Point(X, Y);

            if ((Math.Abs(destination.X - X) < 50) && (Math.Abs(destination.Y - Y) < 50))
            {
                Logger.WriteLogFile("arrivee en :" + destination.X + ";" + destination.Y);
                randomPos();
            }
            Logger.WriteLogFile("vitese :" + this.Speed.X + ";" + this.Speed.Y);

            if (this.BlockedBy != null)
            {
                randomPos();
                this.BlockedBy = null;

            }
            else
            {
                Move();
            }

            if (time == 0)
            {
                food = FoodAroundMe();
                mat = BricksAroundMe();
                colo = EnemiesAroundMe();
                foreach (Food spot in food)
                {
                    bcolony.AddFood(spot);
                }
                foreach (Brick spot in mat)
                {
                    bcolony.AddMat(spot);
                }
                foreach (Ant spot in colo)
                {
                    if(spot.GetType().Name == "RedGueen" || spot.GetType().Name == "GreenQueen")
                    {
                        // Ajout de la reine
                    }
                }
                time = 50;
            }
            else
            {
                time--;
            }
        }
        public void randomPos()
        {
            Random random = new Random();
            double x = random.NextDouble() * (this.MyColony.World().width -200);
            x += 100;
            double y = random.NextDouble() * (this.MyColony.World().height -200);
            y += 100;
            this.destination = new Point(x, y);
            this.Speed.X = destination.X - X;
            this.Speed.Y = destination.Y - Y;
            Logger.WriteLogFile("nouvelle direction :" + destination.X + ";" + destination.Y);
        }
    }
}
