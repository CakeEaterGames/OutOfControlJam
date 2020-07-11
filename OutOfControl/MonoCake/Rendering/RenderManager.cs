using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoCake.Rendering
{
    public static class RenderManager
    {
        public static Stack<RenderTarget2D> Targets { get; private set; } = new Stack<RenderTarget2D>();

        public static RenderTarget2D CurrentTraget { get; private set; }

        public static void PushTarget(RenderTarget2D rt)
        {
            Targets.Push(rt);
            CurrentTraget = rt;
        }
        public static RenderTarget2D PopTarget()
        {
            var rt = Targets.Pop();
            CurrentTraget = GetTarget();
            return rt;
        }
        public static RenderTarget2D GetTarget()
        {
            return Targets.Peek();
        }

        public static RenderTarget2D CreateTarget(int w = -1, int h = -1, bool permanent = false, string name = "")
        {
            if (w == -1 || h == -1)
            {
                w = CakeEngine.config.CoreWidth;
                h = CakeEngine.config.CoreHeight;
            }
            var usage = RenderTargetUsage.DiscardContents;
            if (permanent)
            {
                usage = RenderTargetUsage.PreserveContents;
            }
            var rt = new RenderTarget2D(CakeEngine.staticDevice, w, h, false, SurfaceFormat.Color, DepthFormat.None, 0, usage)
            {
                Name = name
            };
            return rt;
        }

        public static void SetTarget(RenderTarget2D rt)
        {
            CakeEngine.staticDevice.SetRenderTarget(rt);
        }
        public static void SetTarget()
        {
            SetTarget(GetTarget());
        }
        public static void ClearTarget(Color color)
        {
            CakeEngine.staticDevice.Clear(color);
        }
        public static void ClearTarget()
        {
            ClearTarget(Color.Transparent);
        }


        public static void SpriteBatchBegin()
        {
            SpriteBatchBeginPixels();
        }
        public static void SpriteBatchBegin
        (
            SpriteSortMode sortMode = SpriteSortMode.Deferred,
            BlendState blendState = null,
            SamplerState samplerState = null,
            DepthStencilState depthStencilState = null,
            RasterizerState rasterizerState = null,
            Effect effect = null,
            Nullable<Matrix> transformMatrix = null
        )
        {
            CakeEngine.spriteBatch.Begin(sortMode, blendState, samplerState, depthStencilState, rasterizerState, effect, transformMatrix);
        }

        public static void SpriteBatchBeginPixels()
        {
            CakeEngine.spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        }
        public static void SpriteBatchBeginBlur()
        {
            CakeEngine.spriteBatch.Begin(samplerState: SamplerState.LinearClamp);
        }

        public static void SpriteBatchEnd()
        {
            CakeEngine.spriteBatch.End();
        }


    }
}
