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
    public class HPbar : BasicObject
    {
        public double filled = 1;
        GameObject frame;
        GameObject bar;

        public Color Color { get => bar.color; set => bar.color = value; }

        public HPbar()
        {
             frame = new GameObject(GlobalContent.LoadImg("hp frame", true));
             bar = new GameObject(GlobalContent.LoadImg("hp bar", true));


            bar.SetCenter(4);
            frame.SetCenter(4);

            bar.AddRender(this);
            frame.AddRender(this);

            bar.X = 1;

            bar.color = Color.Red;
        }
        public override void Update()
        {
            bar.ScaleW = Animation.Recurrent(bar.ScaleW, filled, divBy: 3);
        }
    }
}
