using System;
using System.IO;
using System.Windows;

namespace BasicCRUD
{
    static class ExceptionLogger
    {
        public static void LogException(FileInfo loggingFile, bool message, Exception exceptionToLog)
        {
            try
            {
                if(loggingFile.Exists)
                {
                    using (StreamWriter streamWriter = new StreamWriter(loggingFile.OpenWrite()))
                    {
                        streamWriter.Write(exceptionToLog.ToString());
                    }
                }
                else
                {
                    loggingFile.CreateText();
                    using (StreamWriter streamWriter = new StreamWriter(loggingFile.OpenWrite()))
                    {
                        streamWriter.Write(exceptionToLog.ToString());
                    }
                }
                if(message)
                {
                    MessageBox.Show("An error has occured:" + exceptionToLog.ToString());
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Critical error. Cannot log error." + ex.ToString());
            }
        }
    }
}
