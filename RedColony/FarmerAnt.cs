using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Krohonde.RedColony
{
    public class FarmerAnt : Ant
    {
        public FarmerAnt(Point location, Point speed, RedColony colony) : base(location, speed, colony) 
        { }

        public override void Live()
        {
            throw new NotImplementedException();
        }
    }
}
