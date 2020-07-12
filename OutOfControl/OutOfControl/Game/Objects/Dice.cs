using Microsoft.Xna.Framework;
using MonoCake;
using MonoCake.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutOfControl
{
    class Dice : GameObject
    {

        public static Dictionary<ActionManager.Action, string> ActionImages;

        public ActionManager.Action Action;
        public bool isGolden = false;
        GameObject DiceFrame;

        public Dice()
        {

            All.Add(this);
            initActionImages();
        }

        public void init(ActionManager.Action act, bool gold)
        {
            Action = act;
            isGolden = gold;

            RotationDirection = Math.Sign(Gameplay.RNG.Next(-10, 10));
            timer = Gameplay.RNG.Next(45, 100);
            RotationSpeed = Tools.Rand(Math.PI / 60.0, Math.PI / 30.0);
            ScrollSpeed = Gameplay.RNG.Next(2, 5+1);

            DiceFrame = new GameObject();

            AddImg(GlobalContent.LoadImg("_Dice Sheet", true), "");
            for (int i = 0; i < Imgs[""].Width - Imgs[""].Height; i += (int)ScrollSpeed)
            {
                AddFrame("", 1, new Rectangle(i, 0, Imgs[""].Height, Imgs[""].Height));
            }
            SetCenter(5);

            GotoAndPlay(Gameplay.RNG.Next(0,Frames.Count));

            if (!isGolden)
            {
                DiceFrame.SetImg(GlobalContent.LoadImg("DiceFrame", true)).SetCenter(5).AddUR(this);
            }
            else
            {
                DiceFrame.SetImg(GlobalContent.LoadImg("DiceGoldFrame", true)).SetCenter(5).AddUR(this);
            }
        }

        int timer = 120;
        double RotationGoal = 0;

       public double GoalX = 0;
        public double GoalY = 0;

        public double RotationDirection = 1;
        public double MaxTimer = 1;
        public double RotationSpeed = 1;
        public int ScrollSpeed = 1;

        public static List<Dice> All = new List<Dice>();

        public override void Update()
        {
            foreach (var d in All)
            {
                if (d != this && d.HitTestObjet(this))
                { 
                    var dx = X - d.X;
                    var dy = Y - d.Y;

                    //GoalX += Math.Sign(dx);
                    //GoalY += Math.Sign(dy);

                    GoalX += dx/10;
                    GoalY += dy / 10;

                  
                }
            }

            GoalX = Tools.Limit(100, 1180, GoalX);
            GoalY = Tools.Limit(100, 620, GoalY);

            if (toClear)
            {
                clearDice();
            }
            if (toClear && Y > 720 * 1.5)
            {
                Destruct();
            }

            X = Animation.Recurrent(X, GoalX, divBy: 5);
           Y = Animation.Recurrent(Y, GoalY, divBy: 5);

            if (timer > 0)
            {
                timer--;
                Rotation += RotationSpeed*RotationDirection;
                RotationGoal = Rotation + Math.PI / 1.0 * RotationSpeed*RotationDirection;
                if (timer <= 0)
                {
                    StopRot();
                }
            }
            else
            {
                Rotation = Animation.Recurrent(Rotation, RotationGoal, divBy: 5);
            }


            if (Math.Abs(KEY.MouseX - X) < 100)
            {
                Alpha = (Math.Abs(KEY.MouseX - X)) / 100.0;
                Alpha = Math.Max(0.5, Alpha);
            }
            else
            {
                Alpha = 1;
            }

          
            

            DiceFrame.Rotation = this.Rotation;
            DiceFrame.Alpha = this.Alpha / 2;
            DiceFrame.X = X;
            DiceFrame.Y = Y;
            DiceFrame.ScaleW = ScaleW;
            DiceFrame.ScaleH = ScaleH;
        }

        public void StopRot()
        {
            GotoAndStop(0);

            RotationSpeed /= 10;
            Frames.Clear();
            SetImg(GlobalContent.LoadImg(ActionImages[Action], true));

            if ((Action == ActionManager.Action.up) || (Action == ActionManager.Action.right) || (Action == ActionManager.Action.left) || (Action == ActionManager.Action.down))
            {
                RotationGoal = (Gameplay.RNG.NextDouble() - 0.5) * (Math.PI / 10);
            }

            if (!isGolden)
            {
                DiceFrame.Destruct();
            }

        }

        bool toClear = false;
        public void clearDice()
        {
            GoalY = 720 * 2;
            toClear = true;
        }


        public override void Destruct()
        {
            base.Destruct();
            All.Remove(this);
        }



        public void initActionImages()
        {
            if (ActionImages == null)
            {
                ActionImages = new Dictionary<ActionManager.Action, string>();
                ActionImages.Add(ActionManager.Action.defence_buff, "ArmorBuffDice");
                ActionImages.Add(ActionManager.Action.shoot, "ArrowDice");
                ActionImages.Add(ActionManager.Action.atack_buff, "AttackBuffDice");
                ActionImages.Add(ActionManager.Action.attack, "AttackDice");
                ActionImages.Add(ActionManager.Action.cursed, "CurseDice");
                ActionImages.Add(ActionManager.Action.breakStone, "DestroyStoneDice");
                ActionImages.Add(ActionManager.Action.down, "DownDice");
                ActionImages.Add(ActionManager.Action.fireball, "FirebalDice");
                ActionImages.Add(ActionManager.Action.freeze, "FrostDice");
                ActionImages.Add(ActionManager.Action.healAll, "HealAllDice");
                ActionImages.Add(ActionManager.Action.heal, "HealDice");
                ActionImages.Add(ActionManager.Action.left, "LeftDice");
                ActionImages.Add(ActionManager.Action.right, "RightDice");
                ActionImages.Add(ActionManager.Action.scatter, "ScatterDice");
                ActionImages.Add(ActionManager.Action.defence, "ShieldDice");
                ActionImages.Add(ActionManager.Action.addStone, "StoneDice");
                ActionImages.Add(ActionManager.Action.strongAttack, "StrongAttackDice");
                ActionImages.Add(ActionManager.Action.swap, "SwapDice");
                ActionImages.Add(ActionManager.Action.up, "UpDice");
            }

        }

        public void QickStopRot()
        {
            StopRot();
            if ((Action == ActionManager.Action.up) || (Action == ActionManager.Action.right) || (Action == ActionManager.Action.left) || (Action == ActionManager.Action.down))
            {
                Rotation = (Gameplay.RNG.NextDouble() - 0.5) * (Math.PI / 10);
                Rotation = RotationGoal;
            }
        }

     }
}
