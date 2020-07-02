using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Krohonde;
using System.Windows;
using System.Drawing;

namespace Krohonde.BlueColony
{
    public class BlueColony : Colony
    {
        public List<Food> listeFood = new List<Food>();
        public List<Brick> listeMat = new List<Brick>();
        public BlueColony(System.Windows.Point loc, IMotherNature world) : base (Color.Blue, loc,world)

        {
            queen = new BlueQueen(new System.Drawing.Point((int)loc.X, (int)loc.Y), new System.Drawing.Point(0, 0), this);
        }

        public override void Hatch(Egg egg)
        {
            eggs.Remove(egg);
            switch (egg.Type)
            {
                case MotherNature.AntTypes.FarmerAnt: ants.Add(new FarmerAnt(new System.Windows.Point(egg.Location.X, egg.Location.Y), new System.Windows.Point(5, 5), this)); break;
                case MotherNature.AntTypes.WorkerAnt: ants.Add(new WorkerAnt(new System.Windows.Point(egg.Location.X, egg.Location.Y), new System.Windows.Point(5, 5), this)); break;
                case MotherNature.AntTypes.ScoutAnt: ants.Add(new ScoutAnt(new System.Windows.Point(egg.Location.X, egg.Location.Y), new System.Windows.Point(5, 5), this)); break;
                case MotherNature.AntTypes.SoldierAnt: ants.Add(new SoldierAnt(new System.Windows.Point(egg.Location.X, egg.Location.Y), new System.Windows.Point(5, 5), this)); break;
            }
        }

        public System.Windows.Point unblock(Ant ant, System.Windows.Point destination)
        {
            System.Windows.Point unblock = destination;

            if (ant.Blocked == "oui, par mère nature")
            {
                if (destination.X <= 0)
                {
                    unblock.X = 10;
                }
                else
                {
                    if (destination.Y <= 0)
                    {
                        unblock.Y = 10;
                    }
                    else
                    {
                        unblock.Y = -10;
                    }

                    unblock.X = -10;
                }

            }
            else
            {
                unblock.Y = -1 * destination.X;
                unblock.X = -1 * destination.Y;
            }

            return unblock;
        }
        public void AddFood(Food item)
        {
            bool present = false;
            foreach (Food element in listeFood)
            {
                if (item.Location.X == element.Location.X && item.Location.Y == element.Location.Y)
                {
                    present = true;
                }
            }
            if (present == false)
            {
                listeFood.Add(item);
                Logger.WriteLogFile("nourriture :" + item.Location.X + ":" + item.Location.Y);
            }
        }
        public void AddMat(Brick item)
        {
            bool present = false;
            foreach (Brick element in listeMat)
            {
                if (item.Location.X == element.Location.X && item.Location.Y == element.Location.Y)
                {
                    present = true;
                }
            }
            if (present == false)
            {
                listeMat.Add(item);
                Logger.WriteLogFile("ressource :" + item.Location.X + ":" + item.Location.Y);
            }
        }

        /*  A supprimer
        public bool CanMove(Ant ant, System.Windows.Point destination)
        {
            int width = ant.Colony.World().width;
            int height = ant.Colony.World().height;
            Logger.WriteLogFile("width:" + width.ToString());
            Logger.WriteLogFile("height:" + height.ToString());
            

            if  (destination.X >= width  -  50 || destination.Y >= height  -  50 || destination.X <= 50 || destination.Y <= 50) {
                return false;
            
            } else
            {
                return true;
            }

        } */
    }
}
