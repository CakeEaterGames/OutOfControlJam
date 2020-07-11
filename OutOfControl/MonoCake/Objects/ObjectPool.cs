using MonoCake.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoCake.Objects
{
    public class ObjectPool : BasicObject
    {
        public bool[] isActive;
        public int[] lifetime;
        public int[] maxLifetime;

        public ObjectPool(int size)
        {
            isActive = new bool[size];
            lifetime = new int[size];
            maxLifetime = new int[size];
        }
        public ObjectPool()
        {

        }

        public int GetInactiveObject()
        {
            int i = Array.IndexOf(isActive, false);
            if (i > -1)
            {
                return i;
            }
            else
            {
                return FindTheOldest();
            }
        }

        public int GetObject(int maxLifeTime = 0)
        {
            var i = GetInactiveObject();
            Activate(i);
            return i;
        }

        public virtual void Activate(int i, int maxLifeTime = 0)
        {
            isActive[i] = true;
            lifetime[i] = 0;
            maxLifetime[i] = maxLifeTime;
        }

        public virtual void Deactivate(int i)
        {
            isActive[i] = false;
            lifetime[i] = 0;
            maxLifetime[i] = 0;
        }

        public int FindTheOldest()
        {
            int max = 0;
            int index = 0;
            for (int i = 0; i <= lifetime.Length; i++)
            {
                if (lifetime[i] > max)
                {
                    max = lifetime[i];
                    index = i;
                }
            }

            return index;
        }

        public override void Update()
        {

            if (ToUpdate)
            {
                for (int i = 0; i <= isActive.Length; i++)
                {
                    UpdateObject(i);

                    if (isActive[i])
                    {
                        if (maxLifetime[i] > 0 && lifetime[i] >= maxLifetime[i])
                        {
                            Deactivate(i);
                        }
                        else
                        {
                            lifetime[i]++;
                        }
                    }
                }
            }

        }

        public virtual void UpdateObject(int i)
        {

        }

        public override void Render()
        {
            if (ToRender)
            {

            }
        }



    }
}
