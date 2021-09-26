using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bzy.Jobs.BusinessJobs
{
    /// <summary>
    /// 测试定时任务
    /// </summary>
    public class TimeJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            if (!File.Exists(@"D:\yf\myweb\TestJobLog\testjob.txt"))
            {
                FileStream fs = new FileStream(@"D:\yf\myweb\TestJobLog\testjob.txt", FileMode.OpenOrCreate);
                fs.Close();
            }
            File.AppendAllText(@"D:\yf\myweb\TestJobLog\testjob.txt", "我是任务1~~" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + Environment.NewLine);
            //写个日志
            return;
        }
    }
    /// <summary>
    /// 测试定时任务
    /// </summary>
    public class Time2Job : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            if (!File.Exists(@"D:\yf\myweb\TestJobLog\testjob.txt"))
            {
                FileStream fs = new FileStream(@"D:\yf\myweb\TestJobLog\testjob.txt", FileMode.OpenOrCreate);
                fs.Close();
            }
            File.AppendAllText(@"D:\yf\myweb\TestJobLog\testjob.txt", "我是任务2~~" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + Environment.NewLine);
            //写个日志
            return;
        }
    }
}
