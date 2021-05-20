using System.Collections.Generic;

namespace RochesterConverter.Application.Interface
{
    public interface IImageProcesser
    {
        List<string> OCRImages(int imagesNumber);
    }
}
