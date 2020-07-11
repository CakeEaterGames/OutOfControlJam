using Microsoft.Xna.Framework;
using MonoCake.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutOfControl
{
   public class Entity : GameObject
    {
        public int pX = 0;
        public int pY = 0;
        public int HP = 10;
        public int MaxHP = 10;

        public bool isEnemy = false;
        public bool CanAttack = false;

        public int damage = 0;
        public int CritChance = 0;
        public int level = 1;

        public double luck = 1;
        public double despair = 1;

        public enum EType
        {
            warrior,
            mage,
            healer,
            archer,
            potion_seller,
            stoner,


            stone,
            slime,

        };

        public virtual void AttackAnim(Entity e)
        {

        }

    }
}
