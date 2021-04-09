using System;
using System.Collections.Generic;
using System.Text;
using VTS.Data.Entities;

namespace VTS.Data.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDBContext dbContext)
        : base(dbContext)
        {

        }
    }
}
