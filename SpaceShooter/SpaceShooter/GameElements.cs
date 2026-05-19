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
using System.IO;

namespace SpaceShooter
{
    static class GameElements
{
        static Texture2D menuSprite;
        static Microsoft.Xna.Framework.Vector2 menuPos;
        static Player player;
        public static Player Player => player;
        static List<Enemy> enemies;
        static List<Powerup> powerups;
        static Texture2D goldCoinSprite;
        static Texture2D bulletBoostSprite;
        static SpriteFont Arial32;
        static Menu menu;
        static Background background;
        static HighScore highScore;
        static GameWindow gameWindow;
        static ContentManager gameContent;
        static Random random = new Random();
        static bool ignoreInput = false;
        static int wave = 0;
        static int enemiesPerWave = 4;
        

        //olika gamestates
        public enum State
        {
            Menu,
            Run,
            EnterHighScore,
            HighScore,
            Quit
        };
        public static State currentState;

        //Initialize - ropas upp av game1.initilize då spelet startar.

        public static void Initialize() { 
            powerups = new List<Powerup>();
            highScore = new HighScore(10);
        }

        // LoadContent - anropas av game1.loadcontent när spelet startar

        public static void LoadContent(ContentManager content, GameWindow window) {
            menuSprite = content.Load<Texture2D>("menu");
            menuPos.X = window.ClientBounds.Width / 2 - menuSprite.Width / 2;
            menuPos.Y = window.ClientBounds.Height / 2 - menuSprite.Height / 2;

            player = new Player(content.Load<Texture2D>("ship"), 380, 400, 2.5f, 4.5f, content.Load<Texture2D>("bullet"));

            enemies = new List<Enemy>();
            //wave = 1;
            //GenerateEnemies(window, content, enemiesPerWave);

            Arial32 = content.Load<SpriteFont>("Fonts/Arial32");
            goldCoinSprite = content.Load<Texture2D>("coin");
            bulletBoostSprite = content.Load<Texture2D>("bulletboost");

            menu = new Menu((int)State.Menu);
            menu.AddItem(content.Load<Texture2D>("Menu/start"), (int)State.Run, window, content);
            menu.AddItem(content.Load<Texture2D>("Menu/highscore"), (int)State.HighScore, window, content);
            menu.AddItem(content.Load<Texture2D>("Menu/exit"), (int)State.Quit, window, content);

            background = new Background(content.Load<Texture2D>("background"), window);

            highScore.LoadFromFile("highscore.txt");
            gameWindow = window;
            gameContent = content;

        }

