﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Krohonde.RedColony
{
    public class SoldierAnt : Ant
    {
        public SoldierAnt(Point location, Point speed, RedColony colony) : base(location, speed, colony)
        {
        }

        public override void Live()
        {
            Move();
        }
    }
}
