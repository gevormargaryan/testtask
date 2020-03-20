using System.Configuration;
using System.Threading.Tasks;
using BeIT.MemCached;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Quartz;
using Quartz.Impl;
using TestApp.Models.Constants;
using TestApp.Models.MemCahedModels;
using TestApp.Services;
using TestApp.Services.Jobs;

namespace TestApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class EmployeeController : ControllerBase
    {
        private MemcachedService _memcachedService;

        public EmployeeController(MemcachedService memcachedService)
        {
            _memcachedService = memcachedService;            
        }

        [HttpGet]
        public Employee Get()
        {
            return _memcachedService.Get();
        }

        [HttpPost]
        public async Task<Employee> Set(Employee employee)
        {
            await _memcachedService.Set(employee);
            return employee;
        }
    }
}
