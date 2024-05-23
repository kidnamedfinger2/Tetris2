using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace learningtilemap
{
    public class Tile
    {
        public bool isFilled;
        protected Texture2D texture;
        public Color color; // Determines block type with color
        public int mapPositionX, mapPositionY;
        public Vector2 position;

        public Tile(Vector2 _position, Color _color, int x, int y)
        {
            texture = Library.tileTexture;
            mapPositionX = x;
            mapPositionY = y;
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

        public Block()
        {

        }

        public bool CanMoveDown()
        {

            return false;
        }

        public bool CanMove(int direction)
        {

            return false;
        }

        public void Move(int direction)
        {
            // Check if block can move towards direction

            #region Code for moveing block side to side
            if (CanMove(direction))
            {
                for (int i = 0; i < tiles.Length; i++)
                {
                    // Here we add set the tiles position to the next tile in the desired direction

                    tiles[i].position = Library.tileMap[tiles[i].mapPositionX + (1 * direction), tiles[i].mapPositionY].position;
                    tiles[i].mapPositionY = tiles[i].mapPositionX + (1 * direction);
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

                    tiles[i].position = Library.tileMap[tiles[i].mapPositionX, tiles[i].mapPositionY + 1].position;
                    tiles[i].mapPositionY = tiles[i].mapPositionY + 1;
                }
            }
            #endregion
        }
    }
}
