using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Krohonde.BlueColony
{
    public class BlueQueen : Queen
    {
        public BlueQueen(Point location, Point speed, Colony colony) : base(location, speed, colony)
        { }

        public bool CanLayEgg(Point location)
        {
            Rectangle newcrib = new Rectangle(location, new System.Drawing.Size( Colony.CRIB_SIZE, Colony.CRIB_SIZE));
            foreach (Egg crib in MyColony.Nursery)
                if (newcrib.IntersectsWith(new Rectangle(crib.Location, new System.Drawing.Size(Colony.CRIB_SIZE, Colony.CRIB_SIZE))))
                {
                    return false;
                }
            return true;
        }

        public override void Live(double deltatime)
        {
            Logger.WriteLogFile("food: " + MyColony.FoodStore.ToString());
            
            int[] population = { 0, 0, 0, 0 };
            foreach (Ant superfourmi in MyColony.Population)
            {
                switch (superfourmi.GetType().Name.ToString())
                {
                    case "WorkerAnt": population[0]++; break;
                    case "FarmerAnt": population[1]++; break;
                    case "ScoutAnt": population[2]++; break;
                    case "SoldierAnt": population[3]++; break;
                }
            }
            Logger.WriteLogFile("population: " + population.ToString());

            if (Energy > 15000 && MyColony.FoodStore > 0)
            {
                if (CanLayEgg(SDLocation))
                {
                    Speed = new Point(0, 0);
                    if (population[2] < 4) { LayEgg(MotherNature.AntTypes.ScoutAnt, MyColony.Queen.SDLocation); }
                    else if (population[1] < 4) { LayEgg(MotherNature.AntTypes.FarmerAnt, MyColony.Queen.SDLocation); }
                    else if (population[0] < 4) { LayEgg(MotherNature.AntTypes.WorkerAnt, MyColony.Queen.SDLocation); }
                    else if (population[3] < 4) { LayEgg(MotherNature.AntTypes.SoldierAnt, MyColony.Queen.SDLocation); }
                    Logger.WriteLogFile("egg");
                }
                else
                {
                    Speed = new Point(MotherNature.alea.Next(0, 32), MotherNature.alea.Next(0, 32));
                     Move(deltatime);
                    Logger.WriteLogFile("move");
                }
            }
            else
            {
                if (Energy < MotherNature.MAX_ENERGY - MotherNature.FOOD_TO_ENERGY * MotherNature.MAX_QUEEN_BITE_SIZE)
                {
                    Eat(2147483647);
                    Logger.WriteLogFile("miam");
                }
                else
                {
                    DoNothing();
                    Logger.WriteLogFile("rien");
                }
            }
        }
    }
}