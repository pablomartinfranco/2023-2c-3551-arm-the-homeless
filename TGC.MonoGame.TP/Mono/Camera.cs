using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TGC.Components
{
    public class Camera
    {
        private const float DISTANCE = 400f;
        private const float SPEED = 2f;
        private const float ANGLE_X = 45f;
        
        private readonly GraphicsDevice GraphicsDevice;
        public Matrix World { get; private set; }
        public Matrix View { get; private set; }
        public Matrix Projection { get; private set; }

        public Camera(GraphicsDevice graphicsDevice)
        {
            GraphicsDevice = graphicsDevice;
            World = Matrix.Identity;
            View = Matrix.CreateLookAt((Vector3.UnitX + Vector3.UnitY + Vector3.UnitZ) * DISTANCE, Vector3.Zero, Vector3.Up);
            // View = Matrix.CreateLookAt(Vector3.UnitX * DISTANCE, Vector3.Zero, Vector3.Up);
            // View *= Matrix.CreateRotationX(MathHelper.ToRadians(ANGLE_X));
            Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, GraphicsDevice.Viewport.AspectRatio, 0.1f, 1000.0f);
        }

        private Matrix Rotate(Keys key, GameTime gameTime, float speed = SPEED)
        {
            if (key == Keys.Right)
            {
                var position = View.Translation;
                View *= Matrix.CreateTranslation(-position);
                View *= Matrix.CreateRotationX(MathHelper.ToRadians(-ANGLE_X));
                View *= Matrix.CreateRotationY(speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
                View *= Matrix.CreateRotationX(MathHelper.ToRadians(ANGLE_X));
                View *= Matrix.CreateTranslation(position);
            }
            else if (key == Keys.Left)
            {
                var position = View.Translation;
                View *= Matrix.CreateTranslation(-position);
                View *= Matrix.CreateRotationX(MathHelper.ToRadians(-ANGLE_X));
                View *= Matrix.CreateRotationY(-speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
                View *= Matrix.CreateRotationX(MathHelper.ToRadians(ANGLE_X));
                View *= Matrix.CreateTranslation(position);
            }
            return View;
        }

        private Matrix Zoom(Keys key, GameTime gameTime, float speed = SPEED)
        {
            float zoomFactor = speed * 100 * (1 - (float)Math.Exp(-speed * (float)gameTime.ElapsedGameTime.TotalSeconds));

            if (key == Keys.Up) View *= Matrix.CreateTranslation(0, 0, zoomFactor);

            else if (key == Keys.Down) View *= Matrix.CreateTranslation(0, 0, -zoomFactor);

            return View;
        }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Left)) Rotate(Keys.Left, gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.Right)) Rotate(Keys.Right, gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.Up)) Zoom(Keys.Up, gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.Down)) Zoom(Keys.Down, gameTime);
        }

        public void Draw(Matrix view, Matrix projection) { }

        public void Move(Vector3 translation)
        {
            World *= Matrix.CreateTranslation(translation);
        }
    }
}