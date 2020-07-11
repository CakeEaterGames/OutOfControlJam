using System;
using MonoCake;
using MonoCake.Objects;
namespace OutOfControl
{
    public class Main : BasicObject
    {

        public Main()
        {
            GameObject a = new GameObject();
            a.SetImg(GlobalContent.LoadImg("ananas", true));
            a.AddUR(this);
        }
        public override void Update()
        {
            Console.WriteLine("Hello world");
        }

    }
}
