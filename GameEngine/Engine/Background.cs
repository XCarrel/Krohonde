using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GameEngine.Engine
{
    public class Background
    {
        public string Tag = "";
        public Bitmap BackgroundIamge = null;

        public Background(Image source, string Tag)
        {
            this.Tag = Tag;
            BackgroundIamge = new Bitmap(source);

            Log.Info($"[Background] ({Tag}) has been registred");
           // GameEngine.RegisterSprite(this);
        }

        public void DestroySelf()
        {
          //  GameEngine.UnRegisterSprite(this);
            BackgroundIamge = null;
        }
    }
}
