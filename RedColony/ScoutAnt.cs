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
    
    public class ScoutAnt : Ant
    {
        /*Classe d'une resource custom (Resource standar + food + estUsée) */
        [Serializable]
        public class ResourceCustom
        {
            public bool isUsed;
            public bool isFood;
            public Resource resource;
            public int tickAcurate;
        }
        /*Variables globales*/
        public Point goToPosition;
        public Resource ressourceLast = null;
        public bool isPickuping = false;
        private bool isCharging = false;
        public static List<ResourceCustom> ressources = new List<ResourceCustom>(0);

        public ScoutAnt(Point location, Point speed, RedColony colony) : base(location, speed, colony)//Setup
        {
            GeneratePosition();//Génération d'une destination aléatoire
        }
        private int counter = 0;
        public override void Live()//Update
        {
            counter++;
            if (Energy > 15000)
            {
                isCharging = false;
                MoveToPosition(MotherNature.LastFrameDuration, goToPosition);//Avance d'un tick en direction de la destination
                CheckFor();
            }else 
            if (Energy > 5000 && !isCharging)
            {
                MoveToPosition(MotherNature.LastFrameDuration, goToPosition);//Avance d'un tick en direction de la destination
                if ((counter % 3) == 0)
                {
                    CheckFor();
                }
            }
            else
            {
                isCharging = true;
                Resource red = GoToResource(new Point(X, Y), true, true);
                Point goPos = new Point(red.Location.X, red.Location.Y);
                Speed.X = goPos.X - X;
                Speed.Y = goPos.Y - Y;
                bool arrivedToPosition = ((goPos.X - X) < 2 && (goPos.X - X) > -2) && ((goPos.Y - Y) < 2 && (goPos.Y - Y) > -2);
                bool canMove = !arrivedToPosition && (Energy > 2000);
                if (canMove)
                {
                    Move();
                }
                if (arrivedToPosition)
                {
                    if ((counter % 3) == 0)
                    {
                        Pickup(red);
                    }
                    else
                    {
                        if (!(EatFromBag(1, MotherNature.DigestionFor.Energy)))
                        {
                            DesactivateRessource(red);
                        }
                    }
                }
            }

            foreach (Ant enemy in EnemiesAroundMe())
            {
                SoldierAnt.PointAnEnemy(enemy,1);
            }
        }
        public static void DesactivateRessource(Resource ressourceADelete)//Supprime la resource une fois qu'elle est utilisée
        {
            foreach (ResourceCustom resActif in ressources.ToList())
            {
                if(resActif.resource == ressourceADelete)
                {
                    ressources.Remove(resActif);
                }
            }
        }
        public static Resource GoToResource(Point position,bool bypasseUse, bool isForFood)//Checher la resource la plus proche (selon la position demandée, le bypasse, et la nourriture / brique)
        {
            Resource nearest = null;
            float nearestDistance = 100000;
            foreach (ScoutAnt.ResourceCustom res in ScoutAnt.ressources)
            {
                if (bypasseUse || !res.isUsed)
                {
                    if(res.isFood == isForFood)
                    {
                        int xPos = res.resource.Location.X;
                        int yPos = res.resource.Location.Y;
                        int xMyPos = Convert.ToInt32(position.X);
                        int yMyPos = Convert.ToInt32(position.Y);
                        float distance = Math.Abs(xPos - xMyPos) + Math.Abs(yPos - yMyPos);
                        if(distance < nearestDistance)
                        {
                            nearestDistance = distance;
                            nearest = res.resource;
                        }
                    }
                }
            }
            return nearest;
        }
        public void CheckFor()//Détecte les ressources à côté (briques et nourriture)
        {
            List<ResourceCustom> currentCheck = new List<ResourceCustom>(0);
            foreach(Brick briqueProche in BricksAroundMe())
            {
                ResourceCustom ressourceToAdd = new ResourceCustom();
                ressourceToAdd.isUsed = false;
                ressourceToAdd.isFood = false;
                ressourceToAdd.resource = briqueProche;
                ressourceToAdd.tickAcurate = 100;
                currentCheck.Add(ressourceToAdd);
            }
            foreach (Food foodProche in FoodAroundMe())
            {
                ResourceCustom ressourceToAdd = new ResourceCustom();
                ressourceToAdd.isFood = true;
                ressourceToAdd.isUsed = false;
                ressourceToAdd.resource = foodProche;
                ressourceToAdd.tickAcurate = 100;
                currentCheck.Add(ressourceToAdd);
            }
           
            foreach(ResourceCustom ressourceAChecker in currentCheck)//Ajoute seulement les nouvelles entrées
            {
                bool isAlreadyFounded = false;
                foreach (ResourceCustom ressourceChecker in ressources)
                {
                    if(ressourceChecker.resource == ressourceAChecker.resource)
                    {
                        //Logger.WriteLogFile("RED COLONY : UPDATE TICK TO 120!" + ressourceAChecker.isFood + " X : " + ressourceAChecker.resource.Location.X + " Y : " + ressourceAChecker.resource.Location.Y);
                        ressourceChecker.tickAcurate = 100;
                        isAlreadyFounded = true;
                    }
                }
                if (!isAlreadyFounded)
                {
                    //Logger.WriteLogFile("RED COLONY : Nouvelle resource trouvée !"+ ressourceAChecker.isFood+" X : "+ ressourceAChecker.resource.Location.X+" Y : "+ ressourceAChecker.resource.Location.Y);
                    Logger.WriteLogFile("RED COLONY : Nouvelle resource trouvée !");
                    ressources.Add(ressourceAChecker);
                }
            }
        }
        public void MoveToPosition(double deltatime, Point goPos)//Avance d'un tick en direction de la destination
        {
            if(BlockedBy != null)
            {
                GeneratePosition();//Génération d'une destination aléatoire
            }
            float limitArrived = 3f;
            bool canMove = true;
            bool arrivedToPosition = ((goPos.X - X) < limitArrived && (goPos.X - X) > -limitArrived) && ((goPos.Y - Y) < limitArrived && (goPos.Y - Y) > -limitArrived);
            Speed.X = goPos.X - X;
            Speed.Y = goPos.Y - Y;
            canMove = !arrivedToPosition && (Energy > 2000);
            if (canMove)
            {
                Move();
            }
            if (arrivedToPosition)
            {
                if(ressourceLast != null)
                {
                    DropPheromon(MotherNature.PheromonTypes.Build);
                    Pickup(ressourceLast);
                }
                GeneratePosition();//Génération d'une destination aléatoire
            }
        }
        public void GeneratePosition()//Génération d'une destination aléatoire
        {
            Random r = new Random();
            goToPosition = new Point(r.Next(100, 1600), r.Next(80, 800));
        }
    }
}
