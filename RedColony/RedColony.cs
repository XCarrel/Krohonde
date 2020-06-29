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
        public RedColony(System.Windows.Point loc, IMotherNature world) : base(Color.Red,loc, world)
        {
        }

        public override void Hatch(Larvae egg)
        {
            eggs.Remove(egg);
            switch (egg.Type)
            {
                case MotherNature.AntTypes.FarmerAnt: ants.Add(new FarmerAnt(new System.Windows.Point(egg.SDLocation.X, egg.SDLocation.Y), new System.Windows.Point(5, 5), this)); break;
                case MotherNature.AntTypes.WorkerAnt: ants.Add(new WorkerAnt(new System.Windows.Point(egg.SDLocation.X, egg.SDLocation.Y), new System.Windows.Point(5, 5), this)); break;
                case MotherNature.AntTypes.ScoutAnt: ants.Add(new ScoutAnt(new System.Windows.Point(egg.SDLocation.X, egg.SDLocation.Y), new System.Windows.Point(5, 5), this)); break;
                case MotherNature.AntTypes.SoldierAnt: ants.Add(new SoldierAnt(new System.Windows.Point(egg.SDLocation.X, egg.SDLocation.Y), new System.Windows.Point(5, 5), this)); break;
            }
        }
    }

}
