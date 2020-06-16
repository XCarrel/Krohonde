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

        public override void Spawn(int nbAnts)
        {
            for (int i=0; i<nbAnts; i++)
            {
                ants.Add(new WorkerAnt(new System.Windows.Point(location.X + MotherNature.alea.Next(0,200)-100, location.Y + MotherNature.alea.Next(0, 200) - 100), new System.Windows.Point(MotherNature.alea.Next(0, 9) - 4, MotherNature.alea.Next(0, 9) - 4), this));
            }
        }

    }
}
