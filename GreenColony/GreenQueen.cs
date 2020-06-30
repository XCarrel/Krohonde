using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Krohonde.GreenColony
{
    public class GreenQueen: Queen
    {
        private bool laidanegg = false;
        public GreenQueen(Point location, Point speed, Colony colony) : base(location, speed, colony)
        { }
        public override void Live(double deltatime)
        {
            if (!laidanegg) 
                MyColony.LayEgg(MotherNature.AntTypes.FarmerAnt, new System.Drawing.Point((int)(MyColony.Location.X + 20), (int)MyColony.Location.Y), this);
            laidanegg = true;
        }

    }
}
