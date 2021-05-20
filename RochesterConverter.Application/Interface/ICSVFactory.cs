using RochesterConverter.Domain;
using System.Collections.Generic;

namespace RochesterConverter.Application.Interface
{
    public interface ICSVFactory
    {
        List<string> OCRImages(int imagesNumber);
        List<string[]> CreateListViewData(Order order);
        string CreateCSVData(IEnumerable<IEnumerable<string>> listViewStringList);
        Order MakeOrderData(List<string> OcrStringLineList);
        void SaveCSV(string path, string data);

    }
}
