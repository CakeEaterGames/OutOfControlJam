using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoCake
{
    public class RenderParameters
    {
        public double X { get; set; } = 0;
        public double Y { get; set; } = 0;

        public double ScaleW { get; set; } = 1;
        public double ScaleH { get; set; } = 1;

        public double Alpha { get; set; } = 1;
        public bool IsVisable { get; set; } = true;

        public double Rotation { get; set; } = 0;
        public Vector2 RotationPoint { get; set; } = new Vector2();
        public double MultyRenderX { get; set; } = 0;
        public double MultyRenderY { get; set; } = 0;



        public RenderParameters CombineWith(RenderParameters rp)
        {
            RenderParameters rp2 = new RenderParameters
            {
                Alpha = Alpha * rp.Alpha,
                X = X * rp.ScaleW + rp.X,
                Y = Y * rp.ScaleH + rp.Y,
                ScaleW = ScaleW * rp.ScaleW,
                ScaleH = ScaleH * rp.ScaleH,
                IsVisable = IsVisable && rp.IsVisable
            };
            return rp2;
        }

        public void SetXYtoMouse()
        {
            X = KEY.MouseX;
            Y = KEY.MouseY;
        }
    }
}
