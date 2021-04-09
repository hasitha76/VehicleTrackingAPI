using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VTS.Business.DTO;
using VTS.Business.DTO.Google;
using VTS.Data.Entities;
using VTS.Data.Repositories;

namespace VTS.Business.Services
{
    public class TrackingService : ITrackingService
    {
        private readonly IUnitOfWork _uow;

        public TrackingService(IUnitOfWork unitofwork)
        {
            _uow = unitofwork;
        }

        public async Task<bool> RecordPosition(RecordingDTO model)
        {
            if (model != null)
            {
                var location = new Location()
                {
                    DeviceID = model.DeviceID,
                    Latitude = model.Latitude,
                    Longitude = model.Longitude
                };

                _uow.LocationRepository.Create(location);
                var result = await _uow.SaveChanges();
                if (result > 0)
                    return true;
            }

            return false;
        }

        public async Task<LocationDTO> GetCurrentPosition(Guid vehicleId)
        {
            var vehicle= _uow.VehicleRepository.Find(x => x.ID == vehicleId,new string[] { "Device" }).FirstOrDefault();

            if (vehicle == null)
                return null;

            var locations = _uow.LocationRepository.Find (x=>x.DeviceID == vehicle.Device.ID).Select(l=>
                     new LocationDTO()
                     {
                         DeviceID = l.DeviceID,
                         Latitude = l.Latitude,
                         Longitude = l.Longitude,
                         RecordedTime = l.CreatedOn
                     });
            var location = locations.OrderByDescending(x => x.RecordedTime).FirstOrDefaultAsync();
            location.Result.Location = await GetMatchingLocation(location.Result.Latitude, location.Result.Longitude);
            return await location;
        }

        public async Task<List<LocationDTO>> GetVehiclePositions(Guid vehicleId, DateTime startTime, DateTime endTime)
        {
            var vehicle = _uow.VehicleRepository.Find(x => x.ID == vehicleId, new string[] { "Device" }).FirstOrDefault();

            if (vehicle == null)
                return null;

            var locations = _uow.LocationRepository.Find(x => x.DeviceID == vehicle.Device.ID && x.CreatedOn >= startTime && x.CreatedOn < endTime).Select(l =>
                      new LocationDTO()
                      {
                          DeviceID = l.DeviceID,
                          Latitude = l.Latitude,
                          Longitude = l.Longitude,
                          RecordedTime = l.CreatedOn,
                          
                      });

            foreach (var item in locations)
            {
                item.Location=await GetMatchingLocation(item.Latitude, item.Longitude);
            }
            return await locations.ToListAsync();

        }

        /// <summary>
        /// Reverse Geocoding- Get Matching Location Name from Lat/Lon
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lon"></param>
        /// <returns></returns>
        private async Task<string> GetMatchingLocation(decimal lat,decimal lon)
        {
            try
            {
                // google cloud API Key
                var API_KEY = "";
                var requestUri = string.Format("https://maps.googleapis.com/maps/api/geocode/json?latlng={0},{1}&key={2}", lat, lon, API_KEY);

                var client = new HttpClient();

                HttpResponseMessage response = await client.GetAsync(requestUri);
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadAsStringAsync();

                var deserializedResult = JsonConvert.DeserializeObject<Geocode>(result);

                if (deserializedResult.status == "REQUEST_DENIED")
                    return string.Empty;

                // get the sub locality from address component
                return deserializedResult.results.Select(x => new Address_Components
                {
                    long_name = x.address_components.FirstOrDefault().long_name

                }).Where(x => x.types.ToString() == "sublocality").ToString();
            }
            catch (Exception)
            {

                return string.Empty;
            }
            
        }

    }
}
