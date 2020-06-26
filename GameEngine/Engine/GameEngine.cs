using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.CompilerServices;

namespace GameEngine.Engine
{
    public class Canvas : Form
    {
        public Background ImageBackground;
        public System.Windows.Forms.PictureBox World;
        public Canvas()
        {
            this.DoubleBuffered = true;
            this.AutoScroll = true;
        }
    }

    public class MultiFormContext : ApplicationContext
    {
        private int openForms;
        public MultiFormContext(params Form[] forms)
        {
            openForms = forms.Length;

            foreach (var form in forms)
            {
                form.FormClosed += (s, args) =>
                {
                    //When we have closed the last of the "starting" forms, 
                    //end the program.
                    if (Interlocked.Decrement(ref openForms) == 0)
                        ExitThread();
                };

                form.Show();
            }
        }
    }

    public abstract class GameEngine
    {

        private Vector2 ScreenSize = new Vector2(512,512);
        private string Title= "New Game";
        private Thread GameLoopThread = null;
        private int delay;
        private static List<Shape2D> AllShapes = new List<Shape2D>();
        private static List<Sprite2D> AllSprites = new List<Sprite2D>();


        public Background BackImage = null;
        public Canvas Window = null;
        public Canvas LogWindow = null;
        public Color BackgroundColor = Color.Beige;
        public Vector2 CameraPosition = Vector2.Zero();
        public float CameraAngle = 0f;
        public bool AllObjReady ;
        public GameEngine(Vector2 ScrennSize, string Title, Background TheBackground)
        {
            Log.Info("Game is starting");
            this.ScreenSize = ScrennSize;
            this.Title = Title;
            this.BackImage = TheBackground;
            this.AllObjReady = false;
            Window = new Canvas();
            InitWindwosComponent();
            //Window.Size = new Size((int)this.ScreenSize.X, (int)this.ScreenSize.Y);
            //Window.Text = this.Title;

            // Window.BackgroundImage = this.BackImage.BackgroundIamge;
            LogWindow = new Canvas();
            LogWindow.Size = new Size(400, 400);
            LogWindow.Text = "Log";
            OnceLoad();
            GameLoopThread = new Thread(GameLoop);
            GameLoopThread.Start();
            delay = 250;
            //Application.Run(Window);
            Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(Window);
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            // Application.Run(new MultiFormContext( Window, LogWindow));
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            GetKeyUp(e);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            GetKeyDown(e);
        }

        public static void RegisterShap(Shape2D Shap)
        {
            AllShapes.Add(Shap);
        }
        public static void UnRegisterShap(Shape2D Shap)
        {
            AllShapes.Remove(Shap);
        }

        public static void RegisterSprite(Sprite2D Sprite)
        {
            AllSprites.Add(Sprite);
        }

        public static void UnRegisterSprite(Sprite2D Sprite)
        {
            AllSprites.Remove(Sprite);
        }

        void GameLoop()
        {
            OnLoad();
            while (GameLoopThread.IsAlive)
            {
                try
                {
                    OnDraw();
                    //Window.BeginInvoke((MethodInvoker)delegate { Window.World.Refresh(); });
                    Window.World.Invalidate();
                    OnUpdate();
                    Thread.Sleep(delay);
                    //delay = 500;
                }
                catch
                {
                    Log.Error("Game has not been found, waiting ...");
                }
            }
       

        }

        public abstract void InitWindwosComponent();
        public abstract void Renderer(object sender, PaintEventArgs e);
        public abstract void OnceLoad();

        public abstract void OnLoad();
        /// <summary>
        /// OnUpdate is use to update physics (Movement, etc...)
        /// </summary>
        public abstract void OnUpdate();
        /// <summary>
        /// OnDraw method is use to update sprites
        /// </summary>
        public abstract void OnDraw();

        public abstract void GetKeyDown(KeyEventArgs e);
        public abstract void GetKeyUp(KeyEventArgs e);
    }
}
