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
        public virtual int Points => 1;

        protected void FacePlayer()
        {
            Player player = GameElements.Player;

            Microsoft.Xna.Framework.Vector2 direction =
                new Microsoft.Xna.Framework.Vector2(player.X - vector.X, player.Y - vector.Y);

            rotation = (float)Math.Atan2(direction.Y, direction.X) + MathHelper.PiOver2;
        }
        
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
    class Astroid : Enemy
    {
        public Astroid(Texture2D texture, float X, float Y) : base(texture, X, Y, 0f, 3f)
        {
            Life = 2;
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

    class Tripod : Enemy
    {
        float chaseSpeed;

        public override int Points => 2;

        public Tripod(Texture2D texture, float X, float Y) : base(texture, X, Y, 0f, 3f)
        {
            chaseSpeed = 1.1f;
        }

        public override void Update(GameWindow window)
        {
            // Hämta spelaren
            Player player = GameElements.Player;

            // riktning mot spelaren
            Microsoft.Xna.Framework.Vector2 direction =
                new Microsoft.Xna.Framework.Vector2(player.X - vector.X, player.Y - vector.Y);

            if (direction != Microsoft.Xna.Framework.Vector2.Zero)
                direction.Normalize();

            // rör sig mot spelaren
            vector += direction * chaseSpeed;

            //FacePlayer();

            // döda om den åker utanför skärmen
            if (vector.Y > window.ClientBounds.Height ||
                vector.X < -texture.Width ||
                vector.X > window.ClientBounds.Width)
            {
                isAlive = false;
            }
        }
    }

}
