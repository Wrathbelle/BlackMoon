using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackMoon.World.TileScroller
{
    public static class TileShape
    {
        public enum Direction8
        {
            North, East, South, West, NorthEast, SouthEast, SouthWest, NorthWest
        }

        public enum Direction4
        {
            North, East, South, West
        }

        #region 2d objects

        public static VertexPositionTexture[] Flat(float x, float y, float z, float width, float height)
        {
            VertexPositionTexture[] tileVerts = new VertexPositionTexture[6];
            //negative width, postive height. Don't know why, but it works. (Drunk)

            tileVerts[0].Position = new Vector3(-width * x, height * y, z);
            tileVerts[1].Position = new Vector3(-width * x, height + (height * y), z);
            tileVerts[2].Position = new Vector3(-width + (-width * x), height * y, z);

            tileVerts[3].Position = tileVerts[1].Position;
            tileVerts[4].Position = new Vector3(-width + (-width * x), height + (height * y), z);
            tileVerts[5].Position = tileVerts[2].Position;

            tileVerts[0].TextureCoordinate = new Vector2(0, 0);
            tileVerts[1].TextureCoordinate = new Vector2(0, 1);
            tileVerts[2].TextureCoordinate = new Vector2(1, 0);

            tileVerts[3].TextureCoordinate = tileVerts[1].TextureCoordinate;
            tileVerts[4].TextureCoordinate = new Vector2(1, 1);
            tileVerts[5].TextureCoordinate = tileVerts[2].TextureCoordinate;

            return tileVerts;
        }

        public static VertexPositionTexture[] RampWall(float x, float y, float z, float width, float height, Direction4 dir)
        {
            VertexPositionTexture[] tileVerts = new VertexPositionTexture[3];
            float Left = -width * x;
            float Right = -width + (-width * x);

            float Up = y;
            float Down = y + height;

            float Top = height + (z * height);
            float Bot = z * height;

            Vector3 origin = new Vector3();
            Vector3 verticalVector = new Vector3();
            Vector3 horizontalVector = new Vector3();

            switch (dir)
            {
                case Direction4.North:
                    origin = new Vector3(Left, Up, Bot);
                    verticalVector = new Vector3(Left, Up, Top);
                    horizontalVector = new Vector3(Left, Down, Bot);
                    break;
                case Direction4.East:
                    origin = new Vector3(Right, Up, Bot);
                    verticalVector = new Vector3(Right, Up, Top);
                    horizontalVector = new Vector3(Left, Up, Bot);
                    break;
                case Direction4.South:
                    origin = new Vector3(Right, Down, Bot);
                    verticalVector = new Vector3(Right, Down, Top);
                    horizontalVector = new Vector3(Right, Up, Bot);
                    break;
                case Direction4.West:
                    origin = new Vector3(Left, Down, Bot);
                    verticalVector = new Vector3(Left, Down, Top);
                    horizontalVector = new Vector3(Right, Down, Bot);
                    break;
            }

            tileVerts[0] = new VertexPositionTexture(origin, new Vector2(0, 0));
            tileVerts[1] = new VertexPositionTexture(verticalVector, new Vector2(0, 1));
            tileVerts[2] = new VertexPositionTexture(horizontalVector, new Vector2(1, 0));


            return tileVerts;
        }

        public static VertexPositionTexture[] Wall(float x, float y, float z, float width, float height, Direction8 dir)
        {
            VertexPositionTexture[] tileVerts = new VertexPositionTexture[6];

            Vector3 TopLeft = new Vector3();
            Vector3 BotRight = new Vector3();

            float Left = -width * x;
            float Right = -width + (-width * x);

            float Up = y;
            float Down = y + height;

            float Top = height + (z * height);
            float Bot = z * height;

            switch (dir)
            {
                case Direction8.North:
                case Direction8.NorthEast:
                    TopLeft = new Vector3(Left, Up, Top);
                    break;
                case Direction8.East:
                case Direction8.SouthEast:
                    TopLeft = new Vector3(Right, Up, Top);
                    break;
                case Direction8.South:
                case Direction8.SouthWest:
                    TopLeft = new Vector3(Right, Down, Top);
                    break;
                case Direction8.West:
                case Direction8.NorthWest:
                    TopLeft = new Vector3(Left, Down, Top);
                    break;
            }

            switch (dir)
            {
                case Direction8.NorthWest:
                case Direction8.North:
                    BotRight = new Vector3(Right, Up, Bot);
                    break;
                case Direction8.NorthEast:
                case Direction8.East:
                    BotRight = new Vector3(Right, Down, Bot);
                    break;
                case Direction8.SouthEast:
                case Direction8.South:
                    BotRight = new Vector3(Left, Down, Bot);
                    break;
                case Direction8.SouthWest:
                case Direction8.West:
                    BotRight = new Vector3(Left, Up, Bot);
                    break;
            }

            Vector3 BotLeft = new Vector3(TopLeft.X, TopLeft.Y, Bot);
            Vector3 TopRight = new Vector3(BotRight.X, BotRight.Y, Top);

            tileVerts[0].Position = TopLeft;
            tileVerts[1].Position = BotLeft;
            tileVerts[2].Position = TopRight;

            tileVerts[3].Position = BotLeft;
            tileVerts[4].Position = BotRight;
            tileVerts[5].Position = TopRight;


            tileVerts[0].TextureCoordinate = new Vector2(1, 0);
            tileVerts[1].TextureCoordinate = new Vector2(1, 1);
            tileVerts[2].TextureCoordinate = new Vector2(0, 0);

            tileVerts[3].TextureCoordinate = tileVerts[1].TextureCoordinate;
            tileVerts[4].TextureCoordinate = new Vector2(0, 1);
            tileVerts[5].TextureCoordinate = tileVerts[2].TextureCoordinate;

            return tileVerts;
        }

        #endregion


        #region 3d objects

        public static VertexPositionTexture[] Ramp(float x, float y, float z, float width, float height, float destZ, Direction4 dir)
        {
            VertexPositionTexture[] tileVerts = new VertexPositionTexture[6];

            Vector3 TopLeft = new Vector3();
            Vector3 BotRight = new Vector3();
            Vector3 TopRight = new Vector3();
            Vector3 BotLeft = new Vector3();



            float Left = -width * x;
            float Right = -width + (-width * x);

            float Up = y;
            float Down = y + height;

            float Top = destZ + (z * height);
            float Bot = z * height;


            switch (dir)
            {
                case Direction4.North:
                    TopLeft = new Vector3(Left, Up, Top);
                    BotRight = new Vector3(Right, Down, Bot);

                    TopRight = new Vector3(Right, Up, Top);
                    BotLeft = new Vector3(Left, Down, Bot);
                    break;
                case Direction4.East:
                    TopLeft = new Vector3(Right, Up, Top);
                    BotRight = new Vector3(Left, Down, Bot);

                    TopRight = new Vector3(Right, Down, Top);
                    BotLeft = new Vector3(Left, Up, Bot);
                    break;
                case Direction4.South:
                    TopLeft = new Vector3(Right, Down, Top);
                    BotRight = new Vector3(Left, Up, Bot);

                    TopRight = new Vector3(Left, Down, Top);
                    BotLeft = new Vector3(Right, Up, Bot);
                    break;
                case Direction4.West:
                    TopLeft = new Vector3(Left, Down, Top);
                    BotRight = new Vector3(Right, Up, Bot);

                    TopRight = new Vector3(Left, Up, Top);
                    BotLeft = new Vector3(Right, Down, Bot);
                    break;
            }

            tileVerts[0].Position = TopLeft;
            tileVerts[1].Position = BotLeft;
            tileVerts[2].Position = TopRight;

            tileVerts[3].Position = BotLeft;
            tileVerts[4].Position = BotRight;
            tileVerts[5].Position = TopRight;


            tileVerts[0].TextureCoordinate = new Vector2(0, 0);
            tileVerts[1].TextureCoordinate = new Vector2(0, 1);
            tileVerts[2].TextureCoordinate = new Vector2(1, 0);

            tileVerts[3].TextureCoordinate = tileVerts[1].TextureCoordinate;
            tileVerts[4].TextureCoordinate = new Vector2(1, 1);
            tileVerts[5].TextureCoordinate = tileVerts[2].TextureCoordinate;


            return tileVerts;
        }

        public static VertexPositionTexture[] Cube(float x, float y, float z, float width, float height)
        {
            List<VertexPositionTexture> walls = new List<VertexPositionTexture>();
            walls.AddRange(Wall(x, y, z, width, height, Direction8.North));
            walls.AddRange(Wall(x, y, z, width, height, Direction8.West));
            walls.AddRange(Wall(x, y, z, width, height, Direction8.South));
            walls.AddRange(Wall(x, y, z, width, height, Direction8.East));
            walls.AddRange(Flat(x, y, z, width, height));
            walls.AddRange(Flat(x, y, z + 1, width, height));


            return walls.ToArray();
        }

        #endregion
    }
}
