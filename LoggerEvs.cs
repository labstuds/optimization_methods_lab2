using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace LoggerEvsSpace
{    
    public static class LoggerEvs
    {
        public delegate void LoggerMessageCameHandler(string message);
        public static event LoggerMessageCameHandler messageCame;   // Событие поступления сообщения
        private static string logsFileName = "";        // Имя файла логов
        private static object syncObj = new object();   // Объект для синхронизации
        private static StreamWriter swLogs = null;
        /// <summary>
        /// Метод добавления записи в лог
        /// </summary>
        /// <param name="logMessage">Добавляемая запись в лог</param>
        public static void writeLog(string logMessage)
        {
            try
            {
                // Путь
                string pathToLogs = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
                // Создать директорию логов, если ее еще нет
                if (!Directory.Exists(pathToLogs))
                    Directory.CreateDirectory(pathToLogs);
                // Если файл еще не был создан
                if (logsFileName == "")
                    logsFileName = Path.Combine(pathToLogs, string.Format("Logs_{0:dd.MM.yyyy HH_mm_ss}.log", DateTime.Now));
                // Текст логов
                string logsText = string.Format("[{0:dd.MM.yyyy HH:mm:ss}] {1}\r\n", DateTime.Now, logMessage);
                // Вызвать событие
                messageCame(logsText);
                // Записать логи
                lock (syncObj) // Заблокировать доступ к этому участку кода
                {
                    if (swLogs == null)
                    {
                        swLogs = new StreamWriter(logsFileName, true, Encoding.GetEncoding("Windows-1251"));
                        swLogs.AutoFlush = true;
                    }
                    swLogs.Write(logsText);                    
                }
            }
            catch
            {
                // Запись в лог не была удачной, ничего не делать.
            }
        }
    }
}
