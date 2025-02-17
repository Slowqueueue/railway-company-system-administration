﻿using RailwayCompanyIS.Data;
using RailwayCompanyIS.Data.Models;
using RailwayCompanyIS.Data.ServiceSpecification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RailwayCompanyIS.Service
{
    public class VehicleService : IVehicle
    {
        private readonly ApplicationDBContext _context;

        public VehicleService(ApplicationDBContext context)
        {
            _context = context;
        }

        public IEnumerable<Vehicle> GetAll()
        {
            return _context.Vehicles;
        }

        public Vehicle GetByRegistration(int registration)
        {
            return GetAll()
                .FirstOrDefault(v => v.RegistrationNumber == registration);
        }

        public IEnumerable<VehicleType> GetVehicleTypes()
        {
            return (IEnumerable<VehicleType>)Enum.GetValues(typeof(VehicleType));
        }
    }
}
