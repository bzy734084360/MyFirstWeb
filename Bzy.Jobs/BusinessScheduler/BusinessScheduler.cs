using Bzy.Jobs.BusinessJobs;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bzy.Jobs.BusinessScheduler
{
    /// <summary>
    /// 业务调度器
    /// </summary>
    public class BusinessScheduler
    {
        /// <summary>
        /// 执行JOB
        /// </summary>
        public static void RunBusinessJob()
        {
            //TimeJob
            ISchedulerFactory factory = new StdSchedulerFactory();
            //生成调度器
            var timeScheduler = factory.GetScheduler().Result;
            //创建任务对象
            IJobDetail timeJob = JobBuilder.Create<TimeJob>().WithIdentity("job1", "group1").Build();
            //创建一个触发器
            ITrigger timeJobtrigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .WithCronSchedule("0/5 * * * * ?")//从配置文件获取
                .Build();

            //如果已存在相同任务删除
            if (timeScheduler.CheckExists(timeJob.Key).Result)
            {
                timeScheduler.DeleteJob(timeJob.Key);
            }
            //将任务与触发器添加到调度器中运行
            timeScheduler.ScheduleJob(timeJob, timeJobtrigger);
            timeScheduler.Start();
        }
    }
}
