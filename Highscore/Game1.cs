using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace Highscore;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    SpriteFont myFont;

    HighScore highScore;

    enum State
    {
        PrintHighScore,
        EnterHighScore
    }
    State currentState;


    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        highScore = new HighScore(10);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        highScore.LoadFromFile("highscore.txt");

        myFont = Content.Load<SpriteFont>("myFont");

        // TODO: use this.Content to load your game content here
    }

    protected override void UnloadContent()
    {
        highScore.SaveToFile("highscore.txt");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        switch (currentState)
        {
        case State.EnterHighScore: // Skriv in oss i listan
        // Fortsätt så länge HighScore.EnterUpdate() returnerar true:
        if(highScore.EnterUpdate(gameTime, 10))
        currentState = State.PrintHighScore;
        break;
        default: // Highscore-listan (tar emot en tangent)
        KeyboardState keyboardState = Keyboard.GetState();
        if(keyboardState.IsKeyDown(Keys.E))
        currentState = State.EnterHighScore;
        break;
        }


        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        switch (currentState)
        {
        case State.EnterHighScore: // Skriv in oss i listan
        highScore.EnterDraw(_spriteBatch, myFont);
        break;
        default: // Rita ut highscore-listan
        highScore.PrintDraw(_spriteBatch, myFont);
        break;
        }
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
