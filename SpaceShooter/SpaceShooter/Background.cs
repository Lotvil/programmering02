using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter
{
    class BackgroundSprite : GameObject
    {
        public BackgroundSprite(Texture2D texture, float X, float Y) : base(texture, X, Y)
        {
        }

        public void Update(GameWindow window, int nrBackgroundsY)
        {
            vector.Y += 2f; // Flytta bakgrunden

            // Kontrollera om bakgrunden åker ut i nederkant
            if(vector.Y > window.ClientBounds.Height)
            {
                // Flytta bakgrundsbilden så den hamnar längst upp igen
                vector.Y = vector.Y - nrBackgroundsY * texture.Height;
            }
        }
    }

    class Background
    {
        BackgroundSprite[,] background;
        int nrBackgroundsX, nrBackgroundsY;

        public Background(Texture2D texture, GameWindow window)
        {
            // Hur många bilden ska vi ha i bredd och höjd?
            double tmpX = (double)window.ClientBounds.Width / texture.Width;
            nrBackgroundsX = (int)Math.Ceiling(tmpX);
            double tmpY = (double)window.ClientBounds.Height / texture.Height;
            // Avrunda, lägg till en extra
            nrBackgroundsY = (int)Math.Ceiling(tmpY)+1;

            // Sätt storlek på vektorn
            background = new BackgroundSprite[nrBackgroundsX, nrBackgroundsY];

            // Fyll vektorn med objekt
            for (int i = 0; i < nrBackgroundsX; i++)
            {
                for (int j = 0; j < nrBackgroundsY; j++)
                {
                    int posX = i * texture.Width;
                    int posY = j * texture.Height - texture.Height;
                    background[i, j] = new BackgroundSprite(texture, posX, posY);
                }
            }
        }

        public void Update(GameWindow window)
        {
            for (int i = 0; i < nrBackgroundsX; i++)
            {
                for (int j = 0; j < nrBackgroundsY; j++)
                {
                    background[i, j].Update(window, nrBackgroundsY);
                }
            }
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            for (int i = 0; i < nrBackgroundsX; i++)
            {
                for (int j =0; j < nrBackgroundsY; j++)
                {
                    background[i, j].Draw(_spriteBatch);
                }
            }
        }
    }

}
