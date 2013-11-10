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
using Microsoft.Kinect;
using Coding4Fun.Kinect.WinForm;
using System.Windows;
using System.Diagnostics;

namespace Sampler
{
    public partial class Form1 : Form
    {
        private static int rectanglesHeight, rectanglesWidth ;
        private System.Drawing.Rectangle [][] rec = new System.Drawing.Rectangle[nColumns][];
        private KinectSensor sensor = KinectSensor.KinectSensors[0];
        private System.Drawing.Point rPoint = new System.Drawing.Point();
        private System.Drawing.Point lPoint = new System.Drawing.Point();
        private const float ClickHoldingRectThreshold = 0.05f;
        private Rect _clickHoldingLastRect;
        private readonly Stopwatch _clickHoldingTimer;
        private const int nRows = 3, nColumns = 4;

        private const float SkeletonMaxX = 0.60f;
        private const float SkeletonMaxY = 0.40f;
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
            rectanglesHeight = panel1.Height/nRows; //4 para caber o logo do mafraLab em baixo
            rectanglesWidth = panel1.Width/nColumns;
            drawRectangles();
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

        private void drawRectangles()
        {
            for (int i=0;i<nColumns;i++){
                rec[i] = new System.Drawing.Rectangle[nRows];
                for (int j=0;j<nRows;j++){
                    rec[i][j] = new System.Drawing.Rectangle(i*rectanglesHeight,j*rectanglesWidth,rectanglesHeight,rectanglesWidth);
                }
            }
                                                   //new System.Drawing.Rectangle(0,0,rectanglesHeight,rectanglesWidth),
                                                   //new System.Drawing.Rectangle(0,50,50,50),
                                                   //new System.Drawing.Rectangle(0,100,50,50),
                                                   //new System.Drawing.Rectangle(0,150,50,50), 
                                                   //new System.Drawing.Rectangle(50,0,50,50),
                                                   //new System.Drawing.Rectangle(50,50,50,50),
                                                   //new System.Drawing.Rectangle(50,100,50,50),
                                                   //new System.Drawing.Rectangle(50,150,50,50), 
                                                   //new System.Drawing.Rectangle(100,0,50,50),
                                                   //new System.Drawing.Rectangle(100,50,50,50),
                                                   //new System.Drawing.Rectangle(100,100,50,50),
                                                   //new System.Drawing.Rectangle(100,150,50,50)};
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

        public void button1_MouseHover(object sender, EventArgs e)
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
           // Player1.MediaEnded += MediaPlayer_Loop;

            
            
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

        private void Form1_Load(object sender, EventArgs e)
        {
            var parameters = new TransformSmoothParameters();
            parameters.Smoothing = 0.7f;
            parameters.Correction = 0.3f;
            parameters.Prediction = 0.4f;
            parameters.JitterRadius = 1.0f;
            parameters.MaxDeviationRadius = 0.5f;
            sensor.SkeletonStream.Enable(parameters);

            sensor.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);
            sensor.DepthStream.Enable(DepthImageFormat.Resolution640x480Fps30);

            sensor.AllFramesReady += SensorAllFramesReady;
            try
            {
                sensor.Start();
            }
            catch (System.IO.IOException)
            {
                //another app is using Kinect
                
            }
        }

        void SensorAllFramesReady(object sender, AllFramesReadyEventArgs e)
        {
            SensorDepthFrameReady(e);
            SensorSkeletonFrameReady(e);
        }
        void SensorDepthFrameReady(AllFramesReadyEventArgs e)
        {
            // if the window is displayed, show the depth buffer image
                //using (DepthImageFrame depthFrame = e.OpenDepthImageFrame())
                //{
                //    if (depthFrame == null)
                //    {
                //        return;
                //    }

                //    video.Source = depthFrame.ToBitmapSource();
                //}
            
        }


