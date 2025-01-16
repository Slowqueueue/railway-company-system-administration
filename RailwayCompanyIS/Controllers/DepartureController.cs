using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RailwayCompanyIS.Data.Models;
using RailwayCompanyIS.Data.ServiceSpecification;
using RailwayCompanyIS.Models.Carrier;
using RailwayCompanyIS.Models.Departure;
using RailwayCompanyIS.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace RailwayCompanyIS.Controllers
{
    public class DepartureController : Controller
    {
        private readonly IDeparture _deparatureService;
        private readonly ICity _cityService;
        private readonly ICarrier _carrierService;
        private readonly IPaymentCategory _paymentCategoryService;
        private readonly IVehicle _vehicleService;
        private readonly IDistance _distanceService;
        private readonly IPriceManager _priceService;
        private readonly IDateOfArrival _dateOfArrivalService;

        public DepartureController(IDeparture departureService,ICity cityService,ICarrier carrierService,IPaymentCategory paymentCategoryService,IVehicle vehicleService,IDistance distanceService,IPriceManager priceManager, IDateOfArrival dateOfArrivalService)
        {
            _deparatureService = departureService;
            _cityService = cityService;
            _carrierService = carrierService;
            _paymentCategoryService = paymentCategoryService;
            _vehicleService = vehicleService;
            _distanceService = distanceService;
            _priceService = priceManager;
            _dateOfArrivalService = dateOfArrivalService;
        }

        public IActionResult Index()
        {
            var departures = _deparatureService.GetAll();

            var departuresListing = departures.Select(d => new DepartureDetailModel
            {
                Id = d.Id,
                NumberOfSeats = d.NumberOfSeats,
                Carrier = d.Carrier,
                CityFrom = d.CityFrom,
                CityTo = d.CityTo,
                DateOfDeparture = d.DateOfDeparture,
                DateOfArrival = d.DateOfArrival,
                Distance = d.Distance,
                PaymentCategory = d.PaymentCategory,
                PriceOfCard = d.PriceOfCard,
                Vehicle = d.Vehicle
            });

            var model = new DepartureIndexModel
            {
                Departures = departuresListing
            };


            return View(model);
        }


        public IActionResult Detail(int id)
        {
            var departure = _deparatureService.GetById(id);

            var model = new DepartureDetailModel
            {
                Id = departure.Id,
                NumberOfSeats = departure.NumberOfSeats,
                CityFrom = departure.CityFrom,
                CityTo = departure.CityTo,
                DateOfDeparture = departure.DateOfDeparture,
                DateOfArrival = departure.DateOfArrival,
                Carrier = departure.Carrier,
                Distance = departure.Distance,
                PaymentCategory = departure.PaymentCategory,
                PriceOfCard = departure.PriceOfCard,
                Vehicle = departure.Vehicle
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Create([Bind]DepartureInputModel departureInput)
        {
            int DepartureForUpdtId;
            Departure newDeparture, DepartureForUpdt;
            if (departureInput.depid != 0)
            {
                DepartureForUpdtId = departureInput.depid;
                DepartureForUpdt = _deparatureService.GetById(DepartureForUpdtId);
                DepartureForUpdt.CityFrom = _cityService.GetByName(departureInput.CityFrom);
                DepartureForUpdt.CityTo = _cityService.GetByName(departureInput.CityTo);
                DepartureForUpdt.DateOfDeparture = departureInput.DateOfDeparture;
                DepartureForUpdt.Carrier = _carrierService.GetByName(departureInput.Carrier);
                DepartureForUpdt.PaymentCategory = _paymentCategoryService.GetByName(departureInput.PaymentCategory);
                DepartureForUpdt.Vehicle = _vehicleService.GetByRegistration(departureInput.VehicleRegistration);

                if (_cityService.IsSameCity(DepartureForUpdt.CityFrom, DepartureForUpdt.CityTo))
                {
                    TempData["msg"] = "Рейс в один и тот же город невозможен!";
                    return Redirect("/Departure/Edit/" + departureInput.depid);
                }

                if (departureInput.VehicleRegistration == 0)
                {
                    TempData["msg"] = "Не введен регистрационный номер!";
                    return Create();
                }

                if (DepartureForUpdt.Vehicle == null)
                {
                    TempData["msg"] = "Не выбран номер поезда!";
                    return Create();
                }

                try
                {
                    DepartureForUpdt.Distance = _distanceService.CalculateDistance(DepartureForUpdt.CityFrom, DepartureForUpdt.CityTo);
                }
                catch (Exception)
                {
                    TempData["msg"] = "Не удалось вычислить расстояние между городами!";
                    return Redirect("/Departure/Edit/" + departureInput.depid);
                }

                try
                {
                    DepartureForUpdt.PriceOfCard = _priceService.CalculatePrice(DepartureForUpdt.Distance.DistanceBetween, DepartureForUpdt.PaymentCategory.Price);
                }
                catch (Exception)
                {
                    TempData["msg"] = "Не удалось вычислить цену билета!";
                    return Redirect("/Departure/Edit/" + departureInput.depid);
                }

                try
                {
                    DepartureForUpdt.DateOfArrival = _dateOfArrivalService.CalculateDate(DepartureForUpdt.Distance.DistanceBetween, DepartureForUpdt.Vehicle.VehicleType.ToString(), DepartureForUpdt.DateOfDeparture);
                }
                catch (Exception)
                {
                    TempData["msg"] = "Не удалось вычислить цену билета!";
                    return Redirect("/Departure/Edit/" + departureInput.depid);
                }

                DepartureForUpdt.NumberOfSeats = DepartureForUpdt.Vehicle.Capacity;

                if (_deparatureService.Update(DepartureForUpdt))
                {
                    TempData["msg"] = "Данные рейса изменены!";
                }
                else
                {
                    TempData["msg"] = "Данные рейса не изменены!";
                }
                return Redirect("/Departure");
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    TempData["msg"] = "Ошибка создания рейса!";
                    return Create();
                }

                newDeparture = new Departure
                {
                    CityFrom = _cityService.GetByName(departureInput.CityFrom),
                    CityTo = _cityService.GetByName(departureInput.CityTo),
                    DateOfDeparture = departureInput.DateOfDeparture,
                    Carrier = _carrierService.GetByName(departureInput.Carrier),
                    PaymentCategory = _paymentCategoryService.GetByName(departureInput.PaymentCategory),
                    Vehicle = _vehicleService.GetByRegistration(departureInput.VehicleRegistration),
                };

                if (_cityService.IsSameCity(newDeparture.CityFrom, newDeparture.CityTo))
                {
                    TempData["msg"] = "Рейс в один и тот же город невозможен!";
                    return Create();
                }

                if (departureInput.VehicleRegistration == 0)
                {
                    TempData["msg"] = "Не введен регистрационный номер!";
                    return Create();
                }

                if (newDeparture.Vehicle == null)
                {
                    TempData["msg"] = "Не выбран номер поезда!";
                    return Create();
                }

                try
                {
                    newDeparture.Distance = _distanceService.CalculateDistance(newDeparture.CityFrom, newDeparture.CityTo);
                }
                catch (Exception)
                {
                    TempData["msg"] = "Не удалось вычислить расстояние между городами!";
                    return Redirect("/Departure/Edit/" + departureInput.depid);
                }

                try
                {
                    newDeparture.PriceOfCard = _priceService.CalculatePrice(newDeparture.Distance.DistanceBetween, newDeparture.PaymentCategory.Price);
                }
                catch (Exception)
                {
                    TempData["msg"] = "Не удалось вычислить цену билета!";
                    return RedirectToPage("/Index");
                }

                try
                {
                    newDeparture.DateOfArrival = _dateOfArrivalService.CalculateDate(newDeparture.Distance.DistanceBetween, newDeparture.Vehicle.VehicleType.ToString(), newDeparture.DateOfDeparture);
                }
                catch (Exception)
                {
                    TempData["msg"] = "Не удалось вычислить цену билета!";
                    return Redirect("/Departure/Edit/" + departureInput.depid);
                }
                newDeparture.NumberOfSeats = newDeparture.Vehicle.Capacity;

                if (_deparatureService.Add(newDeparture))
                {
                    TempData["msg"] = "Рейс создан!";
                }
                else
                {
                    TempData["msg"] = "Рейс не создан!";
                }
                return RedirectToPage("/Index");
            }
        }

        public IActionResult Delete(int id)
        {
            _deparatureService.Delete(id);

            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var departure = _deparatureService.GetById(id);

            var model = new DepartureInputModel
            {
                depid = departure.Id,
                Cities = _cityService.GetAll(),
                CityTo = departure.CityTo.Name,
                CityFrom = departure.CityFrom.Name,
                DateOfDeparture = departure.DateOfDeparture,
                DateOfArrival = departure.DateOfArrival,
                Carriers = _carrierService.GetAll(),
                Carrier = departure.Carrier.Name,
                PaymentCategories = _paymentCategoryService.GetAll(),
                PaymentCategory = departure.PaymentCategory.Name,
                Vehicles = _vehicleService.GetAll(),
                VehicleRegistration = departure.Vehicle.RegistrationNumber
            };
            return View(model);
        }

        public IActionResult Create()
        {
            var cities = _cityService.GetAll();
            var carriers = _carrierService.GetAll();
            var paymentCategory = _paymentCategoryService.GetAll();
            var vehicles = _vehicleService.GetAll();

            var model = new DepartureInputModel
            {
                Cities = cities,
                Carriers = carriers,
                PaymentCategories = paymentCategory,
                Vehicles = vehicles,
            };
            return View(model);
        }
    }
}