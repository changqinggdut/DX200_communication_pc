/* TcpSvr1.c */
/* Copyright 2009 YASKAWA ELECTRIC All Rights reserved. */

#include "motoPlus.h"

// for GLOBAL DATA DEFINITIONS
SEM_ID semid;

// for IMPORT API & FUNCTIONS
extern void moto_plus0_task(void);//extern可以置于变量或者函数前，以标示变量或者函数的定义在别的文件中，提示编译器遇到此变量和函数时在其他模块中寻找其定义。此外extern也可用来进行链接指定。

// for LOCAL DEFINITIONS
static int tid1;

void mpUsrRoot(int arg1, int arg2, int arg3, int arg4, int arg5,
	       int arg6, int arg7, int arg8, int arg9, int arg10)
{
	tid1 = mpCreateTask(MP_PRI_TIME_NORMAL, MP_STACK_SIZE, (FUNCPTR)moto_plus0_task,//可以将机器人任务：moto_plus0_task放在另外一个源文件里面。
			    arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
	semid = mpSemBCreate(SEM_Q_FIFO, SEM_EMPTY);	// バイナリセマフォ
	puts("Exit mpUsrRoot!");
	mpExitUsrRoot;	//(or) mpSuspendSelf;
}
<<<<<<< HEAD
=======


>>>>>>> 通信
