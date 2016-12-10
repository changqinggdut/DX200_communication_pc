using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;



using Basler.Pylon;//引用basler相机官方给的函数库
using Emgu;
using Emgu.CV;
using Emgu.Util;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using System.Collections;



namespace bo_communication_with_dx200
{
    public partial class Form1 : Form
    {
         static byte[] result = new byte[1024];
         public string[,] EnterCoordination=new string[100,6];
         int NumofEnterCoordinationPoint=0;
         int PicturePoint = 0;
         string PictureAddress="";
         string PictureProcessedAddress = "";
         string M;


         IPAddress ip = IPAddress.Parse("192.168.255.1");
         int port = 11000;
         Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        public Form1()
        {
            InitializeComponent();
            
        }


            private void button1_Click(object sender, EventArgs e)
            {
                SocketCommunicationInitialization(); //通信初始化

            }

       #region socket通信子程序
            private void SocketCommunicationInitialization()//socket 通信初始化
            {
            
                try
                {
                    clientSocket.Connect(new IPEndPoint(ip, port)); //配置服务器IP与端口
                    
                    button1.BackColor=Color.Green;

                    Thread th = new Thread(recMsg);//新建后台接受信息线程。
                    th.IsBackground = true;
                    th.Start(clientSocket);


                }
                catch
                {
                    
                    button1.BackColor = Color.Red;
                    return;
                }
            
        }
 
            void recMsg(object o)//后台接受信息进程
        {
            Socket clientSocket = (Socket)o;
            while (true)
            {
                try
                {
                    byte[] result = new byte[1024];
                    int num=clientSocket.Receive(result);
                    //Console.WriteLine("接收服务器消息：{0}", Encoding.ASCII.GetString(result));

                   string RebackData=Encoding.ASCII.GetString(result);
                   
                }
                catch
                {
                MessageBox.Show("接收异常");
                button1.BackColor = Color.Red;

                }
            }
        }

        #endregion

       #region 判断输出的是否为数字
           

            private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
            {

                if( !(char.IsNumber(e.KeyChar))&&e.KeyChar!=(char)8)
                {
                    e.Handled=true;
                }
                //else MessageBox.Show("请输入数字！！");
            }

