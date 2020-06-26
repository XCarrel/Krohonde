using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Engine;
using System.Drawing;
using System.Windows.Forms;
using System.Windows;
using Krohonde;
using Krohonde.RedColony;
using Krohonde.GreenColony;


namespace GameEngine
{
    class AntGame : Engine.GameEngine
    {

        private const int FOOD_CLUSTERS = 8;
        private const int FOOD_CLUSTER_SIZE = 40;
        private const int BRICK_CLUSTERS = 6;
        private const int BRICK_CLUSTER_SIZE = 40;
        private const int NB_ROCKS = 10;

        private const int MAX_ROCK_WIDTH = 100;
        private const int MIN_ROCK_WIDTH = 10;
        private const int MAX_ROCK_HEIGHT = 100;
        private const int MIN_ROCK_HEIGHT = 10;
        private const int CLEAR_ZONE_RADIUS = 200; // Empty zone around anthills
        private const int ANT_SIGHT_RANGE = 100; // How far ants can see
        private const int ANT_SMELL_RANGE = 1000; // How far ants can smell a full-intensity pheromon

        public static Random alea;
        public const int MAX_ENERGY = 30000; // of an ant 
        public const int PHEROMON_LIFE_DURATION = 30; // seconds
        public const int COST_OF_DROPPING_PHEROMON = 30; // units of energy
        public const int COST_OF_LOOKING_AROUND = 10; // units of energy
        public const int COST_OF_SMELLING_AROUND = 20; // units of energy

        public enum PheromonTypes { Food, Danger, Build }

        private List<Colony> colonies;
        private List<FoodCluster> food;
        private List<BrickCluster> bricks;
        private List<Rock> rocks;
        private List<Pheromon> pheromons;
        private IMotherNature myWorld;

        private  int width;
        private  int height;
        private TimeSpan lastupdate;
        

        public AntGame() : base(new Vector2(600,500),"Ant Game",new Background(global::GameEngine.Properties.Resources.grass,"Grass")) 
        {

           
        }

        private void Drawbackground()
        {

        }
        public override void OnLoad()
        {
            this.Window.BackgroundImage = this.BackImage.BackgroundIamge;
        }
        public override void OnDraw()
        {
            

        }
        public static Image RotateImage(Image img, float rotationAngle)
        {
            //create an empty Bitmap image
            Bitmap bmp = new Bitmap(img.Width, img.Height);

            //turn the Bitmap into a Graphics object
            Graphics gfx = Graphics.FromImage(bmp);

            //now we set the rotation point to the center of our image
            gfx.TranslateTransform((float)bmp.Width / 2, (float)bmp.Height / 2);

            //now rotate the image
            gfx.RotateTransform(rotationAngle);

            gfx.TranslateTransform(-(float)bmp.Width / 2, -(float)bmp.Height / 2);

            //now draw our new image onto the graphics object
            gfx.DrawImage(img, 0, 0, img.Width, img.Height);

            //dispose of our Graphics object
            gfx.Dispose();

            //return the image
            return bmp;
        }

        public override void OnUpdate()
        {
            myWorld.Live(); // update
        }

        public override void GetKeyDown(KeyEventArgs e)
        {
           // if (e.KeyCode == Keys.Up) { up = true; }
            //if (e.KeyCode == Keys.Down) { down = true; }
            //if (e.KeyCode == Keys.Left) { left = true; }
            //if (e.KeyCode == Keys.Right) { right = true; }

        }

        public override void GetKeyUp(KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Up) { up = false; }
            //if (e.KeyCode == Keys.Down) { down = false; }
            //if (e.KeyCode == Keys.Left) { left = false; }
            //if (e.KeyCode == Keys.Right) { right = false; }

        }
        
