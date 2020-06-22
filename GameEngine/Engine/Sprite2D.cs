using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GameEngine.Engine
{
    public class Sprite2D
    {
        public Vector2 Position = null;
        public Vector2 Scale = null;
        public string Directory = "";
        public string Tag = "";
        public Bitmap Sprite = null;

        public Sprite2D(Vector2 Position, Vector2 Scale, string Directory, string Tag)
        {
            this.Position = Position;
            this.Scale = Scale;
            this.Directory = Directory;
            this.Tag = Tag;
            Image tmp = Image.FromFile($"Assets/Sprites/{Directory}.png");
            Sprite = new Bitmap(tmp);

            Log.Info($"[SHAPE2D] ({Tag}) has been registred");
            GameEngine.RegisterSprite(this);
        }

        public void DestroySelf()
        {
            GameEngine.UnRegisterSprite(this);
        }
    }
}
