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

        public Enemy(Texture2D texture, float X, float Y, float speedX, float speedY) : base(texture, X, Y, speedX, speedY) 
        { 
        }

        //Update(), uppdaterar fiendens position.

        public abstract void Update(GameWindow window, GameTime gameTime);
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
        public override void Update(GameWindow window, GameTime gameTime) {
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
        public Astroid(Texture2D texture, float X, float Y) : base(texture, X, Y, 0f, 0.3f)
        {
            Life = 2;
        }
        public override void Update(GameWindow window, GameTime gameTime) { 
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

        public Tripod(Texture2D texture, float X, float Y) : base(texture, X, Y, 0f, 0f)
        {
            chaseSpeed = 1.1f;
        }

        public override void Update(GameWindow window, GameTime gameTime)
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
            if (vector.Y > window.ClientBounds.Height)
            {
                isAlive = false;
            }
        }
    }

    class Boss : Enemy
    {
        double lastBurstTime = 0;
        double burstCooldown = 2000; // time between bursts

        double burstStartTime = 0;
        double burstDuration = 4000;

        double lastShotTime = 0;
        double fireRate = 150;

        bool bursting = false;

        Random random = new Random();
        int rndX = 0;

        Texture2D bulletTexture;
        public override int Points => 50;

        public Boss(Texture2D texture, Texture2D bulletTexture, float X, float Y) : base(texture, X, Y, 0f, 0f)
        {
            Life = 70;
            this.bulletTexture = bulletTexture;
        }

        public override void Update(GameWindow window, GameTime gameTime)
        {
            double time = gameTime.TotalGameTime.TotalMilliseconds;

            // start burst
            if (!bursting && time > lastBurstTime + burstCooldown)
            {
                bursting = true;
                burstStartTime = time;
                lastBurstTime = time;
                rndX = random.Next(
                    (int)vector.X,
                    (int)(vector.X + texture.Width)
                );
            }

            // stop burst
            if (bursting && time > burstStartTime + burstDuration)
            {
                bursting = false;
            }

            // fire bullets during burst
            if (bursting && time > lastShotTime + fireRate)
            {
                GameElements.Enemies.Add(
                    new EnemyBullet(
                        bulletTexture,
                        rndX,
                        vector.Y + texture.Height
                    )
                );

                lastShotTime = time;
            }
        }
    }

    class EnemyBullet : Enemy
    {
        public override int Points => 0;
        public EnemyBullet(Texture2D texture, float X, float Y) : base(texture, X, Y, 0f, 6f)
        {
            Life = 100;
        }

        public override void Update(GameWindow window, GameTime gameTime)
        {
            vector.Y += speed.Y;

            // döda om den åker utanför skärmen
            if (vector.Y > window.ClientBounds.Height)
            {
                isAlive = false;
            }
        }
    }

}
