using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;

namespace Krohonde
{
    public class Logger
    {
        
        public static void WriteLogFile(String logDirectoryPath, String logFileName, String logMessage)
        {
            StreamWriter strWriter = null;

            if (!Directory.Exists(logDirectoryPath))
            {
                Directory.CreateDirectory(logDirectoryPath);
            }

            try
            {
                strWriter = File.AppendText(logDirectoryPath + @".\" + logFileName);
                strWriter.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "\t" + logMessage);
            }
            finally
            {
                strWriter.Close();
            }
        }

        public static void WriteLogFile(String logMessage)
        {
            string logDirectoryPath = ConfigurationManager.AppSettings["directory"];
            string logFileName = ConfigurationManager.AppSettings["file"];

            WriteLogFile(logDirectoryPath, logFileName, logMessage);
        }

        

    }
}




