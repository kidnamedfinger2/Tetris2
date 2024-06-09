using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                    Vector2 position = new(x * Data.tileSize + x, y * Data.tileSize + y);

                    tileMap[x, y] = new Tile(position, Color.White, x, y);
                }
            }

            return tileMap;
        }
        public static void CheckForFilledRow()
        {
            List<int> list = new();
            for (int y = Data.tileMap.GetLength(1)-1; y >= 0; y--)
            {
                for (int x = 0; x < Data.tileMap.GetLength(0); x++)
                {
                    if (!Data.tileMap[x, y].isFilled)
                    {
                        break;
                    }
                    else if (x == Data.tileMap.GetLength(0)-1)
                    {
                        list.Add(y);
                    }
                }
            }
            foreach (int y in list)
            {
                for (int x = 0; x < Data.tileMap.GetLength(0); x++)
                {
                    Data.tileMap[x, y].color = Color.White;
                    Data.tileMap[x, y].isFilled = false;
                }
            }
        }
    }
}
