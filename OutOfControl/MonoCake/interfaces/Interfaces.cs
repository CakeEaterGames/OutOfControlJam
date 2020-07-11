using MonoCake.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoCake
{
    public interface IUpdateable
    {
        bool ToUpdate { get; set; }
        void Update();
        void AddUpdate();
        void RemoveUpdate();
    }
    public interface IRenderable
    {
        bool ToRender { get; set; }
        void Render();
        void AddRender();
        void RemoveRender();
    }

    /* public interface IHasObjectsLists
     {
          List<BasicObject> Objects { get; }
          List<BasicObject> RenderObjects { get; }
          List<BasicObject> UpdateObjects { get; }
     }*/

    public interface IDefaultObject : IRenderable, IUpdateable, IDestructable
    {

    }

    public interface IHasSubLayers
    {
        RenderParameters RenderParameters { get; set; }
        void UpdateRenderParameters(RenderParameters rp);
    }
    /* public interface IHasRenderParameters
     {
         RenderParameters LocalRenderParameters { get; set; }
         void SetRenderParameters(RenderParameters rp);
     }
     */
    public interface IOnSubLayer
    {
        void SetLayerTop();
        void SetLayerPos(int destination);
    }

    public interface IDestructable
    {
        void Destruct();
    }
}
