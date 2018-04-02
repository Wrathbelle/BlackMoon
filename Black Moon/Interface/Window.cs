using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using BlackMoon.Core;

namespace BlackMoon.Interface
{
    public class Window : UIComponent
    {
        public WindowSettings settings { get; set; }
        public List<UIComponent> children { get; set; }

        public Window(WindowSettings settings) : base(settings.properties)
        {
            this.settings = settings;
        }

        public override void Update(double deltaTime)
        {
            //Update something

            foreach(UIComponent ui in children)
            {
                ui.Update(deltaTime);
            }
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(MemoryManager.TextureCache[settings.properties.textureName], settings.properties.bounds, settings.properties.color);

            foreach(UIComponent ui in children)
            {
                ui.Draw(sb);
            }
        }

        public struct WindowSettings
        {
            public UIProperties properties;
            public string title;
            public bool closeable;
        };
    }
}
