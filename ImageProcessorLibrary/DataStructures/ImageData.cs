using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessorLibrary.DataStructures
{
    public class ImageData
    {
        public ImageData(string filename, byte[] filebytes)
        {
            Filename = filename;
            Filebytes = filebytes;
        }

        public byte[] Filebytes { get; set; }

        public string Filename { get; set; }
    }
}
