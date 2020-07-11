using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using MonoCake.Rendering;
using MonoCake;

namespace MonoCake.Objects
{
    public class GameObject : BasicObject
    {

        #region Private Fields

        double w, h;
        double scaleW = 1, scaleH = 1;
        double alpha = 1;
        private int currentFrame = 0;

        private double alphaEffect_start = 1;
        private double alphaEffect_speed = 0;

        #endregion

        #region Properties

        #region Visual Representation
        public Texture2D Img { get; protected set; }

        public double X { get; set; }
        public double Y { get; set; }

        public double Ox { get; set; }
        public double Oy { get; set; }

        public double W { get => w; set => w = Math.Max(0, value); }
        public double H { get => h; set => h = Math.Max(0, value); }

        public double ScaleW { get => scaleW; set => scaleW = Math.Max(0, value); }
        public double ScaleH { get => scaleH; set => scaleH = Math.Max(0, value); }

        public double Alpha { get => alpha; set => alpha = Tools.Limit(0, 1, value); }
        public bool IsVisable { get; set; } = true;
        public Color Color { get; set; } = Color.White;

        public double Rotation { get; set; }

        public SpriteEffects Orientation { get; set; } = SpriteEffects.None;
        #endregion

        #region RenderParams

        #endregion

        #region Animation
        public List<Frame> Frames { get; } = new List<Frame>();
        public int CurrentFrame { get => currentFrame; set => currentFrame = (int)Tools.Limit(0, Frames.Count, value); }
        public bool IsPlaying { get; private set; } = true;
        public bool StopAtTheLastFrame { get; set; } = false;

        public Rectangle RenderArea { get; set; } = new Rectangle();
        public Dictionary<string, Texture2D> Imgs { get; } = new Dictionary<string, Texture2D>();
        #endregion

        #endregion

        #region Constructors
        public GameObject()
        {
            //SetLayer(LayerManager.defaultLayer);
        }

        public GameObject(Texture2D texture) : this()
        {
            SetImg(texture);
        }

        public GameObject(Texture2D texture, string name) : this(texture)
        {
            this.Name = name;
        }
        #endregion

        #region Methods

        #region Animation
        public GameObject AddImg(Texture2D tex, string name)
        {
            Imgs.Add(name, tex);
            if (Img == null)
            {
                SetImg(tex);
            }
            return this;
        }

        public GameObject AddFrame(string imageName, Rectangle crop = new Rectangle(), int length = 1)
        {
            AddFrame(imageName, length, crop);
            return this;
        }
        public GameObject AddFrame(string imageName, int length = 1, Rectangle crop = new Rectangle())
        {
            for (int i = 0; i < length; i++)
            {
                Frames.Add(new Frame(imageName, crop));
            }
            return this;
        }

        public GameObject SpriteSheetToAnimation(string imageName, int imgW, int imgH, int repeatX, int repeatY, int length = 1, int stopAt = -1)
        {
            bool end = false;
            for (int j = 0; j < repeatY; j++)
            {
                for (int i = 0; i < repeatX; i++)
                {

                    if (i * repeatX + j == stopAt)
                    {
                        end = true;
                        break;
                    }

                    AddFrame(imageName, length, new Rectangle(i * imgW, j * imgH, imgW, imgH));
                }
                if (end)
                {
                    break;
                }
            }
            return this;
        }

        public GameObject Stop()
        {
            IsPlaying = false;
            return this;
        }

        public GameObject Play()
        {
            IsPlaying = true;
            return this;
        }

        public GameObject GotoAndStop(int frame)
        {
            CurrentFrame = frame;
            Stop();
            return this;
        }

        public GameObject GotoAndPlay(int frame)
        {
            CurrentFrame = frame;
            Play();
            return this;
        }

        public GameObject NextFrame()
        {
            CurrentFrame++;
            if (CurrentFrame >= Frames.Count)
            {
                CurrentFrame = 0;
                if (StopAtTheLastFrame)
                {
                    CurrentFrame = Frames.Count - 1;
                }
            }
            return this;
        }

        public GameObject PrevFrame()
        {
            CurrentFrame--;
            if (CurrentFrame < 0)
            {
                CurrentFrame = Frames.Count - 1;

            }
            return this;
        }

        public override void UpdateAnimation()
        {
            if (Frames.Count > 0)
            {
                FrameScript(CurrentFrame);
                SetImg(Imgs[Frames[CurrentFrame].imgName]);
                if (!Frames[CurrentFrame].crop.IsEmpty)
                {
                    RenderArea = Frames[CurrentFrame].crop;
                    SetSize(RenderArea.Width, RenderArea.Height);
                }
                if (IsPlaying)
                {
                    CurrentFrame++;
                }

                if (CurrentFrame < 0)
                {
                    CurrentFrame = 0;
                }

                if (CurrentFrame >= Frames.Count)
                {
                    CurrentFrame = 0;
                    if (StopAtTheLastFrame)
                    {
                        CurrentFrame = Frames.Count - 1;
                    }
                }

            }
        }

