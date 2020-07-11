using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System.Net.NetworkInformation;
using MonoCake.Objects;

namespace MonoCake
{
    public class CakeEngine : Game
    {
        public static CakeEngine self;

        public GraphicsDeviceManager graphics;

        public static int currentResolution = 0;
        public static int screenW, screenH;
        public static int offsetX, offsetY;
        public static bool isFullScreen = false;

        public static int FullScreenW, FullScreenH;

        public static ContentManager staticContent;
        public static GraphicsDeviceManager staticGraphics;
        public static GraphicsDevice staticDevice;
        public static SpriteBatch spriteBatch;

        public static ProjectConfiguration config;

        public static List<BasicObject> UpdateObjects { get; } = new List<BasicObject>();
        public static List<BasicObject> RenderObjects { get; } = new List<BasicObject>();

        public static List<BasicObject> Objects { get; } = new List<BasicObject>();


        public CakeEngine()
        {

        }

        public static void SetResolution(int currentResolution, bool fullscreen = false)
        {
            screenW = config.AllowedResolutions[currentResolution].X;
            screenH = config.AllowedResolutions[currentResolution].Y;

            staticGraphics.PreferredBackBufferHeight = screenH;
            staticGraphics.PreferredBackBufferWidth = screenW;

            double renderSX = screenW / (double)config.CoreWidth;
            double renderSY = screenH / (double)config.CoreHeight;

            offsetX = 0;
            offsetY = 0;


            if (renderSX != renderSY)
            {
                offsetX = (int)((1 - (Math.Min(renderSX, renderSY) / renderSX)) * screenW) / 2;
                offsetY = (int)((1 - (Math.Min(renderSX, renderSY) / renderSY)) * screenH) / 2;

                renderSX = Math.Max(renderSX, renderSY);
                renderSY = Math.Max(renderSX, renderSY);

            }
            screenW = (int)(config.CoreWidth * renderSX);
            screenH = (int)(config.CoreHeight * renderSY);



            if (fullscreen)
            {
                if (!staticGraphics.IsFullScreen)
                {
                    staticGraphics.ToggleFullScreen();
                }
            }
            else
            {
                if (staticGraphics.IsFullScreen)
                {
                    staticGraphics.ToggleFullScreen();
                }
            }

            CakeEngine.currentResolution = currentResolution;
            staticGraphics.ApplyChanges();
        }

        public static void CloseApp()
        {
            staticContent.Dispose();
            self.Exit();
        }

        public static void UpdateAllObjects()
        {
            for (int i = 0; i < UpdateObjects.Count; i++)
            {
                if (UpdateObjects[i] != null)
                {
                    UpdateObjects[i].UpdateAnimation();
                    UpdateObjects[i].Update();
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

        public static void RenderAllObjects()
        {
            for (int i = 0; i < RenderObjects.Count; i++)
            {
                if (RenderObjects[i] != null)
                {
                    RenderObjects[i].Render();
                    RenderObjects[i].ChainRender();
                }
                else
                {
                    RenderObjects.RemoveAt(i);
                    i--;
                }
            }
        }

    }

}