using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using VTS.Business.DTO;
using VTS.Business.Services;

namespace VTS.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationService _registrationService;
        private readonly ILogger _logger;
        public RegistrationController(IRegistrationService registrationService, ILogger<RegistrationController> logger)
        {
            _registrationService = registrationService;
            _logger = logger;
        }

        /// <summary>
        /// REgister vehicle with user and Device
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[controller]/Vehicle")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterVehicle(RegistrationDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Model validation failed.");
                    return BadRequest(ModelState);
                }

                bool result = await _registrationService.RegisterVehicle(model);

                if (result)
                {
                    _logger.LogInformation("User/Device Registered.");
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
    }
}
