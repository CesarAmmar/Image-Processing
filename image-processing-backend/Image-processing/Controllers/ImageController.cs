using Image_processing.application.DTOs.ImageDTO;
using Image_processing.application.Services.ImageService;
using Image_processing.application.Utilities.Exceptions.ImageExceptions;
using Microsoft.AspNetCore.Mvc;

namespace Image_processing.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;
        
        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpGet(Name = "GetAllImages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetImages()
        {
            try
            {
                var images = await _imageService.GetImagesAsync();
                return Ok(images);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpGet("{id:int:min(1)}", Name = "GetImage")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetImage(int id)
        {
            try
            {
                var image = await _imageService.GetImageAsync(id);
                return Ok(image);
            }
            catch (ImageNotFoundException exception)
            {
                return NotFound(exception.Message);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpPost(Name = "AddImage")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddImage([FromBody] ImageToAddDTO imageToAddDTO)
        {
            try
            {
                var newImage = await _imageService.AddImageAsync(imageToAddDTO);
                return Ok(newImage);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpPut("{id:int:min(1)}", Name = "UpdateImage")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> UpdateImage(int id, [FromBody] ImageToAddDTO imageToAddDTO)
        {
            try
            {
                var imageToUpdate = await _imageService.UpdateImageAsync(id, imageToAddDTO);
                return NoContent();
            }
            catch (ImageNotFoundException exception)
            {
                return NotFound(exception.Message);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpDelete("{id:int:min(1)}", Name = "DeleteImage")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> DeleteImage(int id)
        {
            try
            {
                var result = await _imageService.DeleteImageAsync(id);
                return Ok(result);
            }
            catch (ImageNotFoundException exception)
            {
                return NotFound(exception.Message);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }
    }
}
