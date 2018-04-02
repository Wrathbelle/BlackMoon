using System;
using Microsoft.Xna.Framework.Graphics;

namespace BlackMoon.Interface
{
    public class Button : UIComponent
    {
        public Button(ButtonSettings settings) : base(settings.properties)
        {

        }

        public override void Draw(SpriteBatch sb)
        {
            throw new NotImplementedException();
        }

        public override void Update(double deltaTime)
        {
            throw new NotImplementedException();
        }

        public struct ButtonSettings
        {
            public UIProperties properties;
            public string text;
        }
    }
}
