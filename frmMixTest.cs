using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using System.Windows.Forms;


namespace Sampler
{
    class frmMixTest
    {
        private IWavePlayer waveOutDevice;
        private WaveMixerStream32 mixer;
        private string[] sampleLoaded = new string[4];
        WaveFileReader[] reader = new WaveFileReader[4];
        WaveOffsetStream[] offsetStream = new WaveOffsetStream[4];
        WaveChannel32[] channelSteam = new WaveChannel32[4];

        public frmMixTest()
        {

            

            //Setup the Mixer

            mixer = new WaveMixerStream32();
            mixer.AutoStop = false;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            for (int i = 0; i < 4; i++)
            {
                sampleLoaded[i] = "";

            }

            if (waveOutDevice == null)
            {

                waveOutDevice = new AsioOut();
                waveOutDevice.Init(mixer);

                waveOutDevice.Play();
            }
           

        }

    }
}