            private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
            {
                if (!(char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
                {
                    e.Handled = true;
                }
               // else MessageBox.Show("请输入数字！！");
            }

            private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
                    {
                        if (!(char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
                        {
                            e.Handled = true;
                        }
                        //else MessageBox.Show("请输入数字！！");
                    }

            private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
                        {
                            if (!(char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
                            {
                                e.Handled = true;
                            }
                            //else MessageBox.Show("请输入数字！！");
                        }
            private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
            {
                if (!(char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
                {
                    e.Handled = true;
                }
                //else MessageBox.Show("请输入数字！！");
            }

            private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
            {
                //IsNumber的作用是判断输入按键是否为数字？
                //(char)8是退格键值，可允许用户敲退格键对输入的数字进行修改
                //其他按键无法输入
                if (!(char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
                {
                    e.Handled = true;
                }
                //else MessageBox.Show("请输入数字！！");为什么这里不能加这一句
            }

          
            #endregion

         private void textBox1_TextChanged(object sender, EventArgs e)
            {

            }


           private void textBox3_TextChanged(object sender, EventArgs e)
            {

            }
            private void label7_Click(object sender, EventArgs e)
            {

            }

      
           
            private void button2_Click(object sender, EventArgs e)
            {

                //首先将输入的坐标点保存到一个二维数组，接着再讲这个数据显示出来
                
                EnterCoordination[NumofEnterCoordinationPoint, 0]= textBox1.Text;
                EnterCoordination[NumofEnterCoordinationPoint, 1] = textBox2.Text;
                EnterCoordination[NumofEnterCoordinationPoint, 2] = textBox3.Text;
                EnterCoordination[NumofEnterCoordinationPoint, 3] = textBox4.Text;
                EnterCoordination[NumofEnterCoordinationPoint, 4] = textBox5.Text;
                EnterCoordination[NumofEnterCoordinationPoint, 5] = textBox6.Text;
               

                ++NumofEnterCoordinationPoint;
                listBox1.Items.Add("第" + NumofEnterCoordinationPoint.ToString() + "个坐标点！！");
                listBox1.Items.Add("X:" + textBox1.Text + "\t\t\t" + "RX:" + textBox4.Text);
                listBox1.Items.Add("Y:" + textBox2.Text + "\t\t\t" + "RY:" + textBox5.Text);
                listBox1.Items.Add("Z:" + textBox3.Text + "\t\t\t" + "RZ:" + textBox6.Text);
                

                textBox1.Clear(); textBox2.Clear();
                textBox3.Clear(); textBox4.Clear();
                textBox5.Clear(); textBox6.Clear();

            }

            private void button3_Click(object sender, EventArgs e)
            {
                string s = "2";
                clientSocket.Send(Encoding.ASCII.GetBytes(s));    //Encoding.ASCII.GetBytes(sendMessage)   //a,a.Length,SocketFlags.None
              
            }

            private void button7_Click(object sender, EventArgs e)
            {
                string s = "1";
                clientSocket.Send(Encoding.ASCII.GetBytes(s));    //Encoding.ASCII.GetBytes(sendMessage)   //a,a.Length,SocketFlags.None

                Thread.Sleep(1000);
                //string s = "923857, -38221, -209181, -1789992, -315122, 65610,1074305, -35249, -209211, -1789986, -315089, 65617";
                clientSocket.Send(Encoding.ASCII.GetBytes(M));    //Encoding.ASCII.GetBytes(sendMessage)   //a,a.Length,SocketFlags.None
                



            }

            private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
            {

            }

            private void button8_Click(object sender, EventArgs e)
            {
                const int c_countOfImagesToGrab = 2;//获取2张图像

                // The exit code of the sample application.
              //  int exitCode = 0;


                // Create a camera object that selects the first camera device found。创建一个相机对象，该对象选择第一个被发现的相机设备。
                // More constructors are available for selecting a specific camera device.

                using (Camera camera = new Camera())//需要对这种异常检测的用法注意一下。
                {
                    // Print the model name of the camera.
                    Console.WriteLine("Using camera {0}.", camera.CameraInfo[CameraInfoKey.ModelName]);

                    // Set the acquisition mode to free running continuous acquisition when the camera is opened. 将相机设定为获取模式。
                    camera.CameraOpened += Configuration.AcquireContinuous;//连续取相

                    // Open the connection to the camera device.打开与相机设备的链接
                    camera.Open();

                    // Enable the chunk mode. 启动块模式、

                    if (!camera.Parameters[PLCamera.ChunkModeActive].TrySetValue(true))
                    {
                        throw new Exception("The camera doesn't support chunk features");
                    }

                    // Enable time stamp chunks.
                    camera.Parameters[PLCamera.ChunkSelector].SetValue(PLCamera.ChunkSelector.Timestamp);
                    camera.Parameters[PLCamera.ChunkEnable].SetValue(true);

                    // Enable frame counter chunk if possible. 启动桢数数
                    if (camera.Parameters[PLCamera.ChunkSelector].TrySetValue(PLCamera.ChunkSelector.Framecounter))
                    {
                        camera.Parameters[PLCamera.ChunkEnable].SetValue(true);
                    }
                    // Enable generic counters if possible (USB camera devices).
                    else if (camera.Parameters[PLCamera.ChunkSelector].TrySetValue(PLCamera.ChunkSelector.CounterValue))
                    {
                        camera.Parameters[PLCamera.ChunkEnable].SetValue(true);
                        camera.Parameters[PLCamera.CounterSelector].SetValue(PLCamera.CounterSelector.Counter1);
                        camera.Parameters[PLCamera.CounterEventSource].SetValue(PLCamera.CounterEventSource.FrameStart);
                    }

                    // Enable CRC checksum chunks.
                    camera.Parameters[PLCamera.ChunkSelector].SetValue(PLCamera.ChunkSelector.PayloadCRC16);
                    camera.Parameters[PLCamera.ChunkEnable].SetValue(true);


                    // Start grabbing c_countOfImagesToGrab images.开始获取图像
                    camera.StreamGrabber.Start(c_countOfImagesToGrab);

                    // camera.StreamGrabber.Stop() is called automatically by the RetrieveResult() method
                    // when c_countOfImagesToGrab images have been retrieved.
                   
                        // Wait for an image and then retrieve it. A timeout of 5000 ms is used.
                        IGrabResult grabResult = camera.StreamGrabber.RetrieveResult(5000, TimeoutHandling.ThrowException);
                        using (grabResult)
                        {
                            // Image grabbed successfully?
                            if (grabResult.GrabSucceeded)
                            {
                                PictureAddress = "E:\\实验缓存照片\\" + System.DateTime.Now.Day.ToString() + System.DateTime.Now.Hour.ToString() + System.DateTime.Now.Minute.ToString() + System.DateTime.Now.Second.ToString() + ".png";
                                PictureProcessedAddress = "E:\\实验缓存照片\\" + System.DateTime.Now.Day.ToString() + System.DateTime.Now.Hour.ToString() + System.DateTime.Now.Minute.ToString() + System.DateTime.Now.Second.ToString() + "(Processed).png";
                               
                                ImagePersistence.Save(ImageFileFormat.Png, PictureAddress, grabResult);


                                //Mat original_picture = CvInvoke.Imread(PictureAddress, LoadImageType.AnyColor);
                                //string win1 = "original picture";
                                //CvInvoke.NamedWindow(win1, NamedWindowType.Normal);
                                //CvInvoke.Imshow(win1, original_picture);

                                //CvInvoke.WaitKey(0);
                                //IntPtr mat = original_picture.Ptr;
                                //CvInvoke.cvReleaseMat(ref mat);
                                //CvInvoke.DestroyWindow(win1);

                                pictureBox1.Load(PictureAddress);
                                pictureBox2.Load(PictureAddress);
                                camera.Close();


                            }
                            else
                            {
                                Console.WriteLine("Error: {0} {1}", grabResult.ErrorCode, grabResult.ErrorDescription);
                                MessageBox.Show("something Wrong :Error: {0} {1}, grabResult.ErrorCode, grabResult.ErrorDescription");
                            }
                        }
                  
                    
                }
               


            }

            private void pictureBox2_Click(object sender, EventArgs e)
            
        {
        
         }

            private void button9_Click(object sender, EventArgs e)
            {

                Image<Bgr, byte> scr = new Image<Bgr, byte>(PictureAddress);
                //指定目录创建一张图片。

                Image<Gray, byte> scr1 = new Image<Gray, byte>(scr.Width, scr.Height);
                Image<Gray, byte> scr2 = new Image<Gray, byte>(scr.Width, scr.Height);
                CvInvoke.CvtColor(scr, scr1, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);
                //图像类型转换，bgr 转成 gray 类型。
                CvInvoke.Threshold(scr1, scr2, 100, 255, Emgu.CV.CvEnum.ThresholdType.Binary);
                //对图像进行二值化操作。
              scr2.Save(PictureProcessedAddress);
              pictureBox3.Load(PictureProcessedAddress);
                

                byte[, ,] pixel = new byte[scr2.Cols, scr2.Rows, 0];          
                pixel = scr2.Data;

                CoordinationTransformation coordinationTransformation = new CoordinationTransformation();

                int temp1 = 0;
                string[] p1 = new string[1500];
                //Point L=new Point();

                int j = 0;
                int k = 0;
                int[] TransformResult = new int[2];

                do
                {
                    for (int i = 0; i < scr2.Cols - 20; i++)
                    {
                        if (pixel[j, i, 0] == 0)
                        {
                           TransformResult= coordinationTransformation.sub((double)i, (double)j);
                           p1[k] = TransformResult[0].ToString();
                           p1[k + 1] = TransformResult[1].ToString();
                           p1[k + 2] = (-228878).ToString();
                            p1[k + 3] = (-1789992).ToString();
                            p1[k + 4] = (-315122).ToString();
                            p1[k + 5] = (65610).ToString();
                            k = k + 6;
                          //  L.X = i; L.Y = j;
                        //    CvInvoke.Circle(scr2, L, 1, new MCvScalar(0, 0, 255, 255), 10, LineType.EightConnected);

                            listBox3.Items.Add("第" + (k / 6 + 1).ToString() + "个点的X,Y坐标：" + "\t\t" + TransformResult[0].ToString() + "\t\t" + TransformResult[1].ToString());

                            break;
                        }

                        else temp1++;
                    }
                    j = j + 10;//每20行检测一次，看是否有轨迹点
                }
                while (j < scr2.Rows - 20);

                
                M = string.Join(",", p1);
        
                //CvInvoke.WaitKey(0);

            }

            private void pictureBox3_Click(object sender, EventArgs e)
            {
                         
            }

            private void pictureBox1_Click(object sender, EventArgs e)
            {

            }

            private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
            {
                
            }

            private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
            {

            }

            private void button10_Click(object sender, EventArgs e)
            {
                string s = "1";
                clientSocket.Send(Encoding.ASCII.GetBytes(s));    //Encoding.ASCII.GetBytes(sendMessage)   //a,a.Length,SocketFlags.None
            }

            private void button10_Click_1(object sender, EventArgs e)
            {
                string s = "1";
                clientSocket.Send(Encoding.ASCII.GetBytes(s));    //Encoding.ASCII.GetBytes(sendMessage)   //a,a.Length,SocketFlags.None

                Thread.Sleep(1000);
                //string s = "923857, -38221, -209181, -1789992, -315122, 65610,1074305, -35249, -209211, -1789986, -315089, 65617";
                clientSocket.Send(Encoding.ASCII.GetBytes(M));    //Encoding.ASCII.GetBytes(sendMessage)   //a,a.Length,SocketFlags.None
            }

     }
}

