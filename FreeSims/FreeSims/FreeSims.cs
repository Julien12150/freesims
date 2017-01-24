using System.IO;
using System.Linq;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Technochips.FreeSims.Game;
using Technochips.FreeSims.Game.Entity;
using Technochips.FreeSims.Game.Entity.Item;
using Technochips.FreeSims.Game.HumanMaker;

namespace Technochips.FreeSims
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class FreeSims : Microsoft.Xna.Framework.Game
	{
        bool isControllerMode;

        string lang;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Cursor cursor;

        Sprite sprites;
        ItemSprite itemSprites;

        Language language;

        Control control;
        Menu menu;
        public Game.Game game;
        public Option option;
        public HumanMaker humanMaker;

        public GameState state = GameState.Menu;

        public FreeSims(string[] args)
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            if (args.Length >= 1)
            {
				if (args.Contains("/c"))
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
			Console.WriteLine($"Running on {Environment.OSVersion.VersionString}");
			if (Directory.Exists($"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Path.DirectorySeparatorChar}Julien12150{ Path.DirectorySeparatorChar}FreeSims{ Path.DirectorySeparatorChar}"))
			{
				Directory.CreateDirectory($"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Path.DirectorySeparatorChar}Technochips{ Path.DirectorySeparatorChar}");
				Directory.Move($"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Path.DirectorySeparatorChar}Julien12150{ Path.DirectorySeparatorChar}FreeSims{ Path.DirectorySeparatorChar}",
				               $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Path.DirectorySeparatorChar}Technochips{ Path.DirectorySeparatorChar}FreeSims{ Path.DirectorySeparatorChar}");
			}
			if (File.Exists($"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Path.DirectorySeparatorChar}Technochips{Path.DirectorySeparatorChar}FreeSims{Path.DirectorySeparatorChar}config.txt"))
            {
                StreamReader file = new StreamReader($"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Path.DirectorySeparatorChar}Technochips{Path.DirectorySeparatorChar}FreeSims{Path.DirectorySeparatorChar}config.txt");
                string[] sFile = file.ReadToEnd().Split(Environment.NewLine.ToCharArray());
                foreach (string s in sFile)
                {
                    if (s.Split('=')[0] == "lang")
                    {
                        lang = s.Split('=')[1];
                    }
                }
                file.Close();
            }
            else
            {
                if (!Directory.Exists($"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Path.DirectorySeparatorChar}Technochips{Path.DirectorySeparatorChar}FreeSims{Path.DirectorySeparatorChar}"))
                {
                    Directory.CreateDirectory($"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Path.DirectorySeparatorChar}Technochips{Path.DirectorySeparatorChar}FreeSims{Path.DirectorySeparatorChar}");
                }
                StreamWriter file = new StreamWriter($"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Path.DirectorySeparatorChar}Technochips{Path.DirectorySeparatorChar}FreeSims{Path.DirectorySeparatorChar}config.txt");
                file.WriteLine("lang=en_US");
                file.Close();
            }

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

            sprites = new Sprite(Content);
            itemSprites = new ItemSprite(Content);

            control = new Control(isControllerMode, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            cursor = new Cursor(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, sprites, control);
            language = new Language(lang);

            menu = new Menu(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, spriteBatch, control, sprites, this, cursor, itemSprites, language);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
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
            else
                IsMouseVisible = true;

            if (state == GameState.Game)
            {
                game.Update(gameTime);

                if (control.isControllerMode && control.Start)
                    ChangeState(GameState.Menu);
                else if (!control.isControllerMode && Keyboard.GetState().IsKeyDown(Keys.Escape))
                    ChangeState(GameState.Menu);
            }
            else if (state == GameState.Menu)
                menu.Update(gameTime);
            else if (state == GameState.HumanMaking)
                humanMaker.Update(gameTime);
            else if (state == GameState.Option)
                option.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            if(state == GameState.Game)
            {
                game.Draw(gameTime, graphics.GraphicsDevice);
            }

            if (state == GameState.Menu)
                menu.Draw(gameTime);
            else if (state == GameState.HumanMaking)
                humanMaker.Draw(gameTime);
            else if (state == GameState.Option)
                option.Draw(gameTime);

            spriteBatch.End();

            base.Draw(gameTime);
        }


        public void ChangeState(GameState state)
        {
            this.state = state;
        }
    }
}
