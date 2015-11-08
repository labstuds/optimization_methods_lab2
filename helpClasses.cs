using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace optimizationLab2
{
    public class Lab
    {    
        static public Task task;
    }
        

    public class Vector2
    {
        private double x, y;
        // Для рандомного вектора
        public static Vector2 getRandomVectorKsi()
        {
            Random rnd = new Random();            
            double x = 2 * rnd.NextDouble() - 1;
            double y = 2 * rnd.NextDouble() - 1;
            return new Vector2(x, y);
        }

        public Vector2()
        {
            this.x = 0;
            this.y = 0;
        }

        public double getEuclidNorm()
        {
            return Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
        }
        ///////////////////////////////////////
        
        public Vector2(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public double X
        {
            get { return x; }
            set
            {
                x = value;
            }
        }

        public double Y
        {
            get { return y; }
            set
            {
                y = value;
            }
        }

        public double get(int index)
        {
            if (index == 0)
            {
                return x;
            }
            else if (index == 1)
            {
                return y;
            }
            else
            {
                throw new ArgumentOutOfRangeException("В Vector2 возможны только индексы 0 и 1");
            }
        }

        public void set(int index, double value)
        {
            if (index == 0)
            {
                x = value;
            }
            else if (index == 1)
            {
                y = value;
            }
            else
            {
                throw new ArgumentOutOfRangeException("В Vector2 возможны только индексы 0 и 1");
            }
        }

        public static Vector2 operator +(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X + v2.X, v1.Y + v2.Y);
        }

        public static Vector2 operator -(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X - v2.X, v1.Y - v2.Y);
        }

        public static bool operator <(Vector2 v1, Vector2 v2)
        {
            return v1.Length < v2.Length;
        }

        public static bool operator >(Vector2 v1, Vector2 v2)
        {
            return v2.Length > v2.Length;
        }

        public static bool operator ==(Vector2 v1, Vector2 v2)
        {
            return !(v1 < v2) && !(v1 > v2);
        }

        public static bool operator !=(Vector2 v1, Vector2 v2)
        {
            return !(v1==v2);
        }

        public static Vector2 operator *(double constant, Vector2 v1)
        {
            return new Vector2(v1.X * constant, v1.Y * constant);
        }

        public static Vector2 operator /(Vector2 v1, double constant)
        {
            return new Vector2(v1.X / constant, v1.Y / constant);
        }

        public double Length
        {
            get { return Math.Sqrt(x*x+y*y);}
        }

        public static Vector2 Right
        {
            get { return new Vector2(1, 0); }
        }
        public static Vector2 Up
        {
            get { return new Vector2(0, 1); }
        }

        public Vector2 Clone()
        {
            return new Vector2(this.x, this.y);
        }
        /// <summary>
        /// не скалярное умножение
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static Vector2 operator *(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X * v2.X, v1.Y * v2.Y);
        }

        /// <summary>
        /// Возвращает вектор единичной длины направленный в сторону оси под номером аргумента
        /// </summary>
        /// <param name="axisNumber">Номер оси, куда направлен вектор</param>
        /// <returns>Нормализованный вектор</returns>
        public static Vector2 getNormalizedVectorByAxisNumber(int axisNumber)
        {
            if (axisNumber == 1)
            {
                return Vector2.Right;
            }
            if (axisNumber == 2)
            {
                return Vector2.Up;
            }
            else
                throw new ArgumentException("Требуется получить ось, которую двумерный вектор напрямую представить не может, т.е. номер оси не верен.");
        }

        public override string ToString()
        {
            string outPut = string.Format("({0:f3};{1:f3})", x, y);
            return outPut;
        }
    }
}
