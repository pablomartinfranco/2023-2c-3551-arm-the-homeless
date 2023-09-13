using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ArmTheHomeless.Components
{
    public class Grid
    {
        private readonly GraphicsDevice GraphicsDevice;
        private readonly BasicEffect BasicEffect;
        private VertexPositionColor[] vertices;

        public Grid(GraphicsDevice graphicsDevice)
        {
            GraphicsDevice = graphicsDevice;
            BasicEffect = new BasicEffect(graphicsDevice);

            // Initialize the grid vertices
            InitializeGrid();
        }

        private void InitializeGrid()
        {
            // Define the size and spacing of the grid
            float gridSize = 200.0f;
            int numLines = 21; // Number of grid lines (adjust as needed)

            vertices = new VertexPositionColor[numLines * 4];

            // Calculate the step size between grid lines
            float step = gridSize / (numLines - 1);

            // Calculate the positions of the grid lines
            for (int i = 0; i < numLines; i++)
            {
                float x = -gridSize / 2 + i * step;
                float z = -gridSize / 2 + i * step;

                vertices[i * 4 + 0] = new VertexPositionColor(new Vector3(x, 0, -gridSize / 2), Color.Gray);
                vertices[i * 4 + 1] = new VertexPositionColor(new Vector3(x, 0, gridSize / 2), Color.Gray);

                vertices[i * 4 + 2] = new VertexPositionColor(new Vector3(-gridSize / 2, 0, z), Color.Gray);
                vertices[i * 4 + 3] = new VertexPositionColor(new Vector3(gridSize / 2, 0, z), Color.Gray);
            }
        }

        public void Draw(Camera camera)
        {
            BasicEffect.World = Matrix.Identity;
            BasicEffect.View = camera.View;
            BasicEffect.Projection = camera.Projection;

            foreach (var pass in BasicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, vertices, 0, vertices.Length / 2);
            }
        }
    }
}
