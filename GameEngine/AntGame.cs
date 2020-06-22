using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Engine;
using System.Drawing;
using System.Windows.Forms;

namespace GameEngine
{
    class AntGame : Engine.GameEngine
    {
        Sprite2D player;
        bool up;
        bool down;
        bool left;
        bool right;
        public AntGame() : base(new Vector2(600,500),"Ant Game") { }

        public override void OnLoad()
        {
            BackgroundColor = Color.White;
            player = new Sprite2D(new Vector2(10, 10), new Vector2(30, 30), global::GameEngine.Properties.Resources.ant, "Player");
        }
        public override void OnDraw()
        {
            

        }


        public override void OnUpdate()
        {
            CameraPosition.X+=.1f;
            CameraPosition.Y += .1f;
            CameraAngle += .1f;

        }

        public override void GetKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) { up = true; }
            if (e.KeyCode == Keys.Down) { down = true; }
            if (e.KeyCode == Keys.Left) { left = true; }
            if (e.KeyCode == Keys.Right) { right = true; }

        }

        public override void GetKeyUp(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) { up = false; }
            if (e.KeyCode == Keys.Down) { down = false; }
            if (e.KeyCode == Keys.Left) { left = false; }
            if (e.KeyCode == Keys.Right) { right = false; }

        }
    }
}
