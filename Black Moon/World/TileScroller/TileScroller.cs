using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using ContentImporters.MapHandler;
using System.Linq;
using Microsoft.Xna.Framework;

namespace BlackMoon.World.TileScroller
{
    public class TileScroller
    {
        private Tile[,,] map;
        private MapData mapData;
        private Dictionary<string, Tileset> tilesets;
        private Dictionary<int, TileModsTuple> tileProperties;
        public int tileWidth { get; private set; }
        public int tileHeight { get; private set; }

        public Dictionary<int, Tile> numToTile = new Dictionary<int, Tile>();

        public struct TileModsTuple
        {
            public List<TileMods.Attributes> attributes;
            public TileMods.Shape shape;
            public Tileset tileset;
            public int tileNum;
            public int tilesetNum;
        }

        public TileScroller()
        {
            tilesets = new Dictionary<string, Tileset>();
            tileWidth = 256;
            tileHeight = 256;
        }

        public int MapWidth { get; set; }
        public int MapHeight { get; set; }
        public int LayerCount { get; set; }

        public Tile getTile(int x, int y, int z)
        {
            return map[x, y, z];
        }

        public void loadMap(ContentManager Content, MapData mapData)
        {
            this.mapData = mapData;
            loadTilesets(Content);
            loadMap();
        }

        void DrawTiles(VertexPositionTexture[] tile, GraphicsDevice g, Camera camera, Texture2D texture = null)
        {
            BasicEffect effect = new BasicEffect(g);
            effect.View = camera.ViewMatrix;
            effect.Projection = camera.ProjectionMatrix;

            effect.TextureEnabled = texture != null;
            effect.Texture = texture;

            foreach (var pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                g.DrawUserPrimitives(
                            PrimitiveType.TriangleList,
                    tile,
                    0,
                    tile.Length / 3);
            }
        }

        public void DrawMap(GraphicsDevice g, Camera camera)
        {
            float width = 1;//20,20 tileSizes
            float height = 1;
            float viewDistance = 60;
            float skyViewDistance = 50;

            Dictionary<string, VertexPositionTexture[]> tiles = new Dictionary<string, VertexPositionTexture[]>();

            Vector3 position = camera.position;
            Dictionary<Texture2D, List<VertexPositionTexture>> vertexByTexture = new Dictionary<Texture2D, List<VertexPositionTexture>>();

            TileShape.Direction8 dir8 = TileShape.Direction8.North; //#DEBUG CODE
            TileShape.Direction4 dir4 = TileShape.Direction4.North; //#DEBUG CODE

            for (float x = (int)-position.X - viewDistance; x < ((int)-position.X + viewDistance); x++)
            {
                for (float y = (int)position.Y - viewDistance; y < ((int)position.Y + viewDistance); y++)
                {
                    for (float z = 0; z < LayerCount; z++)
                    {
                        if ((x >= MapWidth) || x < 0 || (y >= MapHeight) || y < 0 || z >= LayerCount || z < 0)
                        {
                            continue;
                        }

                        Tile t = getTile((int)x, (int)y, (int)z);
                        if (t == null || t.tileData.tileNum == 0)
                        {
                            continue;
                        }

                        VertexPositionTexture[] shape = new VertexPositionTexture[0];
                        switch (t.tileData.shape)
                        {
                            case TileMods.Shape.Cube:
                                shape = TileShape.Wall(x, y, z, width, height, dir8);
                                break;
                            case TileMods.Shape.Wall:
                                shape = TileShape.Ramp(x, y, z, width, height, t.tileData.incline == 0f ? 1f : t.tileData.incline, dir4);
                                break;
                            case TileMods.Shape.Flat:
                            default:
                                shape = TileShape.Flat(x, y, z, width, height);
                                break;
                        }

                        if (dir8 == TileShape.Direction8.NorthWest)
                        {
                            shape = TileShape.Cube(x, y, z, width, height);
                        }

                        //#DEBUG CODE
                        if (dir8 == TileShape.Direction8.NorthWest)
                        {
                            dir8 = TileShape.Direction8.North;
                        }
                        else
                        {
                            dir8++;
                        }

                        //#DEBUG CODE
                        if (dir4 == TileShape.Direction4.West)
                        {
                            dir4 = TileShape.Direction4.North;
                        }
                        else
                        {
                            dir4++;
                        }

                        if (!numToTile.Keys.Contains(t.tileData.tileNum))
                        {
                            Texture2D fullTexture = t.tileData.tileset.data.texture;
                            Rectangle sourceRect =
                                    new Rectangle(
                                        (t.tileData.tilesetNum % t.tileData.tileset.data.columns) * t.tileData.tileset.data.tileWidth,
                                        (t.tileData.tilesetNum / t.tileData.tileset.data.columns) * t.tileData.tileset.data.tileHeight,
                                        t.tileData.tileset.data.tileWidth,
                                        t.tileData.tileset.data.tileHeight
                                    );
                            Texture2D tileTexture = new Texture2D(g, sourceRect.Width, sourceRect.Height);
                            Color[] data = new Color[sourceRect.Width * sourceRect.Height];
                            fullTexture.GetData(0, sourceRect, data, 0, data.Length);
                            tileTexture.SetData(data);
                            t.tileData.texture = tileTexture;
                            numToTile.Add(t.tileData.tileNum, t);
                        }

                        if (!vertexByTexture.Keys.Contains(numToTile[t.tileData.tileNum].tileData.texture))
                        {
                            vertexByTexture.Add(numToTile[t.tileData.tileNum].tileData.texture, new List<VertexPositionTexture>());
                        }
                        vertexByTexture[numToTile[t.tileData.tileNum].tileData.texture].AddRange(shape);
                    }
                }
            }

            foreach (Texture2D t in vertexByTexture.Keys)
            {
                DrawTiles(vertexByTexture[t].ToArray(), g, camera, t);
            }
        }

