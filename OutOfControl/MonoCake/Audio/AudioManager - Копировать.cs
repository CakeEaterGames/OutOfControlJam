//using Microsoft.Xna.Framework.Audio;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Media;
//using SoLoud;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace MonoCake
//{
//    public static class AudioManager
//    {
//        public static Dictionary<String, Song> SongList = new Dictionary<String, Song>();
//        public static Dictionary<String, double> DefaultSongVolume = new Dictionary<String, double>();
//        public static String currentSong = "";
//        public static List<String> bgm = new List<String>();

//        public static Dictionary<String, SoundEffect> SoundList = new Dictionary<String, SoundEffect>();
//        public static Dictionary<String, double> DefaultSoundVolume = new Dictionary<String, double>();
//        public static Dictionary<String, SoundEffectInstance> currentSounds = new Dictionary<String, SoundEffectInstance>();

//        public static AudioManagerMode mode = AudioManagerMode.other;
//        public enum AudioManagerMode
//        {
//            bgm, other
//        }

//        static AudioManager()
//        {


//            //----------------------Examples----------------------

//            /*
//            SongList.Add("menu", Engine.loader.Load<Song>("audio/placeholders/Portal 2 - Overgrowth"));

//            SoundList.Add("menu click", Engine.loader.Load<SoundEffect>("audio/sounds/Menu Click"));

//            DefaultSongVolume.Add("menu", 0.1);

//            bgm.Add("bgm1");
//            bgm.Add("bgm2");
//            bgm.Add("bgm3");
//            bgm.Add("bgm4");
//            bgm.Add("bgm5");
//            bgm.Add("bgm6");

//            DefaultSoundVolume.Add("menu click", 0.002);
//            */

//            //----------------------Examples----------------------
//        }


//        public static void Update()
//        {
//            if (currentSong == "" || MediaPlayer.PlayPosition.TotalSeconds >= SongList[currentSong].Duration.TotalSeconds - 5)
//            {
//                if (mode == AudioManagerMode.bgm && !toSlowStopSong)
//                {
//                    SlowStopSong(0.005, bgm[Tools.Rand(0, bgm.Count - 1)]);
//                }

//            }
//            if (currentSong != "")
//            {
//                if (toSlowStopSong && toSlowStopSong)
//                {
//                    StopSong();
//                    toSlowStopSong = false;
//                }

//                if (toSlowStopSong)
//                {
//                    if (MediaPlayer.Volume >= 0)
//                        MediaPlayer.Volume += (float)SongGrovSpeed;
//                    if (MediaPlayer.Volume <= 0)
//                    {
//                        LoopSong(false);
//                        toSlowStopSong = false;
//                        StopSong();
//                        SongVolume(1);
//                        currentSong = "";
//                        if (songAfterSlow != "")
//                        {
//                            PlaySong(songAfterSlow, false);
//                        }
//                    }
//                }
//                else 
//                if (toSlowStartSong)
//                {
//                    if (MediaPlayer.Volume <= 1)
//                        MediaPlayer.Volume += (float)SongGrovSpeed;
//                    if (MediaPlayer.Volume >= DefaultSongVolume[currentSong])
//                    {
//                        toSlowStartSong = false;
//                        SongVolume(DefaultSongVolume[currentSong]);
//                    }
//                }
//            }
//            List<string> remove = new List<string>();
//            foreach (String s in toSlowStopSound.Keys)
//            {
//                if (toSlowStopSound[s])
//                {
//                    currentSounds[s].Volume += (float)SoundGrowSpeed[s];
//                    if (currentSounds[s].Volume <= 0)
//                    {
//                        toSlowStopSound[s] = false;
//                        StopSound(s);
//                        remove.Add(s);
//                    }
//                }
//            }
//            foreach (String s in toSlowStartSound.Keys)
//            {
//                if (toSlowStopSound[s])
//                {
//                    currentSounds[s].Volume += (float)SoundGrowSpeed[s];
//                    if (currentSounds[s].Volume >= DefaultSoundVolume[s])
//                    {
//                        toSlowStartSound[s] = false;
//                        SoundVolume(s, DefaultSoundVolume[s]);
//                        remove.Add(s);
//                    }
//                }
//            }


//            foreach (string s in remove)
//            {
//                toSlowStopSound.Remove(s);
//                toSlowStartSound.Remove(s);
//                SoundGrowSpeed.Remove(s);
//            }


//        }

//        //-------------------------------------------------
//        //----------------------Songs----------------------
//        //-------------------------------------------------

