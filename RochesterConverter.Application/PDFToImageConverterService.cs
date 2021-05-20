using RochesterConverter.Application.Interface;
using System.Collections.Generic;
using System.Drawing;

namespace RochesterConverter.Application
{
    internal class PDFToImageConverterService : IPDFToImageConverterService
    {
        private readonly IFileOperations _fileOperations;

        public PDFToImageConverterService(IFileOperations fileOperations)
        {
            _fileOperations = fileOperations;
        }

        public List<Image> LoadPdfAsImage(string path)
        {
            return _fileOperations.LoadPdfAsImage(path);
        }

        public int SaveImages(List<Image> images)
        {
            return _fileOperations.SaveImages(images);
        }
    }
}
