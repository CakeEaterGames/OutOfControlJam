using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using MonoCake.Objects;

namespace MonoCake.Objects
{
    public class TextField : GameObject
    {
        public static SpriteFont defaultFont;

        public SpriteFont font = defaultFont;
        public String text;

        public Color color;

        public Rectangle hitbox = new Rectangle();
        public double alignX = 0;

        private double scaleW = 1;
        private double scaleH = 1;
        public new double ScaleW { get => scaleW; set { scaleW = Math.Max(0, value); scaleW = scaleH; } }
        public new double ScaleH { get => scaleH; set { scaleH = Math.Max(0, value); scaleW = scaleH; } }

        public enum Align
        {
            left,
            center,
            right,
            custom
        }
        public Align alignment = Align.left;

        public TextField()
            : this("", Color.Black, defaultFont)
        {


        }

        public TextField(String tex)
            : this(tex, Color.Black, defaultFont)
        { }

        public TextField(String tex, Color col)
            : this(tex, col, defaultFont)
        { }

        public TextField(String tex, Color col, SpriteFont fnt)
        {
            text = tex;
            font = fnt;
            color = col;
        }

        public new void Scale(double scale)
        {
            ScaleH = scale;
            ScaleW = scale;
        }

        public override void StandartRender()
        {
            var rp = CurrentRenderParameters;
            Vector2 renderVect = GetRenderVect();
            renderVect.X = (float)(renderVect.X * rp.ScaleW + rp.X);
            renderVect.Y = (float)(renderVect.Y * rp.ScaleH + rp.Y);

            CakeEngine.spriteBatch.DrawString(
                font,
                text,
                renderVect,
                color * (float)(this.Alpha * rp.Alpha),
                (float)Rotation,
                new Vector2((float)Ox, (float)Oy),
                (float)(ScaleH * rp.ScaleH),
                SpriteEffects.None,
                1
            );


        }


        public override void Render()
        {

            var rp = CurrentRenderParameters;
            if (ToRender && IsVisable && rp.IsVisable)
            {
                StandartRender();
            }
        }

        public Vector2 GetRenderVect()
        {
            switch (alignment)
            {
                case Align.left:
                    alignX = 0;
                    break;
                case Align.right:
                    alignX = -font.MeasureString(text.ToString()).X * ScaleH;
                    break;
                case Align.center:
                    alignX = -(font.MeasureString(text.ToString()).X / 2) * ScaleH;
                    break;
            }

            return new Vector2(
               (float)(X + alignX),
               (float)(Y)
               );
        }

    }
}
