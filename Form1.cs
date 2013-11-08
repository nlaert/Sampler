using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.Wave;
using NAudio.CoreAudioApi;
using System.Windows.Media;

namespace Sampler
{
    public partial class Form1 : Form
    {
        Uri prjUri = new Uri("C:/Users/Nick/Documents/Visual Studio 2013/Projects/Sampler/Sampler/Resources/");
        private static Stream[] strs = 
            {Properties.Resources.synth_03, 
             Properties.Resources.massive_synth,
             Properties.Resources.mov_baix_electro,
             Properties.Resources.SevenNationArmy,
             Properties.Resources.drums,
             Properties.Resources.drumming,
             Properties.Resources.beat126_4,
             Properties.Resources.spring_beats_4};
        //IWavePlayer waveOutDevice;
        //private static WaveStream mainOutputStream = CreateInputStream(fileName);
        //private static string fileName ="C:/Users/Nick/Documents/Visual Studio 2013/Projects/Sampler/Sampler/Resources/drums";

        //Stream b9 = Properties.Resources.festival_in;
        private static Stream lab = Properties.Resources.mafralab;
        SoundPlayer[] snds = new SoundPlayer[strs.Length];
        SoundPlayer sndLab = new SoundPlayer(lab);
       MediaPlayer Player = new MediaPlayer();
        public Form1()
        {
            InitializeComponent();
            for (int i = 0; i < snds.Length; i++)
            {
                snds[i] = new SoundPlayer(strs[i]);
            }

            //try
            //{
            //    waveOutDevice = new AsioOut();
            //}
            //catch (Exception driverCreateException)
            //{
            //    MessageBox.Show(String.Format("{0}", driverCreateException.Message));
            //    return;
            //}
            
        }

        //private static WaveStream CreateInputStream(string fileName)
        //{
        //    WaveChannel32 inputStream;
        //    if (fileName.EndsWith(".wav"))
        //    {
        //        WaveStream readerStream = new WaveFileReader(fileName);
        //        if (readerStream.WaveFormat.Encoding != WaveFormatEncoding.Pcm)
        //        {
        //            readerStream = WaveFormatConversionStream.CreatePcmStream(readerStream);
        //            readerStream = new BlockAlignReductionStream(readerStream);
        //        }
        //        if (readerStream.WaveFormat.BitsPerSample != 16)
        //        {
        //            var format = new WaveFormat(readerStream.WaveFormat.SampleRate,
        //               16, readerStream.WaveFormat.Channels);
        //            readerStream = new WaveFormatConversionStream(format, readerStream);
        //        }
        //        inputStream = new WaveChannel32(readerStream);
        //    }
        //    else
        //    {
        //        throw new InvalidOperationException("Unsupported extension");
        //    }
        //    return inputStream;
        //}

        private void button1_MouseHover(object sender, EventArgs e)
        {
            snds[0].PlayLooping();
            
        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
            snds[1].PlayLooping();
        }

        private void button3_MouseHover(object sender, EventArgs e)
        {
            snds[2].PlayLooping();
        }

        private void button4_MouseHover(object sender, EventArgs e)
        {
            snds[3].PlayLooping();
        }

        private void button5_MouseHover(object sender, EventArgs e)
        {
            snds[4].PlayLooping();
        }

        private void button6_MouseHover(object sender, EventArgs e)
        {
            snds[5].PlayLooping();
        }

        private void button7_MouseHover(object sender, EventArgs e)
        {
            snds[6].PlayLooping();
        }

        private void button8_MouseHover(object sender, EventArgs e)
        {
            snds[7].PlayLooping();
        }

        private void button9_MouseHover(object sender, EventArgs e)
        {
           // snds[8].PlayLooping();
        }

        private void button10_MouseHover(object sender, EventArgs e)
        {
           // snds[9].PlayLooping();
            //try
            //{
            //    waveOutDevice.Init(mainOutputStream);
            //}
            //catch (Exception initException)
            //{
            //    MessageBox.Show(String.Format("{0}", initException.Message), "Error Initializing Output");
            //    return;
            //}
            //waveOutDevice.Play();
        }

        private void button11_MouseHover(object sender, EventArgs e)
        {
            //snds[10].PlayLooping();
            System.Windows.Media.MediaPlayer Player1 = new System.Windows.Media.MediaPlayer();
            Uri uri = new Uri(prjUri, "drums.wav");
            Player1.Open(uri);
            Player1.Volume = 0.99;
            Player1.Play();
            Player1.MediaEnded += MediaPlayer_Loop;

            
            
        }

        private void MediaPlayer_Loop(object sender, EventArgs e)
        {
            MediaPlayer player = sender as MediaPlayer;
            if (player == null)
                return;

            player.Position = new TimeSpan(0);
            player.Play();
        }
       
        private void button12_MouseHover(object sender, EventArgs e)
        {
            //snds[11].PlayLooping();
            //Uri u = new Uri(@"\\Sampler\Resources\drums.wav");
            //Uri uri = new Uri(u,"SevenNationArmy.wav");
            //Player.Open(u);
            //Player.Volume=0.99;
            //Player.Play();
            
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            sndLab.Play();
        }


        public Uri SevenNationArmy { get; set; }
    }
}
