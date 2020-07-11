using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoCake.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoCake.Effects
{
    public class NegativeEffect
    {
        private double strength = 1;
        public double Strength { get => strength; set => strength = Tools.Limit(0, 1, value); }

        private Effect negativeEffect;
        public NegativeEffect(Effect fx)
        {
            negativeEffect = fx;

        }

        public RenderTarget2D ApplyEffect(RenderTarget2D tex, RenderTarget2D output)
        {
            RenderManager.SetTarget(output);
            RenderManager.ClearTarget();

            negativeEffect.Parameters["amount"].SetValue((float)Strength);
            RenderManager.SpriteBatchBegin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, negativeEffect);

            CakeEngine.spriteBatch.Draw(tex, new Rectangle(0, 0, tex.Width, tex.Height), Color.White);

            RenderManager.SpriteBatchEnd();

            return output;
        }

    }
}
