using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ArmTheHomeless.Tanks
{
    public class Terrain
    {
        private const int WIDTH = 250;
        private const int LENGTH = 250;
        private const float MAX_HEIGHT = 0.5f;
        private const float BIAS = 20;

        private readonly GraphicsDevice GraphicsDevice;
        private readonly BasicEffect BasicEffect;
        private readonly VertexPositionColor[] Vertices;
        private readonly short[] Indices;

        public Terrain(GraphicsDevice graphicsDevice)
        {
            GraphicsDevice = graphicsDevice;
            BasicEffect = new BasicEffect(GraphicsDevice)
            {
                VertexColorEnabled = true
            };

            Vertices = GenerateRandomVertices(WIDTH, LENGTH, MAX_HEIGHT);

            Indices = GenerateIndices(WIDTH, LENGTH);
        }

        private VertexPositionColor[] GenerateRandomVertices(int width, int length, float maxHeight)
        {
            var vertices = new VertexPositionColor[width * length];
            var random = new Random();

            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < length; z++)
                {
                    // Generate a random value between 0 and 1 with a bias towards lower values
                    float randomValue = (float)Math.Pow(random.NextDouble(), BIAS); // Adjust the exponent for the desired bias
                    float height = randomValue * maxHeight;

                    Color color = DetermineColorBasedOnHeight(height, maxHeight);
                    vertices[x + z * width] = new VertexPositionColor(
                        new Vector3(x - width / 2, height, z - length / 2), color
                    );
                }
            }

            return vertices;
        }

        private Color DetermineColorBasedOnHeight(float height, float maxHeight)
        {
            float normalizedHeight = height / maxHeight;

            if (normalizedHeight < 0.2f)
            {
                return Color.BurlyWood;
            }
            else if (normalizedHeight < 0.4f)
            {
                return Color.SaddleBrown;
            }
            else
            {
                return Color.Green;
            }
        }

        private short[] GenerateIndices(int width, int length)
        {
            var indices = new short[(width - 1) * (length - 1) * 6];
            int index = 0;

            for (short x = 0; x < width - 1; x++)
            {
                for (short z = 0; z < length - 1; z++)
                {
                    short lowerLeft = (short)(x + z * width);
                    short lowerRight = (short)((x + 1) + z * width);
                    short topLeft = (short)(x + (z + 1) * width);
                    short topRight = (short)((x + 1) + (z + 1) * width);

                    indices[index++] = lowerLeft;
                    indices[index++] = lowerRight;
                    indices[index++] = topLeft;
                    indices[index++] = topLeft;
                    indices[index++] = lowerRight;
                    indices[index++] = topRight;
                }
            }

            return indices;
        }

        public void Draw(Camera camera)
        {
            BasicEffect.World = Matrix.Identity;
            BasicEffect.View = camera.View;
            BasicEffect.Projection = camera.Projection;

            foreach (var pass in BasicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();

                GraphicsDevice.DrawUserIndexedPrimitives(
                    PrimitiveType.TriangleList, Vertices, 0,
                    Vertices.Length, Indices, 0, Indices.Length / 3
                );
            }
        }
    }
}
