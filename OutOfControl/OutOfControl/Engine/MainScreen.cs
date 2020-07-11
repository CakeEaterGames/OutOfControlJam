using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoCake.Objects;
using MonoCake.Rendering;
using MonoCake;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using MonoCake.Other;

namespace OutOfControl
{
    public class MainScreen : Layer
    {

        readonly RenderTarget2D target;
        public static Main main;

        public MainScreen()
        {
            target = RenderManager.CreateTarget(name: "screen");
            RenderManager.PushTarget(target);


            main = new Main();
            main.AddUR(this);

            /*   Editor editor = new Editor();
               editor.MainObject = main;
               editor.AddUR(this);
               */
        }



        public override void Update()
        {
            ChainUpdate();
            Engine.UpdateAllObjects();
            if (KEY.IsDown(Keys.T) && KEY.LClick)
            {
                dwrite.line();
                dwrite.line("Tree");
                dwrite.line();
                ChainDrawTree("", true);
            }
            ChainUpdateRenderParameters();

        }



        public override void Render()
        {
            RenderManager.PushTarget(null);
            RenderManager.PushTarget(target);

            RenderManager.SetTarget(RenderManager.GetTarget());
            RenderManager.ClearTarget();
            RenderManager.SpriteBatchBeginPixels();

            UpdateRenderParameters(new RenderParameters());

            //All objects(nested and local) are rendered here
            ChainRender();
            Engine.RenderAllObjects();


            RenderManager.SpriteBatchEnd();
            RenderManager.PopTarget();

            RenderManager.SetTarget();
            RenderManager.ClearTarget(Engine.config.BgColor);

            RenderManager.SpriteBatchBeginPixels();

            Engine.spriteBatch.Draw(target, new Rectangle(Engine.offsetX, Engine.offsetY, Engine.screenW, Engine.screenH), Color.White);
            RenderManager.SpriteBatchEnd();

            RenderManager.PopTarget();
        }




        public void UpdateRenderParameters(RenderParameters rp)
        {
            //  main.SetRenderParameters(rp.CombineWith(RenderParameters));
        }
    }
}
