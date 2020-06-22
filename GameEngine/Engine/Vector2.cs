using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Engine
{
    public class Vector2
    {
        public float X { get; set; }
        public float Y { get; set; }

        public Vector2()
        {
            X = Zero().X;
            Y = Zero().Y;
        }

        public Vector2( float X, float Y)
        {
            this.X = X;
            this.Y = Y;
        }
        /// <summary>
        /// Init the point to coordonate <0,0>
        /// </summary>
        /// <returns>A Vector2 at pos. <0,0> </returns>
        public static Vector2 Zero()
        {
            return new Vector2(0,0);
        }
    }


}
