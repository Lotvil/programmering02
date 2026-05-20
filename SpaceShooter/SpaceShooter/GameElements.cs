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
using System.ComponentModel;

namespace SpaceShooter
{
    static class GameElements
{       
        // Alla element till spellogiken
        static Texture2D menuSprite;
        static Microsoft.Xna.Framework.Vector2 menuPos;
        static Player player;
        public static Player Player => player;
        static List<Enemy> enemies;
        public static List<Enemy> Enemies => enemies;
        static List<Powerup> powerups;
        static Texture2D goldCoinSprite;
        static Texture2D bulletBoostSprite;
        static Texture2D heartSprite;
        static List<Warning> warnings;
        static Texture2D warningSprite;
        static List<PendingEnemy> pendingEnemies;
        static double spawnDelay = 2000;
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
        
        // Gamestates
        public enum State
        {
            Menu,
            Run,
            om,
            EnterHighScore,
            HighScore,
            Quit
        };
        public static State currentState;

        // Initialize - ropas upp av game1.initilize då spelet startar.
        public static void Initialize() { 
            powerups = new List<Powerup>();
            highScore = new HighScore(10);
            pendingEnemies = new List<PendingEnemy>();
            warnings = new List<Warning>();
        }

        // LoadContent - anropas av game1.loadcontent när spelet startar
        public static void LoadContent(ContentManager content, GameWindow window) {
            menuSprite = content.Load<Texture2D>("menu");
            menuPos.X = window.ClientBounds.Width / 2 - menuSprite.Width / 2;
            menuPos.Y = window.ClientBounds.Height / 2 - menuSprite.Height / 2;

            // Laddar in texturer till spelaren, powerups, bakgrund och meny samt highscore från fil
            player = new Player(content.Load<Texture2D>("ship"), 380, 400, 2.5f, 4.5f, content.Load<Texture2D>("bullet"));

            enemies = new List<Enemy>();

            Arial32 = content.Load<SpriteFont>("Fonts/Arial32");
            goldCoinSprite = content.Load<Texture2D>("coin");
            bulletBoostSprite = content.Load<Texture2D>("bulletboost");
            heartSprite = content.Load<Texture2D>("heart");
            warningSprite = content.Load<Texture2D>("warning");
            menu = new Menu((int)State.Menu);
            menu.AddItem(content.Load<Texture2D>("Menu/start"), (int)State.Run, window, content);
            menu.AddItem(content.Load<Texture2D>("Menu/highscore"), (int)State.HighScore, window, content);
            menu.AddItem(content.Load<Texture2D>("Menu/exit"), (int)State.Quit, window, content);
            menu.AddItem(content.Load<Texture2D>("omspelet"), (int)State.om, window, content);

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
                    ignoreInput = false; // Förhindra att val görs av misstag direkt när menyn visas
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

            // Uppdatera spelarens position
            player.Update(window, gameTime);

            foreach (PendingEnemy pe in pendingEnemies.ToList())
            {
                if (gameTime.TotalGameTime.TotalMilliseconds >= pe.SpawnTime)
                {
                    enemies.Add(pe.Enemy);
                    pendingEnemies.Remove(pe);
                }
            }

            // Loopa genom alla fiender och kolla kollison med kulor och spelare, samt uppdatera deras position
            foreach (Enemy e in enemies.ToList())
            {
                // Kontrollerar kollison mellan fiende och kulor

                foreach (Bullet b in player.Bullets)
                {
                    if (e.CheckCollision(b)) // Vid kollison
                    {
                        e.Life -= 1;
                        b.IsAlive = false; // Ta bort kulan

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

                        e.IsAlive = false; // Ta bort fienden vid kollison med spelaren

                        if (player.Life <= 0) // Dödar spelaren när liven tar slut
                        {
                            player.IsAlive = false;
                        }
                    }
                    e.Update(window, gameTime);
                }
                else
                {
                    enemies.Remove(e);
                }

            }
            int newPowerup = random.Next(0, 600); // Spawnar powerups
            if (0 < newPowerup && newPowerup < 3) // 0.3% chans varje frame att en GoldCoin ska spawnas
            {
                int rndX = random.Next(0, window.ClientBounds.Width - goldCoinSprite.Width);
                int rndY = random.Next(0, window.ClientBounds.Height - goldCoinSprite.Height);
                powerups.Add(new GoldCoin(goldCoinSprite, rndX, rndY, gameTime));
            }
            if (newPowerup == 3) // 0.15% chans varje frame att en BulletBoost ska spawnas
            {
                int rndX = random.Next(0, window.ClientBounds.Width - bulletBoostSprite.Width);
                int rndY = random.Next(0, window.ClientBounds.Height - bulletBoostSprite.Height);
                powerups.Add(new BulletBoost(bulletBoostSprite, rndX, rndY, gameTime));
            }
            if (newPowerup == 4 && player.Life < 3 && wave > 2) // 0.15% chans varje frame att en Heart ska spawnas 
            {//                                                    om spelaren har mindre än 3 liv och våg 3 har passerats.
                int rndX = random.Next(0, window.ClientBounds.Width - heartSprite.Width);
                int rndY = random.Next(0, window.ClientBounds.Height - heartSprite.Height);
                powerups.Add(new Heart(heartSprite, rndX, rndY, gameTime));
            }

            // Gå igenom listan powerups och kolla om spelaren plockar upp någon
            foreach (Powerup p in powerups.ToList())
            {
                if (p.IsAlive)
                {
                    // Kollar om powerupens tid tagit slut
                    p.Update(gameTime);

                    // Kollar om powerupen nuddar spelaren
                    if (p.CheckCollision(player))
                    {
                        // Ta bort powerup och ge spelaren poäng och effekt
                        powerups.Remove(p);
                        player.Points += p.Points; // Ge spelaren poäng
                        if (p is BulletBoost) //Bulletboost ger snabbare eldhastighet
                        {
                            player.BulletBoost(gameTime);
                        }
                        if (p is Heart) // Heart ger spelaren ett extra liv
                        {
                            player.Life += 1;
                        }
                    }
                }
                else
                { // Ta bort powerupen då den dött.
                    powerups.Remove(p);
                }
            }

            foreach (Warning w in warnings.ToList())
            {
                if (w.IsAlive)
                {
                    w.Update(gameTime);
                }
                else
                {
                    warnings.Remove(w);
                }
            }

            if (wave == 6 || wave == 11) //Spawnar fiender under bossvågarna
            {
                int enemySpawn = random.Next(0, 500);

                if (enemySpawn > 3 && wave > enemySpawn)
                {
                    Texture2D mineTexture = content.Load<Texture2D>("mine");

                    int rndX = random.Next(0, window.ClientBounds.Width - mineTexture.Width);
                    int rndY = random.Next(0, window.ClientBounds.Height / 2);

                    warnings.Add(new Warning(warningSprite, rndX, rndY, gameTime, spawnDelay)); // Spawnar en varning innan fienden spawnas
                    QueueEnemy(new Tripod(mineTexture, rndX, rndY), gameTime, spawnDelay);
                }

                if ( enemySpawn > 3+10 && wave+10 > enemySpawn)
                {
                    Texture2D tripodTexture = content.Load<Texture2D>("tripod");

                    int rndX = random.Next(0, window.ClientBounds.Width - tripodTexture.Width);
                    warnings.Add(new Warning(warningSprite, rndX, 0, gameTime, spawnDelay)); // Spawnar en varning innan fienden spawnas
                    QueueEnemy(new Tripod(tripodTexture, rndX, 0), gameTime, spawnDelay);
                }
            }

            if (!player.IsAlive) //Tar spelaren till Highscore-inmatnings-sidan när den dör
            {
                return State.EnterHighScore;
            }

            if (enemies.Count == 0 && pendingEnemies.Count == 0)
            {
                wave++;
                if (wave > 3)
                {
                    enemiesPerWave += 1; // Makes game progressively harder
                }
                GenerateEnemies(window, content, enemiesPerWave, gameTime);
            }

            return State.Run;

        }

