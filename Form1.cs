using Coding4Fun.Kinect.WinForm;
using Microsoft.Kinect;
using NAudio.CoreAudioApi;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;

namespace Sampler
{
    public partial class Form1 : Form
    {
        private static int rectanglesHeight, rectanglesWidth ;
        private System.Drawing.Rectangle [][] rectangleArray = new System.Drawing.Rectangle[nColumns][];
        private KinectSensor sensor = KinectSensor.KinectSensors[0];
        private System.Drawing.Point rPoint = new System.Drawing.Point();
        private System.Drawing.Point lPoint = new System.Drawing.Point();
        int oldsoundl = 0;
        int oldsoundr = 0;
        bool startedL = false, startedR = false;
        private const float ClickHoldingRectThreshold = 0.05f;
        //private Rect _clickHoldingLastRect;
        //private readonly Stopwatch _clickHoldingTimer;
        private const int nRows = 3, nColumns = 4;
        private const float SkeletonMaxX = 0.60f;
        private const float SkeletonMaxY = 0.40f;
        private WaveOut waveOutR, waveOutL;
        int timerSkeleton = 0; 
        private WaveFileReader[] sounds = {
                                              
                                               new WaveFileReader(Properties.Resources.massive_synth),
                                               new WaveFileReader(Properties.Resources.mov_baix_electro),
                                               new WaveFileReader(Properties.Resources.spring_beats_4),
                                               new WaveFileReader(Properties.Resources.beat126_4),
                                               new WaveFileReader(Properties.Resources.drumming),
                                               new WaveFileReader(Properties.Resources.silencio),
                                               new WaveFileReader(Properties.Resources.drums),
                                               new WaveFileReader(Properties.Resources.SevenNationArmy),
                                               new WaveFileReader(Properties.Resources.silencio),
                                              // new WaveFileReader(Properties.Resources.clock),
                                               new WaveFileReader(Properties.Resources.teleport),
                                               new WaveFileReader(Properties.Resources.emx_12),
                                               new WaveFileReader(Properties.Resources.mafralab)

                                           };
        bool[] playing;
       
        private static Stream lab = Properties.Resources.mafralab;
        SoundPlayer sndLab = new SoundPlayer(lab);
       

        public Form1()
        {
            InitializeComponent();
            rectanglesHeight = panel1.Height/(nRows-1); //4 para caber o logo do mafraLab em baixo
            rectanglesWidth = panel1.Width/nColumns;
            createRectangles();
            playing = new bool[sounds.Length];
           
        }

