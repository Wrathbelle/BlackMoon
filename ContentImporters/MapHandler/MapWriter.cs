using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using Microsoft.Xna.Framework.Content.Pipeline;
using Newtonsoft.Json;

namespace ContentImporters.MapHandler
{
    [ContentTypeWriter]
    class MapWriter : ContentTypeWriter<MapData>
    {
        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return "ContentImporters.MapHandler.MapReader, ContentImporters";
        }

        protected override void Write(ContentWriter output, MapData value)
        {
            var json = JsonConvert.SerializeObject(value.json);
            output.Write(json);
        }

        public override string GetRuntimeType(TargetPlatform targetPlatform)
        {
            return typeof(MapData).AssemblyQualifiedName;
        }
    }
}
