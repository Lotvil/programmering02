using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter
{
    class Powerup : physicalObject
    {
        double timeToDie; // hur länge guldmyntet lever

        public virtual int Points => 1;

        // konstruktor

        public Powerup(Texture2D texture, float X, float Y, GameTime gameTime) : base(texture, X, Y, 0, 2f)
        {
            timeToDie = gameTime.TotalGameTime.TotalMilliseconds + 5000;
        }

        // Update kontrollerar om myntet ska fortsätta leva

        public void Update(GameTime gameTime)
        {
            if (timeToDie < gameTime.TotalGameTime.TotalMilliseconds)
            {
                isAlive = false;
            }
        }
    }

    class GoldCoin : Powerup
    {
        public override int Points => 2;
        public GoldCoin(Texture2D texture, float X, float Y, GameTime gameTime) : base(texture, X, Y, gameTime)
        {
        }
    }
    
    class BulletBoost : Powerup
    {
        public override int Points => 1;
        public BulletBoost(Texture2D texture, float X, float Y, GameTime gameTime) : base(texture, X, Y, gameTime)
        {
        }
    }
}
