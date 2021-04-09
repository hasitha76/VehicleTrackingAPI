using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VTS.Business.DTO;

namespace VTS.Business.Services
{
    public interface ITrackingService
    {
        Task<bool> RecordPosition(RecordingDTO model);
        Task<LocationDTO> GetCurrentPosition(Guid vehicleId);
        Task<List<LocationDTO>> GetVehiclePositions(Guid vehicleId, DateTime startTime, DateTime endTime);
    }
}
