using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace learningtilemap
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

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

            Data.tileMap = Map.CreateMap(Data.width, Data.height);

            //Data.currentBlock = new Block((BlockID)Data.GetRandomInt(0, Enum.GetNames(typeof(BlockID)).Length));
            Data.currentBlock = new Block(BlockID.OBlock);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            Data.tileTexture = Data.CreateTexture(GraphicsDevice, Data.tileSize, Data.tileSize, pixel => Color.White);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Input.GetState();

            if (Data.currentBlock != null)
            {
                Data.currentBlock.Update(gameTime);
                if (Data.currentBlock.isRemoved)
                {
                    Data.currentBlock = null;
                }
            }
            else
            {
                Map.CheckForFilledRow();
                //Data.currentBlock = new Block((BlockID)Data.GetRandomInt(0, Enum.GetNames(typeof(BlockID)).Length));
                Data.currentBlock = new Block(BlockID.OBlock);
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            for (int x = 0; x < Data.width; x++)
            {
                for (int y = 0; y < Data.height; y++)
                {
                    Data.tileMap[x, y].Draw(_spriteBatch);
                }
            }
            if (Data.currentBlock != null)
            {
                Data.currentBlock.Draw(_spriteBatch);
            }
  
            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}