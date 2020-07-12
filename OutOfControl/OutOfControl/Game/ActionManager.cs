using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutOfControl
{
    public class ActionManager
    {
        public LevelGrid grid;

        public ActionManager()
        {
            ActionPool.Add(Action.up);
            ActionPool.Add(Action.down);
            ActionPool.Add(Action.left);
            ActionPool.Add(Action.right);

            ActionPool.Add(Action.scatter);
            ActionPool.Add(Action.swap);
            ActionPool.Add(Action.swap);

            ActionPool.Add(Action.attack);
            ActionPool.Add(Action.attack);
            ActionPool.Add(Action.attack);
           // ActionPool.Add(Action.defence);

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

            fireball, freeze,

            heal, healAll,

            shoot,

            atack_buff,
            defence_buff,

            addStone,
            breakStone,

            MoveToPlayer,
            Curse,
            cursed,
            defence,
        }


        List<Action> ActionPool = new List<Action>();

        public void AddCharToPool(Entity.EType t)
        {

            switch (t)
            {
                case Entity.EType.warrior:
                    addIfNo(Action.strongAttack);
                    break;
                case Entity.EType.wizard:
                    addIfNo(Action.fireball);
                    addIfNo(Action.freeze);
                    break;
                case Entity.EType.ranger:
                    addIfNo(Action.shoot);
                    break;
                case Entity.EType.healer:
                    addIfNo(Action.heal);
                    addIfNo(Action.healAll);
                    break;
                case Entity.EType.stoner:
                    addIfNo(Action.addStone);
                    addIfNo(Action.breakStone);
                    break;
                case Entity.EType.buff_guy:
                    addIfNo(Action.atack_buff);
                    addIfNo(Action.defence_buff);
                    break;
            }
        }

        void addIfNo(Action t)
        {
            if (!ActionPool.Contains(t))
            {
                ActionPool.Add(t);
            }
        }

        public void AddCurse()
        {
            ActionPool.Add(Action.cursed);
        }


        public Action RandomAction()
        {
             return ActionPool[Gameplay.RNG.Next(0, ActionPool.Count)];
        }


        public void EnemyTurn()
        {
            for (int i = 0; i < grid.Entities.Count; i++)
            {
                while (i < grid.Entities.Count && grid.Entities[i] == null)
                {
                    grid.Entities.RemoveAt(i);
                }
                if (i >= grid.Entities.Count)
                {
                    break;
                }

                Entity e = grid.Entities[i];
                if (e.isEnemy && e.MoveSet.Count != 0)
                {

                    PerformAction(e, e.MoveSet[e.currentCycle], true, false);
                    if (e.FreezeAmount <= 0)
                    {
                        e.currentCycle++;
                        e.currentCycle %= e.MoveSet.Count;
                    }



                }

            }
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

        public void Hurt(Entity e, double Damage)
        {
            if (e.DefenceBuff > 0)
            {
                e.HP -= (int)(Damage / 2);
                e.DefenceBuff--;
            }
            else
            {
                e.HP -= (int)Damage;
            }

            e.ShakeAnim();
            if (e.HP <= 0)
            {
                e.Destruct();
                grid.RemoveEntity(e);
                grid.Entities[grid.Entities.IndexOf(e)] = null;

                var st = e.type;
                var sl = e.level;
                for (int i=0;i<Gameplay.Warriors.Count ;i++)
                {
                    var a = Gameplay.Warriors[i];
                    if (a.type == st && a.level == sl)
                    {
                        Gameplay.Warriors.RemoveAt(i);
                        break;
                    }
                }

            }


        }

        public void ExplodeAct(Entity e, double Damage)
        {
            if (e != null)
            {
                e.MarkAnim("FireBall");
                Hurt(e, Damage);
            }

        }

        public void PerformAction(Entity e, Action act, bool forEnemies = false, bool isGolden = false)
        {

            if (e != null && (e.isEnemy == forEnemies))
            {


                if (e != null && e.FreezeAmount > 0)
                {
                    e.FreezeAmount--;

                }
                else
                {

                    switch (act)
                    {
                        
                        case Action.attack:
                            if (e.CanAttack)
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
                                    e.AttackAnim(look);
                                    Hurt(look, (int)e.CalcDamage());
                                }

                            }
                            break;


                        case Action.strongAttack:
                            if (e.type == Entity.EType.warrior)
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
                                    e.AttackAnim(look);
                                    Hurt(look, (int)(e.CalcDamage()*1.5));
                                }

                            }
                            break;

                        case Action.scatter:
                            var a = Gameplay.RNG.Next(1, 6);
                            switch (a)
                            {
                                case 1: moveAct(e, Action.up); break;
                                case 2: moveAct(e, Action.down); break;
                                case 3: moveAct(e, Action.left); break;
                                case 4: moveAct(e, Action.right); break;
                                case 5: moveAct(e, Action.up); break;
                            }
                            break;
                        case Action.swap:
                            {
                                var en = grid.Entities[Gameplay.RNG.Next(0, grid.Entities.Count)];
                                while (en.isEnemy != e.isEnemy)
                                {
                                    en = grid.Entities[Gameplay.RNG.Next(0, grid.Entities.Count)];
                                }
                                var tx = e.pX;
                                var ty = e.pY;

                                grid.RemoveEntity(e);
                                grid.RemoveEntity(en);

                                e.pX = en.pX;
                                e.pY = en.pY;

                                en.pX = tx;
                                en.pY = ty;

                                grid.SetEntity(e);
                                grid.SetEntity(en);

                                grid.SetRealEntityCoords(en);
                                grid.SetRealEntityCoords(e);
                            }
                            break;
                        case Action.up:
                        case Action.down:
                        case Action.left:
                        case Action.right:
                            moveAct(e, act);
                            break;
                        case Action.fireball:
                            if (e.type == Entity.EType.wizard || e.type == Entity.EType.DarkWizard)
                            {
                                e.JumpAnim();
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
                                    var dmg = e.CalcDamage();

                                    ExplodeAct(look, e.damage);
                                    ExplodeAct(grid.GetEntity(look.pX - 1, look.pY), dmg);
                                    ExplodeAct(grid.GetEntity(look.pX + 1, look.pY), dmg);

                                    ExplodeAct(grid.GetEntity(look.pX - 1, look.pY + 1), dmg);
                                    ExplodeAct(grid.GetEntity(look.pX + 1, look.pY + 1), dmg);

                                    ExplodeAct(grid.GetEntity(look.pX - 1, look.pY - 1), dmg);
                                    ExplodeAct(grid.GetEntity(look.pX + 1, look.pY - 1), dmg);

                                    ExplodeAct(grid.GetEntity(look.pX, look.pY - 1), dmg);
                                    ExplodeAct(grid.GetEntity(look.pX, look.pY + 1), dmg);
                                }
                            }
                            break;
                        case Action.freeze:
                            if (e.type == Entity.EType.wizard || e.type == Entity.EType.DarkWizard)
                            {
                                e.JumpAnim();
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
                                    look.Freeze((int)Math.Ceiling(e.CalcDamage()));
                                }
                            }
                            break;
                        case Action.healAll:
                            if (e.type == Entity.EType.healer)
                            {
                                e.JumpAnim();
                                foreach (Entity en in grid.Entities)
                                {

                                    if (en.isEnemy == e.isEnemy)
                                    {
                                        if (en.HP < en.MaxHP)
                                        {
                                            en.HP += (int)e.CalcDamage();
                                            en.MarkAnim("Heal");
                                            en.HP = Math.Min(en.HP, en.MaxHP);
                                        }

                                    }
                                }
                            }
                            break;
                        case Action.heal:
                            if (e.type == Entity.EType.healer)
                            {
                                int goal = Gameplay.RNG.Next(0, grid.Entities.Count);
                                int i = goal;
                                do
                                {
                                    Entity en = grid.Entities[i];
                                    if (en.isEnemy == e.isEnemy)
                                    {
                                        if (en.HP < en.MaxHP)
                                        {
                                            e.JumpAnim();
                                            en.HP += (int)e.CalcDamage() * 3;
                                            en.MarkAnim("Heal");
                                            en.HP = Math.Min(en.HP, en.MaxHP);
                                            break;
                                        }

                                    }
                                    i++;
                                    i %= grid.Entities.Count;
                                } while (i != goal);

                            }
                            break;

                        case Action.atack_buff:
                            if (e.type == Entity.EType.buff_guy)
                            {
                                int goal = Gameplay.RNG.Next(0, grid.Entities.Count);

                                Entity en = grid.Entities[goal];
                                while (en.isEnemy != e.isEnemy)
                                {
                                    goal = Gameplay.RNG.Next(0, grid.Entities.Count);
                                    en = grid.Entities[goal];
                                }

                                e.JumpAnim();

                                en.MarkAnim("StrengthBuff");
                                en.StrengthBuff = (int)e.CalcDamage();
                            }
                            break;
                        case Action.defence_buff:
                            if (e.type == Entity.EType.buff_guy)
                            {
                                int goal = Gameplay.RNG.Next(0, grid.Entities.Count);

                                Entity en = grid.Entities[goal];
                                while (en.isEnemy)
                                {
                                    goal = Gameplay.RNG.Next(0, grid.Entities.Count);
                                    en = grid.Entities[goal];
                                }

                                e.JumpAnim();

                                en.MarkAnim("DefenceBuff");
                                en.DefenceBuff = (int)e.CalcDamage();
                            }
                            break;

                        case Action.breakStone:
                            if (e.type == Entity.EType.stoner)
                            {
                                int goal = Gameplay.RNG.Next(0, grid.Entities.Count);
                                int i = goal;
                                do
                                {
                                    Entity en = grid.Entities[i];
                                    if (en.type == Entity.EType.Stone)
                                    {
                                        e.JumpAnim();
                                        Hurt(en, 999);
                                        // en.MarkAnim("Arrow");
                                        break;
                                    }
                                    i++;
                                    i %= grid.Entities.Count;
                                } while (i != goal);
                            }
                            break;
                        case Action.addStone:
                            if (e.type == Entity.EType.stoner)
                            {
                                if (Gameplay.RNG.NextDouble() > 0.4)
                                {
                                    int goal = Gameplay.RNG.Next(0, grid.Entities.Count);
                                    int i = goal;
                                    do
                                    {
                                        Entity en = grid.Entities[i];
                                        if (en != null && en.isEnemy == e.isEnemy && grid.GetEntity(en.pX, en.pY - 1) == null)
                                        {
                                            e.JumpAnim();

                                            Entity.CreateByType(Entity.EType.Stone, grid, en.pX, en.pY - 1,1);

                                            break;
                                        }
                                        i++;
                                        i %= grid.Entities.Count;
                                    } while (i != goal);
                                }
                                else if (grid.Entities.Count < grid.gridW * grid.gridH - 2)
                                {
                                    var sx = Gameplay.RNG.Next(0, grid.gridW);
                                    var sy = Gameplay.RNG.Next(0, grid.gridH);
                                    Entity en = grid.GetEntity(sx, sy);
                                    while (en != null)
                                    {
                                        sx = Gameplay.RNG.Next(0, grid.gridW);
                                        sy = Gameplay.RNG.Next(0, grid.gridH);
                                        en = grid.GetEntity(sx, sy);
                                    }
                                    e.JumpAnim();
                                    Entity.CreateByType(Entity.EType.Stone, grid, sx, sy,1);
                                }
                            }
                            break;

                        case Action.shoot:
                            if (e.type == Entity.EType.ranger || (e.type == Entity.EType.DarkArcher))
                            {
                                int goal = Gameplay.RNG.Next(0, grid.Entities.Count);
                                int i = goal;
                                do
                                {
                                    Entity en = grid.Entities[i];
                                    if (en!=null && en.isEnemy != e.isEnemy)
                                    {
                                        e.JumpAnim();
                                        Hurt(en, e.CalcDamage());
                                        en.MarkAnim("Arrow");
                                        break;
                                    }
                                    i++;
                                    i %= grid.Entities.Count;
                                } while (i != goal);
                            }
                            break;
                        case Action.MoveToPlayer:
                            {
                                var target = grid.LookfromEntity(e, LevelGrid.direction.down);
                                if (target != null && !target.isEnemy)
                                {
                                    moveAct(e, Action.down);
                                }
                                if (target == null || target.isEnemy)
                                {
                                    for (int i = 0; i < 10; i++)
                                    {
                                        target = grid.LookfromEntity(e.pX - i, e.pY, LevelGrid.direction.down);
                                        if (target != null && !target.isEnemy)
                                        {
                                            moveAct(e, Action.left);
                                            break;
                                        }

                                        target = grid.LookfromEntity(e.pX + i, e.pY, LevelGrid.direction.down);
                                        if (target != null && !target.isEnemy)
                                        {
                                            moveAct(e, Action.right);
                                            break;
                                        }
                                    }
                                }
                            }
                            break;

                    }
                }
            }
        }

        public void PerformActionAll(Action act, bool forEnemies = false, bool isGolden = false)
        {

            

            switch (act)
            {
                case Action.down:
                case Action.right:
                    {
                        for (int i = grid.gridW - 1; i >= 0; i--)
                        {
                            for (int j = grid.gridH - 1; j >= 0; j--)
                            {
                                var e = grid.GetEntity(i, j);
                                PerformAction(e, act, forEnemies, isGolden);
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
                                PerformAction(e, act, forEnemies, isGolden);
                            }
                        }
                        break;
                    }

                /* case Action.attack:
                 case Action.fireball:
                 case Action.freeze:
                 case Action.heal:
                 case Action.healAll:
                 case Action.guard:
                 case Action.shoot:
                 case Action.defence_buff:
                 case Action.atack_buff:
                 case Action.addStone:
                 case Action.breakStone:
                 case Action.scatter:
                 case Action.random:
                 case Action.swap:
                 case Action.strongAttack:*/
                default:

                    for (int i = 0; i < grid.Entities.Count; i++)
                    {
                        while (i < grid.Entities.Count && grid.Entities[i] == null)
                        {
                            grid.Entities.RemoveAt(i);
                        }
                        if (i >= grid.Entities.Count)
                        {
                            break;
                        }

                        Entity e = grid.Entities[i];
                        if ((e.isEnemy == forEnemies))
                        {
                            PerformAction(e, act, forEnemies, isGolden);
                        }

                    }
                    break;
            }
        }



        public void Description(Action a)
        {
            Gameplay.chat.AddLine();
            switch (a)
            {
                case ActionManager.Action.no:
                    Gameplay.chat.AddLotsOfText("Doesn't do anything");
                    break;
                case ActionManager.Action.attack:
                    Gameplay.chat.AddLotsOfText("Deals damage to the characters that are in front of other characters");
                    break;
                case ActionManager.Action.strongAttack:
                    Gameplay.chat.AddLotsOfText("Deals A LOT damage to the characters that are in front of the warriors");
                    Gameplay.chat.AddLotsOfText("Only for the " + Entity.EType.warrior);
                    break;
                case ActionManager.Action.up:
                    Gameplay.chat.AddLotsOfText("Move up by one tile");
                    break;
                case ActionManager.Action.down:
                    Gameplay.chat.AddLotsOfText("Move down by one tile");
                    break;
                case ActionManager.Action.left:
                    Gameplay.chat.AddLotsOfText("Move to the left by one tile");
                    break;
                case ActionManager.Action.right:
                    Gameplay.chat.AddLotsOfText("Move to the right by one tile");
                    break;
                case ActionManager.Action.swap:
                    Gameplay.chat.AddLotsOfText("Swap all characters... sometimes???");
                    break;
                case ActionManager.Action.scatter:
                    Gameplay.chat.AddLotsOfText("Move in random directions... sometimes???");
                    break;
                case ActionManager.Action.fireball:
                    Gameplay.chat.AddLotsOfText("Casts a fireball that explodes in 8 directions");
                    Gameplay.chat.AddLotsOfText("Only for the " + Entity.EType.wizard);

                    break;
                case ActionManager.Action.freeze:
                    Gameplay.chat.AddLotsOfText("Freezes a character for a few turns");
                    Gameplay.chat.AddLotsOfText("Only for the " + Entity.EType.wizard);

                    break;
                case ActionManager.Action.heal:
                    Gameplay.chat.AddLotsOfText("Heals a random ally");
                    Gameplay.chat.AddLotsOfText("Only for the " + Entity.EType.healer);

                    break;
                case ActionManager.Action.healAll:
                    Gameplay.chat.AddLotsOfText("Heals all allies, but only a little bit");
                    Gameplay.chat.AddLotsOfText("Only for the " + Entity.EType.healer);
                    break;
                case ActionManager.Action.shoot:
                    Gameplay.chat.AddLotsOfText("Doesn't have to be in front of the character");
                    Gameplay.chat.AddLotsOfText("Shoots a random enemy on the map");

                    Gameplay.chat.AddLotsOfText("Only for the " + Entity.EType.ranger);

                    break;
                case ActionManager.Action.atack_buff:
                    Gameplay.chat.AddLotsOfText("Can also boost other stats like healing power");
                    Gameplay.chat.AddLotsOfText("Gives a random ally an attack buff for a few turns");

                    Gameplay.chat.AddLotsOfText("Only for the " + Entity.EType.buff_guy);
                    break;
                case ActionManager.Action.defence_buff:
                    Gameplay.chat.AddLotsOfText("Gives a random ally a defence buff for a few turns");
                    Gameplay.chat.AddLotsOfText("Only for the " + Entity.EType.buff_guy);
                    break;
                case ActionManager.Action.addStone:
                    Gameplay.chat.AddLotsOfText("Yes... He has a lot of stones with him");
                    Gameplay.chat.AddLotsOfText("Plases a stone somewhere on the map...");
    
                    Gameplay.chat.AddLotsOfText("Only for the " + Entity.EType.stoner);
                    break;
                case ActionManager.Action.breakStone:
                    Gameplay.chat.AddLotsOfText("Apparantly he collects them");
                    Gameplay.chat.AddLotsOfText("Grabs a random stone from the map");
               
                    Gameplay.chat.AddLotsOfText("Only for the " + Entity.EType.stoner);
                    break;
                case ActionManager.Action.MoveToPlayer:
                    Gameplay.chat.AddLotsOfText("Moves closer towards any friendly character");
                    break;
                case ActionManager.Action.Curse:
                    Gameplay.chat.AddLotsOfText("Avoid at all cost");
                    Gameplay.chat.AddLotsOfText("Adds a PERMANENT debuff to your squad");
                    break;
                case ActionManager.Action.cursed:
                    Gameplay.chat.AddLotsOfText("Yep... that's a curse");
                    break;
                case ActionManager.Action.defence:
                case ActionManager.Action.guard:
                    Gameplay.chat.AddLotsOfText("Boosts a defence for a while");
                    break;
            }
          //  Gameplay.chat.AddLotsOfText("Boosts a defence for a while");
         //   Gameplay.chat.AddLine();

        }

    }
}