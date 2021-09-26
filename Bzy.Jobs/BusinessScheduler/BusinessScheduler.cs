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
        private static IScheduler _scheduler;
        public const string JOB_PRIFEX = "JOB_";
        public const string TRIGGER_PRIFEX = "TRG_";

        public static BusinessScheduler CreateInstance()
        {
            return new BusinessScheduler();
        }
        /*
        由7段构成：秒 分 时 日 月 星期 年（可选）
        
        "-" ：表示范围  MON-WED表示星期一到星期三
        "," ：表示列举 MON,WEB表示星期一和星期三
        "*" ：表是“每”，每月，每天，每周，每年等
        "/" :表示增量：0/15（处于分钟段里面） 每15分钟，在0分以后开始，3/20 每20分钟，从3分钟以后开始
        "?" :只能出现在日，星期段里面，表示不指定具体的值
        "L" :只能出现在日，星期段里面，是Last的缩写，一个月的最后一天，一个星期的最后一天（星期六）
        "W" :表示工作日，距离给定值最近的工作日
        "#" :表示一个月的第几个星期几，例如："6#3"表示每个月的第三个星期五（1=SUN...6=FRI,7=SAT）
        
        如果Minutes的数值是 '0/15' ，表示从0开始每15分钟执行
        
        如果Minutes的数值是 '3/20' ，表示从3开始每20分钟执行，也就是‘3/23/43’
        */

        /// <summary>
        /// 执行JOB
        /// </summary>
        public void RunBusinessJob()
        {
            ISchedulerFactory factory = new StdSchedulerFactory();
            //创建调度器
            _scheduler = factory.GetScheduler();

            #region 多项任务

            //多项任务
            //获取JOB集合
            var jobAndTriggerMapping = new Dictionary<IJobDetail, Quartz.Collection.ISet<ITrigger>>();

            //TimeJob1
            var timeJob1 = JobBuilder.Create<TimeJob>().WithIdentity(JOB_PRIFEX + "timeJob1", "group1").Build();
            if (_scheduler.CheckExists(timeJob1.Key))
            {
                _scheduler.DeleteJob(timeJob1.Key);
            }
            jobAndTriggerMapping[timeJob1] = new Quartz.Collection.HashSet<ITrigger>() {
            TriggerBuilder.Create()
                .WithIdentity(TRIGGER_PRIFEX + "timeJob1", "group1")
                .WithCronSchedule("0/5 * * * * ?")//从配置文件获取
                .Build()};

            //TimeJob2
            var timeJob2 = JobBuilder.Create<Time2Job>().WithIdentity(JOB_PRIFEX + "timeJob2", "group1").Build();
            if (_scheduler.CheckExists(timeJob2.Key))
            {
                _scheduler.DeleteJob(timeJob2.Key);
            }
            jobAndTriggerMapping[timeJob2] = new Quartz.Collection.HashSet<ITrigger>() {
            TriggerBuilder.Create()
                .WithIdentity(TRIGGER_PRIFEX + "timeJob2", "group1")
                .WithCronSchedule("0/7 * * * * ?")//从配置文件获取
                .Build()};

            _scheduler.ScheduleJobs(jobAndTriggerMapping, true);
            _scheduler.Start();

            #endregion

            #region 单项任务

            ////单项任务
            ////生成调度器
            //var timeScheduler = factory.GetScheduler();
            ////创建任务对象
            //IJobDetail timeJob = JobBuilder.Create<TimeJob>().WithIdentity("job1", "group1").Build();
            ////创建一个触发器
            //ITrigger timeJobtrigger = TriggerBuilder.Create()
            //    .WithIdentity("trigger1", "group1")
            //    .WithCronSchedule("0/5 * * * * ?")//从配置文件获取
            //    .Build();

            ////如果已存在相同任务删除
            //if (timeScheduler.CheckExists(timeJob.Key))
            //{
            //    timeScheduler.DeleteJob(timeJob.Key);
            //}
            ////将任务与触发器添加到调度器中运行
            //timeScheduler.ScheduleJob(timeJob, timeJobtrigger);
            //timeScheduler.Start();

            #endregion
        }

        public void StopScheduler()
        {
            _scheduler.Shutdown();
        }
    }
}
