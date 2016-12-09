%---------------------------
% 空间3维坐标旋转 子程序
% 输入X、Y、Z坐标，以及旋转角RotA = [RotX,RotY,RotZ]
% 其中RotX,RotY,RotZ分别表示3维坐标绕X、Y、Z轴的旋转角度，单位为弧度
% 输出旋转后的坐标X',Y',Z'
%---------------------------

function [X2_3D,Y2_3D,Z2_3D] = Rotation3D(X1_3D,Y1_3D,Z1_3D,RotA)

% 检测输入数据的行列数
    [Szy,Szx] = size(X1_3D);
% 矩阵初始化
    X2_3D = X1_3D;
    Y2_3D = Y1_3D;
    Z2_3D = Z1_3D;
% 绕X轴旋转
    AngX  = RotA(1);
    if AngX ~= 0
        for idy = 1:Szy
            for idx = 1:Szx
                TmpX   = X2_3D(idy,idx);
                TmpY   = Y2_3D(idy,idx);
                TmpZ   = Z2_3D(idy,idx);
                X2_3D(idy,idx) =  TmpX;
                Y2_3D(idy,idx) =  TmpY*cos(AngX)-TmpZ*sin(AngX);
                Z2_3D(idy,idx) =  TmpY*sin(AngX)+TmpZ*cos(AngX);
            end
        end
    end
% 绕Y轴旋转
    AngY  = RotA(2);
    if AngY ~= 0
        for idy = 1:Szy
            for idx = 1:Szx
                TmpX   = X2_3D(idy,idx);
                TmpY   = Y2_3D(idy,idx);
                TmpZ   = Z2_3D(idy,idx);
                X2_3D(idy,idx) =  TmpX*cos(AngY)+TmpZ*sin(AngY);
                Y2_3D(idy,idx) =  TmpY;
                Z2_3D(idy,idx) =  -TmpX*sin(AngY)+TmpZ*cos(AngY);
            end
        end
    end
% 绕Z轴旋转
    AngZ  = RotA(3);
    if AngZ ~= 0
        for idy = 1:Szy
            for idx = 1:Szx
                TmpX   = X2_3D(idy,idx);
                TmpY   = Y2_3D(idy,idx);
                TmpZ   = Z2_3D(idy,idx);
                X2_3D(idy,idx) =  TmpX*cos(AngZ)-TmpY*sin(AngZ);
                Y2_3D(idy,idx) =  TmpX*sin(AngZ)+TmpY*cos(AngZ);
                Z2_3D(idy,idx) =  TmpZ;
            end
        end
    end
  
return
