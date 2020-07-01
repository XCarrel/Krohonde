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
        private Point UnblockSpeed;
        private BlueColony colony;

        public FarmerAnt(Point location, Point speed, BlueColony colony) : base(location, speed, colony)
        {
            this.colony = colony;
        }

        public override void Live(double deltatime)
        {
            Point location = new Point(X, Y);

            if(location == UnblockSpeed){
            this.BlockedBy = null;
            }

            if(this.BlockedBy != null){
            colony.unblock(this, this.Speed);
            } else {
            Move(deltatime);
            }
       
        }

        
    }
}
