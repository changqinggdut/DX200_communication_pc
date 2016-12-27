using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Util;
using Emgu.CV.Cuda;
using Emgu.CV.XFeatures2D;
using System.Drawing;
using System.Collections;
using Emgu.CV.Structure;
using System.IO;

namespace bo_communication_with_dx200
{
    class CoordinationTransformation
    {
        public int[] sub(double x1, double y1)
        {
            double a = 3.1416;double b = 0;double c=2.8392;//需要增加注释
            int[] result = new int[2];
            double x0 = 0; double y0 = 0; double z0 = -228878;

            Matrix<double> V = new Matrix<double>(1, 3);
            V.Data[0, 0] = x0;
            V.Data[0, 1] = y0;
            V.Data[0, 2] = z0;
            Matrix<double> R = new Matrix<double>(1, 3);
            R.Data[0, 0] = 0.0699 * x1 - 37.8648;
            R.Data[0, 1] = 0.0699 * y1 -43.0345;
            R.Data[0, 2] = 0; 

            Matrix<double> S = new Matrix<double>(3, 3);
            S.Data[0, 0] = Math.Cos(a);
            S.Data[0, 1] = Math.Sin(a);
            S.Data[0, 2] = 0;
            S.Data[1, 0] = -Math.Sin(a);
            S.Data[1, 1] = Math.Cos(a);
            S.Data[1, 2] = 0;
            S.Data[2, 0] = 0;
            S.Data[2, 1] = 0;
            S.Data[2, 2] = 1;
            Matrix<double> W = new Matrix<double>(3, 3);
            W.Data[0, 0] = Math.Cos(b);
            W.Data[0, 1] = Math.Sin(b);
            W.Data[0, 2] = 0;
            W.Data[1, 0] = -Math.Sin(b);
            W.Data[1, 1] = Math.Cos(b);
            W.Data[1, 2] = 0;
            W.Data[2, 0] = 0;
            W.Data[2, 1] = 0;
            W.Data[2, 2] = 1;
            Matrix<double> T = new Matrix<double>(3, 3);
            T.Data[0, 0] = 1;
            T.Data[0, 1] = 0;
            T.Data[0, 2] = 0;
            T.Data[1, 0] = 0;
            T.Data[1, 1] = Math.Cos(c);
            T.Data[1, 2] = Math.Sin(c);
            T.Data[2, 0] = 0;
            T.Data[2, 1] = -Math.Sin(c);
            T.Data[2, 2] = Math.Cos(c);

            Matrix<double> T1 = new Matrix<double>(3, 3);
            T1.Data[0, 0] = Math.Cos(c);
            T1.Data[0, 1] = Math.Sin(c);
            T1.Data[0, 2] = 0;
            T1.Data[1, 0] = -Math.Sin(c);
            T1.Data[1, 1] = Math.Cos(c);
            T1.Data[1, 2] = 0;
            T1.Data[2, 0] = 0;
            T1.Data[2, 1] = 0;
            T1.Data[2, 2] = 1;    
          
            Matrix<double> U = new Matrix<double>(1, 3);
            U.Data[0, 0] = 945.3303;
            U.Data[0, 1] = -24.9752;
            U.Data[0, 2] = -228878;


            Matrix<double> S1 = new Matrix<double>(1, 3);
            Matrix<double> S2 = new Matrix<double>(1, 3);
            Matrix<double> S3 = new Matrix<double>(1, 3);
            Matrix<double> S4 = new Matrix<double>(1, 3);
            S1 = R.Mul(S);
            S2 = S1.Mul(W);
            S3=S2.Mul(T);
            S4 = S3.Mul(T1);
            V = S4+ U;
          

            result[0] =(int) (V.Data[0, 0]);
            result[1] =(int) (V.Data[0, 1]);


          return (result);

            
        }
    }
}
