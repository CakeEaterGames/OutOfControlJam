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
        static double global_Luck = 1;
        static double global_Despair = 1;
        public static Random RNG;

        public Gameplay()
        {
            RNG = new Random();
 


            GameObject bg = new GameObject();
            bg.SetImg(GlobalContent.LoadImg("bg",true));
            bg.AddUR(this);

            StartFight();
        }

        public LevelGrid Grid;
        public ActionManager ActionManager;

        public void StartFight() {

            InitGrid();
            SpawnEnemies();
            SpawnPlayers();

            foreach (Entity e in Grid.Entities)
            {
                Console.WriteLine(Grid.LookfromEntity(e, LevelGrid.direction.up));
            }

        }

        public void InitGrid()
        {
            Grid = new LevelGrid(6,6);
            Grid.AddUR(this);
            Grid.BaseRenderParameters.X = 1280 / 2;
            Grid.BaseRenderParameters.Y = 720 / 2;
            Grid.BaseRenderParameters.ScaleW = 3;
            Grid.BaseRenderParameters.ScaleH = 3;

            ActionManager = new ActionManager();
            ActionManager.grid = Grid;
        }
        public void SpawnEnemies()
        {
            Entity.CreateByType(Entity.EType.Skeleton, Grid, 0, 0);
            Entity.CreateByType(Entity.EType.DarkKnight, Grid, 1, 0);
            Entity.CreateByType(Entity.EType.DarkWizard, Grid, 2, 0);
            Entity.CreateByType(Entity.EType.Witch, Grid, 3, 1);
            Entity.CreateByType(Entity.EType.Stone, Grid, 3, 2);

        }
        public void SpawnPlayers()
        {
            Entity.CreateByType(Entity.EType.warrior, Grid, 0, 5);
            Entity.CreateByType(Entity.EType.healer, Grid, 1, 5);
            Entity.CreateByType(Entity.EType.ranger, Grid, 2, 5);
            Entity.CreateByType(Entity.EType.stoner, Grid, 3, 5);
            Entity.CreateByType(Entity.EType.wizard, Grid, 4, 4);
            Entity.CreateByType(Entity.EType.buff_guy, Grid, 0, 4);
     
        }

      
        public override void Update()
        {
            // Console.WriteLine(123);
            if (KEY.IsTyped(Keys.Q))
            {
                ActionManager.PerformActionAll(ActionManager.Action.attack, false);
            }
            if (KEY.IsTyped(Keys.F))
            {
                ActionManager.PerformActionAll(ActionManager.Action.fireball, false);
            }
            if (KEY.IsTyped(Keys.G))
            {
                ActionManager.PerformActionAll(ActionManager.Action.freeze, false);
            }
            if (KEY.IsTyped(Keys.H))
            {
                ActionManager.PerformActionAll(ActionManager.Action.heal, false);
            }
            if (KEY.IsTyped(Keys.A))
            {
                ActionManager.PerformActionAll(ActionManager.Action.healAll, false);
            }
            if (KEY.IsTyped(Keys.S))
            {
                ActionManager.PerformActionAll(ActionManager.Action.shoot, false);
            }

            if (KEY.IsTyped(Keys.B))
            {
                ActionManager.PerformActionAll(ActionManager.Action.atack_buff, false);
            }
            if (KEY.IsTyped(Keys.N))
            {
                ActionManager.PerformActionAll(ActionManager.Action.defence_buff, false);
            }

            if (KEY.IsTyped(Keys.O))
            {
                ActionManager.PerformActionAll(ActionManager.Action.breakStone, false);
            }
            if (KEY.IsTyped(Keys.P))
            {
                ActionManager.PerformActionAll(ActionManager.Action.addStone, false);
            }

            if (KEY.IsTyped(Keys.C))
            {
                ActionManager.PerformActionAll(ActionManager.Action.scatter, false);
            }
            if (KEY.IsTyped(Keys.V))
            {
                ActionManager.PerformActionAll(ActionManager.Action.swap, false);
            }

            if (KEY.IsTyped(Keys.Up))
            {
                ActionManager.PerformActionAll(ActionManager.Action.up, false);
            }
            if (KEY.IsTyped(Keys.Down))
            {
                ActionManager.PerformActionAll(ActionManager.Action.down, false);
            }
            if (KEY.IsTyped(Keys.Left))
            {
                ActionManager.PerformActionAll(ActionManager.Action.left, false);
            }
            if (KEY.IsTyped(Keys.Right))
            {
                ActionManager.PerformActionAll(ActionManager.Action.right, false);
            }


            if (KEY.IsTyped(Keys.E))
            {
                ActionManager.EnemyTurn();
            }
            
        }
    }
}
