using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace GameEngine.Engine
{
    class Canvas : Form
    {
        public Canvas()
        {
            this.DoubleBuffered = true;
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
        private Canvas Window = null;
        private Canvas LogWindow = null;
        private Thread GameLoopThread = null;
        private static List<Shape2D> AllShapes = new List<Shape2D>();
        private static List<Sprite2D> AllSprites = new List<Sprite2D>();



        public Color BackgroundColor = Color.Beige;
        public Vector2 CameraPosition = Vector2.Zero();
        public float CameraAngle = 0f;
        public GameEngine(Vector2 ScrennSize, string Title)
        {
            Log.Info("Game is starting");
            this.ScreenSize = ScrennSize;
            this.Title = Title;
            Window = new Canvas();
            Window.Size = new Size((int)this.ScreenSize.X, (int)this.ScreenSize.Y);
            Window.Text = this.Title;
            Window.Paint += Renderer;
            Window.KeyDown += Window_KeyDown;
            Window.KeyUp += Window_KeyUp;

            LogWindow = new Canvas();
            LogWindow.Size = new Size(400, 400);
            LogWindow.Text = "Log";

            GameLoopThread = new Thread(GameLoop);
            GameLoopThread.Start();

            //Application.Run(Window);
            Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MultiFormContext( Window, LogWindow));
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
                    Window.BeginInvoke((MethodInvoker)delegate { Window.Refresh(); });
                    OnUpdate();
                    Thread.Sleep(1);
                }
                catch
                {
                    Log.Error("Game has not been found, waiting ...");
                }
            }

        }
        private void Renderer(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(BackgroundColor);

            g.TranslateTransform(CameraPosition.X, CameraPosition.Y);
            g.RotateTransform(CameraAngle);

            foreach (Shape2D shap in AllShapes)
            {
                g.FillRectangle(new SolidBrush(Color.Red), shap.Position.X, shap.Position.Y, shap.Scale.X, shap.Scale.Y);
            }

            foreach (Sprite2D sprite in AllSprites)
            {
                g.DrawImage(sprite.Sprite, sprite.Position.X, sprite.Position.Y, sprite.Scale.X, sprite.Scale.Y);
            }
        }

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
