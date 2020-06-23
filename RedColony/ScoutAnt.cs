﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Krohonde.RedColony
{
    public class ScoutAnt : Ant
    {
        public ScoutAnt(Point location, Point speed, RedColony colony) : base(location, speed, colony)
        {
        }

        public override void Live(double deltatime)
        {
            Move(deltatime);
        }
    }
}