using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Point = System.Drawing.Point;
using Krohonde.Creatures;
using Creatures;

namespace Krohonde.World
{
    public class Colony
    {
        private Point[] hill;
        private List<Ant> ants;

        public Colony(Point location)
        {
            // Anthill
            hill = new Point[] {
                new Point { X = location.X-50, Y = location.Y-30 },
                new Point { X = location.X, Y = location.Y-55 },
                new Point { X = location.X+50, Y = location.Y-33 },
                new Point { X = location.X+35, Y = location.Y+30 },
                new Point { X = location.X-30, Y = location.Y+33 }
            };
            ants = new List<Ant>();
        }

        public void Spawn(int nbIndividuals)
        {
            Random alea = new Random(); //TODO use unique generator
            for (int i = 0; i < nbIndividuals; i++)
            {
                ants.Add(new WorkerAnt(new Point(this.ClientSize.Width / 2 + alea.Next(-50, 50), this.ClientSize.Height / 2 + alea.Next(-50, 50)), new Vector(alea.Next(-10, 10), alea.Next(-10, 10)), myWorld));
            }
        }

        public Point[] Hill { get => hill; }

        public List<Ant> Population { get => ants; }
    }
}
