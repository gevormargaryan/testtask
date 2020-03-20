using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.Configuration;
using System.Threading;
using TestApp.Models.MemCahedModels;
using TestApp.Controllers;
using TestApp.Services;
using TestApp.Services.Jobs;
using System.Threading.Tasks;
using Bogus;

namespace TestApp.Tests
{
    public class Tests
    {
        private MemcachedService _memcached;
        private IConfiguration _config;
        private SchedulerService _schedulerService;

        [SetUp]
        public void Setup()
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.Development.json");
            // configurationBuilder.AddXmlFile("BeITMemcached.exe.config");
            _config = configurationBuilder.Build();

            _schedulerService = new SchedulerService();
            _schedulerService.Start();
            _memcached = new MemcachedService(_config, _schedulerService);
        }

        [Test]
        public async Task GetEmployee()
        {
            var controller = new EmployeeController(_memcached);

            var employeeFaker = new Faker<Employee>()
                .RuleFor(u => u.FullName, (f, u) => ((Faker)f).Name.FullName())
                .RuleFor(u => u.Role, (f, u) => ((Faker)f).Name.JobDescriptor());
            var employee = employeeFaker.Generate();

            Employee setResult = await controller.Set(employee);

            Thread.Sleep(5000);
            var getResult = controller.Get();
            Assert.AreEqual(setResult, getResult);
        }

        [Test]
        public async System.Threading.Tasks.Task SetEmployeeAsync()
        {
            var controller = new EmployeeController(_memcached);

            var employeeFaker = new Faker<Employee>()
                .RuleFor(u => u.FullName, (f, u) => ((Faker)f).Name.FullName())
                .RuleFor(u => u.Role, (f, u) => ((Faker)f).Name.JobDescriptor());
            var employee = employeeFaker.Generate();

            var result = await controller.Set(employee);
            Assert.AreEqual(employee, result);
        }
    }
}