        private void loadMap()
        {
            Console.WriteLine("Loading map...");
            MapWidth = mapData.json["width"];
            MapHeight = mapData.json["height"];
            LayerCount = Enumerable.Count(mapData.json["layers"]);
            map = new Tile[MapWidth, MapHeight, LayerCount];

            int currentLayer = 0;
            foreach (dynamic layer in mapData.json["layers"])
            {
                for (int h = 0; h < MapHeight; h++)
                {
                    for (int w = 0; w < MapWidth; w++)
                    {
                        TileData data = new TileData();
                        TileModsTuple tileMods;
                        data.tileNum = layer["data"][w + (h * MapWidth)];

                        if (data.tileNum == 0)
                        {
                            continue;
                        }

                        if (tileProperties.TryGetValue(data.tileNum, out tileMods))
                        {
                            data.attributes = tileMods.attributes;
                            data.tileset = tileMods.tileset;
                            data.tilesetNum = tileMods.tilesetNum;
                            data.shape = tileMods.shape;
                        }
                        map[w, h, currentLayer] = new Tile(data);
                    }
                }
                currentLayer++;
            }
        }

        private void addProperties(int tileNum, dynamic properties)
        {
            TileModsTuple data;
            if (tileProperties.TryGetValue(tileNum, out data))
            {
                data.attributes = new List<TileMods.Attributes>();
                Console.WriteLine(properties);
                foreach (dynamic property in properties)
                {
                    string propertyName = property.Name;
                    switch (propertyName)
                    {
                        case "type":
                            string propertyValue = property.Value;
                            data.attributes.Add((TileMods.Attributes)Enum.Parse(typeof(TileMods.Attributes), propertyValue, true));
                            break;
                        case "shape":
                            string shapeValue = property.Value;
                            Console.WriteLine(tileNum + " : " + shapeValue);
                            data.shape = ((TileMods.Shape)Enum.Parse(typeof(TileMods.Shape), shapeValue, true));
                            break;
                    }
                }

                tileProperties[tileNum] = data;
            }
        }

        private void loadTilesets(ContentManager Content)
        {
            //Load tile properties
            tileProperties = new Dictionary<int, TileModsTuple>();


            foreach (dynamic tilesetData in mapData.json["tilesets"])
            {
                Console.WriteLine("Loading tileset: " + tilesetData.name);

                TilesetData data = new TilesetData();
                data.firstgid = tilesetData.firstgid;

                data.columns = tilesetData.columns;
                data.imageHeight = tilesetData.imageheight;
                data.imageWidth = tilesetData.imagewidth;
                data.margin = tilesetData.margin;
                data.name = tilesetData.name;
                data.spacing = tilesetData.spacing;
                data.tileCount = tilesetData.tilecount;
                data.texture = Content.Load<Texture2D>(data.name);
                data.tileHeight = tilesetData.tileheight;
                data.tileWidth = tilesetData.tilewidth;
                data.type = tilesetData.type;


                Tileset tileset = new Tileset(data);
                tilesets.Add(data.name, tileset);

                //Add baseline properties for every tile
                int tileCount = data.tileCount;
                Console.WriteLine("tile count {0}", tileCount);
                for (int i = 0; i < tileCount; i++)
                {
                    TileModsTuple properties = new TileModsTuple();
                    properties.tileset = tileset;
                    properties.tileNum = i + data.firstgid;
                    properties.tilesetNum = i;
                    tileProperties.Add(properties.tileNum, properties);
                    Console.WriteLine("added tileset data '{1}' to tile: {0} ", properties.tileNum, tileset.data.name);
                }
                if (tilesetData["tileproperties"] != null)
                {
                    foreach (dynamic tile in tilesetData["tileproperties"])
                    {
                        int tileNum;
                        int.TryParse(tile.Name, out tileNum);
                        tileNum += data.firstgid;
                        Console.WriteLine("Added property {0} for tilenum {1}", tile.Value, tileNum);
                        addProperties(tileNum, tile.Value);
                    }
                }
            }
        }
    }
}