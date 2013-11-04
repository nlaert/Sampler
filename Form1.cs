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

namespace Sampler
{
    public partial class Form1 : Form
    {
        private static Stream[] strs = 
            {Properties.Resources.synth_03, 
             Properties.Resources.massive_synth,
             Properties.Resources.mov_baix_electro,
             Properties.Resources.SevenNationArmy,
             Properties.Resources.drums,
             Properties.Resources.drumming,
             Properties.Resources.beat126_4,
             Properties.Resources.spring_beats_4};


        //Stream b9 = Properties.Resources.festival_in;
        private static Stream lab = Properties.Resources.mafralab;
        SoundPlayer[] snds = new SoundPlayer[strs.Length];
        SoundPlayer sndLab = new SoundPlayer(lab);
        var Player = new System.Windows.Media.MediaPlayer();
        public Form1()
        {
            InitializeComponent();
            for (int i = 0; i < snds.Length; i++)
            {
                snds[i] = new SoundPlayer(strs[i]);
            }
        }

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
        }

        private void button11_MouseHover(object sender, EventArgs e)
        {
            //snds[10].PlayLooping();
        }

        private void button12_MouseHover(object sender, EventArgs e)
        {
            //snds[11].PlayLooping();
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            sndLab.Play();
        }

    }
}
