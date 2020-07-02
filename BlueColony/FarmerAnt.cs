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
        private Point UnblockDestination;
        private BlueColony colony;

        public FarmerAnt(Point location, Point speed, BlueColony colony) : base(location, speed, colony)
        {
            this.colony = colony;
        }

        public override void Live()
        {
            Point location = new Point(X, Y);
            
            if (location == UnblockDestination)
            {
                this.BlockedBy = null;
                this.Speed = new Point(Destination.X - X, Destination.Y - Y);
            }

            if (this.BlockedBy != null)
            {
                UnblockDestination = colony.unblock(this, Destination);
                this.Speed = new Point (UnblockDestination.X - X, UnblockDestination.Y - Y);

            }
            else
            {
                ///Move();
            }
        }
       
        

        
    }
}
