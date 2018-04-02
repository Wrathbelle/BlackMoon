using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace BlackMoon.World.TileScroller
{
    public class Tileset
    {
        public TilesetData data;


        public Tileset(TilesetData data)
        {
            this.data = data;
        }
    }

    public struct TilesetData
    {
        public int firstgid;

        public int columns;
        public int imageHeight;
        public int imageWidth;
        public int margin;
        public string name;
        public int spacing;
        public int tileCount;
        public int tileHeight;
        public int tileWidth;
        public string type;

        public Texture2D texture;

        public Dictionary<int, List<TileMods>> tileProperties;
    };
}
