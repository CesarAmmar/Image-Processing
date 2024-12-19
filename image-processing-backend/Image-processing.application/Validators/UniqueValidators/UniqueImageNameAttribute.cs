using Image_processing.infrastructure.Repositories.ImageRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Image_processing.application.Validators.UniqueValidators
{
    public class UniqueImageNameAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                IImageRepository _imageRepository = (IImageRepository)validationContext.GetService(typeof(IImageRepository));
                var image = _imageRepository.GetAsync(i => i.Name == value).Result;
                if (image != null)
                {
                    return new ValidationResult("A image with this name already exists");
                }
                return ValidationResult.Success;
            }
            return new ValidationResult("The image name cannot be empty");
        }
    }
}
