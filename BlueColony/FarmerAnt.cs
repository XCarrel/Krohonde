﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Krohonde.BlueColony
{
    public class FarmerAnt : Ant
    {
        public FarmerAnt(Point location, Point speed, BlueColony colony) : base(location, speed, colony)
        {
        }

        public override void Live(double deltatime)
        {
            Move(deltatime);
        }
    }
}