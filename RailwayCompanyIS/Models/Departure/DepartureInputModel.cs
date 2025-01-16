using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RailwayCompanyIS.Models.Departure
{
    public class DepartureInputModel
    {
        public string CityFrom { get; set; }
        public string CityTo { get; set; }
        public string Carrier { get; set; }
        public string PaymentCategory { get; set; }
        public int VehicleRegistration { get; set; }
        public DateTime DateOfDeparture { get; set; }
        public DateTime DateOfArrival { get; set; }
        public int depid { get; set; }

        public IEnumerable<RailwayCompanyIS.Data.Models.City> Cities { get; set; }
        public IEnumerable<RailwayCompanyIS.Data.Models.Carrier> Carriers { get; set; }
        public IEnumerable<RailwayCompanyIS.Data.Models.PaymentCategory> PaymentCategories { get; set; }
        public IEnumerable<RailwayCompanyIS.Data.Models.Vehicle> Vehicles { get; set; }
    }
}
