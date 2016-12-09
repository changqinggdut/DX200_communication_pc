#include "motoPlus.h"

int compare_vaule(MP_CART_POS_RSP_DATA output_value, MP_MOVL_SEND_DATA move_data);//子函数声明

void continuous_move(int MoveData[][6] , int n)  
{

    MP_SERVO_POWER_SEND_DATA set_power;
	MP_STD_RSP_DATA status_out;
	memset(&set_power, CLEAR, sizeof(MP_SERVO_POWER_SEND_DATA));
	memset(&status_out, CLEAR, sizeof(MP_STD_RSP_DATA));
	set_power.sServoPower = 1;
	mpSetServoPower(&set_power, &status_out);
	
    mpTaskDelay(100);
	
    int CompareResult = 0;
	MP_MOVL_SEND_DATA move_data[n];
	MP_STD_RSP_DATA std_data[n];
    int i;
	for (i = 0; i < n; i++)
	{

		memset(&move_data[i], CLEAR, sizeof(MP_IMOV_SEND_DATA));
		memset(&std_data[i], CLEAR, sizeof(MP_STD_RSP_DATA));

		move_data[i].sCtrlGrp = 0;
		move_data[i].lSpeed = 500;
		move_data[i].sVType = 0;
		move_data[i].sFrame = 1;
		move_data[i].lPos[0] = MoveData[i][0];
		move_data[i].lPos[1] = MoveData[i][1];
		move_data[i].lPos[2] = MoveData[i][2];
		move_data[i].lPos[3] = MoveData[i][3];
		move_data[i].lPos[4] = MoveData[i][4];
		move_data[i].lPos[5] = MoveData[i][5];
		
	}
	
	MP_CTRL_GRP_SEND_DATA  input_value; 
	MP_CART_POS_RSP_DATA   output_value;
	
	int k;
	for (k = 0; k < n; k++)
	{

		mpMOVL(&move_data[k], &std_data[k]);
		
	    memset(&input_value, CLEAR, sizeof(MP_CTRL_GRP_SEND_DATA));  //此“块”相当于mpTaskDelay(10000)
	    memset(&output_value, CLEAR, sizeof(MP_CART_POS_RSP_DATA));
	    input_value.sCtrlGrp = 0; 
	    CompareResult = 0;
	    
	    while (CompareResult == 0)
	    {
	      mpGetCartPos(&input_value, &output_value);
		  CompareResult = compare_vaule(output_value, move_data[k]);
		  
	    }
	    
	    mpTaskDelay(100);

	}

	set_power.sServoPower = 0;
	mpSetServoPower(&set_power, &status_out);

}


/****************************比较子函数****************************************************************/
int compare_vaule(MP_CART_POS_RSP_DATA output_value, MP_MOVL_SEND_DATA move_data)
{
	int status = 0;

	if (    ((output_value.lPos[0] / 1000) == (move_data.lPos[0] / 1000)) &&
		    ((output_value.lPos[1] / 1000) == (move_data.lPos[1] / 1000)) &&
		    ((output_value.lPos[2] / 1000) == (move_data.lPos[2] / 1000)) &&
		    ((output_value.lPos[3] / 1000) == (move_data.lPos[3] / 1000)) &&
		    ((output_value.lPos[4] / 1000) == (move_data.lPos[4] / 1000)) &&
		    ((output_value.lPos[5] / 1000) == (move_data.lPos[5] / 1000))
		)
		status = 1;
	else  status = 0;
	return (status);
}