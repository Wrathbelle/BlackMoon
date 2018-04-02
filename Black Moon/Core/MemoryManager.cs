using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace BlackMoon.Core
{
    public static class MemoryManager
    {
        public static bool DebugMode = false;
        public static Dictionary<string, Texture2D> TextureCache;
        public static Dictionary<string, SpriteFont> FontCache;

        static MemoryManager()
        {
            TextureCache = new Dictionary<string, Texture2D>();
            FontCache = new Dictionary<string, SpriteFont>();
        }
    }
}
