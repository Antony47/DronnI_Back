using DronnI_Back.Models;
using DronnI_Back.Models.DbModels;
using DronnI_Back.Models.RequestModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TokyoBike.Helpers;

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
        [HttpGet]
        public IActionResult Get()
        {
            List<Rent> ss = appCtx.Rents.ToList();
            return Json(ss);
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

        [HttpPost("startRent")]
        public IActionResult StartRent([FromBody] RentModel rentModel)//for people who haven't fly license
        {
            Rent rent = appCtx.Rents.FirstOrDefault(s => s.DroneId == rentModel.DroneId && s.EndTime == null);
            if (rent == null)
            {
                Drone drone = appCtx.Drones.FirstOrDefault(d => d.Id == rentModel.DroneId);
                User user = (User)HttpContext.Items["User"];//??
                if (drone != null)
                {
                    Rent primaryRent = new Rent
                    {
                        DroneId = rentModel.DroneId,
                        Drone = drone,
                        CustomerId = user.Id,
                        Customer = user,
                        OperatorId = 1,
                        StartTime = DateTime.Now
                    };
                    appCtx.Rents.Add(primaryRent);
                    appCtx.SaveChanges();
                    return Ok();
                }
            }
            return BadRequest();
        }

    }
}
