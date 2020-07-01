using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Krohonde.RedColony
{
    
    
    public class ScoutAnt : Ant
    {
        [Serializable]
        public class ResourceCustom
        {
            public bool isUsed;
            public bool isFood;
            public Resource resource;
        }
        public Point goToPosition;
        public Resource ressourceLast = null;
        public bool isPickuping = false;
        public static List<ResourceCustom> ressources = new List<ResourceCustom>(0);

        public ScoutAnt(Point location, Point speed, RedColony colony) : base(location, speed, colony)
        {
            GeneratePosition();
        }
        private int tick = 0;
        public override void Live(double deltatime)
        {
            if (tick > 100)
            {
                Resource procheSelonScouts = GoToResource(new Point(X, Y), true, true);
                goToPosition = new Point(procheSelonScouts.Location.X, procheSelonScouts.Location.Y);
            }
            MoveToPosition(deltatime, goToPosition);
            CheckFor();
            tick = tick++;
            
            
            /*List <Ant> fourmis = MyColony.Population;
            foreach(Ant ant in fourmis)
            {
                if(ant.)
            }*/
        }
        public Resource GoToResource(Point position,bool bypasseUse, bool isForFood)
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
        public void CheckFor()
        {
            List<ResourceCustom> currentCheck = new List<ResourceCustom>(0);
            bool isAFood = false;
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
            foreach(ResourceCustom ressourceAChecker in currentCheck)
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
                    Logger.WriteLogFile("Nouvelle resource trouvée !"+ ressourceAChecker.isFood+" X : "+ ressourceAChecker.resource.Value);
                    ressources.Add(ressourceAChecker);
                }
            }
        }
        public void MoveToPosition(double deltatime, Point goPos)
        {
            if(this.Blocked != "non")
            {
                GeneratePosition();
            }
            float limitArrived = 3f;
            bool canMove = true;
            bool arrivedToPosition = ((goPos.X - X) < limitArrived && (goPos.X - X) > -limitArrived) && ((goPos.Y - Y) < limitArrived && (goPos.Y - Y) > -limitArrived);
            Speed.X = goPos.X - X;
            Speed.Y = goPos.Y - Y;
            canMove = !arrivedToPosition && (Energy > 2000);
            if (canMove)
            {
                Move(deltatime);
            }
            if (arrivedToPosition)
            {
                if(ressourceLast != null)
                {
                    DropPheromon(MotherNature.PheromonTypes.Build);
                    Pickup(ressourceLast);
                }
                GeneratePosition();
            }
        }
        public void GeneratePosition()
        {
            Random r = new Random();
            goToPosition = new Point(r.Next(200, 1200), r.Next(150, 600));
        }
    }
}
