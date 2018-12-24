using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using MyPortal.Controllers.Api;
using MyPortal.Dtos;
using MyPortal.Models;
using NUnit.Framework;

namespace MyPortal.UnitTests
{
    [TestFixture]
    public class ApiLogsControllerTests
    {
        private LogsController _controller;
        private MyPortalDbContext _context;

        [OneTimeSetUp]
        public void TestFixtureSetup()
        {
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
        }

        [SetUp]
        public void Setup()
        {
            EffortProviderFactory.ResetDb();
            
            Mapper.Reset();
            _context = ContextControl.GetTestData();
            ContextControl.InitialiseMaps();

            _controller = new LogsController(_context);
        }
        
        [OneTimeTearDown]
        public void Clear()
        {
            _controller.Dispose();
            _context.Dispose();
        }

        [Test]
        public void GetLogs_GetsLogsForStudent()
        {
            var student = _context.Students.SingleOrDefault(x => x.FirstName == "John");
            
            Assert.IsNotNull(student);

            var result = _controller.GetLogs(student.Id);           
            
            Assert.AreEqual(4, result.Count());
            Assert.AreEqual(student.Id, result.First().StudentId);
        }

        [Test]
        public void GetLog_ReturnsLog()
        {
            var log = _context.Logs.SingleOrDefault(x => x.Message == "Test3");
            
            Assert.IsNotNull(log);

            var result = _controller.GetLog(log.Id);
            
            Assert.AreEqual(3, result.TypeId);
        }

        [Test]
        public void CreateLog_CreatesNewLog()
        {
            var init = _context.Logs.Count();
            
            var student = _context.Students.SingleOrDefault(x => x.FirstName == "Aaron");
            var initForStudent = _context.Logs.Count(x => x.StudentId == student.Id);
            
            Assert.IsNotNull(student);

            var newLog = new Log()
                {Date = DateTime.Now, Message = "CreateLog", TypeId = 1, AuthorId = 1, StudentId = student.Id};

            _controller.CreateLog(Mapper.Map<Log, LogDto>(newLog));

            var result = _context.Logs.Count();

            var result2 = _context.Logs.Count(x => x.StudentId == student.Id);
            
            Assert.AreEqual(init + 1, result);
            Assert.AreEqual(initForStudent + 1, result2);
        }

        [Test]
        public void UpdateLog_UpdatesLog()
        {
            var log = _context.Logs.SingleOrDefault(x => x.Message == "Test3");
            
            Assert.IsNotNull(log);

            log.Message = "Hello World";

            _controller.UpdateLog(Mapper.Map<Log, LogDto>(log));

            var result = _context.Logs.SingleOrDefault(x => x.Id == log.Id);
            
            Assert.IsNotNull(result);
            
            Assert.AreEqual("Hello World", result.Message);
        }

        [Test]
        public void DeleteLog_DeletesLog()
        {
            var init = _context.Logs.Count();

            var log = _context.Logs.SingleOrDefault(x => x.Message == "Test2");
            
            Assert.IsNotNull(log);

            _controller.DeleteLog(log.Id);

            var result = _context.Logs.Count();
            
            Assert.AreEqual(init - 1, result);
        }
    }
}