using System;
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


        public WorkerAnt(Point location, Point speed, RedColony colony) : base(location, speed, colony)
        {
             
        }

        public override void Live(double deltatime)
        {
            double distMin = 5000;


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

                Speed.X = closest.Location.X - X;
                Speed.Y = closest.Location.Y - Y;
            }


            Move(deltatime);
        }
    }
}