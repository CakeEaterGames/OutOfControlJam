using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutOfControl
{
    class ActionManager
    {
        public LevelGrid grid;

        public ActionManager()
        {

        }

        public enum Action
        {
            no,

            attack,
            strongAttack,
            guard,
            up, down, left, right,
            swap,
            scatter,
            random,

            fireball, freeze,

            heal, healAll,

            shoot,

            atack_potion,
            defence_potion,

            addStone,
            breakStone,
        }

        public void moveAct(Entity e, Action act)
        {
            var x = e.pX;
            var y = e.pY;
            if (act == Action.up) y--;
            if (act == Action.down) y++;
            if (act == Action.left) x--;
            if (act == Action.right) x++;

            if (grid.GetEntity(x, y) == null)
            {
                grid.MoveEntity(e, x, y);
                grid.SetRealEntityCoords(e);
            }
        }

        public void PerformAction(Entity e, Action act, bool forEnemies = false, bool isGolden = false)
        {

        }

        public void PerformAction(Action act, bool forEnemies = false, bool isGolden = false)
        {
            switch (act)
            {
                case Action.attack:
                    {
                        foreach (Entity e in grid.Entities)
                        {
                            if (e.CanAttack && (e.isEnemy == forEnemies))
                            {
                                Entity look;
                                if (forEnemies)
                                {
                                    look = grid.LookfromEntity(e, LevelGrid.direction.down);
                                }
                                else
                                {
                                    look = grid.LookfromEntity(e, LevelGrid.direction.up);
                                }

                                if (look != null && (look.isEnemy != forEnemies))
                                {
                                    look.HP -= e.damage;
                                    e.AttackAnim(look);
                                    look.ShakeAnim();
                                    if (look.HP<=0)
                                    {
                                        look.Destruct();
                                        grid.RemoveEntity(look);
                                        //grid.Entities.Remove(look);
                                    }
                                   // Console.WriteLine(e + " Attacked " + look);
                                }

                            }
                        }
                    };
                    break;

                case Action.down:
                case Action.right:
                    {
                        for (int i = grid.gridW - 1; i >= 0; i--)
                        {
                            for (int j = grid.gridH - 1; j >= 0; j--)
                            {
                                var e = grid.GetEntity(i, j);
                                if (e != null)
                                {
                                    if ((e.isEnemy == forEnemies))
                                    {
                                        moveAct(e, act);
                                    }
                                }
                            }
                        }
                        break;
                    }
                case Action.up:
                case Action.left:
                    {
                        for (int i = 0; i < grid.gridW; i++)
                        {
                            for (int j = 0; j < grid.gridH; j++)
                            {
                                var e = grid.GetEntity(i, j);
                                if (e != null)
                                {
                                    if ((e.isEnemy == forEnemies))
                                    {
                                        moveAct(e, act);
                                    }
                                }
                            }
                        }
                        break;
                    }

                case Action.fireball:
                    foreach (Entity e in grid.Entities)
                    {
                        if ((e.type==Entity.EType.wizard || e.type == Entity.EType.DarkWizard) && (e.isEnemy == forEnemies))
                        {

                        }
                    }
                    break;
            }
        }

    }
}