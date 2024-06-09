using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace learningtilemap
{
    public static class Data
    {
        public static Block currentBlock;

        public static Tile[,] tileMap;

        public static int width = 10, height = 15, tileSize = 64;

        public static Texture2D tileTexture;

        public static Texture2D CreateTexture(GraphicsDevice device, int width, int height, Func<int, Color> paint)
        {
            //initialize a texture
            Texture2D texture = new(device, width, height);

            //the array holds the color for each pixel in the texture
            Color[] data = new Color[width * height];
            for (int pixel = 0; pixel < data.Count(); pixel++)
            {
                //the function applies the color according to the specified pixel
                data[pixel] = paint(pixel);
            }

            //set the color
            texture.SetData(data);

            return texture;
        }
        public static bool InBounds(int x, int y)
        {
            return 0 <= y && y < tileMap.GetLength(1) && 0 <= x && x < tileMap.GetLength(0);
        }

        public static int GetRandomInt(int min, int max)
        {        
            Random random = new();
            int number = random.Next(min, max);
            return number;
        }

    }
}
