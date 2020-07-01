using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace Krohonde.BlueColony
{
    public class WorkerAnt : Ant
    {
        private Point Destination;
        private BlueColony colony;
        int i = 0;

        public WorkerAnt(Point location, Point speed, BlueColony colony) : base(location, speed, colony)
        {
            this.colony = colony;
        }

        public override void Live(double deltatime)
        {
            List <Brick> liste = colony.listeMat;
            if (liste.Count > 0)
            {
                FindMat(liste);
                //aller à la pos
            }
            else {
                //chercher
            }
            


            /*if (this.Energy > 15000)
            {
                Point location = new Point(this.X, this.Y);

                if (Destination == location)
                {
                    i += 1;
                    switch (i)
                    {
                        case 1: Destination = new Point(MyColony.Location.X + 0, MyColony.Location.Y + 200); break;
                        case 2: Destination = new Point(MyColony.Location.X + -200, MyColony.Location.Y); break;
                        case 3: Destination = new Point(MyColony.Location.X + 0, MyColony.Location.Y - 200); break;
                    }

                }
                else
                {
                    Destination = new Point(MyColony.Location.X + 200, MyColony.Location.Y);

                    this.Speed = new Point(Destination.X - location.X, Destination.Y - location.Y);
                    Logger.WriteLogFile("Cap vers:" + Speed.ToString());
                    Logger.WriteLogFile("location:" + location.ToString());
                    Move(deltatime);

                }
            }*/




        }
        public Point FindMat(List<Brick> listeMat)
        {
            double distancemin = 10000000;
            Point location = new Point();
            foreach (Brick element in listeMat)
            {
                double distance = Math.Sqrt(Math.Pow(element.Location.X - X, 2) + Math.Pow(element.Location.Y - Y, 2));
                if (distance < distancemin)
                {
                    distancemin = distance;
                    location.X = element.Location.X;
                    location.Y = element.Location.Y;
                }
            }
            return location;
        }


    }
}
