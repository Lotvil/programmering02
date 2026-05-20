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

    class Menu
    {
        List<MenuItem> menu;
        int selected = 0;

        //används för att rita ut menuitems på olika höjd
        float currentHeight = 0;

        //lastchange gör så man inte kan bläddra i menyerna FÖR snabbt
        double lastChange = 0;


        //representerar ssjälva menyn
        int defaultMenuState;

        //konstruktor
        public Menu (int defaultMenuState)
        {
            menu = new List<MenuItem> ();
            this.defaultMenuState = defaultMenuState;
        }

        //Lägger till menyval
        public void AddItem (Texture2D itemTexture, int state, GameWindow window, ContentManager content)
        {
            Texture2D tmpbox = content.Load<Texture2D>("Menu/start");
            float X = window.ClientBounds.Width /2 - tmpbox.Width/2;
            float Y = 60+currentHeight;

            //ändra currentheight
            currentHeight += itemTexture.Height + 20;

            //skapa temporärt objekt och lägg till i listan
            MenuItem temp = new MenuItem(itemTexture, new Vector2(X, Y), state);
            menu.Add (temp);
        }

        public int Update(GameTime gameTime) 
        {
            //läs in tangenttryckningar
            KeyboardState keyboardState = Keyboard.GetState();

            //byte mellan olika menyval med paus på 130 ms mellan varje byte
            if (lastChange + 130 < gameTime.TotalGameTime.TotalMilliseconds)
            {
                //Gå ett steg ner.
                if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S))
                {
                    selected++;
                    //se till att valen loopas
                    if (selected > menu.Count - 1)
                    {
                        selected = 0;
                    }
                }
                //Gå upp ett steg
                if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W))
                {
                    selected--;
                    //se till att valen loopas
                    if(selected < 0)
                    {
                        selected = menu.Count - 1; // det sista menyvalet.
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

        public void Draw (SpriteBatch _spriteBatch)
        {
            for (int i=0; i<menu.Count; i++)
            {
                //det aktiva valet ritas ut med speciall toning
                if (i == selected)
                {
                    _spriteBatch.Draw(menu[i].Texture, menu[i].Position, Color.LightYellow);
                }

                //annars ritas det utan toning
                else
                {
                    _spriteBatch.Draw(menu[i].Texture, menu[i].Position, Color.White);
                }
            }
        }
    }
}
