using System;
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
        public int NbMaxAntTypeFarmer = 2;
        public int NbMaxAntTypeWorker = 2;
        public int NbMaxAntTypeScout = 2;
        public int NbMaxAntTypeSoldier = 2;
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
            /*Logger.WriteLogFile("Nombre de fourmis travailleuse :" + NbWorkerAnt);
            Logger.WriteLogFile("Nombre de fourmis Fermiere :" + NbFarmerAnt);
            Logger.WriteLogFile("Nombre de fourmis scout :" + NbScoutAnt);
            Logger.WriteLogFile("Nombre de fourmis soldat :" + NbSoldierAnt);*/
            if (NbFarmerAnt < NbMaxAntTypeFarmer)
            {
                LayEgg(MotherNature.AntTypes.FarmerAnt, new Point((int)X, (int)Y + 15));
                //NbMaxAntTypeFarmer = NbMaxAntTypeFarmer + 1;
            }
            if (NbWorkerAnt < NbMaxAntTypeWorker)
            {
                //NbMaxAntTypeWorker = NbMaxAntTypeWorker + 1;
                if (NbWorkerAnt < NbMaxAntTypeWorker) LayEgg(MotherNature.AntTypes.WorkerAnt, new Point((int)X, (int)Y - 15));
            }
            if (NbScoutAnt < NbMaxAntTypeScout)
            {
                //NbMaxAntTypeScout = NbMaxAntTypeScout + 1;
                if (NbScoutAnt < NbMaxAntTypeScout) LayEgg(MotherNature.AntTypes.ScoutAnt, new Point((int)X + 15, (int)Y));
            }
            if (NbSoldierAnt < NbMaxAntTypeSoldier)
            {
                //NbMaxAntTypeSoldier = NbMaxAntTypeSoldier + 1;
                if (NbSoldierAnt < NbMaxAntTypeSoldier) LayEgg(MotherNature.AntTypes.SoldierAnt, new Point((int)X - 15, (int)Y));
            }
            DoNothing(); // The queen MUST either do something (Move, Eat, Lay an egg) or announce that she does nothing
        }

    }
}
