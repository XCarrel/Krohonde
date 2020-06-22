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

        public Pheromon(Point loc, int val, MotherNature.PheromonTypes pherotype) : base(loc, val) 
        {
            pheromontype = pherotype;
        }

        public MotherNature.PheromonTypes PheromonType { get => pheromontype; }
    }
}
