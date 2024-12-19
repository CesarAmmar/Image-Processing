using Image_processing.application.DTOs.ImageDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Image_processing.application.Services.AcceleratorService.CpuService
{
    public interface ICpuService
    {
        public Task<ImageToAddDTO> ApplyFilterAsync(ImageToAddDTO image, string filterName);
    }
}
