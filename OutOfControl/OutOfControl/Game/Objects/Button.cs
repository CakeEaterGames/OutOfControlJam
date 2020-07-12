using Microsoft.Xna.Framework;
using MonoCake;
using MonoCake.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pellicalo
{
    class Button : GameObject
    {
        public BasicObject ParentClass;
        public TextField textField = new TextField("Sample Text", new Color(216, 106, 69));
        public Button()
        {
            AddImg(GlobalContent.LoadImg("btn2",true),"");
            AddFrame("", 1, new Rectangle(432, 0, 215, 23));
            AddFrame("", 1, new Rectangle(649, 0, 215, 23));
            GotoAndStop(0);

            SetCenter(5);
            textField.alignmentH = TextField.Align.center;
            textField.alignment = TextField.Align.center;

            textField.AddUR(this);
        }
        public override void Update()
        {
            textField.ScaleH = ScaleH / 2.25;
            textField.ScaleW = ScaleW / 2.25;
            textField.X = this.X;
            textField.Y = this.Y;

            GotoAndStop(0);
            if (GetAbsoluteRect().Contains((int)KEY.MouseX, (int)KEY.MouseY))
            {
                this.color = Color.Yellow;
                if (KEY.LDown)
                {
                    GotoAndStop(1);
                }
                if (KEY.LClick)
                {
                    Click();
                    
                }
                if (KEY.RClick)
                {
                    RClick();
                }

            }
            else
            {
                this.color = Color.White;
            }
        }

        public virtual void Click()
        {

        }
        public virtual void RClick()
        {

        }
    }
}
