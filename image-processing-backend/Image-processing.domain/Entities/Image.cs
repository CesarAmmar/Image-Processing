using System;
using System.Collections.Generic;

namespace Image_processing.domain.Entities;

public partial class Image
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public byte[] Image1 { get; set; } = null!;
}
