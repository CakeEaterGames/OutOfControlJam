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
    public class ChatBox : BasicObject
    {
		public ChatBox()
        {
            BaseRenderParameters.X = 10;
            BaseRenderParameters.Y = 10;

            BaseRenderParameters.ScaleW = 0.5;
            BaseRenderParameters.ScaleH = 0.5;
        }
        public List<TextField> texts = new List<TextField>();
		public void addText(string txt)
        {
            foreach (var t in texts)
            {
                t.Y += TextField.defaultFont.MeasureString(txt).Y;
            }
            var tx = new TextField(txt, Color.White);
            tx.AddUR(this);
            texts.Add(tx);

        }

		public void AddLine()
        {
            addText("---------------------------");
        }

        int maxLine = 30;

		public void AddLotsOfText(string txt)
        {
            var a = txt.Split(' ');
            string collect = "";
            Stack<string> col = new Stack<string>();
            for (int i = 0;i<a.Length;i++)
            {
                collect += a[i]+" ";
                if (collect.Length>= maxLine)
                {
                    col.Push(collect);
                    collect = "";
                  
                }
            }

            col.Push(collect);

            while (col.Count>0)
            {
                addText(col.Pop());
            }

        }

		public void Clear()
        {
            foreach (var t in texts)
            {
                t.Destruct();
            }
            texts.Clear();
        }
    }
}
