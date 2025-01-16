using RailwayCompanyIS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RailwayCompanyIS.Models.Carrier
{
    public class CarrierVehicleInputModel
    {
        public int CarrierId { get; set; }
        public int RegistrationNumber { get; set; }
        public  VehicleType VehicleType { get; set; }
        public int Capacity { get; set; }
        public bool Tourist { get; set; }
        public IEnumerable<VehicleType> VehicleTypes { get; set; }
    }
}