//        static bool toSlowStopSong = false;
//        static bool toSlowStartSong = false;
//        static double SongGrovSpeed = 0;
//        static String songAfterSlow = "";

//        public static void PlaySong(String name, bool loop = false)
//        {
//            /*Soloud soloud = new Soloud();
//            Speech speech = new Speech();

//            speech.setText("Hello c sharp api");

//            soloud.init(SoLoud.Soloud.CLIP_ROUNDOFF);

//            soloud.setGlobalVolume(4);
//            soloud.play(speech);
//            Console.WriteLine("soloud");
//            */

//            MediaPlayer.Play(SongList[name]);
//            currentSong = name;
//            if (DefaultSongVolume.ContainsKey(name))
//            {
//                SongVolume(DefaultSongVolume[name]);
//            }
//            MediaPlayer.IsRepeating = loop;
//        }

//        public static void Destruct()
//        {

//        }

//        public static void StopSong()
//        {
//            currentSong = "";
//            toSlowStopSong = false;
//            MediaPlayer.Stop();
//        }
//        public static void PauseSong()
//        {
//            MediaPlayer.Pause();
//        }
//        public static void ResumeSong()
//        {
//            MediaPlayer.Resume();
//        }
//        public static void SongVolume(double v)
//        {
//            MediaPlayer.Volume = (float)v;
//        }
//        public static void LoopSong(bool l)
//        {
//            MediaPlayer.IsRepeating = l;
//        }
//        public static void SlowStopSong(double speed, string queue = "")
//        {
//            toSlowStopSong = true;
//            SongGrovSpeed = -speed;
//            songAfterSlow = queue;
//        }
//        public static void SlowStartSong(String name, double speed, bool loop = false)
//        {
//            PlaySong(name, loop);
//            SongVolume(0);
//            toSlowStartSong = true;
//            SongGrovSpeed = speed;
//        }
//        public static void PlayRandomBgm()
//        {
//            SlowStartSong(bgm[Tools.Rand(0, bgm.Count - 1)], 0.0001);
//        }

//        //-------------------------------------------------
//        //---------------------Sounds----------------------
//        //-------------------------------------------------



//        public static Dictionary<String, bool> toSlowStopSound = new Dictionary<String, bool>();
//        public static Dictionary<String, bool> toSlowStartSound = new Dictionary<String, bool>();
//        public static Dictionary<String, double> SoundGrowSpeed = new Dictionary<String, double>();

//        public static void SinglePlay(String name)
//        {
//            if (DefaultSoundVolume.ContainsKey(name))
//            {
//                SinglePlay(name, DefaultSoundVolume[name]);
//            }
//            else
//            {
//                SinglePlay(name, 1);
//            }

//        }

//        public static void SinglePlay(String name, double volume)
//        {
//            volume = Math.Max(0, volume);
//            var s = SoundList[name].CreateInstance();
//            s.Volume = (float)volume;
//            s.Play();
//        }
//        public static void PlayAndStoreSound(String name, String Key, bool loop = false)
//        {
//            PlayAndStoreSound(name, Key, DefaultSoundVolume[name]);
//        }
//        public static void PlayAndStoreSound(String name, String Key, double volume, bool loop = false)
//        {
//            var s = SoundList[name].CreateInstance();
//            currentSounds.Add(Key, s);
//            s.Volume = (float)volume;
//            s.IsLooped = loop;
//            s.Play();
//        }

//        public static void StopSound(String Key)
//        {
//            currentSounds[Key].Stop();
//            currentSounds.Remove(Key);
//        }

//        public static void StopAllSounds()
//        {
//            foreach (var s in currentSounds.Values)
//            {
//                s.Stop();
//            }
//            currentSounds.Clear();
//            currentSong = "";
//            MediaPlayer.Stop();
//        }

//        public static void PauseSound(string key)
//        {
//            currentSounds[key].Pause();
//        }
//        public static void ResumeSound(string key)
//        {
//            currentSounds[key].Resume();
//        }
//        public static void SoundVolume(string key, double v)
//        {
//            currentSounds[key].Volume = (float)v;
//        }
//        public static void LoopSound(string key, bool l)
//        {
//            currentSounds[key].IsLooped = l;
//        }
//        public static void SlowStopSound(String key, double speed)
//        {
//            toSlowStopSound[key] = true;
//            SoundGrowSpeed[key] = -speed;

//        }
//        public static void SlowStartSound(String name, string key, double speed, bool loop = false)
//        {
//            PlayAndStoreSound(name, key, loop);
//            SoundVolume(key, 0);
//            toSlowStartSound[key] = true;
//            SoundGrowSpeed[key] = speed;
//        }
//    }
//}
