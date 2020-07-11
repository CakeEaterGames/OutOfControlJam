using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoCake.Objects
{
    public class ObjectPoolExpencive<T> : ObjectPool
    {
        public T[] objects;
        public ObjectPoolExpencive(int size) : base(size)
        {
            objects = new T[size];
        }
    }
}
