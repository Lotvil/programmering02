using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter
{
    // Powerup - klass för alla powerups i spelet, ärver från physicalObject,
    class Powerup : physicalObject
    {
        double timeToDie; // Hur länge powerupen ska leva i ms

        public virtual int Points => 1;

        // konstruktor
        public Powerup(Texture2D texture, float X, float Y, GameTime gameTime) : base(texture, X, Y, 0, 2f)
        {
            timeToDie = gameTime.TotalGameTime.TotalMilliseconds + 5000;
        }

        // Update kontrollerar om powerupen ska fortsätta leva
        public void Update(GameTime gameTime)
        {
            if (timeToDie < gameTime.TotalGameTime.TotalMilliseconds)
            {
                isAlive = false;
            }
        }
    }

    // Goldcoin - klass för guldmynt som ger 5 poäng, ärver från Powerup
    class GoldCoin : Powerup
    {
        public override int Points => 5;
        public GoldCoin(Texture2D texture, float X, float Y, GameTime gameTime) : base(texture, X, Y, gameTime)
        {
        }
    }
    
    // BulletBoost - klass för powerup som gör att spelaren kan skjuta snabbare, ärver från Powerup
    class BulletBoost : Powerup
    {
        public override int Points => 1;
        public BulletBoost(Texture2D texture, float X, float Y, GameTime gameTime) : base(texture, X, Y, gameTime)
        {
        }
    }

    // Heart - klass för powerup som ger spelaren extra liv, ärver från Powerup
    class Heart : Powerup
    {
        public override int Points => 3;
        public Heart(Texture2D texture, float X, float Y, GameTime gameTime) : base(texture, X, Y, gameTime)
        {
        }
    }
}
