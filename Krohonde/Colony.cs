using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Krohonde
{
    public abstract class Colony
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

        public abstract void Spawn(int nbAnts);

        public System.Drawing.Point[] Hill { get => hill; }

        public List<Ant> Population { get => ants; }

        public Color Color { get => color; }

        public System.Windows.Point Location { get => location; }

        public MotherNature World { get => myWorld; }
    }
}
