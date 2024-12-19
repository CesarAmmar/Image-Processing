using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Image_processing.application.Services.AcceleratorService
{
    public interface IAcceleratorService
    {
        public Task<int> GetCoresAsync();
        public Task<List<string>> GetAcceleratorsAsync();
    }
}
