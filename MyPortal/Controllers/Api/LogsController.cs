﻿using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using MyPortal.Dtos;
using MyPortal.Models;

namespace MyPortal.Controllers.Api
{
    public class LogsController : ApiController
    {
        private readonly MyPortalDbContext _context;

        public LogsController()
        {
            _context = new MyPortalDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [HttpGet]
        [Route("api/logs/{student}")]
        public IEnumerable<LogDto> GetLogs(int student)
        {
            return _context.Logs.Where(l => l.Student == student)
                .OrderByDescending(x => x.Date)
                .ToList()
                .Select(Mapper.Map<Log, LogDto>);
        }

        [HttpGet]
        [Route("api/logs/log/{id}")]
        public LogDto GetLog(int id)
        {
            var log = _context.Logs.SingleOrDefault(l => l.Id == id);

            if (log == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return Mapper.Map<Log, LogDto>(log);
        }

        [HttpPost]
        [Route("api/logs/new")]
        public IHttpActionResult CreateLog(LogDto data)
        {
            var log = Mapper.Map<LogDto, Log>(data);
            _context.Logs.Add(log);
            _context.SaveChanges();

            return Ok("Log created");
        }

        [Route("api/logs/log/edit")]
        [HttpPost]
        public IHttpActionResult UpdateLog(LogDto logDto)
        {
            if (logDto == null)
                return Content(HttpStatusCode.BadRequest, "No valid data was received");

            var logInDb = _context.Logs.SingleOrDefault(l => l.Id == logDto.Id);

            if (logInDb == null)
                return Content(HttpStatusCode.NotFound, "Log not found");

            //var c = Mapper.Map(logDto, logInDb);

            logInDb.Type = logDto.Type;
            logInDb.Message = logDto.Message;

            _context.SaveChanges();

            return Ok("Log updated");
        }

        [Route("api/logs/log/{id}")]
        public IHttpActionResult DeleteLog(int id)
        {
            var logInDb = _context.Logs.SingleOrDefault(l => l.Id == id);

            if (logInDb == null)
                return Content(HttpStatusCode.NotFound, "Log does not exist");

            _context.Logs.Remove(logInDb);
            _context.SaveChanges();

            return Ok("Log deleted");
        }
    }
}