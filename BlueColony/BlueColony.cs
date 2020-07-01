using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Krohonde;
using System.Windows;
using System.Drawing;

namespace Krohonde.BlueColony
{
    public class BlueColony : Colony
    {
        public BlueColony(System.Windows.Point loc, IMotherNature world) : base (Color.Blue, loc,world)
        {
            queen = new BlueQueen(new System.Drawing.Point((int)loc.X, (int)loc.Y), new System.Drawing.Point(0, 0), this);
        }

        public override void Hatch(Egg egg)
        {
            eggs.Remove(egg);
            switch (egg.Type)
            {
                case MotherNature.AntTypes.FarmerAnt: ants.Add(new FarmerAnt(new System.Windows.Point(egg.Location.X, egg.Location.Y), new System.Windows.Point(5, 5), this)); break;
                case MotherNature.AntTypes.WorkerAnt: ants.Add(new WorkerAnt(new System.Windows.Point(egg.Location.X, egg.Location.Y), new System.Windows.Point(5, 5), this)); break;
                case MotherNature.AntTypes.ScoutAnt: ants.Add(new ScoutAnt(new System.Windows.Point(egg.Location.X, egg.Location.Y), new System.Windows.Point(5, 5), this)); break;
                case MotherNature.AntTypes.SoldierAnt: ants.Add(new SoldierAnt(new System.Windows.Point(egg.Location.X, egg.Location.Y), new System.Windows.Point(5, 5), this)); break;
            }
        }

    }
}
