using System;
using System.Collections.Generic;
using System.Text;
using VTS.Data.Entities;

namespace VTS.Data.Repositories
{
    public class LocationRepository : GenericRepository<Location>, ILocationRepository
    {
        public LocationRepository(ApplicationDBContext dbContext)
        : base(dbContext)
        {

        }
    }
}
