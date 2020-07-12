using MonoCake.Objects;
using MonoCake;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace OutOfControl
{
    class Gameplay : Scene
    {
        public static double global_Luck = 1;
        public static double global_Despair = 1;
        public static double CurseCount = 0;

        public static int level = 1;
        public static int DiceCount = 3;


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

            //AfterCombat();

            //Death();

            MainMenu();

           // chat.addText("hello world");
         //   chat.AddUR(this);
            
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
            chat.AddRender(this);
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

        public override void Update()
        {
         /*   if (KEY.IsTyped(Keys.T))
            {
                chat.AddLine();
                chat.AddLotsOfText("Hello this is a very long text just to test if everything works yay lel test test test test test test test test");
            }*/
         
        }

        public static void GiveCurse()
        {
            CurseCount++;
        }
    }
}
