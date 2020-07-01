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
        public override void Live(double deltatime)
        {
            Logger.WriteLogFile("food : " + MyColony.FoodStore.ToString());

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
                Logger.WriteLogFile(superfourmi.Energy.ToString());
            }

            if (MyColony.Queen.Energy > 15000 && MyColony.FoodStore > 1000)
            {
                if (!SDLocation.IsEmpty)
                {
                    Move(deltatime);
                }
                else
                {
                    LayEgg(MotherNature.AntTypes.FarmerAnt, MyColony.Queen.SDLocation);
                }
            }
            else
            {
                Eat(1);
            }
        }
    }
}
