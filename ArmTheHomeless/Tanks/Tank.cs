using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ArmTheHomeless.Tanks
{
    public class Tank
    {
        private readonly Model Model;
        private Matrix World { get; set; }

        public Tank(Model model, Vector3 position)
        {
            Model = model;
            World = Matrix.CreateTranslation(position + Vector3.Up * 1.5f);
        }

        public void Update()
        {
            
        }

        public void Draw(Camera camera)
        {
            Model.Draw(World, camera.View, camera.Projection);
        }

        public void Move(Vector3 translation)
        {
            World *= Matrix.CreateTranslation(translation);
        }
    }
}