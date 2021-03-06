﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Krohonde.RedColony
{
    public class RedQueen : Queen
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
        public int SpawnAnt = 0;
        public int SpawnAnt2 = 0;
        public int SpawnAnt3 = 1;
        private int Phase = 1;
        private int Mouvement = 0;
        public RedQueen(Point location, Point speed, Colony colony) : base(location, speed, colony)
        { }
        void GeneralRefresh()
        {
            if (SoldierAnt.enemyRepered != null)
            {
                foreach (SoldierAnt.EnemyListedActu antCheck in SoldierAnt.enemyRepered.ToList())
                {
                    antCheck.time = antCheck.time - 1;
                    if (antCheck.time < 1)
                    {
                        SoldierAnt.enemyRepered.Remove(antCheck);
                    }
                }
            }
            if (ScoutAnt.ressources.Count > 0)
            {
                foreach (ScoutAnt.ResourceCustom resCust in ScoutAnt.ressources.ToList())
                {
                    resCust.tickAcurate = resCust.tickAcurate - 1;
                    if (resCust.tickAcurate < 1)
                    {
                        ScoutAnt.ressources.Remove(resCust);
                    }
                }
            }
        }
        public override void Live()
        {
            base.Live();
            GeneralRefresh();
            if (Energy > 25000)
            {
                NbWorkerAnt = 0;
                NbFarmerAnt = 0;
                NbScoutAnt = 0;
                NbSoldierAnt = 0;
                //Logger.WriteLogFile("RED COLONY : Queen Live ! ENERGIE : " + Energy);
                foreach (Ant ant in MyColony.Population)
                {
                    if (ant.Fullname.Contains("WorkerAnt"))
                    {
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
                //Logger.WriteLogFile("RED COLONY : Queen counted !");
                if (Phase == 1 && Energy > 25000)
                {
                    //Logger.WriteLogFile("RED COLONY : Phase 1!");
                    switch (SpawnAnt)

                    {
                        case 3:
                            //Logger.WriteLogFile("RED COLONY : EGG SCOUT ! + phase 2");
                            LayEgg(MotherNature.AntTypes.ScoutAnt, new Point((int)X + 16, (int)Y));
                            Phase = 2;
                            break;

                        case 2:
                            //Logger.WriteLogFile("RED COLONY : EGG SCOUT 2!");
                            LayEgg(MotherNature.AntTypes.ScoutAnt, new Point((int)X - 16, (int)Y));
                            break;

                        case 1:
                            //Logger.WriteLogFile("RED COLONY : EGG SCOUT 1!");
                            LayEgg(MotherNature.AntTypes.ScoutAnt, new Point((int)X, (int)Y + 16));
                            break;
                        case 0:
                            //Logger.WriteLogFile("RED COLONY : EGG SCOUT 0!");
                            LayEgg(MotherNature.AntTypes.ScoutAnt, new Point((int)X, (int)Y - 16));
                            break;
                    }
                    SpawnAnt = (SpawnAnt + 1) % 4;
                }
                if (Phase == 2)
                {
                    if (Mouvement < 15)
                    {
                        //Logger.WriteLogFile("RED COLONY : MOVE!");
                        Speed.X = 0;
                        Speed.Y = -100;
                        Move();
                        Mouvement++;
                    }
                    if (Mouvement > 14) Phase = 3;
                }
                if (Phase == 3 && Energy > 25000)
                {
                    //Logger.WriteLogFile("RED COLONY : SPAWN2!");
                    switch (SpawnAnt2)

                    {
                        case 3:
                            LayEgg(MotherNature.AntTypes.ScoutAnt, new Point((int)X + 16, (int)Y));
                            Phase = 4;
                            break;

                        case 2:
                            LayEgg(MotherNature.AntTypes.ScoutAnt, new Point((int)X - 16, (int)Y));
                            break;

                        case 1:
                            LayEgg(MotherNature.AntTypes.ScoutAnt, new Point((int)X, (int)Y + 16));
                            break;
                        case 0:
                            LayEgg(MotherNature.AntTypes.ScoutAnt, new Point((int)X, (int)Y - 16));
                            break;
                    }
                    SpawnAnt2 = (SpawnAnt2 + 1) % 4;
                }
                if (Phase == 4 && Energy > 20000)
                {

                    //Logger.WriteLogFile("RED COLONY : PHASE2!");
                    if (NbFarmerAnt < NbMaxAntTypeFarmer && SpawnAnt3 == 4)
                    {
                        LayEgg(MotherNature.AntTypes.FarmerAnt, new Point((int)X, (int)Y + 16));
                        SpawnAnt3 = 1;
                    }
                    else SpawnAnt3 = 1;
                    if (NbWorkerAnt < NbMaxAntTypeWorker && SpawnAnt3 == 3)
                    {
                        LayEgg(MotherNature.AntTypes.WorkerAnt, new Point((int)X, (int)Y - 16));
                        SpawnAnt3++;
                    }else SpawnAnt3++;
                    if (NbScoutAnt < NbMaxAntTypeScout && SpawnAnt3 == 2)
                    {
                        LayEgg(MotherNature.AntTypes.ScoutAnt, new Point((int)X + 16, (int)Y));
                        SpawnAnt3++;
                    }
                    else SpawnAnt3++;
                    if (NbSoldierAnt < NbMaxAntTypeSoldier && SpawnAnt3 == 1)
                    {
                        LayEgg(MotherNature.AntTypes.SoldierAnt, new Point((int)X - 16, (int)Y));
                        SpawnAnt3++;
                    }else SpawnAnt3++;
                }
            }
            else
            {
                Eat(10);
            }
            DoNothing();
        }

    }
}
