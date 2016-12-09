%---------------------------
% 坐标系匹配子程序
% 输入原坐标系4个点的3维坐标和目标坐标系对应4个点的3维坐标
% 通过4次计算 求平均
% 输出匹配需要的旋转平移矩阵
%---------------------------
function [RotAng,ShiftXYZ] = CorSysMatch(PA,PB)
%----------------------------
% 调用子程序
[RotA,Shift] = CormatchV1(PA,PB);
% 先平移后旋转
PB1(:,1) = PB(:,1) - Shift(2,1);
PB1(:,2) = PB(:,2) - Shift(2,2);
PB1(:,3) = PB(:,3) - Shift(2,3);
for idn =1:5
    RotAng = RotA(idn,:);
    [PB1(:,1),PB1(:,2),PB1(:,3)] = Rotation3D(PB1(:,1),PB1(:,2),PB1(:,3),RotAng);
end
PB1(:,1) = PB1(:,1) + Shift(1,1);
PB1(:,2) = PB1(:,2) + Shift(1,2);
PB1(:,3) = PB1(:,3) + Shift(1,3);
%-------------------------------
% 矩阵平移
PA = [PA(2:end,:); PA(1:1,:)];
PB = [PB(2:end,:); PB(1:1,:)];
[RotA,Shift] = CormatchV1(PA,PB);
% 先平移后旋转
PB2(:,1) = PB(:,1) - Shift(2,1);
PB2(:,2) = PB(:,2) - Shift(2,2);
PB2(:,3) = PB(:,3) - Shift(2,3);
for idn =1:5
    RotAng = RotA(idn,:);
    [PB2(:,1),PB2(:,2),PB2(:,3)] = Rotation3D(PB2(:,1),PB2(:,2),PB2(:,3),RotAng);
end
PB2(:,1) = PB2(:,1) + Shift(1,1);
PB2(:,2) = PB2(:,2) + Shift(1,2);
PB2(:,3) = PB2(:,3) + Shift(1,3);
% 矩阵右移
PB2 = [PB2(end:end,:); PB2(1:end-1,:)];
%-------------------------------
% 矩阵平移
PA = [PA(2:end,:); PA(1:1,:)];
PB = [PB(2:end,:); PB(1:1,:)];
[RotA,Shift] = CormatchV1(PA,PB);
% 先平移后旋转
PB3(:,1) = PB(:,1) - Shift(2,1);
PB3(:,2) = PB(:,2) - Shift(2,2);
PB3(:,3) = PB(:,3) - Shift(2,3);
for idn =1:5
    RotAng = RotA(idn,:);
    [PB3(:,1),PB3(:,2),PB3(:,3)] = Rotation3D(PB3(:,1),PB3(:,2),PB3(:,3),RotAng);
end
PB3(:,1) = PB3(:,1) + Shift(1,1);
PB3(:,2) = PB3(:,2) + Shift(1,2);
PB3(:,3) = PB3(:,3) + Shift(1,3);
% 矩阵右移
PB3 = [PB3(end-1:end,:); PB3(1:end-2,:)];
%-------------------------------
% 矩阵平移
PA = [PA(2:end,:); PA(1:1,:)];
PB = [PB(2:end,:); PB(1:1,:)];
[RotA,Shift] = CormatchV1(PA,PB);
% 先平移后旋转
PB4(:,1) = PB(:,1) - Shift(2,1);
PB4(:,2) = PB(:,2) - Shift(2,2);
PB4(:,3) = PB(:,3) - Shift(2,3);
for idn =1:5
    RotAng = RotA(idn,:);
    [PB4(:,1),PB4(:,2),PB4(:,3)] = Rotation3D(PB4(:,1),PB4(:,2),PB4(:,3),RotAng);
end
PB4(:,1) = PB4(:,1) + Shift(1,1);
PB4(:,2) = PB4(:,2) + Shift(1,2);
PB4(:,3) = PB4(:,3) + Shift(1,3);
% 矩阵右移
PB4 = [PB4(end-2:end,:); PB4(1:end-3,:)];
% 矩阵还原
PA  = [PA(2:end,:); PA(1:1,:)];
PB  = [PB(2:end,:); PB(1:1,:)];
%------------------------------
% 求平均 得到新的标准坐标系
NewPA(:,1) = PB1(:,1)./4 + PB2(:,1)./4 + PB3(:,1)./4 + PB4(:,1)./4;
NewPA(:,2) = PB1(:,2)./4 + PB2(:,2)./4 + PB3(:,2)./4 + PB4(:,2)./4;
NewPA(:,3) = PB1(:,3)./4 + PB2(:,3)./4 + PB3(:,3)./4 + PB4(:,3)./4;
% 求到新标准坐标系的旋转平移量
[RotAng,ShiftXYZ] = CormatchV1(NewPA,PB);

return




%-----