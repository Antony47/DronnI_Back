using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DronnI_Back.Models.DbModels
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public List<Rent> CustomerRent { get; set; }
        public List<Rent> OperatorRent { get; set; }
        public List<Drone> Drones { get; set; }
    }
}
