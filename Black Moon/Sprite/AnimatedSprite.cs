using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace BlackMoon.Sprite
{
    public class AnimatedSprite : Sprite
	{
		public double timeElapsed {get;set;}
		public double frameTime {get;set;}

		public int currentFrame {get;set;}
		public int totalFrames {get;set;}

		public int rows {get;set;}
		public int columns {get;set;}

        public Vector2 velocity { get; set; }
        public float gravity = 0.25f;

        public int frameWidth
        {
            get
            {
                return texture.Width / columns;
            }
        }

        public int frameHeight
        {
            get
            {
                return texture.Height / rows;
            }
        }

        private Dictionary<SpriteMods.Direction, int> dirToFrameStart { get; set; }

        public SpriteMods.Direction faceDirection;
        

        private int dirToFrame() {
            return dirToFrameStart[faceDirection];
        }

        public void setFaceDirectionFrames(int N, int NE, int E, int SE, int S, int SW, int W, int NW, int None)
        {
            dirToFrameStart = new Dictionary<SpriteMods.Direction, int>();
            dirToFrameStart[SpriteMods.Direction.N] = N;
            dirToFrameStart[SpriteMods.Direction.NE] = NE;
            dirToFrameStart[SpriteMods.Direction.E] = E;
            dirToFrameStart[SpriteMods.Direction.SE] = SE;
            dirToFrameStart[SpriteMods.Direction.S] = S;
            dirToFrameStart[SpriteMods.Direction.SW] = SW;
            dirToFrameStart[SpriteMods.Direction.W] = W;
            dirToFrameStart[SpriteMods.Direction.NW] = NW;
        }

		public void Update(double deltaTime){
            timeElapsed += deltaTime;
            if (timeElapsed >= frameTime )
            {
                currentFrame++;
                if (currentFrame == totalFrames)
                {
                    currentFrame = 0;
                }
                timeElapsed = 0;
            }
		}

		public void Draw (SpriteBatch sb){
			int width = texture.Width / columns;
			int height = texture.Height / rows;
			int row = (int)((float)currentFrame / (float)columns);
			int column = currentFrame % columns;

			sourceRectangle = new Rectangle (width * column, (height * row) + (height * dirToFrame()), width, height);
			destRectangle = new Rectangle ((int)position.X - frameWidth/2, (int)position.Y - frameHeight, width, height);

			//sb.Begin ();
			sb.Draw (texture, destRectangle, sourceRectangle, color);
			//sb.End ();
		}
	}
}