        public virtual void FrameScript(int n)
        {

        }


        #endregion

        #region Visual Representation

        public GameObject SetImg(Texture2D tex)
        {
            Img = tex;
            SetSize(Img.Width, Img.Height);
            RenderArea = new Rectangle(0, 0, Img.Width, Img.Height);
            if (Name == "")
            {
                Name = tex.ToString();
            }
            return this;
        }

        public GameObject SetSize(double nw, double nh)
        {
            W = nw;
            H = nh;
            return this;
        }
        public GameObject Scale(double s)
        {
            ScaleH = s;
            ScaleW = s;
            return this;
        }

        public GameObject SetXY(double newx, double newy)
        {
            X = newx;
            Y = newy;
            return this;
        }
        public GameObject SetXY(Point cords)
        {
            X = cords.X;
            Y = cords.Y;
            return this;
        }

        public GameObject SetXYtoMouse()
        {
            SetXY(KEY.MouseX, KEY.MouseY);
            return this;
        }

        public GameObject SetCenter(int x, int y)
        {
            Ox = x;
            Oy = y;
            return this;
        }

        public GameObject SetCenter(double xRatio, double yRatio)
        {
            Ox = xRatio * W;
            Oy = yRatio * H;
            return this;
        }

        public GameObject SetCenter(int pos)
        {
            //7 8 9
            //4 5 6
            //1 2 3
            pos = (int)Tools.Limit(1, 9, pos);
            Rectangle r;
            if (Frames.Count == 0)
            {
                r = new Rectangle((int)X, (int)Y, (int)(W), (int)(H));
            }
            else
            {
                r = new Rectangle((int)X, (int)Y, Frames[CurrentFrame].crop.Width, Frames[CurrentFrame].crop.Height);
            }

            switch (pos)
            {
                case 1:
                    Ox = 0;
                    Oy = r.Height;
                    break;
                case 2:
                    Ox = r.Width / 2;
                    Oy = r.Height;
                    break;
                case 3:
                    Ox = r.Width;
                    Oy = r.Height;
                    break;
                case 4:
                    Ox = 0;
                    Oy = r.Height / 2;
                    break;
                case 5:
                    Ox = r.Width / 2;
                    Oy = r.Height / 2;
                    break;
                case 6:
                    Ox = r.Width;
                    Oy = r.Height / 2;
                    break;
                case 7:
                    Ox = 0;
                    Oy = 0;
                    break;
                case 8:
                    Ox = r.Width / 2;
                    Oy = 0;
                    break;
                case 9:
                    Ox = r.Width;
                    Oy = 0;
                    break;
            }
            return this;
        }

        public Vector2 GetCenter()
        {
            return new Vector2((int)Ox, (int)Oy);
        }

        #endregion

        #region Logistics

        public override void Update()
        {
            base.Update();
        }

        public virtual void ReloadContent()
        {

        }

        public override void Destruct()
        {

            base.Destruct();
            ToRender = false;
            ToUpdate = false;
        }

        #endregion

        #region Rectangles and collisions
        public bool HitTestObjet(GameObject obj)
        {
            return GetRect().Intersects(obj.GetRect());
        }

        public bool HitTestPoint(int x, int y)
        {
            return GetRect().Intersects(new Rectangle(x, y, 0, 0));
        }

        public bool HitTestPoint(double x, double y)
        {
            return HitTestPoint((int)x, (int)y);
        }

        public Rectangle GetRect()
        {
            if (Frames.Count == 0)
            {
                return new Rectangle(
                    (int)(X - Ox * ScaleW),
                    (int)(Y - Oy * ScaleH),
                    (int)(W * ScaleW),
                    (int)(H * ScaleH)
                    );
            }
            else
            {
                return new Rectangle(
                    (int)(X - Ox * ScaleW),
                    (int)(Y - Oy * ScaleH),
                    (int)(Frames[CurrentFrame].crop.Width * ScaleW),
                    (int)(Frames[CurrentFrame].crop.Height * ScaleH)
                    );
            }
        }

