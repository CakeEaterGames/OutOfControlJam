using Microsoft.Xna.Framework;
using MonoCake;
using MonoCake.Objects;
using System;
using System.Collections.Generic;

namespace Pellicalo
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
        public double CritChance = 0;
        public int level = 1;

        public double luck = 1;
        public double despair = 1;

        public EType type = EType.warrior;

        public int currentCycle = 0;
        public int FreezeAmount = 0;
        public List<ActionManager.Action> MoveSet = new List<ActionManager.Action>();

        public int DefenceBuff = 0;
        public int StrengthBuff = 0;

        HPbar HPbar;
        GameObject Mark;
        public Entity()
        {
            HPbar = new HPbar();
            HPbar.AddUR(this);

            Mark = new GameObject();
            Mark.Alpha = 0;
            Mark.AddUR(this);
            Mark.SetImg(GlobalContent.LoadImg("Freeze",true));

           
        }

        public static EType GetHire()
        {
            if (HireList == null)
            {
                HireList = new List<EType>();
                HireList.Add(EType.warrior);
                HireList.Add(EType.wizard);
                HireList.Add(EType.wizard);
                HireList.Add(EType.wizard);
                HireList.Add(EType.stoner);
                HireList.Add(EType.ranger);
                HireList.Add(EType.healer);
                HireList.Add(EType.buff_guy);
            }


            return HireList[Gameplay.RNG.Next(0, HireList.Count)];
        }

        public double GoalX = 0;
        public double GoalY = 0;

        public double startX, startY;

        public bool isAnimating = false;
        double AnimTime = 0;
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
            

            AnimTime = 30;
            isAnimating = true;
        }


        public void JumpAnim()
        {
            startX = X;
            startY = Y;

            GoalY = Y - 10;
          
            AnimTime = 30;
            isAnimating = true;
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

            if (Mark.Alpha > 0)
            {
                Mark.Alpha -= 1 / 60.0;
            }

            HPbar.BaseRenderParameters.X = X+2;
            HPbar.BaseRenderParameters.Y = Y+H+2.5;
            HPbar.filled = (double)HP / (double)MaxHP;

            Mark.SetXY(X,Y);

            if (isAnimating)
            {
                AnimTime--;
                if (AnimTime <= 0)
                {
                    GoalX = startX;
                    GoalY = startY;
                    isAnimating = false;
                }
            }

            if (FreezeAmount>0)
            {
                color = Color.Blue;
            }
            else if (StrengthBuff > 0)
            {
                color = Color.Red;
            }
            else if (DefenceBuff > 0)
            {
                color = Color.Gray;
            }
            else
            {
                color = Color.White;
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
            DarkKnight,
            DarkArcher
        };

        public static List<EType> HireList;
        
       
        public void MarkAnim(string image)
        {
            Mark.SetImg(GlobalContent.LoadImg(image, true));
            Mark.Alpha = 1;
        }
      


        public double LevelMultiplier()
        {
            return Math.Pow(1.5, level-1);
        }

        public double CalcDamage()
        {
            var a = damage * LevelMultiplier();
            if (StrengthBuff>0)
            {
                a *= 2;
                StrengthBuff--;
            }
            if (Gameplay.RNG.NextDouble() > CritChance)
            {
                a *= 2;
            }
            Console.WriteLine(a);
            return a;
        }

        public double PreCalcDamage()
        {
            var a = damage * LevelMultiplier();
            if (StrengthBuff > 0)
            {
                a *= 2;
            }
            return a;
        }


        public static Entity CreateByType(EType t,LevelGrid Grid,int x, int y, int level)
        {
            Entity p = new Entity();

            p.type = t;
            switch (t)
            {
                case EType.warrior:
                    p.SetImg(GlobalContent.LoadImg("Warrior", true));
                    p.CanAttack = true;
                    p.damage = 3;
                    p.MaxHP = 15;
                    break;
                case EType.wizard: 
                    p.SetImg(GlobalContent.LoadImg("Wizard", true));
                    p.CanAttack = true;
                    p.damage = 2;
                    p.MaxHP = 10;
                    break;
                case EType.healer:
                    p.SetImg(GlobalContent.LoadImg("Healer", true));
                    p.MaxHP = 18;
                    p.damage = 1;
                    break;
                case EType.ranger:
                    p.SetImg(GlobalContent.LoadImg("Ranger", true));
                    p.damage = 2;
                    p.MaxHP = 10;
                    break;
                case EType.buff_guy:
                    p.SetImg(GlobalContent.LoadImg("Buff_Guy", true));
                    //p.CanAttack = true;
                    p.damage = 1;
                    p.MaxHP = 15;
                    break;
                case EType.stoner:
                    p.SetImg(GlobalContent.LoadImg("Stoner", true));
                    p.CanAttack = true;
                    p.damage = 1;
                    p.MaxHP = 20;
                    break;

                case EType.Stone:
                    p.SetImg(GlobalContent.LoadImg("Stone", true));
                    p.isEnemy = true;
                    p.MaxHP = 10;
                    break;







                case EType.Skeleton:
                    p.SetImg(GlobalContent.LoadImg("Skeleton", true));
                    p.isEnemy = true;
                    p.CanAttack = true;
                    p.damage = 2;
                    p.MaxHP = 10;

                    p.MoveSet.Add(ActionManager.Action.attack);
                    p.MoveSet.Add(ActionManager.Action.attack);
                    p.MoveSet.Add(ActionManager.Action.scatter);

                    break;
                case EType.DarkKnight:
                    p.SetImg(GlobalContent.LoadImg("DarkKnight", true));
                    p.isEnemy = true;
                    p.CanAttack = true;
                    p.damage = 2;
                    p.MaxHP = 10;

                    p.MoveSet.Add(ActionManager.Action.MoveToPlayer);
                    p.MoveSet.Add(ActionManager.Action.attack);
                    p.MoveSet.Add(ActionManager.Action.MoveToPlayer);
                    
                    break;
                case EType.DarkWizard:
                    p.SetImg(GlobalContent.LoadImg("DarkWizard", true));
                    p.isEnemy = true;
                    p.CanAttack = true;
                    p.damage = 1;
                    p.MaxHP = 10;

                    p.MoveSet.Add(ActionManager.Action.MoveToPlayer);
                    p.MoveSet.Add(ActionManager.Action.freeze);
                    p.MoveSet.Add(ActionManager.Action.fireball);
                    p.MoveSet.Add(ActionManager.Action.scatter);
                    p.MoveSet.Add(ActionManager.Action.fireball);
                    p.MoveSet.Add(ActionManager.Action.scatter);
                    p.MoveSet.Add(ActionManager.Action.scatter);
                    break;
                case EType.Witch:
                    p.SetImg(GlobalContent.LoadImg("Witch", true));
                    p.isEnemy = true;
                    p.MaxHP = 10;

                    p.MoveSet.Add(ActionManager.Action.scatter);
                    p.MoveSet.Add(ActionManager.Action.scatter);
                    p.MoveSet.Add(ActionManager.Action.scatter);
                    p.MoveSet.Add(ActionManager.Action.scatter);
                    p.MoveSet.Add(ActionManager.Action.scatter);
                    p.MoveSet.Add(ActionManager.Action.scatter);
                    p.MoveSet.Add(ActionManager.Action.scatter);
                    p.MoveSet.Add(ActionManager.Action.Curse);
                    break;
                case EType.DarkArcher:
                    p.SetImg(GlobalContent.LoadImg("DarkArcher", true));
                    p.isEnemy = true;
                    p.MaxHP = 10;
                    p.damage = 2;
                    p.MoveSet.Add(ActionManager.Action.MoveToPlayer);
                    p.MoveSet.Add(ActionManager.Action.MoveToPlayer);
                    p.MoveSet.Add(ActionManager.Action.shoot);

                    break;


            }

            if (p.isEnemy)
            {
                p.HPbar.Color = Color.Red;
            }
            else
            {
                p.HPbar.Color = Color.Green;
            }

            p.level = level;

            p.MaxHP = (int)(p.MaxHP * p.LevelMultiplier());
            p.HP = p.MaxHP;

            if (p.MoveSet.Count>0)
            {
                Random rng = new Random();
                p.currentCycle = rng.Next(0, 999) % p.MoveSet.Count;
            }
           
            p.AddUR(Grid);

            p.pX = x;
            p.pY = y;

            Grid.SetRealEntityCoords(p);
            Grid.SetEntity(p);



            return p;
        }

        public void Freeze(int n)
        {
            MarkAnim("Freeze");
            FreezeAmount = n;
            color = Color.Blue;  
        }

        public void FullDescription()
        {
            Gameplay.chat.AddLine();
            Gameplay.chat.AddLotsOfText("HP: " + (int)HP + " / " + (int)MaxHP);
            Gameplay.chat.AddLotsOfText("Level: " + level);
            Gameplay.chat.AddLotsOfText("Is evil: " + isEnemy);
            Gameplay.chat.AddLotsOfText("Can attack: " + CanAttack);
            Gameplay.chat.AddLotsOfText("Damage: " + (int)PreCalcDamage());
            if (FreezeAmount > 0)
            {
                Gameplay.chat.AddLotsOfText("Frozen for " + FreezeAmount + " turns");
            }
            if (DefenceBuff > 0)
            {
                Gameplay.chat.AddLotsOfText("Buffed defence for " + DefenceBuff + " turns");
            }
            if (StrengthBuff > 0)
            {
                Gameplay.chat.AddLotsOfText("Buffed attack for " + StrengthBuff + " turns");
            }
            Gameplay.chat.AddLotsOfText("");


            Description(type);

        }

        public static void Description(EType a)
        {
   
            switch (a)
            {
                case EType.warrior:
                    Gameplay.chat.AddLotsOfText("A simple guy. He sees a bad guy, he smacks a bad guy. Can use a strong attack action. Can perform a default attack");
                    break;
                case EType.wizard:
                    Gameplay.chat.AddLotsOfText("An explosion fanatic. He casts fireball spells and freeze spells. Very good for crowd controll. Can perform a default attack");
                    break;
                case EType.healer:
                    Gameplay.chat.AddLotsOfText("Supportive guy but kind of a freak. He heals his allies with questionable medicine.");
                    break;
                case EType.ranger:
                    Gameplay.chat.AddLotsOfText("A sneaky long range fighter. He can shoot enemies form any point on the map. (hint hint! makes your life easier)");
                    break;
                case EType.buff_guy:
                    Gameplay.chat.AddLotsOfText("A local waifu! The best character in the game. He's very handsome. Oh and I guess he can buff your allies but who cares about that");
                    break;
                case EType.stoner:
                    Gameplay.chat.AddLotsOfText("Yes, That's right. He's called a stoner. He carries stones around him all the time for some reason. Can break and place stones");
                    break;
                case EType.Stone:
                    Gameplay.chat.AddLotsOfText("It's not just a boulder. It's a rock");
                    break;
                case EType.Skeleton:
                    Gameplay.chat.AddLotsOfText("An evil guy. He's mostly dead... I think");

                    break;
                case EType.Witch:
                    Gameplay.chat.AddLotsOfText("An old woman with a cat. She's mostly harmless but every once in a while she might curse you if she's not in a good mood");
                    break;
                case EType.DarkWizard:
                    Gameplay.chat.AddLotsOfText("An evil brother of the Wizard. Yes, he has like a hundreed of evil brothers. Can do the same thisngs as the Wizard");

                    break;
                case EType.DarkKnight:
                    Gameplay.chat.AddLotsOfText("Very aggressive Knight. He will always try to be close to you, so that he could slap you very hard");

                    break;
                case EType.DarkArcher:
                    Gameplay.chat.AddLotsOfText("These guys are for game balance... And evil or whatever. They shoot you from every place on the map");

                    break;
            }
        }


        public override void Destruct()
        {
            
            base.Destruct();

        }
    }
}
