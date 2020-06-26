using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Krohonde
{
    public class Logger
    {
        public enum LogLevel {
            INFO, DEBUG, WARNING, ERROR
        }
        public static void WriteLogFile(LogLevel level, String logDirectoryPath, String logFileName, String logMessage)
        {
            StreamWriter strWriter = null;

            if (!Directory.Exists(logDirectoryPath))
            {
                Directory.CreateDirectory(logDirectoryPath);
            }

            try
            {
                strWriter = File.AppendText(logDirectoryPath + @".\" + logFileName);
                strWriter.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "\t" + level + "\t" + logMessage);
            }
            finally
            {
                strWriter.Close();
            }
            
            
        }
    }
}




