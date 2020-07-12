using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoCake;
using MonoCake.Objects;
using System;
using System.Collections.Generic;

namespace OutOfControl
{
    public class Engine : CakeEngine
    {
        public static MainScreen screen;


        public Engine()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //GlobalContent.a = 1;

            SoLoud.Soloud a = new SoLoud.Soloud();

        }

        public static Config cfg;

        protected override void Initialize()
        {
            self = this;
            staticContent = Content;
            staticDevice = GraphicsDevice;
            staticGraphics = graphics;
            staticDevice = GraphicsDevice;

            FullScreenH = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            FullScreenW = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;

            GlobalContent.Init(GetType().Namespace);

            config = new Config();
            cfg = (Config)config;
            config.Init();

            Fonts.LoadFonts();

            TextField.defaultFont = Content.Load<SpriteFont>("Fonts\\Font");
            AudioManager.Init();
            Sounds.Init();
            screen = new MainScreen();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }




        protected override void UnloadContent()
        {

        }

        protected override void OnExiting(object sender, EventArgs args)
        {
            AudioManager.Destruct();

            base.OnExiting(sender, args);
        }

        protected override void Update(GameTime gameTime)
        {
            //   if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //  Exit();

            KEY.Update();
            AudioManager.Update();



            screen.Update();




            base.Update(gameTime);
        }



        protected override void Draw(GameTime gameTime)
        {

            screen.Render();


            base.Draw(gameTime);
        }
    }
}
