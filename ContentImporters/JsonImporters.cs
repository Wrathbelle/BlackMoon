using System.IO;
using Microsoft.Xna.Framework.Content.Pipeline;
using Newtonsoft.Json;

namespace ContentImporters
{
    [ContentImporter(".spring", ".json", DefaultProcessor = "MapProcessor", DisplayName = "Json & Spring Importer")]
    public class MapImporter : ContentImporter<dynamic>
    {
        public override dynamic Import(string filename, ContentImporterContext context)
        {
            context.Logger.LogMessage("Importing spring file: {0}", filename);

            using (var streamReader = new StreamReader(filename))
            {
                return JsonConvert.DeserializeObject<dynamic>(streamReader.ReadToEnd());
               // JsonSerializer serializer = new JsonSerializer();
               // return (dynamic)serializer.Deserialize(streamReader, typeof(dynamic));
            }
        }
    }
}
