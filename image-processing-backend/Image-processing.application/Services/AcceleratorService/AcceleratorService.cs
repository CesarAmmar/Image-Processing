using ILGPU;
using ILGPU.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Image_processing.application.Services.AcceleratorService
{
    public class AcceleratorService : IAcceleratorService
    {
        public async Task<List<string>> GetAcceleratorsAsync()
        {
            List<string> accelerators = await Task.Run(() => AcceleratorsRetreiver());
            if (accelerators != null)
            {
                return accelerators;
            }
            throw new Exception("There is no accelerators on your device.");
        }
        public async Task<int> GetCoresAsync()
        {
            int coresNumber = await Task.Run(() => CoresRetreiver()) ;
            if (coresNumber > 0)
            {
                return coresNumber;
            }
            throw new Exception("Number of cores are zero.");
        }
        private int CoresRetreiver()
        {
             return Environment.ProcessorCount;
        }
        private List<string> AcceleratorsRetreiver()
        {
            Context context = Context.Create(builder => builder.AllAccelerators());
            List<string> accelerators = new List<string>();
            foreach (Device device in context)
            {
                accelerators.Add(device.AcceleratorType.ToString());
            }

            return accelerators;
        }
    }
}
