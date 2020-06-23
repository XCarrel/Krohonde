using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krohonde
{
    public class Pheromon : Resource
    {
        protected readonly MotherNature.PheromonTypes pheromontype;
        protected readonly Colony colony;

        public Pheromon(Point loc, int val, MotherNature.PheromonTypes pherotype, Colony col) : base(loc, val) 
        {
            pheromontype = pherotype;
            colony = col;
        }

        public MotherNature.PheromonTypes PheromonType { get => pheromontype; }
    }
}
