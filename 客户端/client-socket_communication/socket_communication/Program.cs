using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;


namespace SocketClient
{
    class Program
    {
        private static byte[] result = new byte[1024];
        static void Main(string[] args)
        {
            //设定服务器IP地址
            IPAddress ip = IPAddress.Parse("192.168.255.1");
         

            int port = 11000;
           // string host = "127.0.0.1";

            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //clientSocket.DualMode = true;
            try
            {
                clientSocket.Connect(new IPEndPoint(ip, port)); //配置服务器IP与端口
                Console.WriteLine("连接服务器成功");
            }
            catch
            {
                Console.WriteLine("连接服务器失败，请按回车键退出！");
                return;
            }
            //建立并开启接收线程
            Thread th = new Thread(recMsg);
            th.IsBackground = true;
            th.Start(clientSocket);

            //通过clientSocket接收数据
            //int receiveLength = clientSocket.Receive(result);
            //Console.WriteLine("接收服务器消息：{0}", Encoding.ASCII.GetString(result, 0, receiveLength));
            //通过 clientSocket 发送数据
           //for (int i = 0; i < 10; i++)
          //  {
                try
                {
                        //Console.ReadLine();
                        //Thread.Sleep(1000);    //等待1秒钟
                        //string sendMessage = "fg";
                       int [] myee;
                        myee = new int [3] { 1,0,0 };
                        string[] b;
                        b = new string[3];
                    
                        for (int j = 0; j < 3; j++)
                            {
                             b[j] = myee[j].ToString();
                            }
                   
                        string s = b[0]+","+b[1]+","+b[2];
                        clientSocket.Send(Encoding.ASCII.GetBytes(s));    //Encoding.ASCII.GetBytes(sendMessage)   //a,a.Length,SocketFlags.None
                        Console.WriteLine("向服务器发送消息：" + s);
                }
                catch
                {
                    clientSocket.Shutdown(SocketShutdown.Both);
                    clientSocket.Close();
                    //break;
                }

           // }
            Console.WriteLine("发送完毕，按回车键退出");
            Console.ReadLine();
        }

        static void recMsg(object o)
        {
            Socket clientSocket = (Socket) o;
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

                }
            }
        }
    }
}

