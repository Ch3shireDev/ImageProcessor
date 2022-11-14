using ImageProcessorLibrary.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessorLibrary.Services
{
    public interface IWindowService
    {
        public void ShowImageWindow(ImageData imageData);
    }
}
