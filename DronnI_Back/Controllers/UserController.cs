﻿using DronnI_Back.Models;
using DronnI_Back.Models.DbModels;
using DronnI_Back.Models.RequestModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DronnI_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        ApplicationContext appCtx;
        public UserController(ApplicationContext ctx)
        {
            appCtx = ctx;
        }

        [HttpPost("addDrone")]
        public IActionResult AddDrone([FromBody] DroneModel droneModel)
        {
            User user = appCtx.Users.FirstOrDefault(u => u.Id == droneModel.OwnerId);
            if (user != null)
            {
                Drone drone = new Drone { OwnerId = droneModel.OwnerId, Owner = user };
                appCtx.Drones.Add(drone);
                appCtx.Users.FirstOrDefault(u => u.Id == droneModel.OwnerId).Drones.Add(drone);///???
                appCtx.SaveChanges();
                return Ok();
            }

            return BadRequest(new { errorText = "Invalid OwnerId" });
        }

        [HttpDelete("deleteDrone/{id}")] ///need to add condition
        public IActionResult DeleteDrone(int id)///???????????
        {
            Drone drone = appCtx.Drones.FirstOrDefault(d => d.Id == id);
            if (drone != null)
            {
                appCtx.Drones.Remove(drone);
                //appCtx.Users.FirstOrDefault(u => u.Drones.FirstOrDefault(d => d.Id == id) == droneModel.OwnerId).Drones.
                appCtx.SaveChanges();
                return Ok();
            }

            return BadRequest(new { errorText = "Invalid DroneId" });
        }
    }
}