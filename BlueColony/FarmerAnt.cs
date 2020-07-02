using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Krohonde.BlueColony
{
    public class FarmerAnt : Ant
    {
        private Point Destination;
        private BlueColony colony;

        public FarmerAnt(Point location, Point speed, BlueColony colony) : base(location, speed, colony)
        {
            this.colony = colony;
            Destination = MyColony.World().Colonies().First().Location;
        }

        public override void Live()
        {
            Speed.X = Destination.X - X;
            Speed.Y = Destination.Y - Y;
            Rock bloc = BlockedBy as Rock;
            if (bloc != null)
            {
                // rotate speed 90° left
                double tr = Speed.X;
                Speed.X = -Speed.Y;
                Speed.Y = tr;
                if (Helpers.IsInPolygon(bloc.Shape, new System.Drawing.Point((int)(X + Speed.X), (int)(Y + Speed.Y))))
                {
                    Speed.X = -Speed.X;
                    Speed.Y = -Speed.Y;
                }
            }
            Move();
        }




    }
}
