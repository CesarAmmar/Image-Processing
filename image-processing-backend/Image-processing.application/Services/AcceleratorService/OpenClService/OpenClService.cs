using AutoMapper;
using Image_processing.application.DTOs.ImageDTO;
using Image_processing.application.Services.AcceleratorService.CpuService;
using Image_processing.application.Utilities.FiltersAlgorithms;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Image_processing.application.Services.AcceleratorService.OpenClService
{
    public class OpenClService : IOpenClService
    {
        private readonly Dictionary<string, Func<float[,]>> _filters;
        private readonly ILogger<OpenClService> _logger;
        private readonly IMapper _mapper;
        public OpenClService(ILogger<OpenClService> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _filters = new Dictionary<string, Func<float[,]>>(StringComparer.OrdinalIgnoreCase)
            {
                {"sharpening", Sharpening.GetFilterKernel},
                {"edge-detection", EdgeDetecting.GetFilterKernel},
                {"blurring", Blurring.GetFilterKernel},
            };
        }

        public Task<ImageToAddDTO> ApplyFilterAsync(ImageToAddDTO image, string filterName)
        {
            throw new NotImplementedException();
        }
    }
}
