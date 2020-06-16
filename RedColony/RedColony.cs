using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Krohonde;
using System.Windows;
using System.Drawing;

namespace Krohonde.RedColony
{
    public class RedColony : Colony
    {
        public RedColony(System.Windows.Point loc, MotherNature world) : base(Color.Red,loc, world)
        {
        }

    }
}
