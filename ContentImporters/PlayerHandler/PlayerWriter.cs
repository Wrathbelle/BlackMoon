using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using Microsoft.Xna.Framework.Content.Pipeline;
using Newtonsoft.Json;

namespace ContentImporters.PlayerHandler
{
    [ContentTypeWriter]
    public class PlayerWriter : ContentTypeWriter<PlayerData>
    {
        protected override void Write(ContentWriter output, PlayerData value)
        {
            var json = JsonConvert.SerializeObject(value.json);
            output.Write(json);
        }

        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return "ContentImporters.PlayerHandler.PlayerReader";
        }

        public override string GetRuntimeType(TargetPlatform targetPlatform)
        {
            return typeof(PlayerData).AssemblyQualifiedName;
        }
    }
}
