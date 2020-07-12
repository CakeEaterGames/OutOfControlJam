using Microsoft.Xna.Framework.Input;
using MonoCake;
using MonoCake.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pellicalo
{
    public class MainMenu : Scene
    {
        GameObject bg = new GameObject();
        GameObject Dudes = new GameObject();
        GameObject Trees = new GameObject();
        GameObject Logo = new GameObject();
        GameObject btn1 = new GameObject();
        GameObject btn2 = new GameObject();

        public MainMenu()
        {
            // AudioManager.PlaySong("loop", 1, true);
          
            AudioManager.SinglePlay("hehey");
            bg.
                SetImg(GlobalContent.LoadImg("MenuGrass", true)).
                Scale(1).
                AddUR(this);

            Dudes.
                          SetImg(GlobalContent.LoadImg("MenuDudes", true)).
                          Scale(1).
                          SetCenter(2).
                          SetXY(1280.0 / 2.0, 720.0 * 3).
                          AddUR(this);
            Trees.
                          SetImg(GlobalContent.LoadImg("MenuTrees", true)).
                          Scale(1).
                          AddUR(this);
            Trees.Alpha = 0.9;

            Logo.
                          SetImg(GlobalContent.LoadImg("MenuLogo", true)).
                          Scale(1).
                          SetCenter(5).
                          SetXY(1280.0 / 2.0, 720.0 / 2.0 - 250).
                          AddUR(this);
            btn1.
                          SetImg(GlobalContent.LoadImg("PlayBttn", true)).
                          Scale(1).
                            SetXY(1280.0 / 2.0 + 500, 720.0 * 3).
                            SetCenter(2).
                          AddUR(this);
            btn2.
                        SetImg(GlobalContent.LoadImg("ExitBttn", true)).
                        Scale(1).
                          SetXY(1280.0 / 2.0 - 500, 720.0 * 3).
                          SetCenter(2).
                        AddUR(this);



        }


        public double t = 0;
        public override void Update()
        {
            Console.WriteLine(Logo);
            t++;
            Logo.Scale(1);
            //Logo.SetXYtoMouse();
            Logo.ScaleW = 1 + Math.Sin(t / 60) / 20;
            Logo.ScaleH = 1 + Math.Cos(t / 60) / 20;


            Dudes.Y = Animation.Recurrent(Dudes.Y, 720, divBy: 10);

            btn1.Y = Animation.Recurrent(btn1.Y, 720, divBy: 20);
            btn2.Y = Animation.Recurrent(btn2.Y, 720-15, divBy: 20);

            Dudes.ScaleH = 1 + Math.Sin(t / 60) / 60;

            if (btn1.GetAbsoluteRect().Contains((int)KEY.MouseX, (int)KEY.MouseY))
            {
                btn1.ScaleW = Animation.Recurrent(btn1.ScaleW, 1.05, divBy: 2);
                btn1.ScaleH = Animation.Recurrent(btn1.ScaleW, 1.05, divBy: 2);

                if (KEY.LClick)
                {
                    Gameplay.self.Combat();
                    KEY.ResetClicks();
                }

            }
            else
            {
                btn1.ScaleW = Animation.Recurrent(btn1.ScaleW, 1, divBy: 2);
                btn1.ScaleH = Animation.Recurrent(btn1.ScaleW, 1, divBy: 2);
            }


            if (btn2.GetAbsoluteRect().Contains((int)KEY.MouseX, (int)KEY.MouseY))
            {
                btn2.ScaleW = Animation.Recurrent(btn2.ScaleW, 1.05, divBy: 2);
                btn2.ScaleH = Animation.Recurrent(btn2.ScaleW, 1.05, divBy: 2);

                if (KEY.LClick)
                {
                    Engine.CloseApp();
                }
            }
            else
            {
                btn2.ScaleW = Animation.Recurrent(btn2.ScaleW, 1, divBy: 2);
                btn2.ScaleH = Animation.Recurrent(btn2.ScaleW, 1, divBy: 2);
            }


        }
    }
}
