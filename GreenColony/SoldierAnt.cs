using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Krohonde.GreenColony
{
    public class SoldierAnt : Ant
    {
        Ant followAnt; // La fourmis qu'on escorte
        public SoldierAnt(Point location, Point speed, GreenColony colony) : base(location, speed, colony)
        {

        }

        public override void Live()
        {
            if (followAnt == null) // pas de fourmis a protégé ---> chercher une
            {
                foreach (Ant ant in MyColony.Population)
                {
                    if (ant.GetType().Name == "FarmerAnt")
                    {
                        FarmerAnt fant = ant as FarmerAnt;
                        if (!fant.Protected)
                        {
                            followAnt = ant; // Suivre la fourmis
                            fant.Protected = true;
                            break;
                        }
                    }

                }
                if (followAnt == null) // pas trouver ---> retour base repos
                {
                    MoveToward(new System.Drawing.Point((int)MyColony.Location.X, (int)MyColony.Location.Y));
                }
            }
            else
            {
                MoveToward(followAnt.SDLocation);
            }


            List<Ant> Ennemies = EnemiesAroundMe();
            foreach (Ant Ennemy in Ennemies)
            {
                if (Ennemy.GetType().Name == "SoldierAnt")
                {
                    if (Helpers.Distance(Ennemy.SDLocation, SDLocation) <= HIT_REACH)
                    {
                        Hit(Ennemy);
                    }
                    else
                    {
                        MoveToward(Ennemy.SDLocation);
                    }
                }
            }

            /*List<Pheromon> ListPheromon = SmellsAroundMe();
            
            foreach (Pheromon AllPheromon in ListPheromon)
            {

            }

           
            while (MotherNature.PheromonTypes.Danger >= 1))
            {
                
            }*/
            Move();
        }
        private void MoveToward(System.Drawing.Point targetLocation)
        {
            //calculates the distance
            var distanceToX = targetLocation.X - X;
            var distanceToY = targetLocation.Y - Y;
            //makes distances positive values
            if (distanceToX < 0) distanceToX *= -1;
            if (distanceToY < 0) distanceToY *= -1;

            //adjust signs
            if (X <= targetLocation.X)
            {
                Speed.X = distanceToX;
            }
            else
            {
                Speed.X = -distanceToX;
            }

            if (Y <= targetLocation.Y)
            {
                Speed.Y = +distanceToY;
            }
            else
            {
                Speed.Y = -distanceToY;
            }
        }
    }
}
