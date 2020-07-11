using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoCake
{
    public class ProjectConfiguration
    {
        public string version;

        private int fps = 60;
        public int Fps { get => fps; set { fps = value; CakeEngine.self.TargetElapsedTime = TimeSpan.FromSeconds(1d / (double)value); } }

        public List<Point> AllowedResolutions { get; } = new List<Point>();
        public int CoreWidth { get; protected set; } = 1280;
        public int CoreHeight { get; protected set; } = 720;

        public Color BgColor { get; set; } = Color.White;

        public virtual void Init()
        {

        }
    }
}
