using AutoMapper;
using Image_processing.application.DTOs.ImageDTO;
using Image_processing.application.Utilities.Exceptions.ImageExceptions;
using Image_processing.domain.Entities;
using Image_processing.infrastructure.Repositories.ImageRepository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Image_processing.application.Services.ImageService
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ImageService> _logger;

        public ImageService(IImageRepository imageRepository, IMapper mapper, ILogger<ImageService> logger)
        {
            _imageRepository = imageRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ImageToReturnDTO> AddImageAsync(ImageToAddDTO imageToAddDTO)
        {
            var newImage = _mapper.Map<Image>(imageToAddDTO);
            await _imageRepository.AddAsync(newImage);
            _logger.LogInformation($"New Image Added.");
            return _mapper.Map<ImageToReturnDTO>(newImage);
        }

        public async Task<bool> DeleteImageAsync(int id)
        {
            var existingImage = await _imageRepository.GetAsync(image => image.Id == id);
            if (existingImage is null)
            {
                string message = $"Image with id: {id} not found";
                _logger.LogError(message);
                throw new ImageNotFoundException(message);
            }
            await _imageRepository.DeleteAsync(existingImage);
            _logger.LogInformation($"Image with id: {id} has been deleted.");
            return true;
        }

        public async Task<ImageToReturnDTO> GetImageAsync(int id)
        {
            var image = await _imageRepository.GetAsync(image => image.Id == id);
            if (image is null)
            {
                string message = $"Image with id: {id} not found";
                _logger.LogError(message);
                throw new ImageNotFoundException(message);
            }
            _logger.LogInformation($"Image with name {image.Name} is returned");
            return _mapper.Map<ImageToReturnDTO>(image);
        }

        public async Task<List<ImageToReturnDTO>> GetImagesAsync()
        {
            var images = await _imageRepository.GetAllAsync();
            _logger.LogInformation($"Images with count {images.Count} are returned");
            return _mapper.Map<List<ImageToReturnDTO>>(images);
        }

        public async Task<ImageToReturnDTO> UpdateImageAsync(int id, ImageToAddDTO imageToAddDTO)
        {
            var existingImage = await _imageRepository.GetAsync(image => image.Id == id);
            if (existingImage is null)
            {
                string message = $"Image with id: {id} not found";
                _logger.LogError(message);
                throw new ImageNotFoundException(message);
            }
            existingImage = _mapper.Map<Image>(imageToAddDTO);
            existingImage.Id = id;
            await _imageRepository.UpdateAsync(existingImage);
            _logger.LogInformation($"Updated image {existingImage.Id}");
            return _mapper.Map<ImageToReturnDTO>(existingImage);
        }
    }
}
