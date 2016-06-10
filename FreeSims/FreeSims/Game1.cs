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

        Control control;
        Menu menu;
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
            mainFont = Content.Load<SpriteFont>("font");

            control = new Control(isControllerMode);
            menu = new Menu(spriteBatch, control, mainFont, this);
            // TODO: use this.Content to load your game content here

            cursor = new Cursor(Window.ClientBounds.Width, Window.ClientBounds.Height, cursorSprite, control);
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
            if (!IsMouseVisible && state == GameState.Menu)
                IsMouseVisible = true;
            else if (IsMouseVisible && state == GameState.Game)
                IsMouseVisible = false;
            // Allows the game to exit

            if (state == GameState.Game)
                cursor.Update(gameTime);
            else if (state == GameState.Menu)
                menu.Update(gameTime);

            // TODO: Add your update logic here

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
            if (state == GameState.Game)
                cursor.Draw(gameTime, spriteBatch);
            else if (state == GameState.Menu)
                menu.Draw(gameTime);
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }


        public void ChangeState(GameState state)
        {
            this.state = state;
        }
    }
}