        void SensorSkeletonFrameReady(AllFramesReadyEventArgs e)
        {
            using (SkeletonFrame skeletonFrameData = e.OpenSkeletonFrame())
            {
                if (skeletonFrameData == null)
                {
                    return;
                }

                var allSkeletons = new Skeleton[skeletonFrameData.SkeletonArrayLength];

                skeletonFrameData.CopySkeletonDataTo(allSkeletons);

                foreach (Skeleton sd in allSkeletons)
                {
                    // the first found/tracked skeleton moves the mouse cursor
                    if (sd.TrackingState == SkeletonTrackingState.Tracked)
                    {
                        // make sure both hands are tracked
                        if (sd.Joints[JointType.HandRight].TrackingState == JointTrackingState.Tracked)
                        {
                            var wristRight = sd.Joints[JointType.WristRight];
                            var wristLeft = sd.Joints[JointType.WristLeft];
                            var scaledRightHand = wristRight.ScaleTo((int)Screen.PrimaryScreen.Bounds.Width, (int)Screen.PrimaryScreen.Bounds.Height, SkeletonMaxX, SkeletonMaxY);
                            var scaledLeftHand = wristLeft.ScaleTo((int)Screen.PrimaryScreen.Bounds.Width, (int)Screen.PrimaryScreen.Bounds.Height, SkeletonMaxX, SkeletonMaxY);
                            
                            System.Drawing.Rectangle r = new System.Drawing.Rectangle();
                            System.Drawing.Point righthand = new System.Drawing.Point((int)scaledRightHand.Position.X, (int)scaledRightHand.Position.Y);
                            System.Drawing.Point lefthand = new System.Drawing.Point((int)scaledLeftHand.Position.X, (int)scaledLeftHand.Position.Y);
                            rPoint = righthand;
                            lPoint = lefthand;
                           this.panel1.Invalidate();
                            //foreach (System.Drawing.Rectangle re in rec)
                            //{
                                    //if (re.Contains(righthand) || re.Contains(lefthand))
                                    //{
                                    //    areaManager(re);
                                        //switch(b.Name){
                                        //    case "button1":
                                        //        button1_MouseHover(b, null);
                                        //        break;
                                        //    case "button2":
                                        //        button2_MouseHover(b, null);
                                        //        break;
                                        //    case "button3":
                                        //        button3_MouseHover(b, null);
                                        //        break;
                                        //    case "button4":
                                        //        button4_MouseHover(b, null);
                                        //        break;
                                        //    case "button5":
                                        //        button5_MouseHover(b, null);
                                        //        break;
                                        //    case "button6":
                                        //        button6_MouseHover(b, null);
                                        //        break;
                                        //    case "button7":
                                        //        button7_MouseHover(b, null);
                                        //        break;
                                        //    case "button8":
                                        //        button8_MouseHover(b, null);
                                        //        break;
                                        //    case "button9":
                                        //        button9_MouseHover(b, null);
                                        //        break;
                                        //    case "button10":
                                        //        button10_MouseHover(b, null);
                                        //        break;
                                        //    case "button11":
                                        //        button11_MouseHover(b, null);
                                        //        break;
                                        //    case "button12":
                                        //        button12_MouseHover(b, null);
                                        //        break;
                                                
                                        //}
                 
                                  //  }
                                    
                               // }
                            }

                            //var cursorRightX =  + (int)MouseSpeed.Value;
                           // var cursorRightY = (int)scaledRightHand.Position.Y + (int)MouseSpeed.Value;

                            //var cursorLeftX = (int)scaledLeftHand.Position.X + (int)MouseSpeed.Value;
                           // var cursorLeftY = (int)scaledLeftHand.Position.Y + (int)MouseSpeed.Value;

                            
                            //var leftClick = CheckForClickHold(scaledRightHand);
                            
                            //NativeMethods.SendMouseInput(cursorX, cursorY, (int)SystemParameters.PrimaryScreenWidth, (int)SystemParameters.PrimaryScreenHeight, leftClick);
                        }
                    }
                }
            }
        

        


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
        
        }

        protected override void OnPaint(PaintEventArgs e) 
        {
           
        }

        private void panel1_Paint(object sender, PaintEventArgs e)//GDI PLUS C#
        {
            Graphics g = e.Graphics;
            g.DrawEllipse(new System.Drawing.Pen(new System.Drawing.SolidBrush(System.Drawing.Color.Red), 3f), new Rectangle(rPoint, new System.Drawing.Size(10,10)));
            g.DrawEllipse(new System.Drawing.Pen(new System.Drawing.SolidBrush(System.Drawing.Color.Green), 5f), new Rectangle(lPoint, new System.Drawing.Size(10, 10)));

            for (int i = 0; i < nColumns; i++)
            {
                for (int j = 0; j < nRows; j++)
                {
                    g.DrawRectangle(new System.Drawing.Pen(new System.Drawing.SolidBrush(System.Drawing.Color.Black), 1f), rec[i][j]);
                }
            }
            //foreach (System.Drawing.Rectangle[] r in rec)
            //{
            //    g.DrawRectangle(new System.Drawing.Pen(new System.Drawing.SolidBrush(System.Drawing.Color.Black), 3f), r);
            //}
        }
    }
}
