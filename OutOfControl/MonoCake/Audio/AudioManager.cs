using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using SoLoud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoCake
{
    public static class AudioManager
    {
        public static Dictionary<String, WavStream> SongList = new Dictionary<String, WavStream>();
        public static Dictionary<String, double> DefaultSongVolume = new Dictionary<String, double>();
        public static String currentSong = "";
        public static uint currentSongID = 0;
        public static double currentTime { get { return soloud.getStreamPosition(currentSongID); } }
        public static double currentLength { get { return SongList[currentSong].getLength(); } }


        public static List<String> bgm = new List<String>();

        public static Dictionary<String, Wav> SoundList = new Dictionary<String, Wav>();
        public static Dictionary<String, double> DefaultSoundVolume = new Dictionary<String, double>();
        public static Dictionary<String, SoundEffectInstance> currentSounds = new Dictionary<String, SoundEffectInstance>();

        public static double MasterVolume { get { return soloud.getGlobalVolume(); } set { soloud.setGlobalVolume((float)value); } }
        public static double SoundVolume = 1;
        public static double MusicVolume = 1;

        public static Dictionary<String, String> LoadSoundList = new Dictionary<String, String>();
        public static Dictionary<String, String> LoadSongList = new Dictionary<String, String>();

        public static AudioManagerMode mode = AudioManagerMode.other;
        public enum AudioManagerMode
        {
            bgm, other
        }


        public static Soloud soloud = new Soloud();

        public static String LoadPath = "";
        public static void LoadSound(string key, string path, double defaultVolume = 1)
        {
#if DEBUG
            GhostLoadSound(key, path, defaultVolume);
#else
            Wav wav = new Wav();
            wav.load(LoadPath + path);
            SoundList.Add(key, wav);
            DefaultSoundVolume.Add(key, defaultVolume);
#endif
        }

        public static void GhostLoadSound(string key, string path, double defaultVolume = 1)
        {
            LoadSoundList.Add(key, path);
            DefaultSoundVolume.Add(key, defaultVolume);
        }

        public static void LoadSong(string key, string path, double defaultVolume = 1)
        {
#if DEBUG
            GhostLoadSong(key, path, defaultVolume);
#else
            WavStream wav = new WavStream();
            wav.load(LoadPath + path);
            SongList.Add(key, wav);
            DefaultSongVolume.Add(key, defaultVolume);

#endif

        }
        public static void GhostLoadSong(string key, string path, double defaultVolume = 1)
        {
            LoadSongList.Add(key, path);
            DefaultSongVolume.Add(key, defaultVolume);
        }

        public static void TrueLoadSong(string key)
        {
            if (!SongList.ContainsKey(key) && LoadSongList.ContainsKey(key))
            {
                WavStream wav = new WavStream();
                wav.load(LoadPath + LoadSongList[key]);
                SongList.Add(key, wav);

            }
        }
        public static void TrueLoadSound(string key)
        {
            if (!SoundList.ContainsKey(key) && LoadSoundList.ContainsKey(key))
            {
                Wav wav = new Wav();
                wav.load(LoadPath + LoadSoundList[key]);
                SoundList.Add(key, wav);

            }
        }


        static AudioManager()
        {

        }
        public static void Init()
        {
            soloud.init();
            MasterVolume = 1;
            SoundVolume = 1;
            MusicVolume = 1;
            // soloud.setGlobalVolume(2);
        }


        public static void Update()
        {
            if (currentSong != "" && mode == AudioManagerMode.bgm && currentLength > 1)
            {
                if (currentTime > currentLength - 5)
                {
                    soloud.fadeVolume(currentSongID, 0, 5);
                    PlayRandomBgm();
                }
            }

        }

        //-------------------------------------------------
        //----------------------Songs----------------------
        //-------------------------------------------------

        #region song
        public static void PlaySong(String name, double volume = 1, bool loop = false)
        {
            TrueLoadSong(name);
            var sound = soloud.play(SongList[name], (float)(volume * DefaultSongVolume[name] * MusicVolume));
            currentSong = name;
            currentSongID = sound;
            if (loop)
                soloud.setLooping(sound, 1);
        }


        public static void StopSong()
        {

        }
        public static void PauseSong()
        {

        }
        public static void ResumeSong()
        {

        }
        public static void SongVolume(double v)
        {

        }
        public static void LoopSong(bool l)
        {

        }
        public static void SlowStopSong(double sec = 1)
        {
            soloud.fadeVolume(currentSongID, (float)(0), sec);
            currentSong = "";
            currentSongID = 0;
        }
        public static void SlowStartSong(String name, double startAt = 0, double stopAt = 1, double sec = 1, bool loop = false)
        {
            TrueLoadSong(name);
            var sound = soloud.play(SongList[name], (float)(startAt * DefaultSongVolume[name] * MusicVolume), 0, 1);
            soloud.fadeVolume(sound, (float)(stopAt * DefaultSongVolume[name] * MusicVolume), sec);
            soloud.setPause(sound, 0);
            if (loop)
                soloud.setLooping(sound, 1);
            currentSong = name;

            currentSongID = sound;
        }
        public static void PlayRandomBgm()
        {
            SlowStartSong(bgm[Tools.Rand(0, bgm.Count - 1)], 0, 1, 2);
        }
        #endregion

        //-------------------------------------------------
        //---------------------Sounds----------------------
        //-------------------------------------------------
        #region sounds
        public static void SinglePlay(String name)
        {
            SinglePlay(name, 1);
        }

        public static void SinglePlay(String name, double volume)
        {
            TrueLoadSound(name);
            var sound = soloud.play(SoundList[name], (float)(volume * DefaultSoundVolume[name]));
        }


        public static void StopSound(String Key)
        {

        }

        public static void StopAllSounds()
        {
            soloud.stopAll();
            currentSong = "";
        }

        public static void PauseSound(string key)
        {

        }
        public static void ResumeSound(string key)
        {

        }
        public static void SetSoundVolume(string key, double v)
        {

        }
        public static void LoopSound(string key, bool l)
        {
            //TODO
        }
        public static void SlowStopSound(String key, double speed)
        {

        }
        public static void SlowStartSound(String name, double startAt = 0, double stopAt = 1, double sec = 1, bool loop = false)
        {

            TrueLoadSound(name);
            var sound = soloud.play(SoundList[name], (float)(startAt * DefaultSoundVolume[name]), 0, 1);
            soloud.fadeVolume(sound, (float)(stopAt * DefaultSoundVolume[name]), sec);
            soloud.setPause(sound, 0);
            if (loop)
                soloud.setLooping(sound, 1);
        }

        #endregion

        public static void Destruct()
        {
            soloud.deinit();
            currentSong = "";
        }

    }
}
