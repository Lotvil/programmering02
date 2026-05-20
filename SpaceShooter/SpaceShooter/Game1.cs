using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.IO;

namespace SpaceShooter
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        
        

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            GameElements.currentState = GameElements.State.Menu;
            GameElements.Initialize();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            GameElements.LoadContent(Content, Window);
        }

        protected override void UnloadContent()
        {
            GameElements.SaveHighScore(); // Spara highscore när spelet stängs
        }

        //Spelets huvudloop, där all logik kring states körs
        protected override void Update(GameTime gameTime)
        {
            // Gör så att spelaren kan trycka på escape för att komma tillbaka till menyn när den spelar
            if (Keyboard.GetState().IsKeyDown(Keys.Escape) &&
                GameElements.currentState == GameElements.State.Run)
            {
                GameElements.currentState = GameElements.State.EnterHighScore;
            }

            GameElements.State previousState = GameElements.currentState;

            switch (GameElements.currentState)
            {
                case GameElements.State.Run: // Kör spelet
                    GameElements.currentState = GameElements.RunUpdate(Content, Window, gameTime);
                    break;
                case GameElements.State.HighScore: // Visa highscore
                    GameElements.currentState = GameElements.HighScoreUpdate();
                    break;
                case GameElements.State.Quit: // Avsluta spelet
                    Exit();
                    break;
                case GameElements.State.EnterHighScore: // Mata in namn för highscore
                    GameElements.currentState = GameElements.EnterHighScoreUpdate(gameTime);
                    break;
                case GameElements.State.om: // Visa "om spelet"-skärmen
                    GameElements.currentState = GameElements.omUpdate();
                    break;
                default: // Visa menyn
                    GameElements.currentState = GameElements.MenuUpdate(gameTime);
                    break;
            }

            if (previousState == GameElements.State.Run && GameElements.currentState == GameElements.State.Menu)
            {
                GameElements.Reset(Window, Content, gameTime); // Starta om spelet när man går tillbaka till menyn från att ha spelat
            }
            
            base.Update(gameTime);
        }

        // All lgogik kring att visa de olika skärmarna
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            switch (GameElements.currentState)
            {
                case GameElements.State.Run:
                    GameElements.RunDraw(_spriteBatch);
                    break;
                case GameElements.State.HighScore:
                    GameElements.HighScoreDraw(_spriteBatch);
                    break;
                case GameElements.State.EnterHighScore:
                    GameElements.EnterHighScoreDraw(_spriteBatch);
                    break;
                case GameElements.State.om:
                    GameElements.omDraw(_spriteBatch);
                    break;
                default:
                    GameElements.MenuDraw(_spriteBatch);
                    break;
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        

    }
}
