using Microsoft.Xna.Framework.Input;
using MonoCake;
using MonoCake.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutOfControl
{
    public class CombatScreen : Scene
    {
        public static Random RNG;
        public int gridW;
        public int level;
        public CombatScreen()
        {
            RNG = new Random();

            GameObject bg = new GameObject();
            bg.SetImg(GlobalContent.LoadImg("bg", true)).Scale(5).
            AddUR(this);

            GameObject fg1 = new GameObject();
            fg1.SetImg(GlobalContent.LoadImg("fg1", true)).
                Scale(5).
                AddUR(this);
            GameObject fg2 = new GameObject();
            fg2.SetImg(GlobalContent.LoadImg("fg2", true)).
            Scale(5).
            AddUR(this);


        }

        public LevelGrid Grid;
        public ActionManager ActionManager;

        public override void Init()
        {
            base.Init();
        }

        public void StartFight()
        {
            //=ОКРВВЕРХ.МАТ( КОРЕНЬ(A2*7))

            InitGrid();
            SpawnEnemies();
            SpawnPlayers();
        }

        public void InitGrid()
        {
            gridW = Math.Max(5, (int)Math.Ceiling(Math.Sqrt(level * 7)));
            Grid = new LevelGrid(gridW, (int)(gridW * (5.0 / 6.0)));
            Grid.AddUR(this);
            Grid.BaseRenderParameters.X = 1280 / 2;
            Grid.BaseRenderParameters.Y = 720 / 2;
            Grid.BaseRenderParameters.ScaleW = 1 * (20.0 / gridW);
            Grid.BaseRenderParameters.ScaleH = Grid.BaseRenderParameters.ScaleW;


            ActionManager.grid = Grid;
        }
        public void SpawnEnemies()
        {
            //=ОКРВВЕРХ.МАТ( КОРЕНЬ(A2*4))
            var enemyCount = (int)Math.Ceiling(Math.Sqrt(level * 4));
            var a = 0;

            while (a < enemyCount)
            {
                var p = Grid.FindEmptySpace(true);
                if (a < enemyCount && RNG.NextDouble() < 0.25)
                {
                    Entity.CreateByType(Entity.EType.Skeleton, Grid, p.X, p.Y, (int)(level / 10));
                    a++;
                }
                p = Grid.FindEmptySpace(true);
                if (a < enemyCount && RNG.NextDouble() < 0.2)
                {
                    Entity.CreateByType(Entity.EType.DarkKnight, Grid, p.X, p.Y, (int)(level / 10));
                    a++;
                }
                p = Grid.FindEmptySpace(true);
                if (a < enemyCount && RNG.NextDouble() < 0.15)
                {
                    Entity.CreateByType(Entity.EType.DarkWizard, Grid, p.X, p.Y, (int)(level / 10));
                    a++;
                }
                p = Grid.FindEmptySpace(true);
                if (a < enemyCount && RNG.NextDouble() < 0.1)
                {
                    Entity.CreateByType(Entity.EType.Witch, Grid, p.X, p.Y, (int)(level / 10));
                    a++;
                }

            }

        }
        public void SpawnPlayers()
        {
            /* Entity.CreateByType(Entity.EType.warrior, Grid, 0, 5);
             Entity.CreateByType(Entity.EType.healer, Grid, 1, 5);
             Entity.CreateByType(Entity.EType.ranger, Grid, 2, 5);
             Entity.CreateByType(Entity.EType.stoner, Grid, 3, 5);
             Entity.CreateByType(Entity.EType.wizard, Grid, 4, 4);
             Entity.CreateByType(Entity.EType.buff_guy, Grid, 0, 4);*/

            foreach (var w in Gameplay.Warriors)
            {
                var p = Grid.FindEmptySpace(false);
                var a = Entity.CreateByType(w.type, Grid, p.X, p.Y, w.level);
                Console.WriteLine(a);
            }





        }



        public void ThrowDice(int n)
        {
            for (int i = 0; i < n; i++)
            {
                Dice d = new Dice();
                d.
                    SetXY(1280 / 2, 920).
                    Scale(1.5).
                    AddUR(this);

                d.GoalX = 280 + (1000.0 / n) * i;
                d.GoalY = (720.0 / 2.0);
                d.GoalX += RNG.Next(-100, 100);
                d.GoalY += RNG.Next(-200, 200);

                d.init(ActionManager.RandomAction(), false);

            }


        }



        int BattleTimer = 0;
        BattleStates BattleState = BattleStates.waitForThrow;
        Dice bufferDice;
        enum BattleStates
        {
            waitForThrow,
            SelectAction,
            WaitAfterSelect,
            EnemyTurn,
        }

        public override void Update()
        {
            {/*
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

            if (KEY.IsTyped(Keys.Z))
            {
                ActionManager.PerformActionAll(ActionManager.Action.strongAttack, false);
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

            if (KEY.IsTyped(Keys.D))
            {
                ThrowDice(3);
            }
            */
            }


            switch (BattleState)
            {
                case BattleStates.waitForThrow:
                    if (KEY.LClick)
                    {
                        ThrowDice(Gameplay.DiceCount);
                        BattleState = BattleStates.SelectAction;
                    }
                    break;
                case BattleStates.SelectAction:
                    foreach (var d in Dice.All)
                    {

                        if (d.GetAbsoluteRect().Contains((int)KEY.MouseX, (int)KEY.MouseY) && KEY.LClick)
                        {
                            bufferDice = d;
                            //  ActionManager.PerformActionAll(d.Action, false, d.isGolden);
                            foreach (var dd in Dice.All)
                            {
                                dd.clearDice();
                            }
                            BattleState = BattleStates.WaitAfterSelect;
                            BattleTimer = 60;
                        }


                        if (KEY.IsTyped(Keys.Space))
                        {
                            d.GoalX += 400;
                            d.GoalY += RNG.Next(-300, 300);
                        }
                    }
                    break;
                case BattleStates.WaitAfterSelect:
                    BattleTimer--;
                    if (BattleTimer == 30)
                    {
                        ActionManager.PerformActionAll(bufferDice.Action, false, bufferDice.isGolden);
                    }
                    if (BattleTimer <= 0)
                    {
                        ActionManager.EnemyTurn();
                        BattleState = BattleStates.waitForThrow;
                        Grid.CountEntities();
                        if (Grid.Enemies == 0)
                        {
                            Gameplay.self.AfterCombat();
                        }
                        else  if (Grid.Friends == 0)
                        {

                            Gameplay.self.Death();
                        }

                    }

                    break;
            }




        }
    }
}
