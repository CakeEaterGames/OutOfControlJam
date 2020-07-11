using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoCake;
using MonoCake.Objects;

namespace MonoCake.Objects
{
    public class SceneManager : BasicObject
    {
        public Scene CurrentScene;

        public virtual void SetScene(Scene s)
        {
            SetSceneWithoutInit(s);
            CurrentScene.Init();
        }

        public virtual void SetSceneWithoutInit(Scene s)
        {
            if (CurrentScene != null)
            {
                CurrentScene.Destruct();
            }
            CurrentScene = s;
            CurrentScene.SetParent(this);
        }

        public override void Destruct()
        {
            base.Destruct();
            if (CurrentScene != null)
            {
                CurrentScene.Destruct();
            }
            CurrentScene = null;
        }


    }
}
