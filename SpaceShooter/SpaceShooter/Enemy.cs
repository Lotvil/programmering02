using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter
{
    //Klass för fiender
    abstract class Enemy : physicalObject
    {
        
        //Konstruktor för enemy:

        public Enemy(Texture2D texture, float X, float Y, float speedX, float speedY) : base(texture, X, Y, 6f, 0.3f) 
        { 
        }

        //Update(), uppdaterar fiendens position.

        public abstract void Update(GameWindow window);
        
}
    // mina som studsar från sida till sida
    class Mine : Enemy
    {
        public Mine(Texture2D texture, float X, float Y) : base(texture, X, Y, 6f, 0.3f)
        {
        }
        public override void Update(GameWindow window) {
            vector.X += speed.X;

            if (vector.X > window.ClientBounds.Width - texture.Width || vector.X < 0)
            {
                speed.X *= -1; // byt riktning
            }
            vector.Y += speed.Y;
            //dödar fienden när den åker ut nere
            if (vector.Y > window.ClientBounds.Height)
            {
                isAlive = false;
            }
        }
    }

    //tripod, elak fiende som kör i full kareta mot dig.
    class Tripod : Enemy
    {
        public Tripod(Texture2D texture, float X, float Y) : base(texture, X, Y, 0f, 3f)
        {
        }
        public override void Update(GameWindow window) { 
            vector.Y += speed.Y;
            //dödar fienden när den åker ut nere
            if (vector.Y > window.ClientBounds.Height)
            {
                isAlive = false;
            }
        }
    }

}
