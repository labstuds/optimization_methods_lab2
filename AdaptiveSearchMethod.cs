using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LoggerEvsSpace;
namespace optimizationLab2
{
    public static class AdaptiveSearchMethod
    {        
        public static Vector2 findMinimum(Vector2 x_0, double alpha, double beta, int t0, int M, double R, int N)
        {
            Vector2 answer = new Vector2();
            double n = 1;
            bool wasFound = false;
            Vector2[] yVectors = new Vector2[N + 1];
            Vector2[] xVectors = new Vector2[N + 1];
            Vector2[] rndKsiVectors = new Vector2[N + 1];
            double[] tValues = new double[N + 1];
            Vector2[] zVectors = new Vector2[N + 1];
            LoggerEvs.writeLog("Adaptive searching method started!");
            ////////////////////////////////////////////////
            // Шаг первый
            // Задать начальные условия
            string startConditions = string.Format("- Alpha\t = {0:f4}\r\n- Beta\t = {1:f4}\r\n- t0\t = {2}\r\n- M\t = {3}\r\n- R\t = {4:f5}\r\n- N\t = {5}\r\n- X0\t = {6}.", alpha, beta, t0, M, R, N, x_0.ToString());
            LoggerEvs.writeLog("Step 1: Setting start conditions (values)...\r\n" + startConditions);
            tValues[0] = t0;                        
            // Задать начальную точку            
            xVectors[0] = x_0;
            // Положить
            int k = 0;
            int j = 1;            
            
            while (!wasFound)
            {
                ////////////////////////////////////////////////
                // Шаг второй 
                // Получить случайный вектор 
                rndKsiVectors[j] = Vector2.getRandomVectorKsi();
                LoggerEvs.writeLog(string.Format("Step 2: Getting random ksi-vector: {0}.", rndKsiVectors[j].ToString()));

                ////////////////////////////////////////////////
                // Шаг третий
                // Вычислить y_j
                yVectors[j] = xVectors[k] + tValues[k] * (rndKsiVectors[j] / rndKsiVectors[j].getEuclidNorm());
                LoggerEvs.writeLog(string.Format("Step 3: Counting y_j = {0}.", yVectors[j].ToString()));
                ////////////////////////////////////////////////
                // Шаг четвертый                 
                double f_y_j = Lab.task.formula(yVectors[j]);
                double f_x_k = Lab.task.formula(xVectors[k]);
                LoggerEvs.writeLog(string.Format("Step 4: f_y_j = {0:f4}, f_x_k = {1:f4}", f_y_j, f_x_k));
                // а)
                if (f_y_j < f_x_k)
                {                    
                    zVectors[j] = xVectors[k] + alpha * (yVectors[j] - xVectors[k]);
                    LoggerEvs.writeLog(string.Format("f_y_j < f_x_k: {0:f4} < {1:f4}; z_j = {2}.", f_y_j, f_x_k, zVectors[j].ToString()));
                    // Определить, является ли текущее направление y_j - x_k удачным:
                    double f_z_j = Lab.task.formula(zVectors[j]);
                    if (f_z_j < f_x_k)
                    {                        
                        // Направление поиска удачное                        
                        xVectors[k + 1] = zVectors[j];                        
                        tValues[k + 1] = alpha * tValues[k];
                        k += 1;
                        LoggerEvs.writeLog(string.Format("f_z_j < f_x_k: {0:f4} < {1:f4}; x_k+1 = {2}, t_k = {3:f4}, k = k + 1 = {4}.", f_z_j, f_x_k, zVectors[j].ToString(), tValues[k], k));
                        // Проверить условие окончания поиска
                        if (k < N)
                        {
                            j = 1;
                            // Перейти к шагу 2
                            LoggerEvs.writeLog(string.Format("k < N: {0} < {1}; j = 1; Go to step 2!", k, N));
                        }
                        else if (k == N)
                        {
                            // Поиск завершен
                            answer = xVectors[k];
                            wasFound = true;
                            LoggerEvs.writeLog(string.Format("k == N: {0} == {1}; Stop searching! Answer x* is {2}", k, N, answer.ToString()));
                        }
                    }
                    else if (f_z_j >= f_x_k)
                    {
                        // Направление поиска неудачное, перейти к шагу 5
                        LoggerEvs.writeLog(string.Format("The searching way is unlucky: f_z_j >= f_x_k: {0:f4} >= {1:f4}. Go to step 5!", f_z_j, f_x_k));
                    }
                }
                // б)
                else if (f_y_j >= f_x_k)
                {
                    // Шаг недуачный перейти к шагу 5
                    LoggerEvs.writeLog(string.Format("The step is unlucky: f_y_j >= f_x_k: {0:f4} >= {1:f4} :go to step 5!", f_y_j, f_x_k));
                }

                ////////////////////////////////////////////////
                // Шаг пятый
                // Оценить число неудачных шагов из текущей точки
                // a)
                if (j < M)
                {
                    j += 1;                    
                    LoggerEvs.writeLog(string.Format("Step 5: j < M: {0} < {1}; j = j + 1 = {2}. Go to step 2!", j - 1, M, j));
                    // Перейти к шагу 2
                }
                // б)
                else if (j == M)
                {
                    // Проверить условия окончания
                    if (tValues[k] <= R)
                    {
                        // Процесс закончить:
                        answer = xVectors[k];
                        wasFound = true;
                        // Рассчитать значение функции                        
                        LoggerEvs.writeLog(string.Format("Step 5: Stop searching! Answer x* is {0}, f(x*) = {1:f4}", answer.ToString(), Lab.task.formula(answer)));
                    }
                    else if (tValues[k] > R)
                    {
                        tValues[k] = beta * tValues[k];
                        j = 1;
                        // Перейти к шагу 2
                        LoggerEvs.writeLog(string.Format("Step 5: t_k = beta * t_k = {0:f4}, j = {1}. Go to step 2!", tValues[k], j));
                    }
                }
            }
            return answer;
        }
    }
}
