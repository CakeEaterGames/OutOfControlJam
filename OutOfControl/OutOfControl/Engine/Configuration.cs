using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoCake;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutOfControl
{
    public class Config : ProjectConfiguration
    {
        public SpriteFont ConsolasFont;

        public override void Init()
        {
            version = "1.0";
            CoreWidth = 1280;
            CoreHeight = 720;
            Fps = 60;
            BgColor = Color.White;

            AllowedResolutions.Add(new Point(CoreWidth, CoreHeight));  //0
            AllowedResolutions.Add(new Point(Engine.FullScreenW, Engine.FullScreenH));   //1

            //AllowedResolutions.Add(new Point(640, 360));   //2
            //AllowedResolutions.Add(new Point(720, 405));   //3
            //AllowedResolutions.Add(new Point(864, 486));   //4
            //AllowedResolutions.Add(new Point(960, 540));   //5
            //AllowedResolutions.Add(new Point(1024, 576));  //6
            //AllowedResolutions.Add(new Point(1280, 720));  //7
            //AllowedResolutions.Add(new Point(1920, 1080)); //8

            Engine.SetResolution(0);

            Engine.self.IsMouseVisible = true;
            Engine.self.TargetElapsedTime = TimeSpan.FromSeconds(1d / Fps);

            // ConsolasFont = GlobalContent.Content.Load<SpriteFont>(ShortPath.Get("Consolas"));
        }
    }
}
