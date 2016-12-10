/* TcpSvr2.c */
/* Copyright 2009 YASKAWA ELECTRIC All Rights reserved. */

#include "motoPlus.h"
#include <stdio.h>
#include <string.h>
#include <stdlib.h>

// for API & FUNCTIONS
void moto_plus0_task(void);//子函数的声明
void ap_TCP_Sserver(ULONG portNo);//子函数的声明
#define PORT        11000 //端口号，
#define BUFF_MAX    1023

char* result[100];
int  SetApplicationInfo();
void mpTask1();
void moto_plus0_task();
void ap_TCP_Sserver(ULONG portNo);//子函数的声明



//GLOBAL DATA DEFINITIONS
int nTaskID1;
SEM_ID semid;
unsigned long serverAddr;

int a;
int c;
int b[1000];
char g[1000];


void moto_plus0_task(void)
{
    puts("Activate moto_plus0_task!");

    ap_TCP_Sserver(PORT);

    mpSuspendSelf;//通讯程序停止，表示子任务的停止。
}

//char *strupr(char *string);
char* location(char*string );

void ap_TCP_Sserver(ULONG portNo)
{
   int     sockHandle;
    struct  sockaddr_in     serverSockAddr;
    int     rc;

    printf("Simple TCP server\n");

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

        while(1)
        {
            int     bytesRecv;
            int     bytesSend;
            char    buff[(int)BUFF_MAX + 1];

            memset(buff, 0, sizeof(buff));

            bytesRecv = mpRecv(acceptHandle, buff, (int)BUFF_MAX, 0);//从socket中提取字符

            if (bytesRecv < 0)
                break;

            /* 受信データを大文字に変換して送り返す */
            location(buff);
            
            bytesSend = mpSend(acceptHandle, g, bytesRecv, 0);//向socket写入信息

            if (bytesSend != bytesRecv)
                break;

            if (strncmp(buff, "EXIT", 4) == 0 || strncmp(buff, "exit", 4) == 0)
                break;
        }
        mpClose(acceptHandle);//关闭socket
    }
closeSockHandle:
    mpClose(sockHandle);//关闭socket

    return;
}

char *strupr(char *string)//对字符实现转化，将小写字母转化成大写字母。
{
    int		i;
    int     len;

    len = strlen(string);
	for (i=0; i < len; i++)
	{
	    if (isalpha((unsigned char)string[i]))
	    {
		    string[i] = toupper(string[i]);
        }
	}
    return (string);
}






char *location(char*string ){

/****************将buff转成整形数组**************/
int i=0;
int j;
char * p;
const char * split = ",";

p = strtok (string,split);

while(p!=NULL)
{
   b[i]= atoi(p);
   printf ("%d\n",b[i]);
   i++;
   p = strtok(NULL,split);

}
/****************将一维数组转成二维数组**************/

int L=sizeof(b)/24;
int d[L][6];
int k=0;
for(  i=0; i<L ; ++i )

{

  for( j=0 ; j<6 ; ++j )

    {

        d[i][j]=b[k];

            k++;

    }

}
int x[3]={d[0][0],d[1][0],d[2][0]};


/****************将整形数组转成字符串**************/

int l;
sprintf(g,"");
for (i=0;i<3;i++) {
l = strlen(g);
if (i<2) sprintf(g+l,"\%d\,",x[i]);
else sprintf(g+l,"\%d",x[i]);
};
/****************************************************/
 
return(g);
}


