using System.Collections.Generic;
using System.Drawing;

namespace RochesterConverter.Application.Interface
{
    public interface IPDFToImageConverterService
    {
        public List<Image> LoadPdfAsImage(string path);
        int SaveImages(List<Image> images);
    }
}
