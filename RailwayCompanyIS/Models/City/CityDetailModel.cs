using RailwayCompanyIS.Controllers.FrontHelpers;
using RailwayCompanyIS.Data.Models;
using RailwayCompanyIS.Models.City;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RailwayCompanyIS.Models
{
    public class CityDetailModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ZipCode { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public IEnumerable<RailwayCompanyIS.Data.Models.Carrier> Carriers { get; set; }
        public string Helper { get; set; }
    }
}
