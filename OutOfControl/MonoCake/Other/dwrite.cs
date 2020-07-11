using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MonoCake
{

    public static class dwrite
    {
        public static int wtf = 0;
        [MethodImpl(MethodImplOptions.NoOptimization | MethodImplOptions.NoInlining)]
        public static void line<T>(T s)
        {
#if DEBUG
            Console.WriteLine(s);
#endif
            wtf = 0;
        }
        [MethodImpl(MethodImplOptions.NoOptimization | MethodImplOptions.NoInlining)]
        public static void line()
        {
#if DEBUG
            Console.WriteLine();
#endif
            wtf = 0;
        }
        [MethodImpl(MethodImplOptions.NoOptimization | MethodImplOptions.NoInlining)]
        public static void chars<T>(T s)
        {
#if DEBUG
            Console.Write(s);
#endif
            wtf = 0;
        }


    }
}
