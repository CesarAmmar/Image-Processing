using Image_processing.domain.Entities;
using Image_processing.infrastructure.DataContext;
using Image_processing.infrastructure.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Image_processing.infrastructure.Repositories.ImageRepository
{
    public class ImageRepository : GenericRepository<Image>, IImageRepository
    {
        private readonly ImageProcessingDbContext _dbContext;

        public ImageRepository(ImageProcessingDbContext dbContext) : base(dbContext) 
        {
            _dbContext = dbContext;
        }
    }
}
