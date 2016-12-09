%---------------------------
% 坐标系匹配子程序
% 输入原坐标系3个点的3维坐标和目标坐标系对应3个点的3维坐标
% PA标准坐标系   PB需要转换的坐标系
% 输出匹配需要的旋转平移矩阵
%---------------------------
function [RotA,Shift] = CormatchV1(PA,PB)
%----------------------------
PA1 = zeros(size(PA));
PA2 = zeros(size(PA));
PA3 = zeros(size(PA));
PA4 = zeros(size(PA));
PA5 = zeros(size(PA));
PB1 = zeros(size(PA));
PB2 = zeros(size(PA));
PB3 = zeros(size(PA));
PB4 = zeros(size(PA));
PB5 = zeros(size(PA));
%----------------------------
% 先将两坐标系放到同一标准坐标系下
% Step1 平移 将其中一个点平移到原点
Shift1 = [PA(1,1) PA(1,2) PA(1,3)];
PA1(:,1) = PA(:,1) - Shift1(1);
PA1(:,2) = PA(:,2) - Shift1(2);
PA1(:,3) = PA(:,3) - Shift1(3);
Shift2 = [PB(1,1) PB(1,2) PB(1,3)];
PB1(:,1) = PB(:,1) - Shift2(1);
PB1(:,2) = PB(:,2) - Shift2(2);
PB1(:,3) = PB(:,3) - Shift2(3);
%----------------------------
% Step2 绕Z轴旋转
AngZ1 = atan2(PA1(2,2),PA1(2,1));
AngZ2 = atan2(PB1(2,2),PB1(2,1));
RotZ1 = [0 0 -AngZ1];
[PA2(:,1),PA2(:,2),PA2(:,3)] = Rotation3D(PA1(:,1),PA1(:,2),PA1(:,3),RotZ1);
RotZ2 = [0 0 -AngZ2];
[PB2(:,1),PB2(:,2),PB2(:,3)] = Rotation3D(PB1(:,1),PB1(:,2),PB1(:,3),RotZ2);
% figure(80)
% plot3(PA2(:,1),PA2(:,2),PA2(:,3),'sk-');
% hold on
% plot3(PB2(:,1),PB2(:,2),PB2(:,3),'sb-');
% hold off
%----------------------------
% Step3 绕Y轴旋转
AngY1 = atan2(PA2(2,3),PA2(2,1));
AngY2 = atan2(PB2(2,3),PB2(2,1));
RotY1 = [0 AngY1 0];
[PA3(:,1),PA3(:,2),PA3(:,3)] = Rotation3D(PA2(:,1),PA2(:,2),PA2(:,3),RotY1);
RotY2 = [0 AngY2 0];
[PB3(:,1),PB3(:,2),PB3(:,3)] = Rotation3D(PB2(:,1),PB2(:,2),PB2(:,3),RotY2);
% figure(90)
% plot3(PA3(:,1),PA3(:,2),PA3(:,3),'sk-');
% hold on
% plot3(PB3(:,1),PB3(:,2),PB3(:,3),'sb-');
% hold off
%----------------------------
% Step4 绕X轴旋转
AngX1 = atan2(PA3(3,3),PA3(3,2));
AngX2 = atan2(PB3(3,3),PB3(3,2));
RotX1 = [-AngX1 0 0];
[PA4(:,1),PA4(:,2),PA4(:,3)] = Rotation3D(PA3(:,1),PA3(:,2),PA3(:,3),RotX1);
RotX2 = [-AngX2 0 0];
[PB4(:,1),PB4(:,2),PB4(:,3)] = Rotation3D(PB3(:,1),PB3(:,2),PB3(:,3),RotX2);
% figure(100)
% plot3(PA4(:,1),PA4(:,2),PA4(:,3),'sk-');
% hold on
% plot3(PB4(:,1),PB4(:,2),PB4(:,3),'sb-');
% hold off

%----------------------------
% 计算第第三边是否平行
% 如果不平行则进行修正
AngZZ1 = atan2(PA4(3,2),PA4(3,1));
AngZZ2 = atan2(PB4(3,2),PB4(3,1));
DetZZ  = AngZZ1-AngZZ2;
if abs(DetZZ) > 0.05
%     RotZZ1 = [0 0 0.5*DetZZ];
%     [PA4(:,1),PA4(:,2),PA4(:,3)] = Rotation3D(PA4(:,1),PA4(:,2),PA4(:,3),RotZZ1);
    RotZZ1 = [0 0 0.5*DetZZ];
    [PB4(:,1),PB4(:,2),PB4(:,3)] = Rotation3D(PB4(:,1),PB4(:,2),PB4(:,3),RotZZ1);
else
    RotZZ1 = [0 0 0];
end
% figure(110)
% plot3(PA4(:,1),PA4(:,2),PA4(:,3),'sk-');
% hold on
% plot3(PB4(:,1),PB4(:,2),PB4(:,3),'sb-');
% hold off
% axis equal


% 计算旋转匹配矩阵
Shift(1,:) = Shift1;
Shift(2,:) = Shift2;
RotA(1,:)  = RotZ2;
RotA(2,:)  = RotY2;
RotA(3,:)  = RotX2;
RotA(4,:)  = RotZZ1;
RotA(5,:)  = [-RotX1(1) -RotY1(2) -RotZ1(3)];
% RotA(6,:)  = RotZZ1;

% TRotX1 = [1 0 0;0 cos(AngX1) sin(AngX1);0 -sin(AngX1) cos(AngX1)];
% TRotX2 = [1 0 0;0 cos(AngX2) sin(AngX2);0 -sin(AngX2) cos(AngX2)];
% TRotY1 = [cos(AngY1) 0 -sin(AngY1);0 1 0;sin(AngY1) 0 cos(AngY1)];
% TRotY2 = [cos(AngY2) 0 -sin(AngY2);0 1 0;sin(AngY2) 0 cos(AngY2)];
% TRotZ1 = [cos(AngZ1) sin(AngZ1) 0;-sin(AngZ1) cos(AngZ1) 0;0 0 1];
% TRotZ2 = [cos(AngZ2) sin(AngZ2) 0;-sin(AngZ2) cos(AngZ2) 0;0 0 1];
% TRoySum = TRotZ2*TRotY2*TRotX2*TRotX1*TRotY1*TRotZ1;

% figure(100)
% plot3(PA(:,1),PA(:,2),PA(:,3),'sb-');
% hold on
% plot3(PB4(:,1),PB4(:,2),PB4(:,3),'r','linewidth',3);
% hold off
% axis equal
% title('匹配结果')


return




%-----