using System;
using System.Collections.Generic;
using System.Text;
using VTS.Data.Entities;

namespace VTS.Data.Repositories
{
    public class VehicleRepository : GenericRepository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(ApplicationDBContext dbContext)
        : base(dbContext)
        {

        }
    }
}
