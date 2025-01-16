using RailwayCompanyIS.Models.CarrierViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RailwayCompanyIS.Models.CarrierViewModel
{
    public class CarrierIndexModel
    {
        public IEnumerable<CarrierDetailModel> Carriers { get; set; }
    }
}
