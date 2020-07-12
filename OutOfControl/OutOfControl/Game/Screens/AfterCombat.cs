using MonoCake;
using MonoCake.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Pellicalo
{
    class AfterCombatScreen : Scene
    {
        public AfterCombatScreen()
        {
            GameObject bg = new GameObject();
            bg.SetImg(GlobalContent.LoadImg("BackgroundShop", true)).Scale(5).
            AddUR(this);

            GameObject Guy = new GameObject();
            Guy.SetImg(GlobalContent.LoadImg("ShopGuy", true)).Scale(5).
            AddUR(this);

            GameObject TB = new GameObject();
            TB.SetImg(GlobalContent.LoadImg("TextBoxShop", true)).Scale(5).
            AddUR(this);

            PlaceButtons();
        }

        List<Button> buttons = new List<Button>();
        public void PlaceButtons()
        {
            if (Gameplay.RNG.NextDouble() < 0.9)
            {
                var b = new HireBtn();

                var t = Entity.GetHire();

                b.isCursed = (Gameplay.RNG.NextDouble() < 0.25);
                b.type = t;
                b.updateText();
                buttons.Add(b);
            }
            if (Gameplay.RNG.NextDouble() < 0.8)
            {
                var b = new LevelUpBtn();
                var w = Gameplay.Warriors[Gameplay.RNG.Next(0, Gameplay.Warriors.Count)];

                b.isCursed = (Gameplay.RNG.NextDouble() < 0.25);
                b.entity = w;
                b.updateText();
                buttons.Add(b);
            }
            if (Gameplay.RNG.NextDouble() < 0.8)
            {
                var b = new DiceBtn();
                b.isCursed = (Gameplay.RNG.NextDouble() < 0.25);
                b.updateText();
                buttons.Add(b);
            }
            if (Gameplay.RNG.NextDouble() < 0.8)
            {
                var b = new PureBtn();
                buttons.Add(b);
            }
            var bb = new SkipBtn();
            buttons.Add(bb);
            for (int i = 0; i < buttons.Count; i++)
            {
                var b = buttons[i];
                b.ParentClass = this;
                b.AddUR(this);
                b.Scale(3);
                b.SetXY(1280 / 2, 720 / 2 +( i  - (buttons.Count / 2.0) ) * 100);
            }
        }

        public void Leave()
        {
            Gameplay.level++;
            KEY.ResetClicks();
            Gameplay.self.Combat();
        }
    }

    class HireBtn : Button
    {
 
        public Entity.EType type = Entity.EType.warrior;
        public bool isCursed = false;
        public HireBtn()
        {
         
        }
        public void updateText()
        {
            textField.text = "Hire a " + type;
            if (isCursed)
            {
                textField.text += " But get a CURSE!";
            }
        }
        public override void Click()
        {
            AudioManager.SinglePlay("click2");
            var w = new Gameplay.SaveWarrior();
            w.level = 1;
            w.type = type;
            Gameplay.Warriors.Add(w);
            Gameplay.ActionManager.AddCharToPool(w.type);
            ((AfterCombatScreen)ParentClass).Leave();
            if (isCursed)
            {
                Gameplay.GiveCurse();
            }
        }
    }

    class LevelUpBtn : Button
    {
        public Gameplay.SaveWarrior entity;
        public bool isCursed = false;
        public LevelUpBtn()
        {
           

        }
        public void updateText()
        {
            textField.text = "Level up " + entity.type + " to level " + (entity.level + 1);
            if (isCursed)
            {
                textField.text += " But get a CURSE!";
            }
        }
        public override void Click()
        {
            AudioManager.SinglePlay("click2");
            entity.level++;
            if (isCursed)
            {
                Gameplay.GiveCurse();
            }
            ((AfterCombatScreen)ParentClass).Leave();
        }
    }

    class DiceBtn : Button
    {
        public int amount = 1;
        public bool isCursed = false;
        public DiceBtn()
        {
            
        }
        public void updateText()
        {
            textField.text = "+" + amount + " Dice";
            if (isCursed)
            {
                textField.text += " But get a CURSE!";
            }
        }
        public override void Click()
        {
            AudioManager.SinglePlay("click2");
            Gameplay.DiceCount += amount;
            ((AfterCombatScreen)ParentClass).Leave();
            if (isCursed)
            {
                Gameplay.GiveCurse();
            }
        }
    }

    class SkipBtn : Button
    {
        public SkipBtn()
        {
            textField.text = "Skip";

        }
        public override void Click()
        {
            AudioManager.SinglePlay("click2");
            ((AfterCombatScreen)ParentClass).Leave();
        }
    }

    class PureBtn : Button
    {
        public PureBtn()
        {
            textField.text = "Remove 2 curses";

        }
        
        public override void Click()
        {
            if (Gameplay.ActionManager.ActionPool.Contains(ActionManager.Action.cursed))
            {
                Gameplay.ActionManager.ActionPool.Remove(ActionManager.Action.cursed);
            }
            if (Gameplay.ActionManager.ActionPool.Contains(ActionManager.Action.cursed))
            {
                Gameplay.ActionManager.ActionPool.Remove(ActionManager.Action.cursed);
            }

            AudioManager.SinglePlay("click2");
            ((AfterCombatScreen)ParentClass).Leave();
        }
    }
}
