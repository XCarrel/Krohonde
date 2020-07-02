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
        public static List<EnemyListedActu> enemyRepered;
        public Point goToPosition;
        public SoldierAnt(Point location, Point speed, RedColony colony) : base(location, speed, colony)
        {
        }
        [Serializable]
        public class EnemyListedActu
        {
            public Ant fourmis;
            public int time;
        }
        public static void PointAnEnemy(Ant ant)
        {
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
                enemyRepered.Add(enemy);
            }
        }
        public override void Live()
        {
            if(enemyRepered != null)
            {
                if (enemyRepered.Count > 0)
                {
                    int distance = Math.Abs(Convert.ToInt32(goToPosition.X) - Convert.ToInt32(X)) + Math.Abs(Convert.ToInt32(goToPosition.Y) - Convert.ToInt32(Y));
                    if (distance < 2)
                    {
                        Hit(enemyRepered[0].fourmis);
                    }
                    else
                    {
                        goToPosition = new Point(enemyRepered[0].fourmis.X, enemyRepered[0].fourmis.Y);
                        Speed.X = goToPosition.X - X;
                        Speed.Y = goToPosition.Y - Y;
                    }
                }
            }
            foreach(Ant enemy in EnemiesAroundMe())
            {
                SoldierAnt.PointAnEnemy(enemy);
            }
            //Move();
        }
    }
}
