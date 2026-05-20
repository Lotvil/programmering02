using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter
{
    // Klass för spelaren, ärver från physicalObject
    class Player : physicalObject
    {
        int points = 0;
        List<Bullet> bullets;
        Texture2D bulletTexture;
        double timeSinceLastBullet = 0; //ms
        bool bulletBoostActive = false;
        double bulletBoostStartTime = 0;
        double bulletBoostDuration = 3000;

        // Konstruktor
        public Player(Texture2D texture, float X, float Y, float speedX, float speedY, Texture2D bulletTexture) : base(texture, X, Y, speedX, speedY)
        {
            bullets = new List<Bullet>();
            this.bulletTexture = bulletTexture;
            life = 3;
        }
        public void Update(GameWindow window, GameTime gameTime)
        {
            // Läs in tangenttryck
            KeyboardState keyboardState = Keyboard.GetState();

            // Rör skeppet med wasd eller pilar och se till att det inte rör på sig ur fönstret

            if (vector.X <= window.ClientBounds.Width - texture.Width && vector.X >= 0)
            {
                if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right))
                {
                    vector.X += speed.X;
                }
                if (keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left))
                {
                    vector.X -= speed.X;
                }
            }

            if (vector.Y <= window.ClientBounds.Height - texture.Height && vector.Y >= 0)
            {
                if (keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.Down))
                {
                    vector.Y += speed.Y;
                }
                if (keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Up))
                {
                    vector.Y -= speed.Y;
                }
            }

            // Se till att spelaren inte rör sig utanför fönstret
            if (vector.X < 0)
            {
                vector.X = 0;
            }
            if (vector.X > window.ClientBounds.Width - texture.Width)
            {
                vector.X = window.ClientBounds.Width - texture.Width;
            }
            if (vector.Y < 0)
            {
                vector.Y = 0;
            }
            if (vector.Y > window.ClientBounds.Height - texture.Height)
            {
                vector.Y = window.ClientBounds.Height - texture.Height;
            }

            if (bulletBoostActive) // Kontrollera om bulletBoost powerupen har gått ut
            {
                if (gameTime.TotalGameTime.TotalMilliseconds > bulletBoostStartTime + bulletBoostDuration)
                {
                    bulletBoostActive = false;
                }
            }
            double fireRate = bulletBoostActive ? 100 : 200; // Aplicera snabbare fire rate och bulletspeed
            float bulletSpeed = bulletBoostActive ? 6f : 3f;

            // Skjut skott när mellanslag trycks ned
            if (keyboardState.IsKeyDown(Keys.Space))
            {
                if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastBullet + fireRate)
                {
                    float bulletX = vector.X + texture.Width / 2 - bulletTexture.Width / 2;

                    // SHIFT = diagonal bullets
                    if (keyboardState.IsKeyDown(Keys.LeftShift) ||
                        keyboardState.IsKeyDown(Keys.RightShift))
                    {
                        bullets.Add(new Bullet(bulletTexture, bulletX, vector.Y, -0.8f*bulletSpeed, 0.8f*bulletSpeed));
                        bullets.Add(new Bullet(bulletTexture, bulletX, vector.Y, 0.8f*bulletSpeed, 0.8f*bulletSpeed));
                    }
                    else
                    {
                        bullets.Add(new Bullet( bulletTexture, bulletX, vector.Y, 0f, bulletSpeed));
                    }

                    timeSinceLastBullet = gameTime.TotalGameTime.TotalMilliseconds;
                }
            }

            // Uppdatera skotten, ta bort skott som inte är levande längre
            foreach (Bullet b in bullets.ToList())
            {
                b.Update(window);

                if (!b.IsAlive) { 
                    bullets.Remove(b);
                }
            }

            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                isAlive = false;
            }

            }

        // Rita ut spelaren och dess skott
        public override void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(texture, vector, Color.White);
            foreach(Bullet b in bullets)
            {
                b.Draw(_spriteBatch);
            }
        }

        public int Points { get { return points; } set { points = value; } }

        public List<Bullet> Bullets { get { return bullets; } }

        // Metod för att återställa spelarens position, hastighet, skott, poäng och liv
        public void Reset(float X, float Y, float speedX, float speedY)
        {
            vector.X = X;
            vector.Y = Y;
            speed.X = speedX;
            speed.Y = speedY;
            bullets.Clear();
            timeSinceLastBullet = 0;
            points = 0;
            isAlive = true;
        }

        public void BulletBoost(GameTime gameTime)
        {
            bulletBoostActive = true;
            bulletBoostStartTime = gameTime.TotalGameTime.TotalMilliseconds;
        }

    }
    // Bullet - klass för skotten som spelaren skjuter, ärver från physicalObject
    class Bullet : physicalObject
    {
        public Bullet(Texture2D texture, float X, float Y, float speedX, float speedY)
            : base(texture, X, Y, speedX, speedY)
        {
        }
        public void SpeedBoost(float newSpeed)
        {
            speed.Y = newSpeed;
        }
        public void Update(GameWindow window) // Flyttar skottet
        {
            vector.X += speed.X;
            vector.Y -= speed.Y;
            if (vector.Y < 0 || vector.X < 0 || vector.X > window.ClientBounds.Width) { 
                isAlive = false;
            }
        }
    }
}
