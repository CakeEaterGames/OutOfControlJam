using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoCake
{
    public class Tools
    {
        public static Random rng = new Random();

        public static double Rand(double a, double b)
        {
            return rng.NextDouble() * (b - a) + a;
        }
        public static int Rand(int a, int b)
        {
            b++;
            return (int)(rng.NextDouble() * (b - a) + a);
        }
        public static double Limit(double left, double right, double value)
        {
            return Math.Max(Math.Min(value, right), left);
        }

        public static void PrintList<T>(List<T> l)
        {
            dwrite.chars("[");
            foreach (T v in l)
            {
                dwrite.chars(v.ToString() + ", ");
            }
            dwrite.line("]");
        }
    }
}
