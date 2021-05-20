using Ghostscript.NET.Rasterizer;
using RochesterConverter.Application.Interface;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace RochesterConverter.Infrastructure
{
    internal class FileOperations : IFileOperations
    {
        public List<Image> LoadPdfAsImage(string path)
        {
            using var rasterizer = new GhostscriptRasterizer();
            rasterizer.Open(path);

            var images = new List<Image>();
            for (int i = 0; i < rasterizer.PageCount; i++)
            {
                var image = rasterizer.GetPage(500, i + 1);
                images.Add(image);
            }

            return images;
        }

        public void SaveCSV(string path, string data)
        {
            StreamWriter writer = new StreamWriter(path);
            writer.Write(data);
            writer.Close();
        }

        public int SaveImages(List<Image> images)
        {
            string tempPath = Path.GetTempPath();
            
            for (int i = 0; i < images.Count; i++)
            {
                images.ElementAt(i).Save(@$"{tempPath}\{i + 1}.Bmp", System.Drawing.Imaging.ImageFormat.Bmp);
            }
            return images.Count;
        }
    }
}