        public GameObject GetAbsoulteObject()
        {
            GameObject go = new GameObject();

            go.X = (X * CurrentRenderParameters.ScaleW + CurrentRenderParameters.X);
            go.Y = (Y * CurrentRenderParameters.ScaleH + CurrentRenderParameters.Y);
            go.ScaleH = ScaleH * CurrentRenderParameters.ScaleH;
            go.ScaleW = ScaleW * CurrentRenderParameters.ScaleW;
            go.SetImg(this.Img);
            if (Frames.Count > 0)
            {
                go.AddImg(Imgs[Frames[currentFrame].imgName], Frames[currentFrame].imgName);
                go.AddFrame(Frames[currentFrame].imgName, 1, Frames[currentFrame].crop);
                go.UpdateAnimation();
            }

            go.Ox = Ox;
            go.Oy = Oy;
            go.Alpha = Alpha * CurrentRenderParameters.Alpha;

            return go;
        }

        public Rectangle GetAbsoluteRect()
        {
            if (Frames.Count == 0)
            {
                return new Rectangle
                {
                    X = (int)(X * CurrentRenderParameters.ScaleW + CurrentRenderParameters.X - (Ox * ScaleW)),
                    Y = (int)(Y * CurrentRenderParameters.ScaleH + CurrentRenderParameters.Y - (Oy * ScaleH)),
                    Width = (int)(W * ScaleW * CurrentRenderParameters.ScaleW),
                    Height = (int)(H * ScaleH * CurrentRenderParameters.ScaleH)
                };
            }
            else
            {
                return new Rectangle
                {
                    X = (int)(X * CurrentRenderParameters.ScaleW + CurrentRenderParameters.X - (Ox * ScaleW)),
                    Y = (int)(Y * CurrentRenderParameters.ScaleH + CurrentRenderParameters.Y - (Oy * ScaleH)),
                    Width = (int)(Frames[CurrentFrame].crop.Width * ScaleW * CurrentRenderParameters.ScaleW),
                    Height = (int)(Frames[CurrentFrame].crop.Height * ScaleH * CurrentRenderParameters.ScaleH)
                };
            }
        }
        #endregion

        #region Rendering
        public virtual void StandartRender()
        {
            var rp = CurrentRenderParameters;
            Rectangle renderRect = new Rectangle
            {
                X = (int)Math.Ceiling(X * rp.ScaleW + rp.X),
                Y = (int)Math.Ceiling(Y * rp.ScaleH + rp.Y),
                Width = (int)Math.Ceiling(W * ScaleW * rp.ScaleW),
                Height = (int)Math.Ceiling(H * ScaleH * rp.ScaleH)
            };

            /*  if (renderRect.Contains((int)KEY.MouseX, (int)KEY.MouseY))
              {
                  Color = Color.Yellow;
              }
              else
              {
                  Color = Color.White;
              }*/

            CakeEngine.spriteBatch.Draw(
               Img,
               renderRect,
               RenderArea,
               Color * (float)(Alpha * rp.Alpha),
               (float)Rotation,
               GetCenter(),
               Orientation,
               0f);
        }

        public override void Render()
        {
            var rp = CurrentRenderParameters;
            if (ToRender && IsVisable && rp.IsVisable)
            {
                if (Img != null)
                {
                    StandartRender();
                }
                else
                {
                    dwrite.line("Null texture");
                }
            }
            base.Render();
        }


        #endregion

        #region Miscellaneous

        public GameObject SetAlphaEffect(double startAt, double growSpeed)
        {
            alphaEffect_start = startAt;
            alphaEffect_speed = growSpeed;
            UpdateAlphaEffect();
            return this;
        }

        public GameObject UpdateAlphaEffect()
        {
            if (alphaEffect_start < 0)
            {
                Alpha = 0;
            }
            else if (alphaEffect_start > 1)
            {
                Alpha = 1;
            }
            else
            {
                Alpha = alphaEffect_start;
            }
            alphaEffect_start += alphaEffect_speed;
            return this;
        }

        public GameObject RemoveAlphaEffect()
        {
            alphaEffect_speed = 0;
            UpdateAlphaEffect();
            return this;
        }

        public override string ToString()
        {
            return Name + " x:" + X + " y:" + Y + " w:" + W + " h:" + H + " frame: " + CurrentFrame + " typeOf:" + GetType().ToString();
        }
        /// <summary>
        /// Only for backwards compatibility
        /// </summary>
        /// <example>Use GlobalContent</example>
        /// <returns></returns>
        public Texture2D Load(string path)
        {
            return GlobalContent.LoadImg(path);
        }

        #endregion


        #endregion

    }

    public class Frame
    {
        public string imgName;
        public Rectangle crop;

        public Frame(string imgName, Rectangle crop = new Rectangle())
        {
            this.imgName = imgName;
            this.crop = crop;
        }
    }
}