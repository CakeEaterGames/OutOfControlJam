using MonoCake;
using MonoCake.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pellicalo
{
    public class GameOverScreen : Scene
    {
        GameObject text = new GameObject();
        GameObject bg = new GameObject();

        TextField info = new TextField();
        public GameOverScreen()
        {
            AudioManager.StopAllSounds();
            AudioManager.StopSong();

            AudioManager.PlaySong("gameover",1,true);

            bg.SetImg(GlobalContent.LoadImg("GameOver",true));
            bg.Scale(5);
            bg.AddUR(this);
            bg.Alpha = 0;
         
            text.SetImg(GlobalContent.LoadImg("GameOverText", true));
            text.Scale(5);
            text.AddUR(this);
            text.Alpha = 0;

            info.AddUR(this);
            info.Alpha = 0;

            info.alignment = TextField.Align.center;
            info.text = "You reached level " + Gameplay.level;
            info.color = new Microsoft.Xna.Framework.Color(35,49,64);
            info.SetXY(1280/2,720/2);
            info.Y -= 150;
        }

        public override void Update()
        {
            base.Update();
            if (bg.Alpha<1)
            {
                bg.Alpha += 1 / 120.0;
            }

            if (bg.Alpha >= 0.5)
            {
                if (text.Alpha < 1)
                {
                    text.Alpha += 1 / 120.0;
                }
            }

            if (text.Alpha >= 0.5)
            {
                if (info.Alpha < 1)
                {
                    info.Alpha += 1 / 120.0;
                }
            }
        }
    }
}

