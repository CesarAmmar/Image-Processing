using AutoMapper;
using Image_processing.application.DTOs.ImageDTO;
using Image_processing.domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Image_processing.application.Utilities.AutoMapperConfig
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Image, ImageToAddDTO>().ReverseMap();
            CreateMap<Image, ImageToReturnDTO>().ReverseMap();
        }
    }
}
