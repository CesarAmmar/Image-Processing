using Image_processing.application.Services.AcceleratorService;
using Image_processing.application.Services.AcceleratorService.CpuService;
using Image_processing.application.Services.AcceleratorService.CudaService;
using Image_processing.application.Services.AcceleratorService.OpenClService;
using Image_processing.application.Services.ImageService;
using Image_processing.application.Utilities.AutoMapperConfig;
using Image_processing.infrastructure.DataContext;
using Image_processing.infrastructure.Repositories.ImageRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Image_processing.application
{
    public static class DependencyInjection
    {
        public static void RegisterAppDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(AutoMapperProfiles));
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IAcceleratorService, AcceleratorService>();
            services.AddScoped<ICpuService, CpuService>();
            services.AddScoped<IOpenClService, OpenClService>();
            services.AddScoped<ICudaService, CudaService>();
        }
    }
}
