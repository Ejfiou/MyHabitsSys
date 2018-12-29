using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace MyHabits.Common
{
    /// <summary>
    /// 日志类型
    /// </summary>
    public enum LogType
    {
        系统日志
    }
    public static class LogHelper
    {
        private static readonly ILog LogInfo;
        private static readonly ILog LogError;
        static LogHelper()
        {
            log4net.Config.XmlConfigurator.Configure();
            LogInfo = LogManager.GetLogger("loginfo");
            LogError = LogManager.GetLogger("logerror");
        }

        static object locker = new object();

        /// <summary>
        /// 写入正常日志
        /// </summary>
        /// <param name="info"></param>
        public static void Info(string info)
        {
            try
            {
                LogInfo.Info(info);
            }
            catch { }
        }

        /// <summary>
        /// 写入错误日志
        /// </summary>
        /// <param name="info"></param>
        /// <param name="ex"></param>
        public static void Error(string info, Exception ex)
        {
            try
            {
                LogError.Error(info, ex);
            }
            catch { }
        }
        /// <summary>
        /// 将日志信息写入文件
        /// </summary>
        /// <param name="log"></param>
        public static void WriteTxt(string log)
        {
            try
            {
                lock (locker)
                {
                    string fileName = AppDomain.CurrentDomain.BaseDirectory + "/systemLog.txt";
                    FileInfo file = new FileInfo(fileName);
                    StreamWriter writer = null;
                    writer = !file.Exists ? new StreamWriter(file.Create(), Encoding.UTF8) : new StreamWriter(fileName, true, Encoding.UTF8);
                    using (writer)
                    {
                        writer.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "：" + log);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
