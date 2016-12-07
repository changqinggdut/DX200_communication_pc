using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading.Tasks;
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
                    Console.WriteLine("连接服务器成功");
                    button1.BackColor=Color.Green;

                    Thread th = new Thread(recMsg);//新建后台接受信息线程。
                    th.IsBackground = true;
                    th.Start(clientSocket);


                }
                catch
                {
                    Console.WriteLine("连接服务器失败，请按回车键退出！");
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
                    MessageBox.Show(RebackData);
                   // listBox2.Items.Add(RebackData);

                 
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

                for (int i = 0; i < 10; i++)
                {
                    /* code */
                    MessageBox.Show(i.ToString());
                }


                for (int i = 0; i < 10; ++i)
                {
                    MessageBox.Show(i.ToString());
                }
                
            }

            private void button7_Click(object sender, EventArgs e)
            {
                
                string s = "1,2,2,9,4,4,1,1,5,5,1,1,8,2,5,2,5,2";
                clientSocket.Send(Encoding.ASCII.GetBytes(s));    //Encoding.ASCII.GetBytes(sendMessage)   //a,a.Length,SocketFlags.None
               // Console.WriteLine("向服务器发送消息：" + s);



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
                    while (camera.StreamGrabber.IsGrabbing)
                    {
                        // Wait for an image and then retrieve it. A timeout of 5000 ms is used.
                        IGrabResult grabResult = camera.StreamGrabber.RetrieveResult(5000, TimeoutHandling.ThrowException);
                        using (grabResult)
                        {
                            // Image grabbed successfully?
                            if (grabResult.GrabSucceeded)
                            {

                                string name = "E:\\实验缓存照片\\" + System.DateTime.Now.Day.ToString() + System.DateTime.Now.Minute.ToString() + System.DateTime.Now.Second.ToString() + ".png";
                                ImagePersistence.Save(ImageFileFormat.Png, name, grabResult);


                                Mat original_picture = CvInvoke.Imread(name, LoadImageType.AnyColor);
                                string win1 = "original picture";
                                CvInvoke.NamedWindow(win1, NamedWindowType.Normal);
                                CvInvoke.Imshow(win1, original_picture);

                                CvInvoke.WaitKey(0);
                                //IntPtr mat = original_picture.Ptr;
                                //CvInvoke.cvReleaseMat(ref mat);
                                //CvInvoke.DestroyWindow(win1);

                                pictureBox1.Load(name);
                            
                                camera.Close();   //每一次调用完相机后都需要将相机关闭
                                break;

                              

                            }
                            else
                            {
                                Console.WriteLine("Error: {0} {1}", grabResult.ErrorCode, grabResult.ErrorDescription);
                                MessageBox.Show("something Wrong :Error: {0} {1}, grabResult.ErrorCode, grabResult.ErrorDescription");
                                break;
                            }
                        }
                    }
                    //camera.Parameters[PLCamera.ChunkModeActive].SetValue(false);
                }
               


            }

     }
}

