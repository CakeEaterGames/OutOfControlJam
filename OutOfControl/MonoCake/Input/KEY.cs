using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoCake.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoCake
{
    public class KEY
    {
        public static List<Keys> keysTyped = new List<Keys>();
        public static Keys[] keysDown;
        private static Keys[] keysCheck;

        private static MouseState m;

        private static bool lc = false, rc = false;
        private static bool lc1 = false, rc1 = false;


        public static double MouseX => Mouse.GetState().X * CakeEngine.config.CoreWidth / CakeEngine.screenW;
        public static double MouseY => Mouse.GetState().Y * CakeEngine.config.CoreHeight / CakeEngine.screenH;

        public static bool LClick => lc;
        public static bool RClick => rc;
        public static bool LDown
        {
            get
            {
                if (m.LeftButton == ButtonState.Pressed)
                {
                    return true;
                }
                return false;
            }
        }

        public static bool RDown
        {
            get
            {
                if (m.RightButton == ButtonState.Pressed)
                {
                    return true;
                }
                return false;
            }
        }


        public static int ScrollWheelValue { get { return m.ScrollWheelValue; } }
        public static int LastWheelValue = 0;
        public static bool WheelUp
        {
            get
            {
                if (ScrollWheelValue - LastWheelValue > 0)
                {
                    LastWheelValue = ScrollWheelValue;
                    return true;
                }
                return false;
            }
        }
        public static bool WheelDown
        {
            get
            {
                if (m.ScrollWheelValue - LastWheelValue < 0)
                {
                    LastWheelValue = ScrollWheelValue;
                    return true;
                }
                return false;
            }
        }

        public static void Update()
        {
            m = Mouse.GetState();

            keysDown = Keyboard.GetState().GetPressedKeys();


            keysTyped.Clear();
            for (int i = 0; i < keysDown.Length; i++)
            {
                if (!keysCheck.Contains<Keys>(keysDown[i]))
                {
                    keysTyped.Add(keysDown[i]);
                }

            }

            keysCheck = Keyboard.GetState().GetPressedKeys();


            lc = false;
            rc = false;
            if (m.LeftButton == ButtonState.Pressed && !lc1)
            {
                lc1 = true;
                lc = true;
            }
            if (m.LeftButton == ButtonState.Released)
            {
                lc1 = false;
            }

            if (m.RightButton == ButtonState.Pressed && !rc1)
            {
                rc1 = true;
                rc = true;
            }
            if (m.RightButton == ButtonState.Released)
            {
                rc1 = false;
            }

        }
        public static bool IsTyped(Keys key)
        {
            if (keysTyped != null && keysTyped.Count > 0)
            {
                return keysTyped.Contains<Keys>(key);
            }
            return false;

        }
        public static bool IsDown(Keys key)
        {
            if (keysDown != null && keysDown.Length > 0)
            {
                return keysDown.Contains<Keys>(key);
            }
            return false;
        }
        public static bool ClickedOn(GameObject go)
        {
            return ClickedOn(go.GetRect());
        }
        public static bool ClickedOn(Rectangle rect)
        {
            return (KEY.LClick && rect.Intersects(new Rectangle((int)MouseX, (int)MouseY, 0, 0)));
        }
    }
}
