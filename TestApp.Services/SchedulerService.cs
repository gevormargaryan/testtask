using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestApp.Models.Constants;

namespace TestApp.Services.Jobs
{
    public class SchedulerService
    {
         private IScheduler _scheduler;
         private readonly TimeSpan _defaultInterval = TimeSpan.FromSeconds(60);

         public SchedulerService() { }

         public async void Start()
         {
            _scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await _scheduler.Start();
         }

         public async Task ScheduleTaskAsync<T>(string name, JobDataMap jobDataMap) where T : IJob
         {
             var job = JobBuilder.Create<T>()
                 .WithIdentity(name)
                 .Build();

            foreach( var jobData in jobDataMap)
            {
                job.JobDataMap[jobData.Key] = jobData.Value;
            }

             var trigger = TriggerBuilder.Create()
                 .WithIdentity(name)
                 .StartNow()
                 .Build();
            
             await _scheduler.ScheduleJob(job, trigger);
        }

        public void Shutdown()
         {
             _scheduler.Shutdown();
         }
    }
}
