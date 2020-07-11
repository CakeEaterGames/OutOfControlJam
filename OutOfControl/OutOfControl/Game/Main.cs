using System;
using MonoCake;
using MonoCake.Objects;
namespace OutOfControl
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
