using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using MonoCake.Objects;
namespace MonoCake
{
    public class VideoGameObject : GameObject
    {
        public Video video;
        public VideoPlayer videoPlayer;

        public VideoGameObject(Video video)
        {
            this.video = video;
            videoPlayer = new VideoPlayer();
            W = video.Width;
            H = video.Height;
        }
        public new void Play()
        {
            videoPlayer.Play(video);
        }
        public new void Stop()
        {
            videoPlayer.Stop();
        }
        public void Pause()
        {
            videoPlayer.Pause();
        }
        public void Resume()
        {
            videoPlayer.Resume();
        }

        public bool IsFinished()
        {
            return video.Duration <= videoPlayer.PlayPosition;
        }


        public override void Render()
        {
            var rp = CurrentRenderParameters;
            if (ToRender && IsVisable && rp.IsVisable)
            {
                bool good = true;
                try
                {
                    SetImg(videoPlayer.GetTexture());
                }
                catch (Exception e)
                {
                    dwrite.line(e.ToString());
                    good = false;
                }
                if (good)
                {
                    StandartRender();
                }
            }

        }

        public override void Destruct()
        {
            videoPlayer.Stop();
            videoPlayer.Dispose();
            video.Dispose();
            base.Destruct();
        }

    }
}
