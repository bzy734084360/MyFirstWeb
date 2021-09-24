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
        public Task Execute(IJobExecutionContext context)
        {
            // 在此处插入电子邮件服务可发送电子邮件。
            FileStream fs = new FileStream(@"D:\yf\myweb\TestJobLog\testjob.txt", FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            sw.Close();
            fs.Close();
            //写个日志
            return Task.FromResult<object>(null);
        }
    }
}
