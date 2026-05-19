using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter
{

    //Gameobject, en basklass för att skapa spelobjekt
    class GameObject
{
        protected Texture2D texture;
        protected Microsoft.Xna.Framework.Vector2 vector;
        protected float rotation;

        // Gameobject konstruktor

        public GameObject(Texture2D texture, float X, float Y) 
        {
            this.texture = texture;
            this.vector.X = X;
            this.vector.Y = Y;
            this.rotation = 0f;
        }

        // Draw

        public virtual void Draw(SpriteBatch _spriteBatch) 
        {
            _spriteBatch.Draw(texture, vector, null, Color.White, rotation, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }

        // Egenskaper

        public float X{ get { return vector.X;  } }
        public float Y { get { return vector.Y; } }
        public float Width { get { return texture.Width; } }
        public float Height { get { return texture.Height; } }

    }

    // MovingObject
    abstract class MovingObject : GameObject 
    { 
        protected Microsoft.Xna.Framework.Vector2 speed; //hastighet

        // MovingObject konstruktor

        public MovingObject(Texture2D texture, float X, float Y, float speedX, float speedY) : base(texture, X, Y) { 
            this.speed.X = speedX;
            this.speed.Y = speedY;
        }
    }

    abstract class physicalObject : MovingObject
    {
        protected bool isAlive = true;

        protected int life = 1;

        // Kontruktor

        public physicalObject(Texture2D texture, float X, float Y, float speedX, float speedY) : base(texture, X, Y, speedX, speedY)
        {
        }

        // CheckCollision, kontrollerar om det uppstår kollision mellan detta objekt och ett annat. Fuck AI jag hatar dina bitchass fucking förslag på saker som jag vill göra själv, gå och häng dig själv från en bro där alla kan se

        public bool CheckCollision(physicalObject other)
        {
            Rectangle myRect = new Rectangle(Convert.ToInt32(X), Convert.ToInt32(Y), Convert.ToInt32(Width), Convert.ToInt32(Height));

            Rectangle otherRect = new Rectangle(Convert.ToInt32(other.X), Convert.ToInt32(other.Y), Convert.ToInt32(other.Width), Convert.ToInt32(other.Height));

            return myRect.Intersects(otherRect);
        }

        // egenskper:

        public bool IsAlive
        {
            get { return isAlive; }
            set { isAlive = value; }
        }

        public int Life
        {
            get { return life; }
            set { life = value; }
        }

    }
}
