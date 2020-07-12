using MonoCake;
using MonoCake.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OutOfControl.Gameplay;

namespace OutOfControl
{
    class AfterCombatScreen : Scene
    {
        public AfterCombatScreen()
        {
            GameObject bg = new GameObject();
            bg.SetImg(GlobalContent.LoadImg("bg", true)).Scale(5).
            AddUR(this);


            PlaceButtons();
        }

        List<Button> buttons = new List<Button>();
        public void PlaceButtons()
        {
            if (Gameplay.RNG.NextDouble() < 0.25)
            {
                var b = new HireBtn();

                var t = Entity.GetHire();
                b.type = t;
                buttons.Add(b);
            }
            if (Gameplay.RNG.NextDouble() < 0.25)
            {
                var b = new LevelUpBtn();
                var w = Gameplay.Warriors[Gameplay.RNG.Next(0, Gameplay.Warriors.Count)];
                b.entity = w;
                buttons.Add(b);
            }
            if (Gameplay.RNG.NextDouble() < 0.25)
            {
                var b = new DiceBtn();
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
                b.SetXY(1280 / 2, 720 / 2 + i * 100 - (buttons.Count / 2.0));
            }
        }

        public void Leave()
        {
            Gameplay.level++;
            //Gameplay.level++;
            KEY.ResetClicks();
            Gameplay.self.Combat();
        }
    }

    class HireBtn : Button
    {
 
        public Entity.EType type = Entity.EType.warrior;
        public HireBtn()
        {
            textField.text = "Hire a " + type;
        }
        public override void Click()
        {
            var w = new SaveWarrior();
            w.level = 1;
            w.type = Entity.EType.warrior;
            Gameplay.Warriors.Add(w);
            Gameplay.ActionManager.AddCharToPool(w.type);
            ((AfterCombatScreen)ParentClass).Leave();

        }
    }

    class LevelUpBtn : Button
    {
        public SaveWarrior entity;
        public LevelUpBtn()
        {
            textField.text = "Level up" + entity.type + " to level " + (entity.level + 1)+" "+ entity.level ;

        }
        public override void Click()
        {
            entity.level++;
            ((AfterCombatScreen)ParentClass).Leave();
        }
    }

    class DiceBtn : Button
    {
        public int amount = 1;
        public DiceBtn()
        {
            textField.text = "+" + amount + " Dice";
        }
        public override void Click()
        {
            Gameplay.DiceCount += amount;
            ((AfterCombatScreen)ParentClass).Leave();
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

            ((AfterCombatScreen)ParentClass).Leave();
        }
    }
}
