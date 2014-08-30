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
using TetrisGame.Main;
using TetrisGame.Blocks;
using TetrisGame.Drawable;
using TetrisGame.Drawable.Blocks;

namespace TetrisGame
{

    public class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //Textures
        private Texture2D menuTexture;
        private Texture2D gridTexture;
        private Texture2D anyKeyLabel;
        private Texture2D pauseLabel;
        private Texture2D gameOverLabel;
        private Texture2D blueBrickTexture;
        private Texture2D greenBrickTexture;
        private Texture2D purpleBrickTexture;
        private Texture2D yellowBrickTexture;
        private Texture2D orangeBrickTexture;
        //fonts
        private SpriteFont font;
        //Game objects
        Background background = null;
        GameState gameState = new GameState();
        SongPlayer player;
        BlockGenerator generator;
        private Grid grid;
        //utils
        KeyboardState oldState;
        float lbSeconds = 0;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 748;
            graphics.PreferredBackBufferWidth = 340;
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
            base.Initialize();
            background = new Background(menuTexture);
            gameState.state = GameState.MENU;
            grid = new Grid(generator, gameState, player);
            player.playMenuSong();

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            loadContent();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            //do nothing
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            KeyboardState newState = Keyboard.GetState();
            //mute sound
            if (newState.IsKeyDown(Keys.M) && oldState.IsKeyUp(Keys.M))
            {
                player.mute();
            }
            oldState = newState;
            //menu
            if (GameState.MENU.Equals(gameState.state))
            {

                if (newState.IsKeyDown(Keys.Space))
                {
                    newState = new KeyboardState();
                    gameState.state = GameState.PLAYING;
                    player.playSoundtrack();
                }
            }

            //playing
            if (GameState.PLAYING.Equals(gameState.state)
                || GameState.PAUSED.Equals(gameState.state))
            {
                grid.handleInput(newState, gameTime);
                grid.update(gameTime);
            }

            //gameover
            if (GameState.GAME_OVER.Equals(gameState.state))
            {
                player.stop();
                if (newState.IsKeyDown(Keys.Space))
                {
                    newState = new KeyboardState();
                    grid.reset();
                    gameState.state = GameState.PLAYING;
                    player.playSoundtrack();
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            if (GameState.MENU.Equals(gameState.state))
            {
                drawBackground(gameTime);
            }

            if (GameState.PLAYING.Equals(gameState.state))
            {
                drawGrid();
            }
            if (GameState.PAUSED.Equals(gameState.state))
            {
                drawPause();
            }

            if (GameState.GAME_OVER.Equals(gameState.state))
            {
                drawGameOver();
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void drawGameOver()
        {
            background.texture = gridTexture;
            background.draw(spriteBatch);
            grid.draw(spriteBatch);
            background.draw(spriteBatch, 0.7f);
            spriteBatch.Draw(gameOverLabel, new Rectangle(40, 270, 270, 80), Color.White);
            spriteBatch.DrawString(font, "Score: " + grid.score, new Vector2(120, 350), Color.White);
        }

        private void drawPause()
        {
            background.texture = gridTexture;
            background.draw(spriteBatch);
            grid.draw(spriteBatch);
            spriteBatch.DrawString(font, "Score: " + grid.score, new Vector2(10, 5), Color.White);
            spriteBatch.DrawString(font, "Level: " + grid.level, new Vector2(230, 5), Color.White);
            background.draw(spriteBatch, 0.7f);
            spriteBatch.Draw(pauseLabel, new Rectangle(90, 270, 170, 80), Color.White);
        }

        private void drawGrid()
        {
            background.texture = gridTexture;
            background.draw(spriteBatch);
            grid.draw(spriteBatch);
            spriteBatch.DrawString(font, "Score: " + grid.score, new Vector2(10, 5), Color.White);
            spriteBatch.DrawString(font, "Level: " + grid.level, new Vector2(230, 5), Color.White);
        }

        private void drawBackground(GameTime gameTime)
        {
            background.texture = menuTexture;
            background.draw(spriteBatch);
            showAnyKeyLabel(gameTime);
        }

        //shows "press any key" label
        private void showAnyKeyLabel(GameTime gameTime)
        {
            lbSeconds += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (lbSeconds >= 1.2f)
            {
                lbSeconds = 0;
            }
            else if (lbSeconds >= 0.7f)
            {

                spriteBatch.Draw(anyKeyLabel, new Rectangle(90, 400, 170, 80), Color.White);
            }
        }

        private void loadContent()
        {
            //load audio content
            player = new SongPlayer(this.Content);
            player.loadContent();
            //backhround textures
            menuTexture = Content.Load<Texture2D>("background/level-menu");
            gridTexture = Content.Load<Texture2D>("background/level-game");
            //label textures
            anyKeyLabel = Content.Load<Texture2D>("labels/menu-label");
            pauseLabel = Content.Load<Texture2D>("labels/pause-label");
            gameOverLabel = Content.Load<Texture2D>("labels/gameover-label");
            //fonts
            font = Content.Load<SpriteFont>("fonts/score");
            //brick textures
            blueBrickTexture = Content.Load<Texture2D>("bricks/brick-blue");
            greenBrickTexture = Content.Load<Texture2D>("bricks/brick-green");
            purpleBrickTexture = Content.Load<Texture2D>("bricks/brick-purple");
            yellowBrickTexture = Content.Load<Texture2D>("bricks/brick-yellow");
            orangeBrickTexture = Content.Load<Texture2D>("bricks/brick-orange");
            //brick textures list for random block generator
            List<Texture2D> textures = new List<Texture2D>();
            textures.Add(blueBrickTexture);
            textures.Add(greenBrickTexture);
            textures.Add(purpleBrickTexture);
            textures.Add(yellowBrickTexture);
            textures.Add(orangeBrickTexture);
            //init generator
            generator = new BlockGenerator(textures);

        }
    }
}
