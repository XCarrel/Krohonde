using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krohonde
{
    public class Pheromon
    {
        private Point location; // Where the resource is
        protected readonly MotherNature.PheromonTypes pheromontype;
        protected readonly Colony colony;
        protected readonly TimeSpan birthday;

        public Pheromon(Point loc, MotherNature.PheromonTypes pherotype, Colony col)
        {
            location = loc;
            pheromontype = pherotype;
            colony = col;
            birthday = col.World().universaltime.Elapsed;
        }

        public MotherNature.PheromonTypes PheromonType { get => pheromontype; }

        public Point Location { get => location; }

        /// <summary>
        /// The intensity of the smell decreases linearly with time from 1.0 to 0.0
        /// </summary>
        public double Intensity { get => (MotherNature.PHEROMON_LIFE_DURATION - (colony.World().universaltime.Elapsed.TotalSeconds - birthday.TotalSeconds)) / MotherNature.PHEROMON_LIFE_DURATION; }

        public Colony Colony { get => colony; }
    }
}
