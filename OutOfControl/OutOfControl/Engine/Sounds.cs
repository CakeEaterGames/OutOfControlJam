using MonoCake;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pellicalo
{
    public static class Sounds
    {
        public static void Init()
        {
            Console.WriteLine("Loading Sounds");

            AudioManager.LoadPath = "Audio\\";

            AudioManager.LoadSong("pellicalo-full", "pellicalo-full.wav", 1);
            AudioManager.LoadSound("buff", "buff.wav", 1);
            AudioManager.LoadSound("click", "click.wav", 1);
            AudioManager.LoadSound("click2", "click2.wav", 1);
            AudioManager.LoadSound("dice", "dice.wav", 1);
            AudioManager.LoadSound("explosion", "explosion.wav", 1);
            AudioManager.LoadSound("explosion2", "explosion2.wav", 1);
            AudioManager.LoadSound("fireball", "fireball.wav", 1);
            AudioManager.LoadSound("heal", "heal.wav", 1);
            AudioManager.LoadSound("hehey", "hehey.wav", 1);
            AudioManager.LoadSound("hit", "hit.wav", 1);
            AudioManager.LoadSong("gameover", "pellicalo-gameover.wav", 1);
           
            AudioManager.LoadSound("shoot", "shoot.wav", 1);

            AudioManager.LoadSong("bass", "bass.wav", 0.8);
            AudioManager.LoadSong("in", "in.wav", 0.8);
            AudioManager.LoadSong("loop", "main-loop.wav", 0.8);

            Console.WriteLine("Loading Sounds Done");
        }
    }
}
