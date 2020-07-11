using MonoCake;
using MonoCake.Objects;
using System;

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

        public EType type = EType.warrior;

        HPbar HPbar;
        public Entity()
        {
            HPbar = new HPbar();
            HPbar.AddUR(this);
        }


        public double GoalX = 0;
        public double GoalY = 0;

        public double startX, startY;

        public bool isAtacking = false;
        double atckTime = 0;
        public void AttackAnim(Entity e)
        {
            startX = X;
            startY = Y;

            GoalX = e.X;
            GoalY = e.Y;
            if (e.Y>Y)
            {
                GoalY -= 10;
            }
            else
            {
                GoalY += 10;
            }
            

            atckTime = 30;
            isAtacking = true;
        }

        public int shake = 0;
        Random rng = new Random();
        public void ShakeAnim()
        {
            shake = 10;
        }


        public override void Update()
        {
            X = Animation.Recurrent(X, GoalX, divBy: 3);
            Y = Animation.Recurrent(Y, GoalY, divBy: 3);

            if (shake > 0)
            {
                X += rng.Next(-5, 5);
                Y += rng.Next(-2, 2);
                shake--;
            }
         

            HPbar.BaseRenderParameters.X = X+2;
            HPbar.BaseRenderParameters.Y = Y+H+2.5;
            HPbar.filled = (double)HP / (double)MaxHP;

            if (isAtacking)
            {
                atckTime--;
                if (atckTime <= 0)
                {
                    GoalX = startX;
                    GoalY = startY;
                    isAtacking = false;
                }
            }
            else
            {

            }
        }


        public enum EType
        {
            warrior,
            wizard,
            healer,
            ranger,
            buff_guy,
            stoner,


            Stone,

            Skeleton,
            Witch,
            DarkWizard,
            DarkKnight

        };


        public static Entity CreateByType(EType t,LevelGrid Grid,int x, int y)
        {
            Entity p = new Entity();

            p.type = t;
            switch (t)
            {
                case EType.warrior:
                    p.SetImg(GlobalContent.LoadImg("Warrior", true));
                    p.CanAttack = true;
                    p.damage = 3;
                    p.MaxHP = 10;
                    break;
                case EType.wizard: 
                    p.SetImg(GlobalContent.LoadImg("Wizard", true));
                    p.CanAttack = true;
                    p.damage = 2;
                    p.MaxHP = 5;
                    break;
                case EType.healer:
                    p.SetImg(GlobalContent.LoadImg("Healer", true));
                    p.MaxHP = 15;
                    break;
                case EType.ranger:
                    p.SetImg(GlobalContent.LoadImg("Ranger", true));
                    p.MaxHP = 5;
                    break;
                case EType.buff_guy:
                    p.SetImg(GlobalContent.LoadImg("Buff_Guy", true));
                    p.CanAttack = true;
                    p.damage = 1;
                    p.MaxHP = 10;
                    break;
                case EType.stoner:
                    p.SetImg(GlobalContent.LoadImg("Stoner", true));
                    p.CanAttack = true;
                    p.damage = 1;
                    p.MaxHP = 10;
                    break;

                case EType.Stone:
                    p.SetImg(GlobalContent.LoadImg("Stone", true));
                    p.MaxHP = 10;
                    break;







                case EType.Skeleton:
                    p.SetImg(GlobalContent.LoadImg("Skeleton", true));
                    p.isEnemy = true;
                    p.CanAttack = true;
                    p.damage = 2;
                    p.MaxHP = 5;
                    break;
                case EType.DarkKnight:
                    p.SetImg(GlobalContent.LoadImg("DarkKnight", true));
                    p.isEnemy = true;
                    p.CanAttack = true;
                    p.damage = 3;
                    p.MaxHP = 5;
                    break;
                case EType.DarkWizard:
                    p.SetImg(GlobalContent.LoadImg("DarkWizard", true));
                    p.isEnemy = true;
                    p.CanAttack = true;
                    p.damage = 1;
                    p.MaxHP = 5;
                    break;
                case EType.Witch:
                    p.SetImg(GlobalContent.LoadImg("Witch", true));
                    p.isEnemy = true;
                    p.MaxHP = 5;
                    break;


            }

            p.HP = p.MaxHP;

            p.AddUR(Grid);

            p.pX = x;
            p.pY = y;

            Grid.SetRealEntityCoords(p);
            Grid.SetEntity(p);



            return p;
        }

        public override void Destruct()
        {
            
            base.Destruct();

        }
    }
}
