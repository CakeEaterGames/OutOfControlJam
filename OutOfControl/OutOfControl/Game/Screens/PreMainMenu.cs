using Microsoft.Xna.Framework;
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
    public class PreMainMenu : Scene
    {
        //By CakeEaterGames.ru
        //Code by CakeEater
        //Graphics and art direction by Ehidney
        //Sounds by Savure and Andysha

        //In this game
        //You controll your small handsome squad
        //By choosing one of the actions that are given to you by the dice

        //You don't have a full control over your squad
        //If you'll get cursed
        //Your squad might stop listening to you

        //If you don't understand some elements about the game
        //Use your right mouse button to learn more about each element 

        public GameObject anim = new GameObject();
        public PreMainMenu()
        {
            for (int i = 1;i<=8;i++)
            {
                anim.AddImg(GlobalContent.LoadImg("text" + i, true), ""+(i-1));
            }
           anim.AddUR(this);

            anim.Frames.Clear();
            anim.AddFrame("" + (current * 2), 15, new Rectangle(0, 0, 1280, 720));
            anim.AddFrame("" + (current * 2 + 1), 15, new Rectangle(0, 0, 1280, 720));
            anim.GotoAndPlay(0);
        }
        int current = 0;
        public override void Update()
        {
            if (KEY.LClick)
            {
                current++;
                anim.Frames.Clear();
                anim.AddFrame("" + (current*2), 15, new Rectangle(0, 0, 1280, 720));
                anim.AddFrame("" + (current*2+1), 15, new Rectangle(0, 0, 1280, 720));
                anim.GotoAndPlay(0);
               
            }
            if (current==4)
            {
                Gameplay.self.MainMenu();
            }
        }
    }
}
