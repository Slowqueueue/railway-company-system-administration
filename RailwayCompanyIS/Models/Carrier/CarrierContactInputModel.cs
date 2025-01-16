using RailwayCompanyIS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RailwayCompanyIS.Models.Carrier
{
    public class CarrierContactInputModel
    {
        public int CarrierId { get; set; }
        public ContactType Type { get; set; }
        [Required]
        public string ContactContent { get; set; }
        public IEnumerable<ContactType> ContactTypes { get; set; }

    }
}
