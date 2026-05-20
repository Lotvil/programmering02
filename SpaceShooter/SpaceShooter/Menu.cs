using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter
{
    internal class MenuItem
    {
        Texture2D texture;
        Microsoft.Xna.Framework.Vector2 position;
        int currentState;

        public MenuItem(Texture2D texture, Microsoft.Xna.Framework.Vector2 position, int currentState)
        {
            this.texture = texture;
            this.position = position;
            this.currentState = currentState;
        }

        public Texture2D Texture { get { return texture; } }
        public Microsoft.Xna.Framework.Vector2 Position { get { return position; } }
        public int CurrentState
        { get { return currentState; } }
    }
    // Menu - klass för att skapa menyer i spelet, har en lista av MenuItems
    class Menu
    {
        List<MenuItem> menu;
        int selected = 0;

        // Används för att rita ut menuitems på olika höjd
        float currentHeight = 0;

        // Lastchange gör så att spelaren inte gör mistag i menyn genom att ha en paus på 130ms
        double lastChange = 0;


        // Presenterar Själva menyn
        int defaultMenuState;

        //konstruktor
        public Menu (int defaultMenuState)
        {
            menu = new List<MenuItem> ();
            this.defaultMenuState = defaultMenuState;
        }

        // Lägger till ett menyval
        public void AddItem (Texture2D itemTexture, int state, GameWindow window, ContentManager content)
        {
            Texture2D tmpbox = content.Load<Texture2D>("Menu/start");
            float X = window.ClientBounds.Width /2 - tmpbox.Width/2;
            float Y = 60+currentHeight;

            currentHeight += itemTexture.Height + 20;

            // Skapar temp object av MenuItem och lägger till i listan
            MenuItem temp = new MenuItem(itemTexture, new Vector2(X, Y), state);
            menu.Add (temp);
        }

        public int Update(GameTime gameTime) 
        {
            // Läs in tangenttryckningar
            KeyboardState keyboardState = Keyboard.GetState();

            // Byte mellan olika menyval med paus på 130 ms mellan varje byte
            if (lastChange + 130 < gameTime.TotalGameTime.TotalMilliseconds)
            {
                // Gå ett steg ner i menyn.
                if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S))
                {
                    selected++;
                    if (selected > menu.Count - 1)
                    {
                        selected = 0;
                    }
                }
                // Gå upp ett steg
                if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W))
                {
                    selected--;
                    if(selected < 0)
                    {
                        selected = menu.Count - 1; // Om man går upp från det första valet så hamnar man på det sista valet
                    }
                }

                lastChange = gameTime.TotalGameTime.TotalMilliseconds;
            }

            if (keyboardState.IsKeyDown(Keys.Enter) || keyboardState.IsKeyDown(Keys.Space))
            {
                return menu[selected].CurrentState;
            }

            return defaultMenuState;
            
        }

        // Draw - ritar ut menyn, det aktiva valet ritas ut med en annan ton
        public void Draw (SpriteBatch _spriteBatch)
        {
            for (int i=0; i<menu.Count; i++)
            {
                // Det aktiva valet ritas ut med annan ton
                if (i == selected)
                {
                    _spriteBatch.Draw(menu[i].Texture, menu[i].Position, Color.LightYellow);
                }

                // De andra valen ritas ut i normal ton
                else
                {
                    _spriteBatch.Draw(menu[i].Texture, menu[i].Position, Color.White);
                }
            }
        }
    }
}
