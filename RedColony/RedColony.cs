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
                ants.Add(new WorkerAnt(new System.Windows.Point(location.X, location.Y), new System.Windows.Point(MotherNature.alea.Next(0, 200)-100, MotherNature.alea.Next(0, 200)-100), this));
            }
        }

    }

}
