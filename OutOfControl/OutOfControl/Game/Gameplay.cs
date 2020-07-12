using MonoCake.Objects;
using MonoCake;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace Pellicalo
{
    class Gameplay : Scene
    {
        public static double global_Luck = 1;
        public static double global_Despair = 1;
        public static double CurseCount = 0;

        public static int level = 1;
        public static int DiceCount = 4;


        public struct SaveWarrior
        {
            public int level;
            public Entity.EType type;
        }
        public static List<SaveWarrior> Warriors = new List<SaveWarrior>();

        public static Random RNG;

        public static ActionManager ActionManager;

        public static Gameplay self;

        public static ChatBox chat = new ChatBox();

        public Gameplay()
        {
            AudioManager.PlaySong("in", 1, false);
            self = this;
            RNG = new Random();
            ActionManager = new ActionManager();
           

            for (int i = 0; i <1; i++)
            {
                var a = new SaveWarrior();
                a.type = Entity.EType.warrior;
                a.level = 1;
                Warriors.Add(a);

                ActionManager.AddCharToPool(Entity.EType.warrior);
            }
            {
                var a = new SaveWarrior();
                a.type = Entity.EType.ranger;
                a.level = 1;
                Warriors.Add(a);

                ActionManager.AddCharToPool(Entity.EType.ranger);

            }






            //Combat();

            // AfterCombat();

            //Death();

            // MainMenu();
            PreMainMenu();

            // chat.addText("hello world");
            //   chat.AddUR(this);
            chat.AddUpdate(this);
            
        }

        public Scene CurrentScene;

        public void Combat()
        {
            if(CurrentScene != null)
            CurrentScene.Destruct();
            CombatScreen comb = new CombatScreen();
            comb.level = level;
            comb.AddUR(this);
            comb.ActionManager = ActionManager;
            comb.StartFight();

            CurrentScene = comb;
            chat.RemoveRender();
            chat.AddUR(this);
        }
        public void AfterCombat()
        {
            if (CurrentScene != null)
                CurrentScene.Destruct();
            AfterCombatScreen c = new AfterCombatScreen();
            c.AddUR(this);
            CurrentScene = c;

            chat.RemoveRender();
            chat.AddRender(this);
        }
        public void Death()
        {
            if (CurrentScene != null)
                CurrentScene.Destruct();

            CurrentScene = new GameOverScreen();
            CurrentScene.AddUR(this);

            chat.RemoveRender();
            chat.AddRender(this);

        }
        public void MainMenu()
        {
            if (CurrentScene != null)
                CurrentScene.Destruct();

            CurrentScene = new MainMenu();
            CurrentScene.AddUR(this);

            chat.RemoveRender();
            chat.AddRender(this);

        }
        public void PreMainMenu()
        {
            if (CurrentScene != null)
                CurrentScene.Destruct();

            CurrentScene = new PreMainMenu();
            CurrentScene.AddUR(this);

            chat.RemoveRender();
            chat.AddRender(this);

        }

        public override void Update()
        {
          //  Console.WriteLine(AudioManager.currentLength);

           // Console.WriteLine(AudioManager.currentTime);

          //  Console.WriteLine(AudioManager.currentLength - AudioManager.currentTime);
            if (AudioManager.currentSong=="in")
            {
                if (AudioManager.currentLength / 2 - AudioManager.currentTime<=0)
                {
                    AudioManager.PlaySong("bass",1);
                }
            
            }
            else
            if (AudioManager.currentSong == "bass")
            {
                if (AudioManager.currentLength/2 - AudioManager.currentTime  <= 0)
                {
                    AudioManager.PlaySong("loop", 1,true);
                }

            }

        }

        public static void GiveCurse()
        {
            ActionManager.AddCurse();
        }
    }
}
