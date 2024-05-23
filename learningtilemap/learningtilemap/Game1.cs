using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace learningtilemap
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private float timerDuration = 0.5f;
        private float timer;
        private Block currentBlock;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferHeight = 1080,
                PreferredBackBufferWidth = 1920
            };
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            Library.tileMap = Map.CreateMap(Library.width, Library.height);

            timer = timerDuration;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            Library.tileTexture = Library.CreateTexture(GraphicsDevice, Library.tileSize, Library.tileSize, pixel => Color.White);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (currentBlock != null)
            {
                if (timer >= 0)
                {
                    // When a spesifed time has gone by run move for current block

                    currentBlock.MoveDown();

                    timer = timerDuration;
                }
                else
                {
                    timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            for (int x = 0; x < Library.width; x++)
            {
                for (int y = 0; y < Library.height; y++)
                {
                    Library.tileMap[x, y].Draw(_spriteBatch);
                }
            }

            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}