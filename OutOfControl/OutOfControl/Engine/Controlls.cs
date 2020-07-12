using Microsoft.Xna.Framework.Input;
using MonoCake;

namespace Pellicalo
{
    public static class Controls
    {
        public static bool PREJUMP => KEY.IsTyped(Keys.Space);
        public static bool JUMP => KEY.IsDown(Keys.Space);

        public static bool LEFT => KEY.IsDown(Keys.Left) || KEY.IsDown(Keys.A);
        public static bool RIGHT => KEY.IsDown(Keys.Right) || KEY.IsDown(Keys.D);
        //public static bool UP => KEY.IsDown(Keys.Up);
        //public static bool DOWN => KEY.IsDown(Keys.Down);

        public static bool INTERACT => KEY.IsTyped(Keys.Up) || KEY.IsTyped(Keys.W);
        public static bool PAUSE => KEY.IsTyped(Keys.Escape);

        public static bool NEXTITEM => KEY.IsTyped(Keys.X) || KEY.IsTyped(Keys.O);
        public static bool PREVITEM => KEY.IsTyped(Keys.Z) || KEY.IsTyped(Keys.P);

        public static bool CONFIRM => KEY.IsTyped(Keys.Enter);

        public static bool K1 => KEY.IsTyped(Keys.D1) || KEY.IsTyped(Keys.NumPad1);
        public static bool K2 => KEY.IsTyped(Keys.D2) || KEY.IsTyped(Keys.NumPad2);
        public static bool K3 => KEY.IsTyped(Keys.D3) || KEY.IsTyped(Keys.NumPad3);
        public static bool K4 => KEY.IsTyped(Keys.D4) || KEY.IsTyped(Keys.NumPad4);
        public static bool K5 => KEY.IsTyped(Keys.D5) || KEY.IsTyped(Keys.NumPad5);
        public static bool K6 => KEY.IsTyped(Keys.D6) || KEY.IsTyped(Keys.NumPad6);
        public static bool K7 => KEY.IsTyped(Keys.D7) || KEY.IsTyped(Keys.NumPad7);

        public static bool KPLUS => KEY.IsTyped(Keys.OemPlus) || KEY.IsTyped(Keys.Add);
        public static bool KMINUS => KEY.IsTyped(Keys.OemMinus) || KEY.IsTyped(Keys.Subtract);
        public static bool KMUL => KEY.IsTyped(Keys.Multiply) || KEY.IsTyped(Keys.D0);
        public static bool KDIV => KEY.IsTyped(Keys.Divide) || KEY.IsTyped(Keys.D9);
    }
}

/*

using Microsoft.Xna.Framework.Input;
using MonoCake;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenReboot 
{
    public static class Controlls
    {
        public static Dictionary<ControllNames, KeyDescription> Configuration;

        static Controlls()
        {
 
        Configuration.Add(ControllNames.Jump, new KeyDescription()         { Key =  { Keys.Space, Keys.Space },  Desc = "Jump" });
            Configuration.Add(ControllNames.Left, new KeyDescription()     { Key = Keys.A,      Desc = "Move Left" });
            Configuration.Add(ControllNames.Right, new KeyDescription()    { Key = Keys.D,      Desc = "Move Right" });
            Configuration.Add(ControllNames.Up, new KeyDescription()       { Key = Keys.W,      Desc = "Move Up" });
            Configuration.Add(ControllNames.Down, new KeyDescription()     { Key = Keys.S,      Desc = "Move Down" });
            Configuration.Add(ControllNames.Pause, new KeyDescription()    { Key = Keys.Escape, Desc = "Pause" });
            Configuration.Add(ControllNames.Interact, new KeyDescription() { Key = Keys.E,      Desc = "Interact" });
        }


        public enum ControllNames{
            Jump,
            Left,
            Right,
            Up,
            Down,
            Interact,
            Pause,
        }

        public static bool PREJUMP => KEY.IsTyped(Keys.Space);
        public static bool JUMP => KEY.IsDown(Keys.Space);

        public static bool LEFT => KEY.IsDown(Keys.A);
        public static bool RIGHT => KEY.IsDown(Keys.D);
        public static bool UP => KEY.IsDown(Keys.W);
        public static bool DOWN => KEY.IsDown(Keys.S);

        public static bool INTERACT => KEY.IsDown(Keys.E) || KEY.IsDown(Keys.Enter);
        public static bool PAUSE => KEY.IsTyped(Keys.Escape);


        public struct KeyDescription
        {
            public Keys[] Key;
            public String Desc;
        }
    }
}


*/
