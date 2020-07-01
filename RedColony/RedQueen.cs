﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Krohonde.RedColony
{
    public class RedQueen:Queen
    {
        public string CountAnts = null;
        public int NbWorkerAnt = 0;
        public int NbFarmerAnt = 0;
        public int NbScoutAnt = 0;
        public int NbSoldierAnt = 0;
        public int NbMaxAntTypeFarmer = 10;
        public int NbMaxAntTypeWorker = 15;
        public int NbMaxAntTypeScout = 2;
        public int NbMaxAntTypeSoldier = 300;
        public int SpawnAnt = 1;
        private int Phase = 1;
        public RedQueen(Point location, Point speed, Colony colony) : base(location, speed, colony)
        { }
        public override void Live(double deltatime)
        {
            
            NbWorkerAnt = 0;
            NbFarmerAnt = 0;
            NbScoutAnt = 0;
            NbSoldierAnt = 0;
            foreach (Ant ant in MyColony.Population)
            {
                if (ant.Fullname.Contains("WorkerAnt")){
                    NbWorkerAnt = NbWorkerAnt + 1;
                }
                if (ant.Fullname.Contains("FarmerAnt"))
                {
                    NbFarmerAnt = NbFarmerAnt + 1;
                }
                if (ant.Fullname.Contains("ScoutAnt"))
                {
                    NbScoutAnt = NbScoutAnt + 1;
                }
                if (ant.Fullname.Contains("SoldierAnt"))
                {
                    NbSoldierAnt = NbSoldierAnt + 1;
                }

            }
            if (Phase == 1)
            {
                if (SpawnAnt == 4)
                {
                    LayEgg(MotherNature.AntTypes.ScoutAnt, new Point((int)X + 16, (int)Y));
                    SpawnAnt = 1;
                    Phase = 2;
                }
                if (SpawnAnt == 3)
                {
                    LayEgg(MotherNature.AntTypes.ScoutAnt, new Point((int)X - 16, (int)Y));
                    SpawnAnt++;
                }
                if (SpawnAnt == 2)
                {
                    LayEgg(MotherNature.AntTypes.ScoutAnt, new Point((int)X, (int)Y + 16));
                    SpawnAnt++;
                }
                if (SpawnAnt == 1)
                {
                    LayEgg(MotherNature.AntTypes.ScoutAnt, new Point((int)X, (int)Y - 16));
                    SpawnAnt++;
                }
                
            }



            if(Phase == 2){

                if (NbFarmerAnt < NbMaxAntTypeFarmer && SpawnAnt == 4)
                {
                    LayEgg(MotherNature.AntTypes.FarmerAnt, new Point((int)X + 16, (int)Y + 16));
                    SpawnAnt = 1;
                    //NbMaxAntTypeFarmer = NbMaxAntTypeFarmer + 1;
                }
                if (NbWorkerAnt < NbMaxAntTypeWorker && SpawnAnt == 3)
                {
                    LayEgg(MotherNature.AntTypes.WorkerAnt, new Point((int)X - 16, (int)Y + 16));
                    SpawnAnt++;
                    //NbMaxAntTypeFarmer = NbMaxAntTypeFarmer + 1;
                }
                if (NbScoutAnt < NbMaxAntTypeScout && SpawnAnt == 2)
                {
                    LayEgg(MotherNature.AntTypes.ScoutAnt, new Point((int)X + 16, (int)Y - 16));
                    SpawnAnt++;
                    //NbMaxAntTypeFarmer = NbMaxAntTypeFarmer + 1;
                }
                if (NbSoldierAnt < NbMaxAntTypeSoldier && SpawnAnt == 1)
                {
                    LayEgg(MotherNature.AntTypes.SoldierAnt, new Point((int)X - 16, (int)Y - 16));
                    SpawnAnt++;
                    //NbMaxAntTypeFarmer = NbMaxAntTypeFarmer + 1;
                }
            }
            DoNothing(); // The queen MUST either do something (Move, Eat, Lay an egg) or announce that she does nothing
            
        }

    }
}