        //RunDraw - metod för att rita ut spelet
        public static void RunDraw(SpriteBatch _spriteBatch) {

            background.Draw(_spriteBatch); //Rita bakgrunden först så den hamnar längst bak

            player.Draw(_spriteBatch); // Rita spelaren ovanpå bakgrunden
            foreach (Enemy e in enemies) // Rita fiender
            {
                e.Draw(_spriteBatch);
            }
            foreach (Powerup p in powerups) // Rita powerups
            {
                p.Draw(_spriteBatch);
            }
            foreach (Warning w in warnings) // Rita varningar
            {
                w.Draw(_spriteBatch);
            }
            // Rita poäng och liv
            _spriteBatch.DrawString(Arial32, "Poäng: " + player.Points, new Microsoft.Xna.Framework.Vector2(0, 0), Color.White);
            _spriteBatch.DrawString(Arial32, "Liv: " + player.Life, new Microsoft.Xna.Framework.Vector2(0, 30), Color.White);
            _spriteBatch.DrawString(Arial32, "Våg: " + wave, new Microsoft.Xna.Framework.Vector2(0, 60), Color.White);
        }

        //HighScoreUpdate - uppdate metod för highscore skärmen
        public static State HighScoreUpdate() {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Escape)) 
            { 
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

        // OmUpdate och OmDraw - uppdate och draw metoder för "om spelet"-skärmen
        public static State omUpdate()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                return State.Menu;
            }
            return State.om;
        }

        public static void omDraw(SpriteBatch spriteBatch)
        {
            background.Draw(spriteBatch);

            string text = "Detta spel är skapat av:\n\n" +
                "Leonid Bilhöfer Maksinen\n" +
                "Med stor inspiration från SpaceShooter av Krister Trangius\n\n" +
                "Tack för att du spelar mitt spel!\n\n" +
                "Kontroller:\n" +
                "Flytta: WASD eller piltangenter\n" +
                "Skjut: mellanslag (SHIFT för diagonala skott)\n" +
                "Pausa: Escape\n\n" +
                "Målet är att överleva så länge som möjligt och få så många poäng\n" +
                "som möjligt genom att skjuta fiender och samla powerups.";

            spriteBatch.DrawString(Arial32, text, new Microsoft.Xna.Framework.Vector2(50, 10), Color.White);

            spriteBatch.Draw(goldCoinSprite, new Microsoft.Xna.Framework.Vector2(50, 420), Color.White);
            spriteBatch.Draw(bulletBoostSprite, new Microsoft.Xna.Framework.Vector2(100, 420), Color.White);
            spriteBatch.Draw(heartSprite, new Microsoft.Xna.Framework.Vector2(150, 420), Color.White);
            
        }
        // GenerateEnemies - metod för att generera fiender, anropas i början av varje våg
        public static void GenerateEnemies(GameWindow window, ContentManager content, int count, GameTime gameTime)
        {
            // Spawnar bossar på våg 6 och 11, spawnar då inte vanliga fiender i början av vågen
            Texture2D tmpSprite = content.Load<Texture2D>("boss");
            if (wave == 6)
            {
                warnings.Add(new Warning(warningSprite, window.ClientBounds.Width / 2 - tmpSprite.Width / 2, 0, gameTime, spawnDelay)); // Spawnar en varning innan fienden spawnas
                QueueEnemy(new Boss1(tmpSprite, content.Load<Texture2D>("enemybullet"), window.ClientBounds.Width / 2 - tmpSprite.Width / 2, 0), gameTime, spawnDelay);
                return;
            }
            else if (wave == 11)
            {
                tmpSprite = content.Load<Texture2D>("boss2");
                warnings.Add(new Warning(warningSprite, window.ClientBounds.Width / 2 - tmpSprite.Width / 2, 0, gameTime, spawnDelay)); // Spawnar en varning innan fienden spawnas
                QueueEnemy(new Boss2(tmpSprite, content.Load<Texture2D>("enemybullet"), window.ClientBounds.Width / 2 - tmpSprite.Width / 2, 0), gameTime, spawnDelay);
                return;
            }
            else
            {

                // Spawnar vanliga fiender, antalet fiender som spawnas ökar ju högre våg
                tmpSprite = content.Load<Texture2D>("mine");

                if (wave > 1)
                {
                    for (int i = 0; i < count; i++) // Spawnar mines i våg 2 och uppåt
                    {
                        int rndX = random.Next(0, window.ClientBounds.Width - tmpSprite.Width);
                        int rndY = random.Next(0, window.ClientBounds.Height / 2);
                        warnings.Add(new Warning(warningSprite, rndX, rndY, gameTime, spawnDelay)); // Spawnar en varning innan fienden spawnas
                        QueueEnemy(new Mine(tmpSprite, rndX, rndY), gameTime, spawnDelay);
                    }
                }

                tmpSprite = content.Load<Texture2D>("astroid");

                for (int i = 0; i < count; i++) // Spawnar asteroider i alla vågor
                {
                    int rndX = random.Next(0, window.ClientBounds.Width - tmpSprite.Width);
                    int rndY = random.Next(0, window.ClientBounds.Height / 2);
                    warnings.Add(new Warning(warningSprite, rndX, rndY, gameTime, spawnDelay)); // Spawnar en varning innan fienden spawnas
                    QueueEnemy(new Astroid(tmpSprite, rndX, rndY), gameTime, spawnDelay);
                }

                if (wave > 2)
                {
                    tmpSprite = content.Load<Texture2D>("tripod");

                    for (int i = 0; i < count; i++) // Spawnar tripods i våg 3 och uppåt
                    {
                        int rndX = random.Next(0, window.ClientBounds.Width - tmpSprite.Width);
                        int rndY = random.Next(0, window.ClientBounds.Height / 2);
                        warnings.Add(new Warning(warningSprite, rndX, rndY, gameTime, spawnDelay)); // Spawnar en varning innan fienden spawnas
                        QueueEnemy(new Tripod(tmpSprite, rndX, rndY), gameTime, spawnDelay);
                    }
                }
            }
        }

        // Reset - metod för att starta om spelet, anropas när spelaren dör och efter att highscore matats in
        public static void Reset (GameWindow window, ContentManager content, GameTime gameTime)
        {
            player.Reset(380, 400, 2.5f, 4.5f);

            player.IsAlive = true;
            player.Points = 0;
            player.Life = 3;

            enemies.Clear();
            pendingEnemies.Clear();
            warnings.Clear();
            powerups.Clear();
            wave = 1;
            enemiesPerWave = 4;
            GenerateEnemies(window, content, enemiesPerWave, gameTime);
        }

        // SaveHighScore - metod för att spara highscore till fil, anropas när highscore matats in
        public static void SaveHighScore()
        {
            highScore.SaveToFile("highscore.txt");
        }

        // EnterHighScoreUpdate - uppdate metod för highscore-inmatnings-sidan
        public static State EnterHighScoreUpdate(GameTime gameTime)
        {
            if (highScore.EnterUpdate(gameTime, player.Points))
            {
                highScore.SaveToFile("highscore.txt");

                Reset(gameWindow, gameContent, gameTime);

                ignoreInput = true;

                return State.Menu;
            }

            return State.EnterHighScore;
        }

        // EnterHighScoreDraw - draw metod för highscore-inmatnings-sidan
        public static void EnterHighScoreDraw(SpriteBatch spriteBatch)
        {
            background.Draw(spriteBatch);

            highScore.EnterDraw(spriteBatch, Arial32);
        }

        class PendingEnemy
        {
            public Enemy Enemy;
            public double SpawnTime;

            public PendingEnemy(Enemy enemy, double spawnTime)
            {
                Enemy = enemy;
                SpawnTime = spawnTime;
            }
        }

        public static void QueueEnemy(Enemy enemy, GameTime gameTime, double delay)
        {
            pendingEnemies.Add(new PendingEnemy(enemy, gameTime.TotalGameTime.TotalMilliseconds + delay));
        }
    }
}
