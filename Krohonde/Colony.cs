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
        protected IMotherNature myWorld;
        protected System.Windows.Point location;
        protected System.Drawing.Point[] hill;
        protected List<Ant> ants;
        private readonly Color color;
        private int foodstore;

        public Colony(Color col, System.Windows.Point loc, IMotherNature world)
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

        /// <summary>
        /// Remove an ant from the colony (it's dead or illegal)
        /// </summary>
        /// <param name="ant"></param>
        public void Dispose(Ant ant)
        {
            ants.Remove(ant);
        }

        /// <summary>
        /// An ant intends to dump its food bag into the colony's foodstore
        /// </summary>
        /// <param name="ant"></param>
        /// <returns></returns>
        public bool DumpFood(Ant ant)
        {
            if (!Helpers.IsInPolygon(hill, ant.SDLocation)) return false; // can't dump from outside
            foodstore += ant.FoodBag;
            ant.EmptyFoodBag();
            return true;
        }

        public abstract void Spawn(int nbAnts);

        public System.Drawing.Point[] Hill { get => hill; }

        public List<Ant> Population { get => ants; }

        public Color Color { get => color; }

        public int FoodStore { get => foodstore; }

        public System.Windows.Point Location { get => location; }

        public IMotherNature World() { return myWorld; }
    }
}
