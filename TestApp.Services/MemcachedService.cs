using Microsoft.Extensions.Configuration;
using Quartz;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using TestApp.Models.Constants;
using TestApp.Models.MemCahedModels;
using TestApp.Services.Jobs;
using BeIT.MemCached;

namespace TestApp.Services
{
    public class MemcachedService
    {
        private readonly SchedulerService _schedulerService;
        private readonly MemcachedClient _memcachedClient;
        private readonly IConfiguration _config;

        public MemcachedService(IConfiguration config, SchedulerService schedulerService)
        {
            _schedulerService = schedulerService;
            _config = config;

            try
            {
                MemcachedClient.Setup(_config["memcachedservername"], _config["memcachedservers"].Split(','));
                
            }
            catch (ConfigurationErrorsException e)
            {
            }

            _memcachedClient = MemcachedClient.GetInstance(_config["memcachedservername"]);

        }

        public async Task Set(Employee employee)
        {
            JobDataMap data = new JobDataMap();
            data["memcached"] = _memcachedClient;
            data["object"] = employee;
            await _schedulerService.ScheduleTaskAsync<SetMemcachedJob>(JobNames.SetMemcachedJob, data);
        }

        public Employee Get()
        {
            return (Employee)_memcachedClient.Get(MemcachedKeys.EmployeeKey);
        }
    }
}
