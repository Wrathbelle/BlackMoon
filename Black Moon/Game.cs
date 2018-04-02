using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ContentImporters.MapHandler;
using BlackMoon.World.TileScroller;
using BlackMoon.Player;
using BlackMoon.World;
using BlackMoon.Core;
using System;
using BlackMoon.Core.GameState;
using BlackMoon.Network.client;

namespace BlackMoon
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;

        public int SCREENWIDTH = 1366;
        public int SCREENHEIGHT = 768;

        public StateMachine state;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this) { PreferredBackBufferWidth = SCREENWIDTH, PreferredBackBufferHeight = SCREENHEIGHT, SynchronizeWithVerticalRetrace = true };
            Content.RootDirectory = "Content";
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
            this.Window.AllowAltF4 = false;
            this.IsFixedTimeStep = true;
            this.IsMouseVisible = true;

            state = new StateMachine();
            state.stateMap.Add("startMenu", new StartMenuState(this));
            state.stateMap.Add("optionsMenu", new OptionsMenuState(this));
            state.stateMap.Add("inGame", new InGameState(this));

            state.Change("inGame");
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            MemoryManager.TextureCache["TransparentWindow"] = Content.Load<Texture2D>("transFifth");
            MemoryManager.FontCache["Verdana"] = Content.Load<SpriteFont>("Verdana");
            SessionManager.Start();
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            MemoryManager.TextureCache.Clear();
            MemoryManager.FontCache.Clear();
            SessionManager.Stop();
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            // TODO: Add your update logic here

            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (deltaTime > 1)
                deltaTime = 1;

            state.Update(deltaTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.SpringGreen);
            state.Draw();
            base.Draw(gameTime);
        }
    }
}
