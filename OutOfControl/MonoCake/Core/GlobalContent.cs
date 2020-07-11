using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoCake.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MonoCake
{
    public static class GlobalContent
    {
        public static ContentManager Content;

        public static Dictionary<string, Texture2D> textures;

        public static Assembly asm;
        public static String assemblyName;

        static String[] xnbFiles;
        static String[] resFiles;



        [System.Runtime.CompilerServices.MethodImpl(MethodImplOptions.NoInlining)]
        public static void Init(string nSpace)
        {
            Content = CakeEngine.staticContent;
            textures = new Dictionary<string, Texture2D>();

            GlobalContent.asm = System.Reflection.Assembly.GetCallingAssembly();

            // GlobalContent.assemblyName = System.Reflection.Assembly.GetCallingAssembly().EntryPoint.DeclaringType.Namespace;
            GlobalContent.assemblyName = nSpace;
            GlobalContent.resFiles = GlobalContent.asm.GetManifestResourceNames();


            xnbFiles = Directory.GetFiles("Content", "*.xnb", SearchOption.AllDirectories);


            for (int i = 0; i < resFiles.Length; i++)
            {
                //dwrite.line(resFiles[i]);
                resFiles[i] = resFiles[i].Replace(assemblyName + ".Resources.", "");
                resFiles[i] = resFiles[i].Replace(".png", "");
                resFiles[i] = resFiles[i].Replace('.', '\\');

            }
            for (int i = 0; i < xnbFiles.Length; i++)
            {
                //dwrite.line(xnbFiles[i]);
                xnbFiles[i] = xnbFiles[i].Replace("Content\\", "");
                xnbFiles[i] = xnbFiles[i].Replace(".xnb", "");
            }

#if DEBUG
            string p = Directory.GetCurrentDirectory();
            p = p.Remove(p.IndexOf("\\bin"))+"\\Content";
            // p = "C:\\Users\\torto\\source\\repos\\SevenReboot rewrite\\SevenReboot\\Content"
            BuildShortPaths(true, p);
#else
            BuildShortPaths(false);
#endif

            /*   foreach (var a in ShortPath.Paths)
               {
                   dwrite.line(a.Key + " " + a.Value);
               }*/

            Console.WriteLine("Loading images");
#if !DEBUG
            foreach (string a in resFiles)
            {
                LoadImg(a);
                //dwrite.line(a);
            }
#endif
            Console.WriteLine("Loading images Done");
        }

        public static void BuildShortPaths(bool fromSourse = true, string path = "")
        {
            for (int i = 0; i < resFiles.Length; i++)
            {
                string key = ToLastPath(resFiles[i]);
                if (ShortPath.Paths.ContainsKey(key))
                {
                    Throw2files(resFiles[i], ShortPath.Paths[key]);
                }
                ShortPath.Paths.Add(key, resFiles[i]);

            }

            if (fromSourse)
            {
                string[] ext = { "png", "jpg", "ogg", "wav", "mp3", "mp4", "fx", "xnb" };
                List<string> srcFiles = new List<string>();
                foreach (string s in ext)
                {
                    var a = Directory.GetFiles(path, "*." + s, SearchOption.AllDirectories).ToList();
                    srcFiles = srcFiles.Concat(a).ToList();
                }

                for (int i = 0; i < srcFiles.Count; i++)
                {
                    if (srcFiles[i].Contains("bin") || srcFiles[i].Contains("obj"))
                    {
                        continue;
                    }
                    string val = srcFiles[i].Substring(srcFiles[i].IndexOf("Content") + ("Content".Length + 1));
                    val = val.Remove(val.LastIndexOf("."));

                    string key = ToLastPath(val);
                    if (ShortPath.Paths.ContainsKey(key))
                    {
                        Throw2files(srcFiles[i], ShortPath.Paths[key]);
                    }

                    ShortPath.Paths.Add(key, val);
                    //dwrite.line(key+" "+val);
                }
            }
            else
            {
                for (int i = 0; i < xnbFiles.Length; i++)
                {
                    string key = ToLastPath(xnbFiles[i]);
                    if (ShortPath.Paths.ContainsKey(key))
                    {
                        Throw2files(xnbFiles[i], ShortPath.Paths[key]);
                    }
                    ShortPath.Paths.Add(key, xnbFiles[i]);
                }
            }

        }

        static void Throw2files(string a, string b)
        {
            throw new Exception("There are 2 files with the same name: {" + a + "} and {" + b + "}");
        }

        static string ToLastPath(string path)
        {
            int pos1 = path.LastIndexOf("\\");
            if (pos1 == -1)
            {
                return path;
            }
            else
            {
                return path.Substring(pos1 + 1);
            }

        }

        public static Texture2D LoadImg(string path, bool isShortPath = false)
        {

            if (isShortPath)
            {
                path = ShortPath.Get(path);
            }
            path = path.Replace('/', '\\');
            if (xnbFiles.Contains(path))
            {
                return LoadImgFromContent(path);
            }
            else if (resFiles.Contains(path))
            {
                return LoadImgFromRes(path);
            }
            else
            {
                throw new Exception("Can't find image in Content or Resources " + path);
            }
        }
        public static Texture2D LoadImgFromContent(string path)
        {
            return Content.Load<Texture2D>(path);
        }

        public static Texture2D LoadImgFromRes(string path)
        {
            path = path.Replace('\\', '.');
            path = path.Replace('/', '.');

            if (textures.ContainsKey(path))
            {
                return textures[path];
            }
            else
            {
                Stream str = asm.GetManifestResourceStream(assemblyName + ".Resources." + path + ".png");
                Texture2D tex = Texture2D.FromStream(CakeEngine.staticDevice, str);

                var t = FixAlpha(tex);
                tex.Dispose();

                textures.Add(path, t);
                str.Close();
                str.Dispose();
                return t;

                //return tex;
            }
        }


        public static Texture2D FixAlpha(Texture2D file)
        {
            RenderTarget2D result = null;

            //Setup a render target to hold our final texture which will have premulitplied alpha values
            result = new RenderTarget2D(CakeEngine.staticDevice, file.Width, file.Height);

            RenderManager.SetTarget(result);
            RenderManager.ClearTarget(Color.Black);

            //Multiply each color by the source alpha, and write in just the color values into the final texture
            //TODO: blend states should be constracted once.
            BlendState blendColor = new BlendState
            {
                ColorWriteChannels = ColorWriteChannels.Red | ColorWriteChannels.Green | ColorWriteChannels.Blue,

                AlphaDestinationBlend = Blend.Zero,
                ColorDestinationBlend = Blend.Zero,

                AlphaSourceBlend = Blend.SourceAlpha,
                ColorSourceBlend = Blend.SourceAlpha
            };

            SpriteBatch spriteBatch = new SpriteBatch(CakeEngine.staticDevice);
            spriteBatch.Begin(SpriteSortMode.Immediate, blendColor);
            spriteBatch.Draw(file, file.Bounds, Color.White);
            spriteBatch.End();

            //Now copy over the alpha values from the PNG source texture to the final one, without multiplying them
            BlendState blendAlpha = new BlendState
            {
                ColorWriteChannels = ColorWriteChannels.Alpha,

                AlphaDestinationBlend = Blend.Zero,
                ColorDestinationBlend = Blend.Zero,

                AlphaSourceBlend = Blend.One,
                ColorSourceBlend = Blend.One
            };

            spriteBatch.Begin(SpriteSortMode.Immediate, blendAlpha);
            spriteBatch.Draw(file, file.Bounds, Color.White);
            spriteBatch.End();

            //Release the GPU back to drawing to the screen 
            RenderManager.SetTarget(null);

            return result as Texture2D;
        }

    }

}
public static class ShortPath
{
    public static Dictionary<String, String> Paths = new Dictionary<string, string>();
    public static string Get(string path)
    {
        if (!Paths.ContainsKey(path))
        {
            throw new Exception("No files with {" + path + "} short path");
        }
        return Paths[path];
    }
}

