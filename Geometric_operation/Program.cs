using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometric_operation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 测试 向量平行判断
            //var v1 = new TZVector { x = 123, y = 234, z = 456 };
            //var v2 = new TZVector { x = 123 * 2, y = 234 * 2, z = 456 * 2 };
            //var res = TZVector.TZVectorParallelism(v1, v2);
            //Console.WriteLine(res);

            // 测试 向量叉积计算
            //var v1 = new TZVector { x = 123, y = 234, z = 456 };
            //var v2 = new TZVector { x = 123 * 2, y = 234 * 2, z = (double)(456.5 * 2) };
            // 挡块
            var v1 = new TZVector { x = 104.26, y = 386.17, z = 12.00 };
            // 盖梁
            var v2 = new TZVector { x = 3114.72, y = 11536.94, z = 358.50 };
            var res = TZVector.TZVectorCross(v1, v2);
            Console.WriteLine(res.ToString());

            System.Console.ReadLine();


        }


    }

    class TZMath
    {
        // 保留小数位
        static public double TZReservedDecimalPlace(double num, int n)
        {
            long tmp = (long)(num * Math.Pow(10, n));
            double tmp2 = tmp / Math.Pow(10, n);

            return tmp2;
        }
    }

    /// <summary>
    /// 空间点
    /// </summary>
    class TZPoint
    {
        public double x { get; set; }
        public double y { get; set; }
        public double z { get; set; }
    }
    /// <summary>
    /// 空间向量
    /// </summary>
    class TZVector
    {
        public double x { get; set; }
        public double y { get; set; }
        public double z { get; set; }
        public TZVector() { }
        public TZVector(TZPoint Point1, TZPoint Point2)
        {
            this.x = Point2.x - Point1.x;
            this.y = Point2.y - Point1.y;
            this.z = Point2.z - Point1.z;
        }

        // 返回模长
        public double TZGetMagnitude()
        {
            return (double)Math.Pow((Math.Pow(x, 2) + Math.Pow(y, 2) + Math.Pow(z, 2)), 0.5);
        }

        // 返回归一化向量
        public TZVector TZGetNormalize()
        {
            return new TZVector
            {
                x = x / TZGetMagnitude(),
                y = y / TZGetMagnitude(),
                z = z / TZGetMagnitude()
            };
        }

        // 向量叉积 精度误差 保留5位小数 应该就问题不大了
        static public TZVector TZVectorCross(TZVector V1, TZVector V2)
        {
            V1 = V1.TZGetNormalize();
            V2 = V2.TZGetNormalize();
            int n = 5;
            double x = V1.y * V2.z - V1.z * V2.y;
            x = TZMath.TZReservedDecimalPlace(x, n);
            double y = V1.z * V2.x - V1.x * V2.z;
            y = TZMath.TZReservedDecimalPlace(y, n);
            double z = V1.x * V2.y - V1.y * V2.x;
            z = TZMath.TZReservedDecimalPlace(z, n);

            return new TZVector() { x = x, y = y, z = z };
        }


        // 向量平行判断
        public static bool TZVectorParallelism(TZVector v1, TZVector v2)
        {
            // 成倍数就是平行
            double kx = v1.x / v2.x;
            double ky = v1.y / v2.y;
            double kz = v1.z / v2.z;

            if (kx == ky && ky == kz) return true;

            return false;
        }

        public override string ToString()
        {
            //return "(" + TZMath.TZReservedDecimalPlace(x, 5) + ", " + TZMath.TZReservedDecimalPlace(y, 5) + ", " + TZMath.TZReservedDecimalPlace(z, 5) + ")";
            return "(" + x + ", " + y + ", " + z + ")";
        }
    }

    /// <summary>
    /// 空间平面
    /// </summary>
    class TZPlane
    {
        public double A { get; set; }
        public double B { get; set; }
        public double C { get; set; }
        public double D { get; set; }

        public TZPlane() { }

        /// <summary>
        /// 3点创建平面
        /// </summary>
        /// <param name="Point1"></param>
        /// <param name="Point2"></param>
        /// <param name="Point3"></param>
        public TZPlane(TZPoint Point1, TZPoint Point2, TZPoint Point3)
        {
            // 2向量
            var v1 = new TZVector(Point1, Point2);
            var v2 = new TZVector(Point2, Point3);
            // 法向量 ABC
            var n = TZVector.TZVectorCross(v1, v2);
            this.A = n.x; this.B = n.y; this.C = n.z;
            // D 
            this.D = -(A * Point1.x + B * Point1.y + C * Point1.z);
        }

        /// <summary>
        /// 2向量创建平面
        /// </summary>
        /// <param name="V1"></param>
        /// <param name="V2"></param>
        public TZPlane(TZVector V1, TZVector V2)
        {
            // 法向量 ABC
            var n = TZVector.TZVectorCross(V1, V2);
            this.A = n.x; this.B = n.y; this.C = n.z;
            // D 
            this.D = -(A * V1.x + B * V1.y + C * V1.z);
        }
    }
}
