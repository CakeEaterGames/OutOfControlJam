using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoCake.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoCake.Other
{
    public class Editor : BasicObject
    {
        TextField info = new TextField();
        TextField Controls = new TextField();

        public Editor()
        {
            Controls.text = "";
            info.AddUR(this);
        }

        public void UpdateInfo()
        {
            info.text =
                "\nDebug mode" +
                "\nObject: " + index + ": " + CurrentDebugObject.ToString() +
                "\nParameter: " + currentParam +
                "\nEdit power: " + power;
        }


        public List<BasicObject> startDebugObjects = new List<BasicObject>();

        public List<BasicObject> debugObjects = new List<BasicObject>();
        public BasicObject CurrentDebugObject = new BasicObject();

        public int index = 0;
        param currentParam = param.X;
        public double power = 1;

        public BasicObject MainObject;

        public bool isEnabled = false;

        public void TurnOn()
        {
            isEnabled = true;
            startDebugObjects.Clear();
            MainObject.ChainCollectObjects(startDebugObjects);
            CollectObjects();
            MainObject.RemoveUpdate();
        }

        public void TurnOff()
        {
            GameObject go;
            foreach (var bo in debugObjects)
            {
                if (bo is GameObject)
                {
                    go = (GameObject)bo;
                    go.Color = Color.White;
                }
            }
            isEnabled = false;
            startDebugObjects.Clear();
            debugObjects.Clear();
            MainObject.AddUpdate();
        }

        public void GenerateEdited()
        {
            CollectObjects();
            for (int i = 0; i < debugObjects.Count; i++)
            {

            }
        }

        public void CollectObjects(bool onlyGameObj = false, bool onlyOnMouse = false)
        {
            GameObject go;
            foreach (var bo in debugObjects)
            {
                if (bo is GameObject)
                {
                    go = (GameObject)bo;
                    go.Color = Color.White;
                }
            }

            debugObjects.Clear();
            MainObject.ChainCollectObjects(debugObjects);
            for (int i = 0; i < debugObjects.Count; i++)
            {
                BasicObject bo = debugObjects[i];
                if (bo is GameObject)
                {
                    go = (GameObject)bo;
                    if (onlyOnMouse)
                    {
                        if (!go.GetAbsoluteRect().Contains((int)KEY.MouseX, (int)KEY.MouseY))
                        {
                            debugObjects.RemoveAt(i);
                            i--;
                        }
                    }
                }
                else
                {
                    if (onlyGameObj)
                    {
                        debugObjects.RemoveAt(i);
                        i--;
                    }
                }
            }
        }

        public override void Update()
        {

            if (KEY.IsDown(Keys.U) && KEY.LClick)
            {
                TurnOn();
            }
            if (KEY.IsDown(Keys.U) && KEY.RClick)
            {
                TurnOff();
            }

            UpdateControls();
            UpdateParam();
            EditParam();
            UpdateInfo();
        }

        public void UpdateControls()
        {

            if (KEY.IsDown(Keys.Z) && KEY.LClick)
            {
                CollectObjects(KEY.IsDown(Keys.X), KEY.IsDown(Keys.C));
            }
            if (KEY.IsDown(Keys.LeftShift) && KEY.WheelUp)
            {
                if (CurrentDebugObject is GameObject)
                {
                    var a = (GameObject)CurrentDebugObject;
                    a.Color = Color.White;
                }
                index++;
            }
            if (KEY.IsDown(Keys.LeftShift) && KEY.WheelDown)
            {
                if (CurrentDebugObject is GameObject)
                {
                    var a = (GameObject)CurrentDebugObject;
                    a.Color = Color.White;
                }
                index--;
            }

            if (debugObjects.Count > 0)
            {
                if (index < 0) index = debugObjects.Count - 1;
                index %= debugObjects.Count;
                CurrentDebugObject = debugObjects[index];
            }
            if (CurrentDebugObject is GameObject)
            {
                var go = (GameObject)CurrentDebugObject;
                go.Color = Color.Yellow;
            }

            if (KEY.IsDown(Keys.LeftControl) && KEY.LClick)
            {
                power *= 10;
            }
            if (KEY.IsDown(Keys.LeftControl) && KEY.RClick)
            {
                power /= 10;
            }
        }

        public void EditParam()
        {
            double pow = 0;
            if (KEY.WheelUp)
            {
                pow = power;
            }
            if (KEY.WheelDown)
            {
                pow = -power;
            }

            if (pow != 0)
                if (KEY.IsDown(Keys.LeftControl))
                {
                    if (CurrentDebugObject is GameObject)
                    {
                        var go = (GameObject)CurrentDebugObject;

                        switch (currentParam)
                        {
                            case param.X:
                                go.X += pow;
                                break;
                            case param.Y:
                                go.Y += pow;
                                break;
                            case param.W:
                                go.W += pow;
                                break;
                            case param.H:
                                go.H += pow;
                                break;
                            case param.SW:
                                go.ScaleW += pow;
                                break;
                            case param.SH:
                                go.ScaleH += pow;
                                break;
                            case param.S:
                                go.ScaleW += pow;
                                go.ScaleH += pow;
                                break;
                            case param.Alpha:
                                go.Alpha += pow;
                                break;
                            case param.Rot:
                                go.Rotation += pow;
                                break;
                            case param.Frame:
                                if (pow < 1)
                                {
                                    go.Stop();
                                    go.PrevFrame();
                                }
                                else
                                {
                                    go.Stop();
                                    go.NextFrame();
                                }
                                break;

                        }
                    }

                    switch (currentParam)
                    {
                        case param.RX:
                            CurrentDebugObject.BaseRenderParameters.X += pow;
                            break;
                        case param.RY:
                            CurrentDebugObject.BaseRenderParameters.Y += pow;
                            break;

                        case param.RSW:
                            CurrentDebugObject.BaseRenderParameters.ScaleW += pow;
                            break;
                        case param.RSH:
                            CurrentDebugObject.BaseRenderParameters.ScaleH += pow;
                            break;
                        case param.RS:
                            CurrentDebugObject.BaseRenderParameters.ScaleW += pow;
                            CurrentDebugObject.BaseRenderParameters.ScaleH += pow;
                            break;
                        case param.RAlpha:
                            CurrentDebugObject.BaseRenderParameters.Alpha += pow;
                            break;

                    }
                }
        }


        public void UpdateParam()
        {
            if (KEY.IsDown(Keys.LeftShift))
            {
                if (KEY.IsDown(Keys.X)) currentParam = param.X;
                if (KEY.IsDown(Keys.Y)) currentParam = param.Y;
                if (KEY.IsDown(Keys.W)) currentParam = param.W;
                if (KEY.IsDown(Keys.H)) currentParam = param.H;

                if (KEY.IsDown(Keys.A)) currentParam = param.Alpha;
                if (KEY.IsDown(Keys.R)) currentParam = param.Rot;
                if (KEY.IsDown(Keys.F)) currentParam = param.Frame;

                if (KEY.IsDown(Keys.S)) currentParam = param.S;
                if (KEY.IsDown(Keys.D)) currentParam = param.SW;
                if (KEY.IsDown(Keys.E)) currentParam = param.SH;

                if (KEY.IsDown(Keys.Space))
                {
                    if (KEY.IsDown(Keys.X)) currentParam = param.RX;
                    if (KEY.IsDown(Keys.Y)) currentParam = param.RY;

                    if (KEY.IsDown(Keys.A)) currentParam = param.RAlpha;

                    if (KEY.IsDown(Keys.S)) currentParam = param.RS;
                    if (KEY.IsDown(Keys.D)) currentParam = param.RSW;
                    if (KEY.IsDown(Keys.E)) currentParam = param.RSH;
                }
            }
        }

        enum param
        {
            X, Y, W, H, S, SW, SH, Alpha, Rot, Frame,
            RX, RY, RS, RSW, RSH, RAlpha,
        }

    }
}
