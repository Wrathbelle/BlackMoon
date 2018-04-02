using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BlackMoon
{
    public class Camera
    {
        // We need this to calculate the aspectRatio
        // in the ProjectionMatrix property.
        GraphicsDevice graphicsDevice;
        public Vector3 position = new Vector3(0, 0, 1); //0,20,3
        public Vector3 lookAtVectorBase = new Vector3(0, 1, -.5f);
        float angle;

        public Matrix ViewMatrix
        {
            get
            {
                var lookAtVector = lookAtVectorBase;
                // We'll create a rotation matrix using our angle
                var rotationMatrix = Matrix.CreateRotationZ(angle);
                // Then we'll modify the vector using this matrix:
                lookAtVector = Vector3.Transform(lookAtVector, rotationMatrix);
                lookAtVector += position;

                var upVector = Vector3.UnitZ;

                return Matrix.CreateLookAt(
                    position, lookAtVector, upVector);
            }
        }

        public Matrix ProjectionMatrix
        {
            get
            {
                float fieldOfView = MathHelper.ToRadians(90);//Microsoft.Xna.Framework.MathHelper.PiOver2;
                float nearClipPlane = .1f; //.1f - .00000001f and other memes break depth sorting. leave it alone
                float farClipPlane = 200; //200
                float aspectRatio = graphicsDevice.Viewport.Width / graphicsDevice.Viewport.Height;

                return Matrix.CreatePerspectiveFieldOfView(
                    fieldOfView, aspectRatio, nearClipPlane, farClipPlane);
            }
        }

        public Camera(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
        }

        public void Move(Vector3 moveVector, float angle = 0)
        {
            if (angle == 0)
            {
                angle = this.angle;
            }

            moveVector = Vector3.Transform(moveVector, Matrix.CreateRotationZ(angle));
            this.position += moveVector;
        }

        public void Update(GameTime gameTime)
        {
            // We'll be doing some input-based movement here
            /*KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.W))
            {
                var forwardVector = new Vector3(0, 1, 0);
                var rotationMatrix = Matrix.CreateRotationZ(angle);
                forwardVector = Vector3.Transform(forwardVector, rotationMatrix);

                const float unitsPerSecond = 3;

                this.position += forwardVector * unitsPerSecond *
                    (float)gameTime.ElapsedGameTime.TotalSeconds;

            }
            else if (ks.IsKeyDown(Keys.S))
            {
                var forwardVector = new Vector3(0, 1, 0);
                var rotationMatrix = Matrix.CreateRotationZ(angle);
                forwardVector = Vector3.Transform(forwardVector, rotationMatrix);

                const float unitsPerSecond = 3;

                this.position -= forwardVector * unitsPerSecond *
                    (float)gameTime.ElapsedGameTime.TotalSeconds;

            }

            if (ks.IsKeyDown(Keys.A))
            {
                angle += (float)gameTime.ElapsedGameTime.TotalSeconds * 3;
            }
            else if (ks.IsKeyDown(Keys.D))
            {
                angle -= (float)gameTime.ElapsedGameTime.TotalSeconds * 3;
            }

            if (ks.IsKeyDown(Keys.Q))
            {
                position.Z += (float)gameTime.ElapsedGameTime.TotalSeconds * 3;
            }
            else if (ks.IsKeyDown(Keys.E))
            {
                if ((position.Z - (float)gameTime.ElapsedGameTime.TotalSeconds * 3) > 0.5f)
                {
                    position.Z -= (float)gameTime.ElapsedGameTime.TotalSeconds * 3;
                }
                else
                {
                    position.Z = 0.5f;
                }

            }

            if (ks.IsKeyDown(Keys.Z))
            {
                if (lookAtVectorBase.Z < 1)
                    lookAtVectorBase.Z += .05f;
            }
            else if (ks.IsKeyDown(Keys.C))
            {
                if (lookAtVectorBase.Z > -1)
                    lookAtVectorBase.Z -= .05f;
            }



            if (prevState != ks)
            {
                Console.WriteLine(position);
            }

            this.prevState = ks;*/
        }
    }
}

