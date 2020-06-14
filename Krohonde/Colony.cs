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

        public void Spawn()
        {
            ants.Add(new WorkerAnt(new Point(location.X - 50, location.Y + 50), new Point(5, 5), myWorld));
            ants.Add(new WorkerAnt(new Point(location.X + 50, location.Y + 50), new Point(-5, 5), myWorld));
            ants.Add(new WorkerAnt(new Point(location.X - 50, location.Y - 50), new Point(5, -5), myWorld));
            ants.Add(new WorkerAnt(new Point(location.X + 50, location.Y - 50), new Point(-5, -5), myWorld));
        }

        public System.Drawing.Point[] Hill { get => hill; }

        public List<Ant> Population { get => ants; }
    }
}
