using Image_processing.application.DTOs.ImageDTO;
using Image_processing.application.Services.AcceleratorService.CpuService;
using Image_processing.application.Services.ImageService;
using Microsoft.AspNetCore.Mvc;

namespace Image_processing.Controllers.AcceleratorControllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CPUController : ControllerBase
    {
        private readonly ICpuService _cpuService;
        private readonly IImageService _imageService;

        public CPUController(ICpuService cpuService, IImageService imageService)
        {
            _cpuService = cpuService;
            _imageService = imageService;
        }

        [HttpPost(Name = "CpuProcess")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetProcessedImage([FromBody] ImageToAddDTO imageToAddDTO, string filterName)
        {
            try
            {
                var processedImage = await _cpuService.ApplyFilterAsync(imageToAddDTO, filterName);
                var storedImage = await _imageService.AddImageAsync(processedImage);
                return Ok(storedImage);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }
    }
}
