using System;
using System.Collections.Generic;
using System.Text;
using VTS.Data.Entities;

namespace VTS.Data.Repositories
{
    public class DeviceRepository : GenericRepository<Device>, IDeviceRepository
    {
        public DeviceRepository(ApplicationDBContext dbContext)
        : base(dbContext)
        {

        }
    }
}
