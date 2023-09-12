using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using TGC.Constants;

namespace TGC.MonoGame.TP
{
    public class HUD
    {
        private readonly SpriteFont Font;

        public HUD(ContentManager content)
        {
            Font = content.Load<SpriteFont>(Assets.SPRITES_FONTS + "Arial");
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(Font, "FPS: " + (1.0f / gameTime.ElapsedGameTime.TotalSeconds).ToString("0.00"), new Vector2(10, 10), Color.White);
            spriteBatch.DrawString(Font, "Controls:", new Vector2(10, 40), Color.White);
            spriteBatch.DrawString(Font, "Left / Right Arrow: Rotate", new Vector2(10, 60), Color.White);
            spriteBatch.DrawString(Font, "Up / Down Arrow: Zoom", new Vector2(10, 80), Color.White);
            spriteBatch.End();
        }
    }
}

