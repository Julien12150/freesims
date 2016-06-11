using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Julien12150.FreeSims.Game;

namespace Julien12150.FreeSims
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        bool isControllerMode;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Cursor cursor;

        Texture2D cursorSprite;
        Texture2D humanSprite;

        Control control;
        Menu menu;
        Game.Game game;

        SpriteFont mainFont;
        GameState state = GameState.Menu;

        public Game1(string[] args)
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            if (args.Length >= 1)
            {
                if (args[0] == "/c")
                    isControllerMode = true;
                else
                    isControllerMode = false;
            }
            else
                isControllerMode = false;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            cursorSprite = Content.Load<Texture2D>("cursor");
            humanSprite = Content.Load<Texture2D>("Human/M");
            mainFont = Content.Load<SpriteFont>("font");

            control = new Control(isControllerMode);
            cursor = new Cursor(Window.ClientBounds.Width, Window.ClientBounds.Height, cursorSprite, control);

            menu = new Menu(spriteBatch, control, mainFont, this);
            game = new Game.Game(Window.ClientBounds.Width, Window.ClientBounds.Height, control, cursor, spriteBatch, humanSprite);
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (control.isControllerMode)
                IsMouseVisible = false;

            if (state == GameState.Game)
            {
                cursor.Update(gameTime);
                game.Update(gameTime);

                if (control.isControllerMode && control.Start)
                    ChangeState(GameState.Menu);
                else if (!control.isControllerMode && Keyboard.GetState().IsKeyDown(Keys.Escape))
                    ChangeState(GameState.Menu);
            }
            else if (state == GameState.Menu)
                menu.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();
            if (state == GameState.Game && !IsMouseVisible)
                cursor.Draw(gameTime, spriteBatch);
            else if (state == GameState.Menu)
                menu.Draw(gameTime);


            if(state == GameState.Game)
            {
                game.Draw(gameTime);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }


        public void ChangeState(GameState state)
        {
            this.state = state;
        }
    }
}
