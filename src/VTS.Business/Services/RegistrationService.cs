using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.Business.DTO;
using VTS.Data.Entities;
using VTS.Data.Repositories;

namespace VTS.Business.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IUnitOfWork _uow;

        public RegistrationService(IUnitOfWork unitofwork)
        {
            _uow = unitofwork;
        }

        public async Task<bool> RegisterVehicle(RegistrationDTO model)
        {
            if (model != null)
            {
                var objUser = new User()
                {
                    Name = model.User.Name,
                    Email = model.User.Email
                };
                _uow.UserRepository.Create(objUser);

                var objVehicle = new Vehicle()
                {
                    UserID = objUser.ID,
                    Make = model.Vehicle.Make,
                    Model = model.Vehicle.Model,
                    VIN = model.Vehicle.VIN
                };
                _uow.VehicleRepository.Create(objVehicle);

                var objDevice = new Device()
                {
                    VehicleID = objVehicle.ID,
                    Name = model.Device.Name
                };
                _uow.DeviceRepository.Create(objDevice);

                var result = await _uow.SaveChanges();
                if (result > 0)
                    return true;
            }

            return false;
        }

        public async Task<bool> CanRecordPosition(Guid userId, Guid deviceId)
        {

            var device = _uow.DeviceRepository.Find(x => x.ID == deviceId, new string[] { "Vehicle" }).FirstOrDefault();

            if (device == null)
                return await Task.FromResult(false);

            var result = _uow.UserRepository.Find(x => x.ID == userId, new string[] { "Vehicles" }).Any(x => x.Vehicles.Contains(device.Vehicle));

            return await Task.FromResult(result);
        }
    }
        
}
