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

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {

            Statistic statistic = appCtx.Statistics.FirstOrDefault(s => s.Id == id);
            if (statistic != null)
            {
                return Json(statistic);
            }
            return BadRequest(new { errorText = "Invalid idStatistic." });
        }
    }
}
