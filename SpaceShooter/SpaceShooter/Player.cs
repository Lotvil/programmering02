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
    // Klassen player är till för att skapa ett spelarobjekt, som hanterar spelarens karaktär och ta emot tangenttryck.
    class Player : physicalObject
    {
        int points = 0;
        List<Bullet> bullets;
        Texture2D bulletTexture;
        double timeSinceLastBullet = 0; //ms
        bool bulletBoostActive = false;
        double bulletBoostStartTime = 0;
        double bulletBoostDuration = 3000;

        //KOnstruktor för spelarobjektet
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

            // är skeppet utanför fönstret återställs positionen till kanten av fönstret. (skeppet ser bara ut att stanna vid kanten)
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

            if (bulletBoostActive)
            {
                if (gameTime.TotalGameTime.TotalMilliseconds > bulletBoostStartTime + bulletBoostDuration)
                {
                    bulletBoostActive = false;
                }
            }
            double fireRate = bulletBoostActive ? 100 : 200;
            float bulletSpeed = bulletBoostActive ? 6f : 3f;

             // Kontrollera om bullet boost-effekten har gått ut

            if (keyboardState.IsKeyDown(Keys.Space)) {
                //Kontrollera om spelaren får skjuta:
                if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastBullet + fireRate)
                {
                    //skapa skott
                    Bullet temp = new Bullet(bulletTexture, vector.X + texture.Width / 2 - bulletTexture.Width / 2, vector.Y);

                    temp.SpeedBoost(bulletSpeed);

                    //lägg till skott i listan
                    bullets.Add(temp);

                    timeSinceLastBullet = gameTime.TotalGameTime.TotalMilliseconds; // Sätt tiden för senaste skottet till nuvarande tid
                }
            }

            //för alla skott
            foreach (Bullet b in bullets.ToList())
            {
                b.Update(); //flytta skottet

                //är skottet dött
                if (!b.IsAlive) { 
                    bullets.Remove(b); //tas det bort
                }
            }

            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                isAlive = false;
            }

            }

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

        public void Reset(float X, float Y, float speedX, float speedY)
        {
            //återställ position och hastighet
            vector.X = X;
            vector.Y = Y;
            speed.X = speedX;
            speed.Y = speedY;
            //återställ skott
            bullets.Clear();
            timeSinceLastBullet = 0;
            //återställ poäng
            points = 0;
            //gör så spelaren lever igen
            isAlive = true;
        }

        public void BulletBoost(GameTime gameTime)
        {
            bulletBoostActive = true;
            bulletBoostStartTime = gameTime.TotalGameTime.TotalMilliseconds;
        }

    }
    class Bullet : physicalObject
    {
        //konstruktor
        public Bullet(Texture2D texture, float X, float Y) : base(texture, X, Y, 0, 3f)
        {
        }
        public void SpeedBoost(float newSpeed)
        {
            speed.Y = newSpeed;
        }
        public void Update()
        {
            vector.Y -= speed.Y;
            if (vector.Y < 0) { 
                isAlive = false;
            }
        }
    }
}
