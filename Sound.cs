using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Sampler
{
    class Sound
    {
        private SoundPlayer sound;
        private bool playing;

	    public Sound(Stream strm)
	    {
           sound = new SoundPlayer(strm);
           playing = false;
	    }

        public void Play()
        {
            if(!playing)
                sound.Play();
            playing = true;
        }

        public void PlayLooping()
        {
            if (!playing)
                sound.PlayLooping();
            playing = true;
        }

        public void Stop()
        {
            if(playing)
                sound.Stop();
            playing = false;
        }

        public bool isPlaying()
        {
            return playing;
        }

    }
}
