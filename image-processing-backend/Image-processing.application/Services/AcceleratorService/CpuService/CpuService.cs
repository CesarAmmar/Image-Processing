using AutoMapper;
using ILGPU.Runtime.CPU;
using ILGPU;
using Image_processing.application.DTOs.ImageDTO;
using Image_processing.application.Utilities.FiltersAlgorithms;
using Image_processing.domain.Entities;
using Microsoft.Extensions.Logging;
using ILGPU.Runtime;
using Image_processing.application.Utilities.FiltersAlgorithms.FiltersUtils;

namespace Image_processing.application.Services.AcceleratorService.CpuService
{
    public class CpuService : ICpuService
    {
        private readonly Dictionary<string, Func<float[,]>> _filters;
        private readonly ILogger<CpuService> _logger;
        private readonly IMapper _mapper;
        public CpuService(ILogger<CpuService> logger, IMapper mapper)
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
        public async Task<ImageToAddDTO> ApplyFilterAsync(ImageToAddDTO image, string filterName)
        {
            if (image == null){
                _logger.LogError($"Image '{image}' not found.");
                throw new ArgumentNullException(nameof(image));
            };

            if (string.IsNullOrWhiteSpace(filterName)){
                _logger.LogError($"Filter '{filterName}' not found.");
                throw new ArgumentException("Filter name cannot be empty."); 
            }

            _logger.LogInformation("Applying filter...");
            byte[] bytesToReturn = await Task.Run(() => CpuAcceleratorKernel(image.Image1, filterName));
            var imageProcessed = _mapper.Map<Image>(image);
            image.Image1 = bytesToReturn;
            var imageToReturn = _mapper.Map<ImageToAddDTO>(imageProcessed);

            _logger.LogInformation("Filter successfully applied.");
            return imageToReturn;
        }

        private async Task<byte[]> CpuAcceleratorKernel(byte[] image, string filterName)
        {
            if (!_filters.TryGetValue(filterName, out var filterFunc))
                throw new ArgumentException($"Filter '{filterName}' not found.");

            float[,] kernel = filterFunc();
            var (width, height) = Helpers.GetImageDimensions(image);
            
            
            byte[] flattenedOutput = new byte[width * height];
            
            using var context = Context.CreateDefault();
            using var accelerator = context.CreateCPUAccelerator(0);
            
            var convolutionKernel = accelerator.LoadAutoGroupedStreamKernel<Index2D,
            ArrayView1D<byte, Stride1D.Dense>, ArrayView1D<byte, Stride1D.Dense>,
            ArrayView2D<float, Stride2D.DenseY>, int, int, int>(Convolution.ConvolutionKernel);
            
            using var gpuInput = accelerator.Allocate1D<byte>(image);
            using var gpuOutput = accelerator.Allocate1D<byte>(flattenedOutput);
            using var gpuKernel = accelerator.Allocate2DDenseY<float>(new Index2D(kernel.GetLength(0), kernel.GetLength(1)));
            
            gpuKernel.CopyFromCPU(kernel);
            
            convolutionKernel(new Index2D(width, height), gpuInput.View, gpuOutput.View, gpuKernel.View, kernel.GetLength(0), width, height);
            
            accelerator.Synchronize();
            
            gpuOutput.CopyToCPU(flattenedOutput);
            
            return flattenedOutput;
        
        }
    }
}
