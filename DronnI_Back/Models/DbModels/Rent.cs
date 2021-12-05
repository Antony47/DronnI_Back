using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DronnI_Back.Models.DbModels
{
    public class Rent
    {
        [Key]
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public User Customer { get; set; }
        public int? OperatorId { get; set; }
        public User Operator { get; set; }
        public int DroneId { get; set; }
        public Drone Drone { get; set; }
        public int? StatisticId { get; set; }
        public Statistic Statistic { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}
