using DronnI_Back.Models;
using DronnI_Back.Models.DbModels;
using DronnI_Back.Models.RequestModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DronnI_Back.Helpers;

namespace DronnI_Back.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class RentController : Controller
    {
        ApplicationContext appCtx;

        public RentController(ApplicationContext context)
        {
            appCtx = context;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {

            Rent sightseen = appCtx.Rents.FirstOrDefault(s => s.Id == id);
            if (sightseen != null)
            {
                return Json(sightseen);
            }
            return BadRequest(new { errorText = "Invalid rentId" });
        }

        [HttpGet("getRents")]
        public IActionResult GetRents()
        {
            List<Rent> result = appCtx.Rents.ToList();
            List<Rent> fin = new List<Rent>();
            for(int i = 0; i < result.Count; i++)
            {
                fin.Add(new Rent()
                {
                    Id = result[i].Id,
                    CustomerId = result[i].CustomerId,
                    OperatorId = result[i].OperatorId,
                    Customer = result[i].Customer,
                    DroneId = result[i].DroneId,
                    StatisticId = result[i].StatisticId,
                    Statistic = result[i].Statistic,
                    StartTime = result[i].StartTime,
                    EndTime = result[i].EndTime
                }); 
            }
            return Json(fin);
        }

        [HttpPost("startRent")]
        public IActionResult StartRent([FromBody] RentModel rentModel)//for people who haven't fly license
        {
            Rent rent = appCtx.Rents.FirstOrDefault(s => s.DroneId == rentModel.DroneId && s.EndTime == null);
            if (rent == null)
            {
                Drone drone = appCtx.Drones.FirstOrDefault(d => d.Id == rentModel.DroneId);
                User user = (User)HttpContext.Items["User"];
                if (drone != null)
                {
                    Rent primaryRent = new Rent
                    {
                        DroneId = rentModel.DroneId,
                        Drone = drone,
                        CustomerId = user.Id,
                        Customer = user,
                        OperatorId = 1,
                        Operator = appCtx.Users.FirstOrDefault(d => d.Id == 1),
                        StartTime = DateTime.Now
                    };
                    appCtx.Rents.Add(primaryRent);
                    appCtx.SaveChanges();
                    return Ok();
                }
            }
            return BadRequest();
        }

        [HttpPost("startProRent")]
        public IActionResult StartProRent([FromBody] RentModel rentModel)//for people who have fly license
        {
            Rent rent = appCtx.Rents.FirstOrDefault(s => s.DroneId == rentModel.DroneId && s.EndTime == null);
            if (rent == null)
            {
                Drone drone = appCtx.Drones.FirstOrDefault(d => d.Id == rentModel.DroneId);
                User user = (User)HttpContext.Items["User"];
                if (drone != null)
                {
                    Rent primaryRent = new Rent
                    {
                        DroneId = rentModel.DroneId,
                        Drone = drone,
                        CustomerId = user.Id,
                        Customer = user,
                        OperatorId = user.Id,
                        Operator = user,
                        StartTime = DateTime.Now
                    };
                    appCtx.Rents.Add(primaryRent);
                    appCtx.SaveChanges();
                    return Ok();
                }
            }
            return BadRequest();
        }

        [HttpPost("addStatistic")]
        public IActionResult addStatistic(int idRent, string info)
        {
            Rent rent = appCtx.Rents.FirstOrDefault(r => r.Id == idRent);
            if (rent != null)
            {
                Statistic statistic = new Statistic { Info = info};
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

        [HttpPost("closeOffer/{id}")]
        public IActionResult closeOffer(int idRent)
        {
            Rent rent = appCtx.Rents.FirstOrDefault(r => r.Id == idRent);
            if (rent != null)
            {
                appCtx.Rents.FirstOrDefault(r => r.Id == idRent).EndTime = DateTime.Now;
                appCtx.SaveChanges();
                return Ok();
            }

            return BadRequest(new { errorText = "Invalid OwnerId" });
        }
    }
}

