using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace BlackMoon.Sprite
{
    public class Sprite
	{
		public Texture2D texture;
        public float baseSpeed;
        public float speedMod;
		public Rectangle sourceRectangle;
		public Rectangle destRectangle;
		public Color color = Color.White;
		public float rotation;
		public Vector2 origin;
		public Vector2 scale;
		public SpriteEffects effects;
        
        public float speed
        {
            get
            {
                return baseSpeed * speedMod;
            }
        }

        public Vector2 position { get; set; }

        public int X
        {
            get
            {
                return (int)position.X;
            }
        }

        public int Y
        {
            get
            {
                return (int)position.Y;
            }
        }

        public int Z
        {
            get
            {
                return (int)position.Y;
            }
        }
    }
}

