﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Krohonde.GreenColony
{
    public class WorkerAnt : Ant
    {
        public WorkerAnt(Point location, Point speed, GreenColony colony) : base (location,speed,colony)
        {
        }

        public override void Live()
        {
            base.Live();
        }
    }
}