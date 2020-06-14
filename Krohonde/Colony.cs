using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Krohonde
{
    public class Colony
    {
        private MotherNature myWorld;
        private Point location;
        private System.Drawing.Point[] hill;
        private List<Ant> ants;

        public Colony(Point loc, MotherNature world)
        {
            myWorld = world;
            // Anthill
            location = loc;
            hill = new System.Drawing.Point[] {
                new System.Drawing.Point { X = (int)location.X-50, Y = (int)location.Y-30 },
                new System.Drawing.Point { X = (int)location.X, Y = (int)location.Y-55 },
                new System.Drawing.Point { X = (int)location.X+50, Y = (int)location.Y-33 },
                new System.Drawing.Point { X = (int)location.X+35, Y = (int)location.Y+30 },
                new System.Drawing.Point { X = (int)location.X-30, Y = (int)location.Y+33 }
            };
            ants = new List<Ant>();
        }

        public void Spawn(int nbAnts)
        {
            for (int i=0; i<nbAnts; i++)
            {
                ants.Add(new WorkerAnt(new Point(location.X + myWorld.alea.Next(0,200)-100, location.Y + myWorld.alea.Next(0, 200) - 100), new Point(myWorld.alea.Next(0, 9) - 4, myWorld.alea.Next(0, 9) - 4), myWorld));
            }
        }

        public System.Drawing.Point[] Hill { get => hill; }

        public List<Ant> Population { get => ants; }
    }
}
