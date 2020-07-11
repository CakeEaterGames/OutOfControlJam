using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoCake.Objects
{
    public class BasicObject
    {
        public string Name { get; set; }

        private BasicObject parent = null;

        public List<BasicObject> Objects { get; } = new List<BasicObject>();
        public List<BasicObject> RenderObjects { get; } = new List<BasicObject>();
        public List<BasicObject> UpdateObjects { get; } = new List<BasicObject>();

        public RenderParameters BaseRenderParameters { get; set; } = new RenderParameters();
        public RenderParameters CurrentRenderParameters { get; set; } = new RenderParameters();

        public bool ToUpdate { get; set; }
        public bool ToRender { get; set; }
        public BasicObject Parent { get => parent; set => SetParent(value); }

        public BasicObject SetParent(BasicObject obj = null, bool toKeepUpdateRender = false)
        {
            if (obj != parent)
            {
                bool addU = false, addR = false;
                if (toKeepUpdateRender)
                {
                    addR = this.ToRender;
                    addU = this.ToUpdate;
                }

                RemoveRender();
                RemoveUpdate();
                if (parent != null && parent.Objects.Contains(this))
                {
                    parent.Objects.Remove(this);
                }

                parent = obj;

                if (parent != null)
                {
                    parent.Objects.Add(this);
                }


                if (addR) AddRender();
                if (addU) AddUpdate();


            }
            return this;
        }

        public virtual void SetBaseRenderParameters(RenderParameters rp)
        {
            BaseRenderParameters = rp;

        }
        public virtual void SetCurrentRenderParameters(RenderParameters rp)
        {
            CurrentRenderParameters = BaseRenderParameters.CombineWith(rp);
            ChainUpdateRenderParameters();
        }
        public virtual void ChainUpdateRenderParameters()
        {
            foreach (BasicObject bo in Objects)
            {
                bo.SetCurrentRenderParameters(CurrentRenderParameters);
            }
        }

        public virtual void Update()
        {

        }

        public virtual void UpdateAnimation()
        {

        }

        public virtual void Render()
        {

        }

        public virtual void ChainRender()
        {

            for (int i = 0; i < RenderObjects.Count; i++)
            {
                if (RenderObjects[i] != null)
                {
                    if (RenderObjects[i].ToRender)
                    {
                        RenderObjects[i].Render();
                        RenderObjects[i].ChainRender();
                    }
                }
                else
                {
                    RenderObjects.RemoveAt(i);
                    i--;
                }
            }


        }
        public virtual void ChainUpdate()
        {
            for (int i = 0; i < UpdateObjects.Count; i++)
            {
                if (UpdateObjects[i] != null)
                {
                    if (UpdateObjects[i].ToUpdate)
                    {
                        UpdateObjects[i].UpdateAnimation();
                        UpdateObjects[i].Update();
                    }
                }
                else
                {
                    UpdateObjects.RemoveAt(i);
                    i--;
                    continue;
                }

                if (UpdateObjects[i] != null)
                {
                    if (UpdateObjects[i].ToUpdate)
                    {
                        UpdateObjects[i].ChainUpdate();
                    }
                }
                else
                {
                    UpdateObjects.RemoveAt(i);
                    i--;
                    continue;
                }
            }
        }

        public void ChainCollectObjects(List<BasicObject> all)
        {
            all.Add(this);
            foreach (BasicObject bo in Objects)
            {
                bo.ChainCollectObjects(all);
            }
        }

        public virtual void Destruct()
        {
            RemoveUpdate();
            RemoveRender();
            while (Objects.Count > 0)
            {
                if (Objects[0] != null)
                    Objects[0].Destruct();
                else
                    Objects.RemoveAt(0);
            }
            SetParent(null);
        }
        public virtual BasicObject AddRender(BasicObject parent)
        {
            SetParent(parent);
            AddRender();
            return this;
        }
        public virtual BasicObject AddRender()
        {
            if (parent != null)
            {
                if (!parent.RenderObjects.Contains(this))
                {
                    parent.RenderObjects.Add(this);
                }
            }
            else
            {
                if (!CakeEngine.RenderObjects.Contains(this))
                {
                    CakeEngine.RenderObjects.Add(this);
                }
            }

            ToRender = true;
            return this;
        }

        public virtual BasicObject RemoveRender()
        {
            if (parent != null)
            {
                if (parent.RenderObjects.Contains(this))
                {
                    parent.RenderObjects.Remove(this);
                }
            }
            else
            {
                if (CakeEngine.RenderObjects.Contains(this))
                {
                    CakeEngine.RenderObjects.Remove(this);
                }
            }

            ToRender = false;
            return this;
        }
        public virtual BasicObject AddUpdate(BasicObject parent)
        {
            SetParent(parent);
            AddUpdate();
            return this;
        }
        public virtual BasicObject AddUpdate()
        {
            if (parent != null)
            {
                if (!parent.UpdateObjects.Contains(this))
                {
                    parent.UpdateObjects.Add(this);
                }
            }
            else
            {
                if (!CakeEngine.UpdateObjects.Contains(this))
                {
                    CakeEngine.UpdateObjects.Add(this);
                }
            }

            ToUpdate = true;
            return this;
        }

        /// <summary>
        /// Short for AddRender(); AddUpdate();
        /// </summary>
        public virtual BasicObject AddUR()
        {
            AddUpdate();
            AddRender();
            return this;
        }
        /// <summary>
        /// Short for AddRender(); AddUpdate();
        /// </summary>
        public virtual BasicObject AddUR(BasicObject parent)
        {
            SetParent(parent);
            AddUR();
            return this;
        }

        public virtual BasicObject RemoveUpdate()
        {
            if (parent != null)
            {
                int i = parent.UpdateObjects.IndexOf(this);
                if (i > -1)
                {
                    parent.UpdateObjects[i] = null;
                }
            }
            else
            {
                int i = CakeEngine.UpdateObjects.IndexOf(this);
                if (i > -1)
                {
                    CakeEngine.UpdateObjects[i] = null;
                }
            }
            ToUpdate = false;
            return this;
        }

        public override string ToString()
        {
            string p = "null";
            if (parent != null)
            {
                p = parent.GetType().ToString();
            }

            return "Basic object " + GetType().ToString() + " " + Name + ". Parent: " + p + " Update Obj: " + UpdateObjects.Count + " Render Obj: " + RenderObjects.Count;
        }

        public void ChainDrawTree(string spaces, bool renderObjects)
        {
            List<BasicObject> l;
            if (renderObjects)
                l = RenderObjects;
            else
                l = Objects;

            dwrite.line(spaces + GetType().ToString() + " " + Name);
            foreach (BasicObject g in l)
            {
                g.ChainDrawTree(spaces + "|", renderObjects);
            }
        }

    }
}
