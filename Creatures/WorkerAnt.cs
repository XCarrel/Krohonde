﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Krohonde.World;
using Krohonde.Creatures;
using System.Drawing;

namespace Creatures
{
    public class WorkerAnt : Ant
    {
        public WorkerAnt(Point location, Point speed, MotherNature world) : base (location,speed,world)
        { }
    }
}
