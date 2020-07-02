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
        public Point goToPosition;
        private Point pointOfAction;
        public SoldierAnt(Point location, Point speed, RedColony colony) : base(location, speed, colony)
        {
            Random r = new Random();
            System.Drawing.Point pt= new System.Drawing.Point(Convert.ToInt32(MyColony.Location.X), Convert.ToInt32(MyColony.Location.Y));
            pointOfAction = new Point(MyColony.Location.X + r.Next(-100, 300), MyColony.Location.Y+ r.Next(-100, 300));
        }
        [Serializable]
        public class EnemyListedActu
        {
            public Ant fourmis;
            public int time;
            public int importanceAntDetected;
        }
        public static void PointAnEnemy(Ant ant,int importance)
        {
            Logger.WriteLogFile("RED COLONY : Enemy trouvé = "+ant.Fullname);
            bool isAlreadyInList = false;
            foreach(EnemyListedActu antCheck in enemyRepered)
            {
                if(antCheck.fourmis == ant)
                {
                    isAlreadyInList = true;
                }
            }
            if (!isAlreadyInList)
            {
                EnemyListedActu enemy = new EnemyListedActu();
                enemy.fourmis = ant;
                enemy.time = 60;
                enemy.importanceAntDetected = importance;
                enemyRepered.Add(enemy);
            }
        }
        public override void Live()
        {
            if(enemyRepered != null)
            {
                if (enemyRepered.Count > 0)
                {
                    /*SELECT PART*//*
                    Ant enemyToTarget = enemyRepered[0].fourmis;

                    int distance = Math.Abs(Convert.ToInt32(goToPosition.X) - Convert.ToInt32(X)) + Math.Abs(Convert.ToInt32(goToPosition.Y) - Convert.ToInt32(Y));
                    if (distance < 2)
                    {
                        Hit(enemyToTarget);
                        Logger.WriteLogFile("RED COLONY : Enemy attaque = E." + enemyToTarget.Energy+" N."+enemyToTarget.Fullname+" T."+enemyToTarget.Toughness+" S."+ enemyToTarget.Strength);
                    }
                    else
                    {
                        goToPosition = new Point(enemyToTarget.X, enemyToTarget.Y);
                        Speed.X = goToPosition.X - X;
                        Speed.Y = goToPosition.Y - Y;
                        Move();
                    }
                }
                else
                {
                    int distance = Math.Abs(Convert.ToInt32(goToPosition.X) - Convert.ToInt32(X)) + Math.Abs(Convert.ToInt32(goToPosition.Y) - Convert.ToInt32(Y));
                    if (distance > 2)
                    {
                        goToPosition = pointOfAction;
                        Speed.X = goToPosition.X - X;
                        Speed.Y = goToPosition.Y - Y;
                        Move();
                    }
                }
            }
            foreach(Ant enemy in EnemiesAroundMe())
            {
                SoldierAnt.PointAnEnemy(enemy,2);
            }
        }*/
    }
}
