using Image_processing.application.Services.AcceleratorService;
using Microsoft.AspNetCore.Mvc;

namespace Image_processing.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AcceleratorController : ControllerBase
    {
        private readonly IAcceleratorService _acceleratorService;

        public AcceleratorController(IAcceleratorService acceleratorService)
        {
            _acceleratorService = acceleratorService;
        }

        [HttpGet("core",Name = "GetCores")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCores()
        {
            try
            {
                var coresNumber = await _acceleratorService.GetCoresAsync();
                return Ok(coresNumber);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpGet("accelerator", Name = "GetAccelerators")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAccelerators()
        {
            try
            {
                var accelerators = await _acceleratorService.GetAcceleratorsAsync();
                return Ok(accelerators);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }
    }
}
