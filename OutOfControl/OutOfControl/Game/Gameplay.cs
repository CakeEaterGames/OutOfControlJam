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

        public static int level = 28;
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

        public Gameplay()
        {
            self = this;
            RNG = new Random();
            ActionManager = new ActionManager();
           

            for (int i = 0; i <2; i++)
            {
                var a = new SaveWarrior();
                a.type = Entity.EType.warrior;
                a.level = 1;
                Warriors.Add(a);

                ActionManager.AddCharToPool(Entity.EType.warrior);
            }


            //Combat();

            AfterCombat();
        }

        public Scene CurrentScene;

        public void Combat()
        {
            if(CurrentScene != null)
            CurrentScene.Destruct();
            CombatScreen comb = new CombatScreen();
            comb.level = 1;
            comb.AddUR(this);
            comb.ActionManager = ActionManager;
            comb.StartFight();

            CurrentScene = comb;
        }
        public void AfterCombat()
        {
            if (CurrentScene != null)
                CurrentScene.Destruct();
            AfterCombatScreen c = new AfterCombatScreen();
            c.AddUR(this);
            CurrentScene = c;
        }
        public void Death()
        {
            if (CurrentScene != null)
                CurrentScene.Destruct();

        }
     
        public override void Update()
        {
         
         
        }
    }
}
