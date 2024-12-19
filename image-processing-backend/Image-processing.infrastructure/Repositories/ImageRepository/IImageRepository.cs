using Image_processing.domain.Entities;
using Image_processing.infrastructure.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Image_processing.infrastructure.Repositories.ImageRepository
{
    public interface IImageRepository : IGenericRepository<Image>
    {
    }
}
