using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BillingSystem.Utility
{
    public class Logging
    {
        private static string tracingTraceDirectory = @"D:\\BillingSystem\Logs";
       private static string tracingLogFile = "OutPutFile.txt";
       private static string tracingExceptionFile = "ExceptionFile.txt";
      
        public static String TracingLogFile
       {
           get
           {
               return Path.GetFullPath(tracingTraceDirectory)+"\\"+Path.GetFileNameWithoutExtension(tracingLogFile) + "_" + DateTime.Today.ToString("dd-MMM-yyyy") + Path.GetExtension(tracingLogFile);
           }
       }

       public static String TracingExceptionFile
       {
           get
           {
               if(!Directory.Exists(tracingTraceDirectory))
               {
                   Directory.CreateDirectory(tracingTraceDirectory);
               }
               return Path.GetFullPath(tracingTraceDirectory) + "\\" + Path.GetFileNameWithoutExtension(tracingExceptionFile) + "_" +DateTime.Today.ToString("dd-MMM-yyyy") + Path.GetExtension(tracingExceptionFile);
           }
       }

       #region WriteLog

       public static void WriteLog(string message)
       {
                string path = TracingLogFile;
                message = FormatMessage(message);
                if (!File.Exists(path))
                {
                    string format = "dd-MMM-yyyy";

                    string Date="Date          : " + DateTime.Now.ToString();
                     using(File.Create(path));
                    using (TextWriter tw = new StreamWriter(path,true))
                    {
                        tw.WriteLine(Date);
                        tw.WriteLine(message);
                        tw.Close();
                        tw.Dispose();
                    } 
                    
                }
                else if (File.Exists(path))
                {
                  using(TextWriter tw = new StreamWriter(path,true))
                  { 
                    tw.WriteLine(message);
                    tw.Close();
                    tw.Dispose();
                  }
                }
       }
       #endregion

       #region WriteException
       public static void WriteException(Exception ex)
       {
           string path = TracingExceptionFile;
           string message = FormatException(ex);
           if (!File.Exists(path))
           {
               using (File.Create(path)) ;
              using (TextWriter tw = new StreamWriter(path))
              { 
               tw.WriteLine(message);
               tw.Close();
               tw.Dispose();
               }
           }
           else if (File.Exists(path))
           {
               using (TextWriter tw = new StreamWriter(path, true))
               { 
               tw.WriteLine(message);
               tw.Close();
               tw.Dispose();
               }
           }
       }
       #endregion

       #region FormatException
       public static String FormatException(Exception exception)
       {
           StringBuilder strBuilder = new StringBuilder();

           
           string format = "dd-MMM-yyyy";

           strBuilder.Append("Source        : " + exception.Source.ToString().Trim()).Append("\r\n");
           strBuilder.Append("Method        : " + exception.TargetSite.Name.ToString()).Append("\r\n");
           strBuilder.Append("Date          : " + DateTime.Now.ToString(format, DateTimeFormatInfo.InvariantInfo));
           strBuilder.Append("\r\n");
           strBuilder.Append("User          : " + System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString()).Append("\r\n");
           strBuilder.Append("Time          : " + DateTime.Now.ToShortTimeString()).Append("\r\n");
           strBuilder.Append("Computer      : " + Dns.GetHostName().ToString()).Append("\r\n");
           strBuilder.Append("Error         : " + exception.Message.ToString().Trim().Replace("\r\n","-")).Append("\r\n");
           strBuilder.Append("Stack Trace   : " + exception.StackTrace.ToString().Trim()).Append("\r\n");
           strBuilder.Append("^^-------------------------------------------------------------------^^").Append("\r\n");

           return strBuilder.ToString();
       }

       #endregion

       #region FormatMessage

       public static string FormatMessage(string message)
       {
           StringBuilder strBuilder = new StringBuilder();
          
           //strBuilder.Append("\r\n");
           strBuilder.Append(message+ "\r\n");
           //strBuilder.Append("^^-------------------------------------------------------------------^^").Append("\r\n");

           return strBuilder.ToString();
       }
       #endregion
   
    }
}
