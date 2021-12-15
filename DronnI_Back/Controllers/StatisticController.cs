using DronnI_Back.Models;
using DronnI_Back.Models.DbModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DronnI_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticController : Controller
    {
        ApplicationContext appCtx;

        public StatisticController(ApplicationContext context)
        {
            appCtx = context;
        }
        [HttpGet]
        public IActionResult Get()
        {
            List<Statistic> ss = appCtx.Statistics.ToList();
            return Json(ss);
        }

        [HttpGet("getStatistics")]
        public IActionResult GetStatistics()
        {
            IEnumerable<Statistic> result = appCtx.Statistics.ToList();
            return Json(result);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Statistic statistic = appCtx.Statistics.FirstOrDefault(s => s.Id == id);
            if (statistic != null)
            {
                return Json(statistic);
            }
            return BadRequest(new { errorText = "Invalid idStatistic" });
        }
        
        [HttpDelete("deleteStatistic/{id}")]
        public IActionResult deleteStatistic(int id)
        {
            Statistic statistic = appCtx.Statistics.FirstOrDefault(s => s.Id == id);
            if (statistic != null)
            {
                if (appCtx.Rents.FirstOrDefault(u => u.Statistic == statistic) != null)
                {
                    appCtx.Rents.FirstOrDefault(u => u.Statistic == statistic).Statistic = null;
                    appCtx.Rents.FirstOrDefault(u => u.Statistic == statistic).StatisticId = null;
                }
                
                appCtx.Statistics.Remove(statistic);
                
                appCtx.SaveChanges();
                return Ok();
            }
            return BadRequest(new { errorText = "Invalid idStatistic" });
        }

        [HttpPost("addStatistic")]
        public IActionResult addStatistic(int idRent, string info)
        {
            Rent rent = appCtx.Rents.FirstOrDefault(r => r.Id == idRent);
            if (rent != null)
            {
                Statistic statistic = new Statistic { Info = info };
                appCtx.Statistics.Add(statistic);
                appCtx.SaveChanges();
                appCtx.Rents.FirstOrDefault(r => r.Id == idRent).Statistic = statistic;
                appCtx.Rents.FirstOrDefault(r => r.Id == idRent).StatisticId = statistic.Id;
                appCtx.Rents.FirstOrDefault(r => r.Id == idRent).EndTime = DateTime.Now;
                appCtx.SaveChanges();
                return Ok();
            }

            return BadRequest(new { errorText = "Invalid OwnerId" });
        }

    }
}
