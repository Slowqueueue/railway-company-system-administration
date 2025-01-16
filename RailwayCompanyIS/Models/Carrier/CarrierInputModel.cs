using RailwayCompanyIS.Models.City;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RailwayCompanyIS.Models.Carrier
{
    public class CarrierInputModel
    {
        [Required]
        public string Name { get; set; }
        public string CityName { get; set; }
        [Required]
        public string StreetName { get; set; }
        public int StreetNumber { get; set; }
        public int carid { get; set; }
        public IEnumerable<RailwayCompanyIS.Data.Models.City> Cities { get; set; }
    }
}
