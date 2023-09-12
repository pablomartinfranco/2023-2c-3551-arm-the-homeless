using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TGC.Components;
using TGC.Constants;

namespace TGC.MonoGame.TP
{
    public class TanksGame : Game
    {
        public TanksGame()
        {
            Graphics = new GraphicsDeviceManager(this)
            {
                IsFullScreen = false,
                PreferredBackBufferWidth = 1280,
                PreferredBackBufferHeight = 720,
                // SynchronizeWithVerticalRetrace = false,
                // GraphicsProfile = GraphicsProfile.HiDef,
                // PreferMultiSampling = true,
                // HardwareModeSwitch = false,
                // PreferredDepthStencilFormat = DepthFormat.Depth24Stencil8,
                // PreferredBackBufferFormat = SurfaceFormat.Color,
            };
            Content.RootDirectory = Assets.DIRECTORY;
            IsMouseVisible = true;
        }

        private readonly GraphicsDeviceManager Graphics;
        private Camera Camera { get; set; }
        private SpriteBatch SpriteBatch { get; set; }
        private Tank Tank { get; set; }
        private Effect Effect { get; set; }
        private Terrain Terrain { get; set; }
        private Grid Grid { get; set; }
        public HUD HUD { get; set; }

        protected override void Initialize()
        {
            // GraphicsDevice.RasterizerState = new RasterizerState { CullMode = CullMode.None };

            Camera = new Camera(GraphicsDevice);

            Terrain = new Terrain(GraphicsDevice);

            Grid = new Grid(GraphicsDevice);

            HUD = new HUD(Content);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            Tank = new Tank(Content.Load<Model>(Assets.MODELS + "t90/t90"), Vector3.Zero);

            Effect = Content.Load<Effect>(Assets.EFFECTS + "BasicShader");

            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();

            Camera.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Para dibujar le modelo necesitamos pasarle informacion que el efecto esta esperando.
            // Effect.Parameters["View"].SetValue(View);
            // Effect.Parameters["Projection"].SetValue(Projection);
            // Effect.Parameters["DiffuseColor"].SetValue(Color.DarkBlue.ToVector3());

            Terrain.Draw(Camera);

            Grid.Draw(Camera);

            Tank.Draw(Camera);

            // GraphicsDevice.Clear(ClearOptions.DepthBuffer, Color.Black, 1.0f, 0);
            // GraphicsDevice.BlendState = BlendState.AlphaBlend;
            // GraphicsDevice.SetRenderTarget(null);

            HUD.Draw(SpriteBatch, gameTime);

            GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            base.Draw(gameTime);
        }

        protected override void UnloadContent()
        {
            Content.Unload();

            base.UnloadContent();
        }
    }
}