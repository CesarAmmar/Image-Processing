using Image_processing.application.Validators.UniqueValidators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Image_processing.application.DTOs.ImageDTO
{
    public class ImageToAddDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [UniqueImageName]
        public string Name { get; set; } = null!;
        [Required]
        public byte[] Image1 { get; set; } = null!;
    }
}
