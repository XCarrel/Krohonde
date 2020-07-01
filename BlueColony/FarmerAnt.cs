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

        public FarmerAnt(Point location, Point speed, BlueColony colony) : base(location, speed, colony)
        {
        }

        public override void Live(double deltatime)
        {
            /*
            if(this.Location == UnblockSpeed){
            this.BlockedBy = null;
            }

            if(this.BlockedBy != null){
            unblock();
            } else {
            Move(deltatime);
            }
            */
            Move(deltatime);
        }

        public void unblock()
        {
            UnblockSpeed = this.Speed.X;

            if (this.BlockedBy == this.Colony.World())
            {
                if (this.Speed.X <= 0)
                {
                    this.UnblockSpeed.X = 10;
                }
                else
                {
                    this.UnblockSpeed.X = -10;
                }
                if (this.Speed.Y <= 0)
                {
                    this.UnblockSpeed.Y = 10;
                }
                else
                {
                    this.UnblockSpeed.Y = -10;
                }

            }
            else
            {

            }

        }
    }
}
