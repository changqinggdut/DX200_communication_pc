//mpMain.c
#include "motoPlus.h"

void CalibrateLocation(void)
{

    int i;
    int len;
    
    MP_SERVO_POWER_SEND_DATA set_power;
	MP_STD_RSP_DATA status_out;
	memset(&set_power, CLEAR, sizeof(MP_SERVO_POWER_SEND_DATA));
	memset(&status_out, CLEAR, sizeof(MP_STD_RSP_DATA));
	set_power.sServoPower=1;
    mpSetServoPower(&set_power,&status_out);
    
    MP_START_JOB_SEND_DATA job_info;
	MP_STD_RSP_DATA out_info;
	memset(&job_info,CLEAR,sizeof(MP_START_JOB_SEND_DATA));
	memset(&out_info,CLEAR,sizeof(MP_STD_RSP_DATA));


	char name1[]="CALIBRATECAMERA";
	len=strlen(name1);
    for(i=0;i<len;i++)
    {
      job_info.cJobName[i]=name1[i];
    }
    
	mpStartJob(&job_info,&out_info);
	mpTaskDelay(15000);

	set_power.sServoPower=0;
    mpSetServoPower(&set_power,&status_out);
}


