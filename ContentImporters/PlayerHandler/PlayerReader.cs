using Microsoft.Xna.Framework.Content;
using Newtonsoft.Json.Linq;

namespace ContentImporters.PlayerHandler
{
    public class PlayerReader : ContentTypeReader<PlayerData>
    {
        protected override PlayerData Read(ContentReader input, PlayerData existingInstance)
        {
            var json = input.ReadString();
            dynamic jsonFile = JObject.Parse(json);
            return new PlayerData(jsonFile);
        }
    }
}
