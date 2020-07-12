using System;
using MonoCake;
using MonoCake.Objects;
namespace Pellicalo
{
    public class Main : BasicObject
    {
    
        SceneManager SceneManager = new SceneManager();
        public Main()
        {
            SceneManager.SetScene(new Gameplay());
            SceneManager.AddUR(this);
            SceneManager.CurrentScene.AddUR();
        }
        public override void Update()
        {
          
        }

    }
}
