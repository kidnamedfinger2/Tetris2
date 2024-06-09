using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace learningtilemap
{
    public class Tile
    {
        public bool isFilled;
        protected Texture2D texture;
        public Color color; // Determines block type with color
        public int gridX, gridY;
        public Vector2 position;

        public Tile(Vector2 _position, Color _color, int x, int y)
        {
            texture = Data.tileTexture;
            gridX = x;
            gridY = y;
            position = _position;
            color = _color;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, color);
        }
    }

    public class Block
    {
        private Tile[] tiles = new Tile[4];
        public bool isRemoved = false;
        private float timer = 0.2f;
        private Color color;

        public Block(BlockID Id)
        {
            switch (Id)
            {
                case BlockID.IBlock:
                    color = Color.LightBlue;
                    tiles[0] = new Tile(Vector2.Zero, color, -1, 0);
                    tiles[1] = new Tile(Vector2.Zero, color, 0, 0);
                    tiles[2] = new Tile(Vector2.Zero, color, 1, 0);
                    tiles[3] = new Tile(Vector2.Zero, color, 2, 0);
                    break;
                case BlockID.JBlock:
                    color = Color.DarkBlue;
                    tiles[0] = new Tile(Vector2.Zero, color, 0, 0);
                    tiles[1] = new Tile(Vector2.Zero, color, 0, 1);
                    tiles[2] = new Tile(Vector2.Zero, color, 1, 1);
                    tiles[3] = new Tile(Vector2.Zero, color, 2, 1);
                    break;
                case BlockID.LBlock:
                    color = Color.Orange;
                    tiles[0] = new Tile(Vector2.Zero, color, 0, 1);
                    tiles[1] = new Tile(Vector2.Zero, color, 1, 1);
                    tiles[2] = new Tile(Vector2.Zero, color, 2, 1);
                    tiles[3] = new Tile(Vector2.Zero, color, 2, 0);
                    break;
                case BlockID.OBlock:
                    color = Color.Yellow;
                    tiles[0] = new Tile(Vector2.Zero, color, 0, 0);
                    tiles[1] = new Tile(Vector2.Zero, color, 0, 1);
                    tiles[2] = new Tile(Vector2.Zero, color, 1, 1);
                    tiles[3] = new Tile(Vector2.Zero, color, 1, 0);
                    break;
                case BlockID.SBlock:
                    color = Color.LightGreen;
                    tiles[0] = new Tile(Vector2.Zero, color, 0, 1);
                    tiles[1] = new Tile(Vector2.Zero, color, 1, 1);
                    tiles[2] = new Tile(Vector2.Zero, color, 1, 0);
                    tiles[3] = new Tile(Vector2.Zero, color, 2, 0);
                    break;
                case BlockID.TBlock:
                    color = Color.Purple;
                    tiles[0] = new Tile(Vector2.Zero, color, 1, 0);
                    tiles[1] = new Tile(Vector2.Zero, color, 0, 1);
                    tiles[2] = new Tile(Vector2.Zero, color, 1, 1);
                    tiles[3] = new Tile(Vector2.Zero, color, 2, 1);
                    break;
                case BlockID.ZBlock:
                    color = Color.Red;
                    tiles[0] = new Tile(Vector2.Zero, color, 0, 0);
                    tiles[1] = new Tile(Vector2.Zero, color, 1, 0);
                    tiles[2] = new Tile(Vector2.Zero, color, 1, 1);
                    tiles[3] = new Tile(Vector2.Zero, color, 2, 1);
                    break;
                default:
                    break;
            }
            for (int i = 0; i < tiles.Length; i++)
            {
                tiles[i].gridX += Data.tileMap.GetLength(0) / 2 - 1;
                tiles[i].position = Data.tileMap[tiles[i].gridX, tiles[i].gridY].position;
            }
        }

        public void Update(GameTime gameTime)
        {
            if (Input.HasBeenPressed(Keys.A))
            {
                MoveInDirection(-1);
            }
            else if (Input.HasBeenPressed(Keys.D))
            {
                MoveInDirection(1);
            }

            if (timer <= 0)
            {
                MoveDown();

                timer = 0.2f;
            }
            else
            {
                timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

        }
        public bool CanMoveDown()
        {
            for (int i = 0; i < tiles.Length; i++)
            {
                // check if block is at bottom or if tile below is occupied

                if (tiles[i].gridY == Data.tileMap.GetLength(1)- 1)
                {
                    return false;
                }
                else
                {
                    if (Data.tileMap[tiles[i].gridX, tiles[i].gridY + 1].isFilled)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool CanMoveInDirection(int direction)
        {
            for (int i = 0; i < tiles.Length; i++)
            {
                // check if block is at bottom or if tile below is occupied

                if (!Data.InBounds(tiles[i].gridX + direction, tiles[i].gridY))
                {
                    return false;
                }
                else
                {
                    if (Data.tileMap[tiles[i].gridX + direction, tiles[i].gridY].isFilled)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public void MoveInDirection(int direction)
        {
            // Check if block can move towards direction

            #region Code for moveing block side to side
            if (CanMoveInDirection(direction))
            {
                for (int i = 0; i < tiles.Length; i++)
                {
                    // Here we add set the tiles position to the next tile in the desired direction

                    tiles[i].position = Data.tileMap[tiles[i].gridX + (1 * direction), tiles[i].gridY].position;
                    tiles[i].gridX = tiles[i].gridX + (1 * direction);
                }
            }
            #endregion
        }

        public void MoveDown()
        {
            // Check below block if their is a block or block is at bottom

            #region Code for moveing block down
            if (CanMoveDown())
            {
                for (int i = 0; i < tiles.Length; i++)
                {
                    // Here we add set the tiles position to the tile below in the tile map

                    tiles[i].position = Data.tileMap[tiles[i].gridX, tiles[i].gridY + 1].position;
                    tiles[i].gridY = tiles[i].gridY + 1;
                }
            }
            else
            {
                PlaceBlockOnMap();
            }
            #endregion
        }

        public void PlaceBlockOnMap()
        {
            for (int i = 0; i < tiles.Length; i++)
            {
                Data.tileMap[tiles[i].gridX, tiles[i].gridY] = tiles[i];
                Data.tileMap[tiles[i].gridX, tiles[i].gridY].isFilled = true;
            }
            isRemoved = true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < tiles.Length; i++)
            {
                tiles[i].Draw(spriteBatch);
            }
        }
    }
    public enum BlockID
    {
        IBlock,
        JBlock,
        LBlock,
        OBlock,
        SBlock,
        TBlock,
        ZBlock,
    }
}
