%-------------------------
% 相机坐标系转换程序
% 将图像坐标转换为机器人坐标
% 2016.11.23
%-------------------------
% 图像模拟
clc;clear;
% 输入图像坐标
ImagePoint = [1960 744 0;2190 1636 0;1070 1230 0;1394 262 0;];

%输入机器人坐标
% Robot2 = load('实际坐标点2.txt');
Robot2 = [897537 -333791 -209181;826434 -338889 -209181;874513 -257464 -209181;945319 -297521 -209181;];
Robot  = zeros(4,3);
Robot(1,:)=Robot2(1,:);
Robot(2,:)=Robot2(2,:);
Robot(3,:)=Robot2(3,:);
Robot(4,:)=Robot2(4,:);
% Robot(5,:)=Robot2(31,:);
% 像素缩放
Diset1 = sqrt((ImagePoint(2,1)-ImagePoint(1,1))^2+(ImagePoint(2,2)-ImagePoint(1,2))^2);
Diset2 = sqrt((Robot(2,1)-Robot(1,1))^2+(Robot(2,2)-Robot(1,2))^2);
K      = Diset2/Diset1;

ImagePoint2 = ImagePoint*K;
% K = 0.791;
X1 = 970*K
Y1 = 1478*K
Z1 = 0
% 1338 993 0
% 
% 坐标系统转换矩阵计算
[RotA1,Shift1] = CorSysMatch(Robot,ImagePoint2);
%----------------
% 曲面匹配
% 先平移后旋转
NXDataTxt1 = X1 - Shift1(2,1)
NYDataTxt1 = Y1 - Shift1(2,2)
NZDataTxt1 = Z1 - Shift1(2,3)
for idn =1:5
    RotAng = RotA1(idn,:);
    [NXDataTxt1,NYDataTxt1,NZDataTxt1] = Rotation3D(NXDataTxt1,NYDataTxt1,NZDataTxt1,RotAng);
end
NXDataTxt1 = NXDataTxt1 + Shift1(1,1);
NYDataTxt1 = NYDataTxt1 + Shift1(1,2);
NZDataTxt1 = NZDataTxt1 + Shift1(1,3);
%----------------
% X0 = 970*0.785;
% Y0 = 1478*0.785;
% Z0 = 0;
%----------------
% S1
% X2 = X1 - 143.012
% Y2 = Y1 - 106.65
% Z2 = Z1;
% % %----------------
% % % S2
% AngZ = -3.017;
% X3 =  X2*cos(AngZ)-Y2*sin(AngZ);
% Y3 =  X2*sin(AngZ)+Y2*cos(AngZ);
% Z3 =  Z2;
% % %----------------
% % % S3
% AngX = -3.1416;
% X4 =  X3;
% Y4 =  Y3*cos(AngX)-Z3*sin(AngX);
% Z4 =  Y3*sin(AngX)+Z3*cos(AngX);
% 
% AngZ = 1.5224;
% X5 =  X4*cos(AngZ)-Y4*sin(AngZ);
% Y5 =  X4*sin(AngZ)+Y4*cos(AngZ);
% Z5 =  Z4;
% %----------------
% % S4
% X6 = X5 + 914.46;
% Y6 = Y5 -310.46;
% Z6 = Z5 -209181 ;
% %----------------

    







