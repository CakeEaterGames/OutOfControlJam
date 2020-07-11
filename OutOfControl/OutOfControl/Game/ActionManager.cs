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

        public void PerformAction(Action a)
        {
            switch (a)
            {
                case Action.attack:
                    {
                        foreach (Entity e in grid.Entities)
                        {
                            if (e.CanAttack)
                            {
                                var look = grid.LookfromEntity(e, LevelGrid.direction.up);
                                if (look != null && look.isEnemy)
                                {
                                    look.HP -= e.damage;
                                    e.AttackAnim(look);

                                }
                            }
                        }
                    };
                    break;
            }
        }

    }
}
