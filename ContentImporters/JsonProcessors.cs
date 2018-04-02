using Microsoft.Xna.Framework.Content.Pipeline;
using System;
using ContentImporters.MapHandler;
using ContentImporters.PlayerHandler;

namespace ContentImporters
{
    [ContentProcessor(DisplayName = "Map Processor")]
    public class MapProcessor : ContentProcessor<dynamic, MapData>
    {
        public override MapData Process(dynamic input, ContentProcessorContext context)
        {
            try
            {
                context.Logger.LogMessage("Processing map data");
                var output = new MapData(input);
                return output;
            }
            catch (Exception ex)
            {
                context.Logger.LogMessage("Error {0}", ex);
                throw;
            }
        }
    }


    [ContentProcessor(DisplayName = "Player Processor")]
    public class PlayerProcessor : ContentProcessor<dynamic, PlayerData>
    {
        public override PlayerData Process(dynamic input, ContentProcessorContext context)
        {
            try
            {
                context.Logger.LogMessage("Processing player data");
                var output = new PlayerData(input);
                return output;
            }
            catch (Exception ex)
            {
                context.Logger.LogMessage("Error {0}", ex);
                throw;
            }
        }
    }
}
