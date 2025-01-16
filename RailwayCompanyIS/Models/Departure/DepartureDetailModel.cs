using RailwayCompanyIS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace RailwayCompanyIS.Models.Departure
{
    public class DepartureDetailModel
    {
        public int Id { get; set; }
        public RailwayCompanyIS.Data.Models.City CityFrom { get; set; }
        public RailwayCompanyIS.Data.Models.City CityTo { get; set; }
        public RailwayCompanyIS.Data.Models.Carrier  Carrier { get; set; }
        public PaymentCategory PaymentCategory { get; set; }
        public Vehicle Vehicle { get; set; }
        public Distance Distance { get; set; }
        public int NumberOfSeats { get; set; }
        public DateTime DateOfDeparture { get; set; }
        public DateTime DateOfArrival { get; set; }
        public double PriceOfCard { get; set; }
    }
}
