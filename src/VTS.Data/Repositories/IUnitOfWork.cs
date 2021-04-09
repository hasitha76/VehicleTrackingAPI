using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VTS.Data.Repositories
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IVehicleRepository VehicleRepository { get; }
        IDeviceRepository DeviceRepository { get; }
        ILocationRepository LocationRepository { get; }

        Task<int> SaveChanges();
        
    }
}
