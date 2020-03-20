using BeIT.MemCached;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestApp.Models.MemCahedModels;
using TestApp.Models.Constants;

namespace TestApp.Services.Jobs
{
    public class SetMemcachedJob : IJob
    {
        private readonly ILogger<SetMemcachedJob> _logger;
        public SetMemcachedJob()
        {
            _logger = new LoggerFactory().CreateLogger<SetMemcachedJob>();
        }

        public Task Execute(IJobExecutionContext context)
        {
            MemcachedClient memcachedClient = null;
            Employee employee = null;
            try
            {
                JobDataMap dataMap = context.JobDetail.JobDataMap;
                memcachedClient = (MemcachedClient)dataMap.Get("memcached");
                employee = (Employee)dataMap.Get("object");
                memcachedClient.Set(MemcachedKeys.EmployeeKey, employee);

                return Task.CompletedTask;
            }
            catch(Exception e)
            {
                // Here we can broadcast event, so user will get notification that save object failed
                _logger.Log(LogLevel.Error, e.Message, employee);
            }

            return null;
        }
    }
}
