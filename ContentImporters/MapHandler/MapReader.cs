using Microsoft.Xna.Framework.Content;
using Newtonsoft.Json.Linq;

namespace ContentImporters.MapHandler
{
    public class MapReader : ContentTypeReader<MapData>
    {
        protected override MapData Read(ContentReader input, MapData existingInstance)
        {
            var json = input.ReadString();
            dynamic jsonFile = JObject.Parse(json);
            return new MapData(jsonFile);
        }
    }
}
