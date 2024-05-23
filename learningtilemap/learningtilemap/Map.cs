using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace learningtilemap
{
    public static class Map
    {
        public static Tile[,] CreateMap(int width, int height)
        {
            Tile[,] tileMap = new Tile[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Vector2 position = new Vector2(x * Library.tileSize + x, y * Library.tileSize + y);

                    tileMap[x, y] = new Tile(position, Color.White, x, y);
                }
            }

            return tileMap;
        }
    }
}
