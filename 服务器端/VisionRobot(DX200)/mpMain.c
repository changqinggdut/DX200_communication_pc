//mpMain.c
#include "motoPlus.h"
#include <stdio.h>
#include <string.h>
#include <stdlib.h>


// for GLOBAL DATA DEFINITIONS
void moto_plus0_task(void);//子函数的声明
void ap_TCP_Sserver(ULONG portNo);//子函数的声明
extern void continuous_move(int MoveData[][6] , int n);
extern void CalibrateLocation(void);

void location(char*string );
static int tid1;
#define PORT        11000 //端口号，
#define BUFF_MAX    20479




//GLOBAL DATA DEFINITIONS

int b[1000];
char g[1000];
int pointSum;
int d[500][6];


void mpUsrRoot(int arg1, int arg2, int arg3, int arg4, int arg5,
	       int arg6, int arg7, int arg8, int arg9, int arg10)
{
	tid1 = mpCreateTask(MP_PRI_TIME_NORMAL, MP_STACK_SIZE, (FUNCPTR)moto_plus0_task,//可以将机器人任务：moto_plus0_task放在另外一个源文件里面。
			    arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
	
	puts("Exit mpUsrRoot!");
	mpExitUsrRoot;	//(or) mpSuspendSelf;
}



void moto_plus0_task(void)
{
    
    ap_TCP_Sserver(PORT);

    mpSuspendSelf;//通讯程序停止，表示子任务的停止。
}




void ap_TCP_Sserver(ULONG portNo)
{
    int     sockHandle;
    struct  sockaddr_in     serverSockAddr;
    int     rc;

    

    sockHandle = mpSocket(AF_INET, SOCK_STREAM, 0);//创建socket,这里需要注意三个参数：第一个参数只有一种值，AF_INET表示使用ipv4通信簇， 确定通信类型：tcp或者tdp，该函数会输出一个新建的socket的描述符。
    if (sockHandle < 0)//如果socket创建失败，进行返回
        return;

    memset(&serverSockAddr, 0, sizeof(serverSockAddr));//初始化，
    serverSockAddr.sin_family = AF_INET; //该参数表示使用ipv4的协议。
    //serverAddr = mpInetNtoa("192.168.255.1"); //(重要) IP address of the DX200 (server)
    serverSockAddr.sin_addr.s_addr =  mpInetAddr("192.168.255.1");//INADDR_ANY就是指定地址为0.0.0.0的地址，这个地址事实上表示不确定地址，或“所有地址”、“任意地址”。 一般来说，在各个系统中均定义成为0值。
    serverSockAddr.sin_port = mpHtons(portNo);//将十进制的端口号转化成为网络形式的端口号。

    rc = mpBind(sockHandle, (struct sockaddr *)&serverSockAddr, sizeof(serverSockAddr)); //绑定socket和端口号，对sockt绑定需要知道该socket的描述符，还有需要知道服务器端的端口地址。
    if (rc < 0)
        goto closeSockHandle;//关闭端口号，

    rc = mpListen(sockHandle, SOMAXCONN);//监听端口号
    if (rc < 0)
        goto closeSockHandle;

    while (1)
    {
        int     acceptHandle;
        struct  sockaddr_in     clientSockAddr;
        int     sizeofSockAddr;

        memset(&clientSockAddr, 0, sizeof(clientSockAddr));
        sizeofSockAddr = sizeof(clientSockAddr);

        acceptHandle = mpAccept(sockHandle, (struct sockaddr *)&clientSockAddr, &sizeofSockAddr);//接收来自客户端的链接请求

        if (acceptHandle < 0)
            break;

       /*------------------------------主程序入口-------------------------------*/
        while(1)   
        {
            int     bytesRecv;
            char    buff[(int)BUFF_MAX + 1];

            memset(buff, 0, sizeof(buff));

            bytesRecv = mpRecv(acceptHandle, buff, (int)BUFF_MAX, 0);//从socket中提取字符,当返回值bytesRecv >=0时为接收正常 =-1为接收异常

            if (bytesRecv < 0)
                break;
                
              int stenghtBuff =strlen (buff);//测量buff字节长度
              int buffConvernInt=atoi(buff);//将char型buff转换为整形
              if(stenghtBuff<3)        //  用两位以内的数存接收命令
              {
              
                switch(buffConvernInt)
                {
                case 1:
                       memset(buff, 0, sizeof(buff));
                       bytesRecv= -1;
                       
                       
                      while (bytesRecv < 0)
                     {
                       bytesRecv = mpRecv(acceptHandle, buff, (int)BUFF_MAX, 0);  //接收坐标形如12,34,56,78.....样式的一系列字符串
                       location(buff); //将接收到的字符串转成二维数组d[][6]
                       
                       continuous_move(d,pointSum);//调用子程序motoplusMove里的continuous_move运动程序
                     }
                 break;
                 
                 case 2:
                         CalibrateLocation();//调用子程序CalibrateLocation里的CalibrateLocation运动程序
                       
                   
                 break;
                   
                }
             }
               
           

        }
        mpClose(acceptHandle);//关闭socket
    }
closeSockHandle:
    mpClose(sockHandle);//关闭socket

    return;
}



void location(char*string ){

/****************将buff里的字符串通过逗号分开转成整形数组**************/
int i=0;
int j;
char * p;
const char * split = ",";

p = strtok (string,split);

while(p!=NULL)
{
   b[i]= atoi(p);
   printf ("%d\n",b[i]);
   ++i;
   p = strtok(NULL,split);
  
}

 pointSum=(i/6);//运动总点数

/****************将一维数组转成二维数组**************/


int k=0;
for(  i=0; i<pointSum ; ++i )

{

  for( j=0 ; j<6 ; ++j )

    {

        d[i][j]=b[k];

            k++;

    }

}

}


