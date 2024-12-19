using Image_processing.application.DTOs.ImageDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Image_processing.application.Services.ImageService
{
    public interface IImageService
    {
        public Task<List<ImageToReturnDTO>> GetImagesAsync();
        public Task<ImageToReturnDTO> GetImageAsync(int id);
        public Task<ImageToReturnDTO> AddImageAsync(ImageToAddDTO imageToAddDTO);
        public Task<ImageToReturnDTO> UpdateImageAsync(int id, ImageToAddDTO imageToAddDTO);
        public Task<bool> DeleteImageAsync(int id);
    }
}