        public override void Renderer(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            bool showOrigin = true;
          
                // Colonies
                foreach (Colony colony in myWorld.Colonies())
                {
                    foreach (Ant ant in colony.Population)
                    {
                        Image sourceImage;
                        switch (ant.GetType().Name)
                        {
                            case "WorkerAnt":
                                sourceImage = global::GameEngine.Properties.Resources.worker;
                                break;
                            case "FarmerAnt":
                                sourceImage = global::GameEngine.Properties.Resources.farmer;
                                break;
                            case "SoldierAnt":
                                sourceImage = global::GameEngine.Properties.Resources.soldier;
                                break;
                            case "ScoutAnt":
                                sourceImage = global::GameEngine.Properties.Resources.scout;
                                break;
                            default:
                                sourceImage = global::GameEngine.Properties.Resources.worker;
                                break;
                        }
                        sourceImage = RotateImage(sourceImage, ant.Heading + 90);
                        if (showOrigin) graphics.DrawLine(new Pen(colony.Color, 6), new System.Drawing.Point((int)ant.X, (int)ant.Y), new System.Drawing.Point((int)(ant.X + 24 * ant.Energy / MotherNature.MAX_ENERGY), (int)ant.Y));
                        graphics.DrawImage(sourceImage, (int)ant.X, (int)ant.Y, sourceImage.Width, sourceImage.Height);
                    }
                    graphics.FillClosedCurve(new TextureBrush(Properties.Resources.anthill), colony.Hill);
                    graphics.FillEllipse(new SolidBrush(colony.Color), (float)colony.Location.X - 15, (float)colony.Location.Y - 15, 30, 30); // Colony "flag"
                    graphics.DrawPolygon(new Pen(Color.Black), colony.Hill);
                }

                // Food
                foreach (FoodCluster fc in myWorld.FoodStock())
                {
                    graphics.DrawCurve(new Pen(new TextureBrush(Properties.Resources.pollen), 5), fc.Content.Select(x => x.Location).ToArray());
                }
                // Bricks
                foreach (BrickCluster bc in myWorld.BrickStock())
                {
                    graphics.DrawCurve(new Pen(new TextureBrush(Properties.Resources.brick), 5), bc.Content.Select(x => x.Location).ToArray());
                }

                // Rocks
                foreach (Rock r in myWorld.Rocks())
                {
                    graphics.FillPolygon(new TextureBrush(Properties.Resources.rock), r.Shape);
                    graphics.DrawPolygon(new Pen(Color.Black, 2), r.Shape);
                }

                // Pheromons
                foreach (Pheromon phero in myWorld.Pheromons())
                {
                    Image img;
                    switch (phero.PheromonType)
                    {
                        case MotherNature.PheromonTypes.Build: img = global::GameEngine.Properties.Resources.pherobuild; break;
                        case MotherNature.PheromonTypes.Food: img = global::GameEngine.Properties.Resources.pherofood; break;
                        case MotherNature.PheromonTypes.Danger: img = global::GameEngine.Properties.Resources.pherodanger; break;
                        default: img = global::GameEngine.Properties.Resources.pherodanger; break;
                    }
                    graphics.DrawImage(img, (int)phero.Location.X, (int)phero.Location.Y, img.Width / 2, img.Height / 2);
                    if (showOrigin) graphics.DrawLine(new Pen(phero.Colony.Color, 6), new System.Drawing.Point((int)phero.Location.X, (int)phero.Location.Y), new System.Drawing.Point((int)(phero.Location.X + 24 * phero.Intensity), (int)phero.Location.Y));
                }
            
        }

        public override void OnceLoad()
        {
            if (!this.AllObjReady)
            {
                this.width = 2400;
                this.height = 1800;
                myWorld = new MotherNature(this.width, this.height);
                RedColony rcolo = new RedColony(new System.Windows.Point(400, 200), myWorld);
                rcolo.Spawn(40);
                myWorld.AddColony(rcolo);
                GreenColony gcolo = new GreenColony(new System.Windows.Point(1200, 600), myWorld);
                gcolo.Spawn(40);
                myWorld.AddColony(gcolo);
                myWorld.Initialize();
                colonies = new List<Colony>();
                food = new List<FoodCluster>();
                bricks = new List<BrickCluster>();
                rocks = new List<Rock>();
                pheromons = new List<Pheromon>();
                alea = new Random();
                this.AllObjReady = true;
            }
        }

        public override void InitWindwosComponent()
        {
            Window.World = new System.Windows.Forms.PictureBox();
           ((System.ComponentModel.ISupportInitialize)(Window.World)).BeginInit();
            Window.SuspendLayout();

            Window.BackColor = System.Drawing.Color.White;
            Window.World.BackgroundImage = global::GameEngine.Properties.Resources.grass;
            Window.World.Location = new System.Drawing.Point(0, 0);
            Window.World.Name = "World";
            Window.World.Size = new System.Drawing.Size(1800, 1000);
            //Window.World.TabIndex = 0;
            //Window.World.TabStop = false;
            Window.World.Paint += new System.Windows.Forms.PaintEventHandler(Renderer);


            Window.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            Window.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Window.AutoScroll = true;
            Window.ClientSize = new System.Drawing.Size(1218, 595);
            Window.Controls.Add(Window.World);
            Window.Name = "World";
            Window.Text = "Krohonde";
            Window.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(Window.World)).EndInit();
            Window.ResumeLayout(false);
            Window.PerformLayout();
        }
    }
}
