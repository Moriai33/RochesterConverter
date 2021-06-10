using RochesterConverter.Application.Interface;
using RochesterConverter.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace RochesterConverter.Application
{
    internal class CSVFactory : ICSVFactory
    {
        private static readonly char[] _trimCharArray = new char[] { '$', ',' };
        private readonly IImageProcesser _imageProcesser;
        private readonly IFileOperations _fileOperations;

        public CSVFactory(IImageProcesser imageProcesser, IFileOperations fileOperations)
        {
            _imageProcesser = imageProcesser;
            _fileOperations = fileOperations;
        }

        public List<string> OCRImages(int imagesNumber)
        {
            return _imageProcesser.OCRImages(imagesNumber);
        }

        public List<string[]> CreateListViewData(Order order)
        {
            List<string[]> listViewStringList = new List<string[]>
            {
                new string[] { "","Header/Line","Order Date","Cust#","Ship To Code","UDF Doc#","MAS PO#","UDF PO#","Item Code","Qty","Req#","Explode" },
                new string[] { "", "H", $"{order.OrderDate.Month}/{order.OrderDate.Day}/{order.OrderDate.Year}", $"{order.CustomerAccount}", "", $"{order.PO}", $"{order.GetMasPO()}", $"{order.GetMasPO()}", "", "", "", "" }
            };

            for (int i = 0; i < order.ItemList.Count(); i++)
            {
                listViewStringList.Add(new string[] { $"{order.ItemList.ElementAt(i).ID}", "1", "", "", "", "", "", "", $"{order.ItemList.ElementAt(i).OPC}", $"{order.ItemList.ElementAt(i).OrderNumber}", "", "N" });
            }

            return listViewStringList;
        }

        public string CreateCSVData(IEnumerable<IEnumerable<string>> listViewStringList)
        {
            StringBuilder cvsString = new StringBuilder();
            foreach (var itemRow in listViewStringList)
            {
                for (int i = 1; i < itemRow.Count(); i++)
                {
                    cvsString.Append(itemRow.ElementAt(i) + ",");
                }
                cvsString.Remove(cvsString.Length - 1, 1);
                cvsString.Append("\n");
            }
            return cvsString.ToString();
        }

        public Order MakeOrderData(List<string> OcrStringLineList)
        {
            var total = double.MinValue;
            var subTotal = double.MinValue;
            var itemList = ConvertOcrStringLineListToProductItemList(OcrStringLineList);
            var account = OcrStringLineList.FirstOrDefault(x => x.ToUpper().Contains("ACCOUNT"));
            var created = OcrStringLineList.FirstOrDefault(x => x.ToUpper().Contains("CREATED"));
            var NO = OcrStringLineList.FirstOrDefault(x => x.ToUpper().Contains("NO"));
            var PO = OcrStringLineList.FirstOrDefault(x => x.ToUpper().Contains("PO"));
            var tax = OcrStringLineList.FirstOrDefault(x => x.ToUpper().Contains("TAX"));
            var shipping = OcrStringLineList.FirstOrDefault(x => x.ToUpper().Contains("SHIPPING"));
            var totalList = OcrStringLineList.Where(x => x.ToUpper().Contains("TOTAL"));

            account = SeparatedHyphen(account);
            created = SeparatedSpaces(created);
            NO = SeparatedSpaces(NO);
            PO = SeparatedSpaces(PO);
            shipping = SeparatedSpaces(shipping);
            tax = SeparatedSpaces(tax);
            DateTime.TryParse(created, new CultureInfo("en-US"),0, out var createdDateTime);

            if (shipping != null)
                shipping = shipping.Trim(_trimCharArray);

            if (tax != null)
                tax = tax.Trim(_trimCharArray);

            if (totalList.Count() > 0)
            {
                subTotal = StringToConverterDouble(SeparatedSpaces(totalList.ElementAt(0)).Trim(_trimCharArray), new CultureInfo("en-US"));
                total = StringToConverterDouble(SeparatedSpaces(totalList.ElementAt(1)).Trim(_trimCharArray), new CultureInfo("en-US"));
            }

            var order = new Order(NO, PO, account, createdDateTime, itemList, subTotal, StringToConverterDouble(shipping, new CultureInfo("en-US")), StringToConverterDouble(tax, new CultureInfo("en-US")), total);
            return order;
        }

        public void SaveCSV(string path, string data)
        {
            _fileOperations.SaveCSV(path, data);
        }

        private List<ProductItem> ConvertOcrStringLineListToProductItemList(List<string> OcrStringLineList)
        {
            var productItemList = new List<ProductItem>();
            var itemListData = ConvertOcrStringLineListToItemListData(OcrStringLineList);
            foreach (var shortedString in itemListData)
            {
                var opc = string.Empty;
                var description = string.Empty;
                //var id = int.TryParse(shortedString[0], out var parsedId) ? parsedId : 0;
                var id = StringToIntConverter(shortedString[0]);
                var lre = shortedString[shortedString.Length - 7];
                var orderNumber = StringToIntConverter(shortedString[shortedString.Length - 6]);
                var rcv = StringToIntConverter(shortedString[shortedString.Length - 5]);
                var bo = StringToIntConverter(shortedString[shortedString.Length - 4]);
                var unitCost = StringToConverterDouble(shortedString[shortedString.Length - 3].Trim(_trimCharArray), new CultureInfo("en-US"));
                var orderCost = StringToConverterDouble(shortedString[shortedString.Length - 2].Trim(_trimCharArray), new CultureInfo("en-US"));
                var rcvCost = StringToConverterDouble(shortedString[shortedString.Length - 1].Trim(_trimCharArray), new CultureInfo("en-US"));

                if (shortedString[1].Contains("."))
                    shortedString[1] = shortedString[1].Replace(".", "");
                if (shortedString[1].Length == 10 && long.TryParse(shortedString[1], out var _)) 
                    opc = shortedString[1];

                for (int i = 2; i < shortedString.Length - 7; i++)
                {
                    description += shortedString[i] + " ";
                }
                var productItem = new ProductItem(id, opc, description, lre, orderNumber, rcv, bo, unitCost, orderCost, rcvCost);
                productItemList.Add(productItem);
            }
            return productItemList;
        }

        private List<string[]> ConvertOcrStringLineListToItemListData(List<string> OcrStringLineList)
        {
            var stringLineList = new List<string[]>();
            foreach (var shortedString in OcrStringLineList)
            {
                if (shortedString.Length > 0)
                {
                    if (int.TryParse(shortedString.Substring(0, 1), out var _))
                    {
                        var separatedString = shortedString.Split(" ");
                        if (separatedString.Length > 9)
                        {
                            if (shortedString.ToUpper().Contains("500") & shortedString.ToUpper().Contains("FORT"))
                            {
                                continue;
                            }
                            stringLineList.Add(separatedString);
                        }
                    }
                }
            }
            return stringLineList;
        }

        private int StringToIntConverter(string s)
        {
            if (int.TryParse(s, out var result))
            {
                return result;
            }
            else
            {
                return 0;
            }
        }

        private double StringToConverterDouble(string s, CultureInfo cultureInfo)
        {
            var style = NumberStyles.Number | NumberStyles.AllowCurrencySymbol;

            if (double.TryParse(s, style, cultureInfo, out var result))
            {
                return result;
            }
            else
            {
                return 0;
            }
        }
        private string SeparatedHyphen(string text)
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                text = text.Split("-").ElementAt(1);
            }
            return text;
        }
        public string SeparatedSpaces(string text)
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                text = text.Split(" ").ElementAt(1);
            }
            return text;
        }
    }
}
