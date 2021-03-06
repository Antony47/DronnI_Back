using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DronnI_Back.Models.DbModels
{
    public class Drone
    {
        public int Id { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public string Status { get; set; }
        public int OwnerId { get; set; }
        public User Owner { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