        private void createRectangles()
        {
            
            for (int i = 0; i < nColumns; i++)
            {
                rectangleArray[i] = new System.Drawing.Rectangle[nRows];
                for (int j = 0; j < nRows; j++)
                {
                    rectangleArray[i][j] = new System.Drawing.Rectangle(i * rectanglesHeight, j * rectanglesWidth, rectanglesHeight, rectanglesWidth);
                }
            }
        }
        

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
                LoopStream loopL, loopR;
                foreach (Skeleton sd in allSkeletons)
                {
                    // the first found/tracked skeleton moves the mouse cursor
                    if (sd.TrackingState == SkeletonTrackingState.Tracked)
                    {
                        timerSkeleton = 0;
                        // make sure both hands are tracked
                        if (sd.Joints[JointType.HandRight].TrackingState == JointTrackingState.Tracked)
                        {
                            var wristRight = sd.Joints[JointType.WristRight];
                            var wristLeft = sd.Joints[JointType.WristLeft];
                            var scaledRightHand = wristRight.ScaleTo((int)Screen.PrimaryScreen.Bounds.Width, (int)Screen.PrimaryScreen.Bounds.Height, SkeletonMaxX, SkeletonMaxY);
                            var scaledLeftHand = wristLeft.ScaleTo((int)Screen.PrimaryScreen.Bounds.Width, (int)Screen.PrimaryScreen.Bounds.Height, SkeletonMaxX, SkeletonMaxY);
                            
                            System.Drawing.Point righthand = new System.Drawing.Point((int)scaledRightHand.Position.X, (int)scaledRightHand.Position.Y);
                            System.Drawing.Point lefthand = new System.Drawing.Point((int)scaledLeftHand.Position.X, (int)scaledLeftHand.Position.Y);
                            rPoint = righthand;
                            lPoint = lefthand;
                           this.panel1.Invalidate();
                           for (int i = 0; i < nColumns; i++)
                           {
                               for (int j = 0; j < nRows; j++)
                               {
                                  int aux = i * nRows + j;
                                  if (aux < sounds.Length)
                                  {
                                      if (rectangleArray[i][j].Contains(lefthand))
                                      {
                                          if (!playing[aux])
                                          {
                                              if (startedL)
                                              {
                                                  waveOutL.Stop();
                                                  waveOutL.Dispose();
                                                  waveOutL = null;
                                              }
                                              else
                                              {
                                                 
                                                      loopL = new LoopStream(sounds[aux]);
                                                      waveOutL = new WaveOut();
                                                      waveOutL.Init(loopL);
                                                      waveOutL.Play();
                                                      playing[oldsoundl] = false;
                                                      playing[aux] = true;
                                                      oldsoundl = aux;
                                                      startedL = true;
                                                 
                                              }
                                              
                                          }
                                          
                                      }

                                      if (rectangleArray[i][j].Contains(righthand))
                                      {
                                          if (oldsoundr != aux)
                                          {
                                              if (!playing[aux])
                                              {
                                                  if (startedR)
                                                  {
                                                      waveOutR.Stop();
                                                      waveOutR.Dispose();
                                                      waveOutR = null;
                                                  }
                                                  else
                                                  {
                                              
                                                          loopR = new LoopStream(sounds[aux]);
                                                          waveOutR = new WaveOut();
                                                          waveOutR.Init(loopR);
                                                          waveOutR.Play();
                                                          playing[oldsoundr] = false;
                                                          playing[aux] = true;
                                                          oldsoundr = aux;
                                                          startedR = true;
                                                      
                                                  }

                                                  

                                              }

                                          }

                                      }
                                  }
                               }
                           }

                        }
                            //NativeMethods.SendMouseInput(cursorX, cursorY, (int)SystemParameters.PrimaryScreenWidth, (int)SystemParameters.PrimaryScreenHeight, leftClick);
                        }
                    else if (sd.TrackingState == SkeletonTrackingState.NotTracked)
                    {
                        timerSkeleton++;
                        if (timerSkeleton > 20)
                        {
                            timerSkeleton = 0;
                            if (startedL)
                            {
                                waveOutL.Stop();
                                waveOutL.Dispose();
                                startedL = false;
                                oldsoundl = 0;
                                
                            }
                            if (startedR)
                            {
                                waveOutR.Stop();
                                waveOutR.Dispose();
                                startedR = false;
                                oldsoundr = 0;
                            }
                        }
                    }
                    }
                }
            }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
        
        }

        private void panel1_Paint(object sender, PaintEventArgs e)//GDI PLUS C#
        {
            Graphics g = e.Graphics;
            g.DrawEllipse(new System.Drawing.Pen(new System.Drawing.SolidBrush(System.Drawing.Color.Red), 6f), new Rectangle(rPoint, new System.Drawing.Size(10,10)));
            g.DrawEllipse(new System.Drawing.Pen(new System.Drawing.SolidBrush(System.Drawing.Color.Green), 6f), new Rectangle(lPoint, new System.Drawing.Size(10, 10)));

            for (int i = 0; i < nColumns; i++)
            {
                for (int j = 0; j < nRows; j++)
                {
                    g.DrawRectangle(new System.Drawing.Pen(new System.Drawing.SolidBrush(System.Drawing.Color.Black), 1f), rectangleArray[i][j]);
                }
            }
        }
    }
}
