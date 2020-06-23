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

        
        public override void Spawn(int nbAnts)
        {
            for (int i = 0; i < nbAnts; i++)
            {
                switch (i % 4)
                {
                    case 0:
                        ants.Add(new WorkerAnt(new System.Windows.Point(location.X, location.Y), new System.Windows.Point(MotherNature.alea.Next(0, 200) - 100, MotherNature.alea.Next(0, 200) - 100), this));
                        break;
                    case 1:
                        ants.Add(new FarmerAnt(new System.Windows.Point(location.X, location.Y), new System.Windows.Point(MotherNature.alea.Next(0, 200) - 100, MotherNature.alea.Next(0, 200) - 100), this));
                        break;
                    case 2:
                        ants.Add(new SoldierAnt(new System.Windows.Point(location.X, location.Y), new System.Windows.Point(MotherNature.alea.Next(0, 200) - 100, MotherNature.alea.Next(0, 200) - 100), this));
                        break;
                    case 3:
                        ants.Add(new ScoutAnt(new System.Windows.Point(location.X, location.Y), new System.Windows.Point(MotherNature.alea.Next(0, 200) - 100, MotherNature.alea.Next(0, 200) - 100), this));
                        break;
                }
            }
        }

    }

}
