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
     * Version : 3.6 (01.07.2020) - phase 1
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
        }
        /*Variables globales*/
        public Point goToPosition;
        public Resource ressourceLast = null;
        public bool isPickuping = false;
        public static List<ResourceCustom> ressources = new List<ResourceCustom>(0);

        public ScoutAnt(Point location, Point speed, RedColony colony) : base(location, speed, colony)//Setup
        {
            GeneratePosition();//Génération d'une destination aléatoire
        }
        public override void Live()//Update
        {
            MoveToPosition(MotherNature.LastFrameDuration, goToPosition);//Avance d'un tick en direction de la destination
            CheckFor();
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
                currentCheck.Add(ressourceToAdd);
            }
            foreach (Food foodProche in FoodAroundMe())
            {
                ResourceCustom ressourceToAdd = new ResourceCustom();
                ressourceToAdd.isFood = true;
                ressourceToAdd.isUsed = false;
                ressourceToAdd.resource = foodProche;
                currentCheck.Add(ressourceToAdd);
            }
           
            foreach(ResourceCustom ressourceAChecker in currentCheck)//Ajoute seulement les nouvelles entrées
            {
                bool isAlreadyFounded = false;
                foreach (ResourceCustom ressourceChecker in ressources)
                {
                    if(ressourceChecker.resource == ressourceAChecker.resource)
                    {
                        isAlreadyFounded = true;
                    }
                }
                if (!isAlreadyFounded)
                {
                    Logger.WriteLogFile("RED COLONY : Nouvelle resource trouvée !"+ ressourceAChecker.isFood+" X : "+ ressourceAChecker.resource.Location.X+" Y : "+ ressourceAChecker.resource.Location.Y);
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
