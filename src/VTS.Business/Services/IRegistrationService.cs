using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VTS.Business.DTO;

namespace VTS.Business.Services
{
    public interface IRegistrationService
    {
        Task<bool> RegisterVehicle(RegistrationDTO model);
        Task<bool> CanRecordPosition(Guid userId, Guid deviceId);
    }
}
