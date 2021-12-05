using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DronnI_Back.Models.RequestModels
{
    public class RentModel
    {
        public int DroneId { get; set; }
        public int? OperatorId { get; set; }
    }
}
