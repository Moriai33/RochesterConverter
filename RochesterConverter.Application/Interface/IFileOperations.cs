using System.Collections.Generic;
using System.Drawing;

namespace RochesterConverter.Application.Interface
{
    public interface IFileOperations
    {
        List<Image> LoadPdfAsImage(string path);
        int SaveImages(List<Image> images);
        void SaveCSV(string path, string data);
    }
}
