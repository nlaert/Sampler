﻿using System;
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
<<<<<<< HEAD
        private bool ready = false;
	    public Sound(Stream strm)
	    {
           sound = new SoundPlayer(strm);
           sound.LoadAsync();
           sound.LoadCompleted += sound_LoadCompleted;
           playing = false;
	    }

        void sound_LoadCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            ready = true;
        }

        public void Play()
        {
            if(!playing && ready)
                sound.Play();
            
=======

	    public Sound(Stream strm)
	    {
           sound = new SoundPlayer(strm);
           playing = false;
	    }

        public void Play()
        {
            if(!playing)
                sound.Play();
>>>>>>> d532eacd9c8bcd69977bff92c08d2397c59746a9
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
