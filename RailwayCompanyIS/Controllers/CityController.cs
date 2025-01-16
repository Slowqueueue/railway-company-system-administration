using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using RailwayCompanyIS.Data.Models;
using RailwayCompanyIS.Data.ServiceSpecification;
using RailwayCompanyIS.Models;
using RailwayCompanyIS.Models.City;
using Microsoft.AspNetCore.Mvc;

namespace RailwayCompanyIS.Controllers
{
    public class CityController : Controller
    {
        private readonly ICity _cityService;
        private readonly ICarrier _carrierService;

        public CityController(ICity cityService,ICarrier carrierService)
        {
            _cityService = cityService;
            _carrierService = carrierService;
        }
        public IActionResult Index()
        {
            var citys = _cityService.GetAll();

            var cityListing = citys.Select(c => new CityDetailModel
            {
                Id = c.Id,
                Name = c.Name,
                ImageUrl = c.ImageUrl,
                ZipCode = c.ZipCode,
                Description = c.Description,
                Carriers = _carrierService.GetByCity(c)
            });

            var model = new CityIndexModel
            {
                Cities = cityListing
            };

            return View(model);
        }

        public IActionResult Detail(int id)
        {
            var city = _cityService.GetById(id);

            var model = new CityDetailModel
            {
                Id = city.Id,
                Name = city.Name,
                Carriers = _carrierService.GetByCity(city),
                ImageUrl = city.ImageUrl,
                ZipCode = city.ZipCode,
                Description = city.Description
            };
            return View(model);
        }
    }
}