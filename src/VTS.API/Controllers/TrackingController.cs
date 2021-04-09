using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VTS.Business.DTO;
using VTS.Business.Services;

namespace VTS.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class TrackingController : ControllerBase
    {
        private readonly IRegistrationService _registrationService;
        private readonly ITrackingService _trackingService;
        private readonly ILogger _logger;

        public TrackingController(IRegistrationService registrationService, ITrackingService trackingService, ILogger<RegistrationController> logger)
        {
            _registrationService = registrationService;
            _trackingService = trackingService;
            _logger = logger;
        }

        /// <summary>
        /// Record a Position
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[controller]/RecordPosition")]
        public async Task<IActionResult> Record(RecordingDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Model validation failed.");
                    return BadRequest(ModelState);
                }

                // ensure a device or user cannot update the position of another vehicle
                var canRecord = await _registrationService.CanRecordPosition(model.UserID, model.DeviceID);
                if (!canRecord)
                {
                    _logger.LogError("User or Device has not a permission to record this Location.");
                    return BadRequest("User or Device has not a permission to record this Location.");
                }

                bool result = await _trackingService.RecordPosition(model);
                if (result)
                {
                    _logger.LogInformation("Position Recorded.");
                    return Ok(true);
                }
                else
                {
                    _logger.LogInformation("Error Occured");
                    return Ok(false);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Error Occured");
            }
        }

        /// <summary>
        /// Get Vehicle's Current Location
        /// </summary>
        /// <param name="vehicleId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[controller]/GetCurrentPosition")]
        public async Task<IActionResult> GetPosition(Guid vehicleId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Model validation failed.");
                    return BadRequest(ModelState);
                }

                var result = await _trackingService.GetCurrentPosition(vehicleId);

                _logger.LogInformation($"Retrieve current position of {vehicleId}");
                return Ok(result);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Error Occured");
            }
        }

        /// <summary>
        /// Retrieve the positions of a vehicle during a certain time period
        /// </summary>
        /// <param name="vehicleId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[controller]/GetVehiclePositions")]
        public async Task<IActionResult> GetVehiclePositions(Guid vehicleId,DateTime startTime, DateTime endTime)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Model validation failed.");
                    return BadRequest(ModelState);
                }

                var results = await _trackingService.GetVehiclePositions(vehicleId, startTime, endTime);

                _logger.LogInformation("Retrieve vehicle positions");
                return Ok(results);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Error Occured");
            }
        }


    }
}
