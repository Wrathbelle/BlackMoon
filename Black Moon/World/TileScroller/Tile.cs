using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace BlackMoon.World.TileScroller
{
    public class Tile
    {
        public TileData tileData;

        public bool hasAttribute(TileMods.Attributes attribute)
        {
            return tileData.attributes.Contains(attribute);
        }

        public Tile(TileData tile)
        {
            this.tileData = tile;
        }
    }

    public struct TileData
    {
        public Texture2D texture;
        public int tileNum;
        public Vector2 position;
        public List<TileMods.Attributes> attributes;
        public TileMods.Shape shape;
        public float incline;
        public Tileset tileset;
        public int tilesetNum;
    };

}

