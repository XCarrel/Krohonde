using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Krohonde
{
    public class Colony
    {
        protected MotherNature myWorld;
        protected System.Windows.Point location;
        protected System.Drawing.Point[] hill;
        protected List<Ant> ants;
        private readonly Color color;

        public Colony(Color col, System.Windows.Point loc, MotherNature world)
        {
            myWorld = world;
            // Anthill
            location = loc;
            hill = new System.Drawing.Point[] {
                new System.Drawing.Point { X = (int)location.X-43, Y = (int)location.Y-25 },
                new System.Drawing.Point { X = (int)location.X, Y = (int)location.Y-50 },
                new System.Drawing.Point { X = (int)location.X+43, Y = (int)location.Y-25 },
                new System.Drawing.Point { X = (int)location.X+43, Y = (int)location.Y+25 },
                new System.Drawing.Point { X = (int)location.X, Y = (int)location.Y+50 },
                new System.Drawing.Point { X = (int)location.X-43, Y = (int)location.Y+25 },
            };
            ants = new List<Ant>();
            color = col;
        }

        public void Spawn(int nbAnts)
        {
            for (int i=0; i<nbAnts; i++)
            {
                ants.Add(new WorkerAnt(new System.Windows.Point(location.X + myWorld.alea.Next(0,200)-100, location.Y + myWorld.alea.Next(0, 200) - 100), new System.Windows.Point(myWorld.alea.Next(0, 9) - 4, myWorld.alea.Next(0, 9) - 4), myWorld));
            }
        }

        public System.Drawing.Point[] Hill { get => hill; }

        public List<Ant> Population { get => ants; }

        public Color Color { get => color; }

        public System.Windows.Point Location { get => location; }
    }
}
