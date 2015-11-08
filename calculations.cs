using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using LoggerEvsSpace;


namespace optimizationLab2
{
    public abstract class Task
    {
        public abstract double formula(double x1, double x2);
        public double formula(Vector2 vector)
        {
            return formula(vector.X, vector.Y);
        }
    }

    public class Task211:Task
    {
        public override double formula(double x1, double x2)
        {
            return Math.Pow(x1*x1+x2-11,2) + Math.Pow(x1+x2*x2-7,2);
        }
    }

    public class Task21:Task
    {
        public override double formula(double x1, double x2)
        {
            return 4 * Math.Pow(x1 - 5, 2) + Math.Pow(x2 - 6, 2);
        }
    }

    public class HDMethod
    {
        public static Vector2 calculate(double x1,double x2,double h1,double h2,double eps)
        {
            LoggerEvs.writeLog("Hook Jeeves method started!");
            int iterationsCount = 1;
            Vector2 b1 = new Vector2(x1, x2);
            Vector2 h = new Vector2(h1,h2);
            LoggerEvs.writeLog("Input Data: (x1;x2) = (" + x1 + ";" + x2 + "). (h1;h2) = (" + h1 + ";" + h2 + ").");
        step1:
            LoggerEvs.writeLog("Step 1.");
            Vector2 xk = b1.Clone();
            LoggerEvs.writeLog("Step 2.");
            Vector2 b2 = getBasisPoint(b1, h);
            LoggerEvs.writeLog("Results of step 1 and step 2. xk = " + xk + ", b2 = " + b2);
        step3:
            LoggerEvs.writeLog("Iteration number " + iterationsCount);
            LoggerEvs.writeLog("Step 3.");
            xk = b1 + 2*(b2-b1);
            LoggerEvs.writeLog("xk = "+xk);
            iterationsCount++;
            LoggerEvs.writeLog("Step 4.");
            Vector2 x = getBasisPoint(xk,h);
            LoggerEvs.writeLog("New x = "+x);
            b1 = b2.Clone();
            if(Lab.task.formula(x)<Lab.task.formula(b1))
            {
                LoggerEvs.writeLog("Step 6.");
                LoggerEvs.writeLog("f(x)<f(b1), f(x) = " + Lab.task.formula(x) + ", f(b1)" + Lab.task.formula(b1));
                b2 = x.Clone();
                LoggerEvs.writeLog("b2 = "+b2);
                goto step3;
            }
            if (Lab.task.formula(x) > Lab.task.formula(b1))
            {
                LoggerEvs.writeLog("Step 7.");
                LoggerEvs.writeLog("f(x)>f(b1), f(x) = " + Lab.task.formula(x) + ", f(b1)" + Lab.task.formula(b1));
                goto step1;
            }
            LoggerEvs.writeLog("Step 8.");
            if (h.Length<=eps)
            {
                LoggerEvs.writeLog("Step 10.");
                LoggerEvs.writeLog("Stopping criterion... The result is " + b1);
                return b1;
            }
            h /= 10;
            LoggerEvs.writeLog("Step 9. Now h = "+h);
            goto step1;
            throw new Exception("Что-то вообще не так");
        }

        public static Vector2 getBasisPoint(Vector2 b1,Vector2 h)
        {
            double fb = Lab.task.formula(b1.X, b1.Y);
            int i = 1;
            int n = 2;
            double f;
            while(i<=n)
            {
                f = Lab.task.formula(b1 + h * Vector2.getNormalizedVectorByAxisNumber(i));
                if(f<fb)
                {
                    b1 = b1 + h * Vector2.getNormalizedVectorByAxisNumber(i);
                    fb = f;
                }
                else if(f>fb)
                {
                    f = Lab.task.formula(b1 - h * Vector2.getNormalizedVectorByAxisNumber(i));
                    if(f<fb)
                    {
                        b1 = b1 - h * Vector2.getNormalizedVectorByAxisNumber(i);
                        fb = f;
                    }
                }
                i++;
            }
            return b1;
        }
    }
}
