using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter
{
    // Klass för alla fiender
    abstract class Enemy : physicalObject
    {
        
        // Konstruktor för enemy:
        public Enemy(Texture2D texture, float X, float Y, float speedX, float speedY) : base(texture, X, Y, speedX, speedY) 
        { 
        }

        // Uppdaterar fiendens position.
        public abstract void Update(GameWindow window, GameTime gameTime);
        // Poäng som spelaren får när den dödar fienden
        public virtual int Points => 1;
    }
    // Fiende som rör sig från sida till sida
    class Mine : Enemy
    {
        public Mine(Texture2D texture, float X, float Y) : base(texture, X, Y, 6f, 0.3f)
        {
        }
        public override void Update(GameWindow window, GameTime gameTime) {
            vector.X += speed.X;

            if (vector.X > window.ClientBounds.Width - texture.Width || vector.X < 0)
            {
                speed.X *= -1; // Byter håll
            }
            vector.Y += speed.Y;
            // Dödar fienden när den åker ut i nederkant
            if (vector.Y > window.ClientBounds.Height)
            {
                isAlive = false;
            }
        }
    }

    // Fiende som faller ner från himlen.
    class Astroid : Enemy
    {
        public Astroid(Texture2D texture, float X, float Y) : base(texture, X, Y, 0f, 0.3f)
        {
            Life = 2; // Asteroider tar två skott att döda
        }
        public override void Update(GameWindow window, GameTime gameTime) { 
            vector.Y += speed.Y;
            // Dödar fienden när den åker ut i nederkant
            if (vector.Y > window.ClientBounds.Height)
            {
                isAlive = false;
            }
        }
    }

    // Fiende som följer efter spelaren
    class Tripod : Enemy
    {
        float chaseSpeed;

        public override int Points => 2; // Tripods är värda mer poäng än vanliga fiender eftersom de är farligare

        public Tripod(Texture2D texture, float X, float Y) : base(texture, X, Y, 0f, 0f)
        {
            chaseSpeed = 1.1f;
        }

        public override void Update(GameWindow window, GameTime gameTime)
        {
            // Hämta spelaren
            Player player = GameElements.Player;

            // Riktning mot spelaren
            Microsoft.Xna.Framework.Vector2 direction =
                new Microsoft.Xna.Framework.Vector2(player.X - vector.X, player.Y - vector.Y);

            if (direction != Microsoft.Xna.Framework.Vector2.Zero)
                direction.Normalize();

            // Rör sig mot spelaren
            vector += direction * chaseSpeed;

            // Dödar fienden när den åker ut i nederkant
            if (vector.Y > window.ClientBounds.Height)
            {
                isAlive = false;
            }
        }
    }

    // Boss klass som skjuter mot spelaren i bursts
    class Boss : Enemy
    {
        double lastBurstTime = 0;
        public virtual double BurstCooldown => 2000; // Tid mellan varje burst i ms

        double burstStartTime = 0;
        public virtual double BurstDuration => 4000; // Hur länge varje burst varar i ms

        double lastShotTime = 0;
        double fireRate = 150; // Tid mellan varje skott under en burst i ms

        bool bursting = false;

        Random random = new Random();
        int rndX = 0;

        Texture2D bulletTexture;

        public Boss(Texture2D texture, Texture2D bulletTexture, float X, float Y) : base(texture, X, Y, 0f, 0f)
        {
            this.bulletTexture = bulletTexture;
        }

        public override void Update(GameWindow window, GameTime gameTime)
        {
            double time = gameTime.TotalGameTime.TotalMilliseconds;

            // start burst
            if (!bursting && time > lastBurstTime + BurstCooldown)
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
            if (bursting && time > burstStartTime + BurstDuration)
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

    // Två olika bossar som ärver från Boss-klassen
    class Boss1 : Boss
    {
        public override int Points => 50;

        public Boss1(Texture2D texture, Texture2D bulletTexture, float X, float Y) : base(texture, bulletTexture, X, Y)
        {
            Life = 70;
        }
    }

    class Boss2 : Boss
    {
        public override double BurstCooldown => 0;
        public override double BurstDuration => 2000;
        public override int Points => 100;

        public Boss2(Texture2D texture, Texture2D bulletTexture, float X, float Y) : base(texture, bulletTexture, X, Y)
        {
            Life = 150;
        }
    }

    // Fiende som skjuter nedåt mot spelaren (bossens skott)
    class EnemyBullet : Enemy
    {
        public override int Points => 0;
        public EnemyBullet(Texture2D texture, float X, float Y) : base(texture, X, Y, 0f, 7f)
        {
            Life = 100;
        }

        public override void Update(GameWindow window, GameTime gameTime)
        {
            vector.Y += speed.Y;

            // Dödar fienden när den åker ut i nederkant
            if (vector.Y > window.ClientBounds.Height)
            {
                isAlive = false;
            }
        }
    }

}
