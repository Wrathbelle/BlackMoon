using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlackMoon.Interface
{
    public abstract class UIComponent
    {
        public UIComponent(UIProperties objectSettings)
        {
            this.objectSettings = objectSettings;
        }

        public UIProperties objectSettings;

        public abstract void Draw(SpriteBatch sb);
        public abstract void Update(double deltaTime);

        public struct UIProperties
        {
            public Rectangle bounds;
            public bool visible;
            public Color color;
            public string textureName;
        }
    }
}
