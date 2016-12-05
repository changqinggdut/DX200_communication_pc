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

namespace bo_communication_with_dx200
{
    public partial class Form1 : Form
    {
         static byte[] result = new byte[1024];
         public string[,] EnterCoordination=new string[100,6];
         int NumofEnterCoordinationPoint=0;

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
                  IPAddress ip = IPAddress.Parse("192.168.255.1");
                  int port = 11000;
            
                Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            
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
                    int receiveLength = clientSocket.Receive(result);
                    Console.WriteLine("接收服务器消息：{0}", Encoding.ASCII.GetString(result));
                    // Encoding.ASCII.GetString(result,0,result.Length);
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

            }


    }

}

