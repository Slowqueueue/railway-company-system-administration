using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RailwayCompanyIS.Data.Models;
using RailwayCompanyIS.Data.ServiceSpecification;
using RailwayCompanyIS.Models.Carrier;
using RailwayCompanyIS.Models.CarrierViewModel;
using RailwayCompanyIS.Models.City;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace RailwayCompanyIS.Controllers
{
    public class CarrierController : Controller
    {
        private readonly ICarrier _carrierService;
        private readonly ICity _cityService;
        private readonly IContact _contactService;
        private readonly IVehicle _vehicleService;

        public CarrierController(ICarrier carrierService,ICity cityService,IContact contactService,IVehicle vehicleService)
        {
            _carrierService = carrierService;
            _cityService = cityService;
            _contactService = contactService;
            _vehicleService = vehicleService;
        }

        public IActionResult AddContact(int id)
        {
            var contactTypes = _contactService.GetContactTypes();

            var model = new CarrierContactInputModel
            {
                ContactTypes = contactTypes,
                CarrierId = id
            };

            return View(model);
        }

       
        [HttpPost]
        public IActionResult Delete(int id)
        {
            if(!_carrierService.Delete(id))
            {
                TempData["msg"] = "Машинист привязан к рейсам, удаление невозможно!";
            }
            return RedirectToAction("Index");
        }

        public IActionResult AddVehicle(int id)
        {
            var vehicleType = _vehicleService.GetVehicleTypes();

            var model = new CarrierVehicleInputModel
            {
                VehicleTypes = vehicleType,
                CarrierId = id
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult AddVehicle([Bind]CarrierVehicleInputModel vehicleInputModel,int id)
        {
            if (!ModelState.IsValid)
            {
                TempData["msg"] = "Ошибка добавления поезда!";
                return Index();
            }
            Vehicle vehicle = new Vehicle
            {
                RegistrationNumber = vehicleInputModel.RegistrationNumber,
                Tourist = vehicleInputModel.Tourist,
                Capacity = vehicleInputModel.Capacity,
                VehicleType = vehicleInputModel.VehicleType
            };

            Carrier carrier = _carrierService.GetById(id);
            carrier.Vehicles.Add(vehicle);

            if (_carrierService.Update(carrier))
            {
                TempData["msg"] = "Поезд добавлен!";
            }
            else
            {
                TempData["msg"] = "Поезд не добавлен!";
            }

            return RedirectToPage("/..");
        }

        public IActionResult Create()
        {
            var cities = _cityService.GetAll();

            var allCities = cities.Select(c => new City
            {
                Id = c.Id,
                Name = c.Name
            });

            var model = new CarrierInputModel
            {
               Cities = allCities
            };
            return View(model);
        }
        public IActionResult DeleteContact(int id)
        {
            Carrier carrier = _carrierService.GetById(id);
            carrier.Contacts.Clear();
            _carrierService.Update(carrier);
            return Redirect("/Carrier/Detail/" + id);
        }

        public IActionResult DeleteVehicle(int id)
        {
            if (_carrierService.CheckDep(id))
            {
                TempData["msg"] = "Поезд привязан к рейсам, удаление невозможно!";
                return Redirect("/Carrier/Detail/" + id);
            }
            Carrier carrier = _carrierService.GetById(id);
            carrier.Vehicles.Clear();
            _carrierService.Update(carrier);
            return Redirect("/Carrier/Detail/" + id);
        }

        [HttpPost]
        public IActionResult AddContact([Bind]CarrierContactInputModel carrierContact,int id)
        {
            if (!ModelState.IsValid)
            {
                TempData["msg"] = "Нельзя добавить пустой контакт!";
                return RedirectToPage("/Index");
            }
            Contact newContact = new Contact
            {
                ContactContent = carrierContact.ContactContent,
                Type = carrierContact.Type
            };
            
            Carrier carrier = _carrierService.GetById(id);
            carrier.Contacts.Add(newContact);
            if (_carrierService.Update(carrier))
            {
                TempData["msg"] = "Контакт добавлен!";
            }
            else
            {
                TempData["msg"] = "Контакт не добавлен!";
            }

            return RedirectToPage("/Index",new { area = "Carrier"});
        }

        public IActionResult Edit(int id)
        {
            Carrier carrier = _carrierService.GetById(id);

            CarrierInputModel carrierInput = new CarrierInputModel
            {
                carid = carrier.Id,
                Name = carrier.Name,
                StreetName = carrier.Address.StreetName,
                StreetNumber = carrier.Address.StreetNumber,
                Cities = _cityService.GetAll(),
                CityName = carrier.Address.City.Name
            };
            return View(carrierInput);
        }

        [HttpPost]
        public IActionResult Create([Bind]CarrierInputModel carrier)
        {
            int CarrierForUpdtId;
            City newCity;
            Carrier newCarrier, CarrierForUpdt;

            Address newAddress;
            if (carrier.carid != 0)
            {
                if (!ModelState.IsValid)
                {
                    TempData["msg"] = "Заполните все поля!";
                    return Redirect("/Carrier/Edit/" + carrier.carid);
                }
                CarrierForUpdtId = carrier.carid;
                CarrierForUpdt = _carrierService.GetById(CarrierForUpdtId);
                newCity = _cityService.GetByName(carrier.CityName);
                newAddress = new Address
                {
                    StreetName = carrier.StreetName,
                    StreetNumber = carrier.StreetNumber,
                    City = newCity
                };
                CarrierForUpdt.Address = newAddress;
                CarrierForUpdt.Name = carrier.Name;
                if (_carrierService.Update(CarrierForUpdt))
                {
                    TempData["msg"] = "Данные машиниста изменены!";
                }
                else
                {
                    TempData["msg"] = "Данные машиниста не изменены!";
                }
                return Redirect("/Carrier");
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    TempData["msg"] = "Заполните все поля!";
                    return Create();
                }
                newCity = _cityService.GetByName(carrier.CityName);
                newAddress = new Address
                {
                    StreetName = carrier.StreetName,
                    StreetNumber = carrier.StreetNumber,
                    City = newCity
                };
                newCarrier = new Carrier
                {
                    Name = carrier.Name,
                    Address = newAddress
                };

                if (_carrierService.AddNewCarrier(newCarrier))
                {
                    TempData["msg"] = "Машинист добавлен!";
                }
                else
                {
                    TempData["msg"] = "Машинист не добавлен!";
                }
                return RedirectToPage("/Home");
            }
        }

        public IActionResult Index()
        {
            var carriers = _carrierService.GetAll();

            var carriersListing = carriers.Select(c => new CarrierDetailModel
            {
                Id = c.Id,
                Name = c.Name,
                Vehicles = c.Vehicles,
                Address = c.Address,
                Contacts = FrontHelpers.FrontHumanizeHelper.ContactsHumanize(c.Contacts)
            });

            var model = new CarrierIndexModel
            {
                Carriers = carriersListing
            };
            return View(model);
        }

        public IActionResult Back(int id)
        {
            return Redirect("/Carrier/Detail/" + id);
        }
        public IActionResult Detail(int id)
        {
            var carrier = _carrierService.GetById(id);

            var model = new CarrierDetailModel
            {
                Id = carrier.Id,
                Name = carrier.Name,
                Address = carrier.Address,
                Contacts = FrontHelpers.FrontHumanizeHelper.ContactsHumanize(carrier.Contacts),
                Vehicles = carrier.Vehicles
            };

            return View(model);
        }
    }
}