        // MenuUpdate - kontrollerar vad användaren väljer i menyn
        public static State MenuUpdate(GameTime gameTime) {
            if (ignoreInput)
            {
                if (Keyboard.GetState().IsKeyUp(Keys.Enter))
                {
                    ignoreInput = false; // unlock input once key is released
                }
                return State.Menu;
            }

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
                        e.Life -= 1;
                        b.IsAlive = false; //ta bort kulan

                        if (e.Life <= 0)
                        {
                            e.IsAlive = false;
                            player.Points += e.Points;
                        }
                    }
                }
                if (e.IsAlive)
                {
                    if (e.CheckCollision(player))
                    {
                        player.Life -= 1;

                        e.IsAlive = false; // instantly kill enemy on contact

                        if (player.Life <= 0)
                        {
                            player.IsAlive = false;
                        }
                    }
                    e.Update(window);
                }
                else
                {
                    enemies.Remove(e);
                }

            }
            int newPowerup = random.Next(0, 400);
            if (1 < newPowerup && newPowerup < 4)
            {
                int rndX = random.Next(0, window.ClientBounds.Width - goldCoinSprite.Width);
                int rndY = random.Next(0, window.ClientBounds.Height - goldCoinSprite.Height);
                powerups.Add(new GoldCoin(goldCoinSprite, rndX, rndY, gameTime));
            }
            if (newPowerup == 0)
            {
                int rndX = random.Next(0, window.ClientBounds.Width - bulletBoostSprite.Width);
                int rndY = random.Next(0, window.ClientBounds.Height - bulletBoostSprite.Height);
                powerups.Add(new BulletBoost(bulletBoostSprite, rndX, rndY, gameTime));
            }

            //Gå igenom listan med mynt ute
            foreach (Powerup p in powerups.ToList())
            {
                if (p.IsAlive)
                {
                    //kollar om myntet dött än
                    p.Update(gameTime);

                    //kollar om det nuddar spelaren
                    if (p.CheckCollision(player))
                    {
                        //ta bort myntet
                        powerups.Remove(p);
                        player.Points += p.Points; // ge spelaren poäng
                        if (p is BulletBoost)
                        {
                            player.BulletBoost(gameTime);
                        }
                    }
                }
                else
                { //ta bort guldmyntet då det dött.
                    powerups.Remove(p);
                }
            }

            if (!player.IsAlive)
            {
                return State.EnterHighScore;
            }

            if (enemies.Count == 0)
            {
                wave++;
                if (wave > 3)
                {
                    enemiesPerWave += 1; // makes game progressively harder
                }
                GenerateEnemies(window, content, enemiesPerWave);
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
            foreach (Powerup p in powerups)
            {
                p.Draw(_spriteBatch);
            }
            _spriteBatch.DrawString(Arial32, "Poäng: " + player.Points, new Microsoft.Xna.Framework.Vector2(0, 0), Color.White);
            _spriteBatch.DrawString(Arial32, "Liv: " + player.Life, new Microsoft.Xna.Framework.Vector2(0, 30), Color.White);
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
        public static void HighScoreDraw(SpriteBatch spriteBatch)
        {
            background.Draw(spriteBatch);

            highScore.PrintDraw(spriteBatch, Arial32);
        }
        public static void GenerateEnemies(GameWindow window, ContentManager content, int count)
        {
            Texture2D tmpSprite = content.Load<Texture2D>("mine");

            if (wave > 1)
            {
                for (int i = 0; i < count; i++)
                {
                    int rndX = random.Next(0, window.ClientBounds.Width - tmpSprite.Width);
                    int rndY = random.Next(0, window.ClientBounds.Height / 2);
                    enemies.Add(new Mine(tmpSprite, rndX, rndY));
                }
            }

            tmpSprite = content.Load<Texture2D>("astroid");

            for (int i = 0; i < count; i++)
            {
                int rndX = random.Next(0, window.ClientBounds.Width - tmpSprite.Width);
                int rndY = random.Next(0, window.ClientBounds.Height / 2);
                enemies.Add(new Astroid(tmpSprite, rndX, rndY));
            }

            if (wave > 2)
            {
                tmpSprite = content.Load<Texture2D>("tripod");

                for (int i = 0; i < count; i++)
                {
                    int rndX = random.Next(0, window.ClientBounds.Width - tmpSprite.Width);
                    int rndY = random.Next(0, window.ClientBounds.Height / 2);
                    enemies.Add(new Tripod(tmpSprite, rndX, rndY));
                }
            }
        }

        public static void Reset (GameWindow window, ContentManager content)
        {
            player.Reset(380, 400, 2.5f, 4.5f);

            player.IsAlive = true;
            player.Points = 0;
            player.Life = 3;

            enemies.Clear();
            powerups.Clear();
            wave = 1;
            enemiesPerWave = 4;
            GenerateEnemies(window, content, enemiesPerWave);
        }

        public static void SaveHighScore()
        {
            highScore.SaveToFile("highscore.txt");
        }

        public static State EnterHighScoreUpdate(GameTime gameTime)
        {
            if (highScore.EnterUpdate(gameTime, player.Points))
            {
                highScore.SaveToFile("highscore.txt");

                Reset(gameWindow, gameContent);

                ignoreInput = true;

                return State.Menu;
            }

            return State.EnterHighScore;
        }

        public static void EnterHighScoreDraw(SpriteBatch spriteBatch)
        {
            background.Draw(spriteBatch);

            highScore.EnterDraw(spriteBatch, Arial32);
        }
    }
}
