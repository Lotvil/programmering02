using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter
{
    static class GameElements
{
        static Texture2D menuSprite;
        static Microsoft.Xna.Framework.Vector2 menuPos;
        static Player player;
        static List<Enemy> enemies;
        static List<GoldCoin> goldCoins;
        static Texture2D goldCoinSprite;
        static SpriteFont Arial32;
        static Menu menu;
        static Background background;

        //olika gamestates
        public enum State { Menu, Run, HighScore, Quit};
        public static State currentState;

        //Initialize - ropas upp av game1.initilize då spelet startar.

        public static void Initialize() { 
            goldCoins = new List<GoldCoin>();
        }

        // LoadContent - anropas av game1.loadcontent när spelet startar

        public static void LoadContent(ContentManager content, GameWindow window) {
            menuSprite = content.Load<Texture2D>("menu");
            menuPos.X = window.ClientBounds.Width / 2 - menuSprite.Width / 2;
            menuPos.Y = window.ClientBounds.Height / 2 - menuSprite.Height / 2;

            player = new Player(content.Load<Texture2D>("ship"), 380, 400, 2.5f, 4.5f, content.Load<Texture2D>("bullet"));

            enemies = new List<Enemy>();
            GenerateEnemies(window, content);

            Arial32 = content.Load<SpriteFont>("Fonts/Arial32");
            goldCoinSprite = content.Load<Texture2D>("coin");

            menu = new Menu((int)State.Menu);
            menu.AddItem(content.Load<Texture2D>("Menu/start"), (int)State.Run, window, content);
            menu.AddItem(content.Load<Texture2D>("Menu/highscore"), (int)State.HighScore, window, content);
            menu.AddItem(content.Load<Texture2D>("Menu/exit"), (int)State.Quit, window, content);

            background = new Background(content.Load<Texture2D>("background"), window);

        }

        // MenuUpdate - kontrollerar vad användaren väljer i menyn
        public static State MenuUpdate(GameTime gameTime) {

            return (State)menu.Update(gameTime);

            
        }

        // MenuDraw - ritar meny
        public static void MenuDraw(SpriteBatch _spriteBatch) {
            background.Draw(_spriteBatch);
            menu.Draw(_spriteBatch);
        }

        // RunUpdate - uppdate metod för spelet
        public static State RunUpdate(ContentManager content, GameWindow window, GameTime gameTime) {

            background.Update(window);

            //uppdatera spelaren position
            player.Update(window, gameTime);

            //gå igenomalla fiender
            foreach (Enemy e in enemies.ToList())
            {
                //kontrollera om den nuddar en kula
                foreach (Bullet b in player.Bullets)
                {
                    if (e.CheckCollision(b)) // vid kollisoin
                    {
                        e.IsAlive = false; //döda fienden
                        player.Points++; // ge spelaren poäng
                    }
                }
                if (e.IsAlive)
                {
                    if (e.CheckCollision(player))
                    {
                        player.IsAlive = false;
                    }
                    e.Update(window);
                }
                else
                {
                    enemies.Remove(e);
                }

            }

            Random random = new Random();
            int newCoin = random.Next(0, 200);
            if (newCoin == 1)
            {
                int rndX = random.Next(0, window.ClientBounds.Width - goldCoinSprite.Width);
                int rndY = random.Next(0, window.ClientBounds.Height - goldCoinSprite.Height);
                goldCoins.Add(new GoldCoin(goldCoinSprite, rndX, rndY, gameTime));
            }

            //Gå igenom listan med mynt ute
            foreach (GoldCoin gc in goldCoins.ToList())
            {
                if (gc.IsAlive)
                {
                    //kollar om myntet dött än
                    gc.Update(gameTime);

                    //kollar om det nuddar spelaren
                    if (gc.CheckCollision(player))
                    {
                        //ta bort myntet
                        goldCoins.Remove(gc);
                        player.Points++; // ge spelaren poäng
                    }
                }
                else
                { //ta bort guldmyntet då det dött.
                    goldCoins.Remove(gc);
                }
            }

            if (!player.IsAlive)
            {
                return State.Menu;
            }

            return State.Run;

        }

        //RunDraw - metod för att rita ut spelet
        public static void RunDraw(SpriteBatch _spriteBatch) {

            background.Draw(_spriteBatch);

            player.Draw(_spriteBatch);
            foreach (Enemy e in enemies)
            {
                e.Draw(_spriteBatch);
            }
            foreach (GoldCoin gc in goldCoins)
            {
                gc.Draw(_spriteBatch);
            }
            _spriteBatch.DrawString(Arial32, "Poäng: " + player.Points, new Microsoft.Xna.Framework.Vector2(0, 0), Color.White);
        }

        //HighScoreUpdate - uppdate metod för highscore skärmen
        public static State HighScoreUpdate() {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Escape)) { 
                return State.Menu;
            }
            return State.HighScore;
        }

        //HighScoreDraw - metod för att rita ut highscore skärmen
        public static void HighScoreDraw(SpriteBatch spriteBatch) {

            //rita lista

        }
        public static void GenerateEnemies(GameWindow window, ContentManager content)
        {
            Random random = new Random();
            Texture2D tmpSprite = content.Load<Texture2D>("mine");
            for (int i = 0; i < 5; i++)
            {
                int rndX = random.Next(0, window.ClientBounds.Width - tmpSprite.Width);
                int rndY = random.Next(0, window.ClientBounds.Height / 2);
                Mine temp = new Mine(tmpSprite, rndX, rndY);
                enemies.Add(temp);
            }
            tmpSprite = content.Load<Texture2D>("tripod");
            for (int i = 0; i < 5; i++)
            {
                int rndX = random.Next(0, window.ClientBounds.Width - tmpSprite.Width);
                int rndY = random.Next(0, window.ClientBounds.Height / 2);
                Tripod temp = new Tripod(tmpSprite, rndX, rndY);
                enemies.Add(temp);
            }
        }

        public static void Reset (GameWindow window, ContentManager content)
        {
            player.Reset(380, 400, 2.5f, 4.5f);

            enemies.Clear();
            GenerateEnemies(window, content);

        }


    }
}
