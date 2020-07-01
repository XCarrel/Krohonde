using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Krohonde.BlueColony
{
    public class SoldierAnt : Ant
    {
        private Point SaveDestination;
        private BlueColony colony;


        public SoldierAnt(Point location, Point speed, BlueColony colony) : base(location, speed, colony)
        {
            this.colony = colony;
        }

        public override void Live(double deltatime)
        {
            Point location = new Point(X, Y);

            if (location == this.Speed)
            {
                this.BlockedBy = null;
            }

            if (this.BlockedBy != null)
            {
                SaveDestination = Speed;
                colony.unblock(this, this.Speed);
            }
            else
            {
                Move(deltatime);
            }
        }
    }
}
