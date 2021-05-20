using RochesterConverter.Application.Interface;
using System.Collections.Generic;
using System.IO;
using Tesseract;

namespace RochesterConverter.Infrastructure
{
    internal class ImageProcesser : IImageProcesser
    {
        public List<string> OCRImages(int imagesNumber)
        {
            List<string> OcrStringLineList = new List<string>();
            string tempPath = Path.GetTempPath();
            for (int i = 0; i < imagesNumber; i++)
            {
                using (var objOcr = new TesseractEngine(@".\TessData", "eng", EngineMode.Default))
                {
                    var img = Pix.LoadFromFile($@"{tempPath}\{i + 1}.Bmp");
                    Page page = objOcr.Process(img);
                    var pageString = page.GetText().Split("\n");
                    foreach (var line in pageString)
                    {
                        OcrStringLineList.Add(line);
                    }
                }
            }
            return OcrStringLineList;
        }
    }
}
