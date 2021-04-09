using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VTS.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDBContext _context;

        public IUserRepository _userRepository;

        public IVehicleRepository _vehicleRepository;

        public IDeviceRepository _deviceRepository;

        public ILocationRepository _locationRepository;

        public UnitOfWork(ApplicationDBContext repositoryContext)
        {
            _context = repositoryContext;
        }

        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_context);
                }

                return _userRepository;
            }
        }

        public IVehicleRepository VehicleRepository
        {
            get
            {
                if (_vehicleRepository == null)
                {
                    _vehicleRepository = new VehicleRepository(_context);
                }

                return _vehicleRepository;
            }
        }

        public IDeviceRepository DeviceRepository
        {
            get
            {
                if (_deviceRepository == null)
                {
                    _deviceRepository = new DeviceRepository(_context);
                }

                return _deviceRepository;
            }
        }

        public ILocationRepository LocationRepository
        {
            get
            {
                if (_locationRepository == null)
                {
                    _locationRepository = new LocationRepository(_context);
                }

                return _locationRepository;
            }
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }

        
    }
}
