using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Krohonde;
using System.Windows;
using System.Drawing;

namespace Krohonde.GreenColony
{
    public class GreenColony : Colony
    {
        public GreenColony(System.Windows.Point loc, MotherNature world) : base (Color.LightGreen, loc,world)
        {
        }

    }
}
