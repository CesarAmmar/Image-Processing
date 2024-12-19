using Image_processing.infrastructure.DataContext;
using Image_processing.infrastructure.Repositories.ImageRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Image_processing.infrastructure
{
    public static class DependencyInjection
    {
        public static void RegisterInfraDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ImageProcessingDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DbConnection"));
            });
            services.AddScoped<IImageRepository, ImageRepository>();
        }
    }
}
