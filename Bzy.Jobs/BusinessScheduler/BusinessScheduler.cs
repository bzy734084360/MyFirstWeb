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
