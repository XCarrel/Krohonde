using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Krohonde.RedColony
{
    /**
    * Auteur : Eliott Jaquier CORP & CO - (P0PC0RN)
    * Version : 3.7 (02.07.2020) - phase 1
    * Comments : Remove
    */
    public class SoldierAnt : Ant
    {
        public static List<EnemyListedActu> enemyRepered = new List<EnemyListedActu>(0);
        public static List<Queen> listOfQueen = new List<Queen>(0);
        private Ant enemyToTarget;
        public Point goToPosition;
        private Point pointOfAction;
        private bool sortirImpasse = false;
        public SoldierAnt(Point location, Point speed, RedColony colony) : base(location, speed, colony)
        {
            Random r = new Random();
            System.Drawing.Point pt = new System.Drawing.Point(Convert.ToInt32(MyColony.Location.X), Convert.ToInt32(MyColony.Location.Y));
            pointOfAction = new Point(MyColony.Location.X + r.Next(-100, 300), MyColony.Location.Y + r.Next(-100, 300));
        }
        [Serializable]
        public class EnemyListedActu
        {
            public Ant fourmis;
            public int time;
            public float importance;
        }
        public static void PointAnEnemy(Ant ant, int importance)
        {
            bool isQueenOk = false;
            foreach (Queen queen in listOfQueen)
            {
                if (queen == ant.Colony.Queen)
                {
                    isQueenOk = true;
                }
            }
            if (!isQueenOk)
            {
                listOfQueen.Add(ant.Colony.Queen);
            }

            bool isAlreadyInList = false;
            foreach (EnemyListedActu antCheck in enemyRepered)
            {
                if (antCheck.fourmis == ant)
                {
                    isAlreadyInList = true;
                }
            }
            if (!isAlreadyInList)
            {
                EnemyListedActu enemy = new EnemyListedActu();
                enemy.fourmis = ant;
                enemy.time = 600;
                float importOfType = 0;
                if (ant.Fullname.Contains("WorkerAnt"))
                {
                    importOfType = 2;
                }
                if (ant.Fullname.Contains("FarmerAnt"))
                {
                    importOfType = 3;
                }
                if (ant.Fullname.Contains("ScoutAnt"))
                {
                    importOfType = 0.5f;
                }
                if (ant.Fullname.Contains("SoldierAnt"))
                {
                    importOfType = 10f;
                }
                enemy.importance = importance * importOfType;
                Logger.WriteLogFile("RED COLONY FORCE : New enemy :" + ant.Fullname + " Importance :" + (importance * importOfType));
                enemyRepered.Add(enemy);
            }
        }
        private int tick;
        public override void Live()
        {
            tick++;
            if (sortirImpasse)
            {
                if ((tick % 10) == 0)
                {
                    Random r = new Random();
                    goToPosition = new Point(X + r.Next(-100, 100), Y + r.Next(-100, 100));
                }
                Speed.X = goToPosition.X - X;
                Speed.Y = goToPosition.Y - Y;
                Move();
                if ((tick % 25) == 0)
                {
                    if(BlockedBy == null)
                    {
                        sortirImpasse = false;
                    }
                }
                
            }
            bool attackQueen = false;
            Queen qu = null;
            foreach (Queen queen in listOfQueen)
            {
                int distance = Math.Abs(Convert.ToInt32(queen.X) - Convert.ToInt32(X)) + Math.Abs(Convert.ToInt32(queen.Y) - Convert.ToInt32(Y));
                if(distance < 100)
                {
                    qu = queen;
                    attackQueen = true;
                }
            }
            if (attackQueen)
            {
                int distance = Math.Abs(Convert.ToInt32(qu.X) - Convert.ToInt32(X)) + Math.Abs(Convert.ToInt32(qu.Y) - Convert.ToInt32(Y));
                if(distance > 30)
                {
                    Logger.WriteLogFile("ATTAKING A QUEEN! ( RED COLONY )");
                    Speed.X = qu.X - X;
                    Speed.Y = qu.Y - Y;
                    Move();
                }
                else
                {
                    Logger.WriteLogFile("QUEEN IS DEAD! ( RED COLONY )");
                    qu.Die();
                    listOfQueen.Remove(qu);
                }
                
                
                /*goToPosition = new Point(qu.X, qu.Y); 
                int distance = Math.Abs(Convert.ToInt32(goToPosition.X) - Convert.ToInt32(X)) + Math.Abs(Convert.ToInt32(goToPosition.Y) - Convert.ToInt32(Y));
                if (distance < 40)
                {
                    Hit(enemyToTarget);
                    Logger.WriteLogFile("RED COLONY FORCE: ATTAQUE :" + enemyToTarget.Fullname + " PV:" + enemyToTarget.Energy + " Distance" + distance);
                }
                else
                {
                    Speed.X = goToPosition.X - X;
                    Speed.Y = goToPosition.Y - Y;
                    Move();
                }*/
            }
            else
            {
                if (enemyRepered.Count > 0)
                {
                    if(enemyToTarget == null || enemyToTarget.Energy < 1 || ((tick%7) == 0)) {
                        /*SELECT PART*/
                        int optimal = 0;
                        int counter = 0;
                        float importance = 0;
                        int distLast = 100000;
                    
                        foreach (EnemyListedActu enemyTest in enemyRepered)
                        {
                            if (enemyTest.importance > importance)
                            {
                                if (enemyTest.fourmis.Energy >= 0)
                                {
                                    importance = enemyTest.importance;
                                    optimal = counter;
                                }
                            }
                            if (enemyTest.importance == importance)
                            {
                                int distActu = Math.Abs(Convert.ToInt32(enemyTest.fourmis.X) - Convert.ToInt32(X)) + Math.Abs(Convert.ToInt32(enemyTest.fourmis.Y) - Convert.ToInt32(Y));
                                if (distLast >= distActu)
                                {
                                    if (enemyTest.fourmis.Energy >= 0)
                                    {
                                        importance = enemyTest.importance;
                                        optimal = counter;
                                    }
                                }
                            }
                            counter++;
                        }
                        enemyToTarget = enemyRepered[optimal].fourmis;
                    }
                    if(BlockedBy != null)
                    {
                        sortirImpasse = true;
                    }
                    else
                    {
                        goToPosition = new Point(enemyToTarget.X, enemyToTarget.Y);
                    }
                    int distance = Math.Abs(Convert.ToInt32(goToPosition.X) - Convert.ToInt32(X)) + Math.Abs(Convert.ToInt32(goToPosition.Y) - Convert.ToInt32(Y));
                    if (distance < 40)
                    {
                        Hit(enemyToTarget);
                        Logger.WriteLogFile("RED COLONY FORCE: ATTAQUE :" + enemyToTarget.Fullname + " PV:" + enemyToTarget.Energy + " Distance" + distance);
                    }
                    else
                    {
                        Speed.X = goToPosition.X - X;
                        Speed.Y = goToPosition.Y - Y;
                        Move();
                    }
                }
                else
                {
                    goToPosition = pointOfAction;
                    int distance = Math.Abs(Convert.ToInt32(goToPosition.X) - Convert.ToInt32(X)) + Math.Abs(Convert.ToInt32(goToPosition.Y) - Convert.ToInt32(Y));
                    if (distance > 40)
                    {
                        if (BlockedBy != null)
                        {
                            sortirImpasse = true;
                        }
                        Speed.X = goToPosition.X - X;
                        Speed.Y = goToPosition.Y - Y;
                        Move();
                    }
                }
            }
            if ((tick % 3) == 0)
            {
                foreach (Ant enemy in EnemiesAroundMe())
                {
                    SoldierAnt.PointAnEnemy(enemy, 2);
                }
            }
        }
    }
}
