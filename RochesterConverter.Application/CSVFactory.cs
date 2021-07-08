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
        private readonly IValidateService _validateService;

        public CSVFactory(IImageProcesser imageProcesser, IFileOperations fileOperations, IValidateService validateService)
        {
            _imageProcesser = imageProcesser;
            _fileOperations = fileOperations;
            _validateService = validateService;
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
            var itemList = ConvertOcrStringLineListToProductItemList(OcrStringLineList);
            var account = GetAccount(OcrStringLineList);
            var created = GetCreated(OcrStringLineList);
            var NO = GetNO(OcrStringLineList);
            var PO = GetPO(OcrStringLineList);
            var shipping = GetShipping(OcrStringLineList);
            var tax = GetTax(OcrStringLineList);
            var total = GetTotal(OcrStringLineList);
            var subTotal = GetSubTotal(OcrStringLineList);

            var order = new Order(NO, PO, account, created, itemList, subTotal, StringToConverterDouble(shipping, new CultureInfo("en-US")), StringToConverterDouble(tax, new CultureInfo("en-US")), total);
            return order;
        }

        private double GetTotal(List<string> OcrStringLineList)
        {
            double total = double.MinValue;
            var totalList = OcrStringLineList.Where(x => x.ToUpper().Contains("TOTAL"));
            if (totalList.Count() > 1)
            {
                total = StringToConverterDouble(SeparatedSpaces(totalList.ElementAt(1)).Trim(_trimCharArray), new CultureInfo("en-US"));
            }

            return total;
        }
        private double GetSubTotal(List<string> OcrStringLineList)
        {
            double subTotal = double.MinValue;
            var totalList = OcrStringLineList.Where(x => x.ToUpper().Contains("TOTAL"));
            if (totalList.Count() > 1)
            {
                subTotal = StringToConverterDouble(SeparatedSpaces(totalList.ElementAt(0)).Trim(_trimCharArray), new CultureInfo("en-US"));
            }
            return subTotal;
        }

        private string GetTax(List<string> OcrStringLineList)
        {
            var tax = OcrStringLineList.FirstOrDefault(x => x.ToUpper().Contains("TAX"));
            tax = SeparatedSpaces(tax);
            if (tax != null)
                tax = tax.Trim(_trimCharArray);

            return tax;
        }

        private string GetShipping(List<string> OcrStringLineList)
        {
            var shipping = OcrStringLineList.FirstOrDefault(x => x.ToUpper().Contains("SHIPPING"));
            shipping = SeparatedSpaces(shipping);
            if (shipping != null)
                shipping = shipping.Trim(_trimCharArray);

            return shipping;
        }

        private DateTime GetCreated(List<string> OcrStringLineList)
        {
            var created = OcrStringLineList.FirstOrDefault(x => x.ToUpper().Contains("CREATED"));
            created = SeparatedSpaces(created);
            DateTime.TryParse(created, new CultureInfo("en-US"), 0, out var createdDateTime);

            return createdDateTime;
        }

        private string GetAccount(List<string> OcrStringLineList)
        {
            var account = OcrStringLineList.FirstOrDefault(x => x.ToUpper().Contains("ACCOUNT"));
            account = SeparatedHyphen(account).Split(" ").ElementAt(0);

            return account;
        }

        private string GetPO(List<string> OcrStringLineList)
        {
            var PO = OcrStringLineList.FirstOrDefault(x => x.ToUpper().Contains("PO"));
            var stringArrayPO = PO.ToUpper().Split("PO:");
            if (stringArrayPO.Length > 1)
            {
                PO = SeparatedSpaces(stringArrayPO.ElementAt(1));
            }
            else
            {
                PO = SeparatedSpaces(PO);
            }

            return PO;
        }

        private string GetNO(List<string> OcrStringLineList)
        {
            var NO = OcrStringLineList.FirstOrDefault(x => x.ToUpper().Contains("NO"));
            var stringArrayNO = NO.ToUpper().Split("NO:");
            if (stringArrayNO.Length > 1)
            {
                NO = SeparatedSpaces(stringArrayNO.ElementAt(1));
            }
            else
            {
                NO = SeparatedSpaces(NO);
            }

            return NO;
        }

        public void SaveCSV(string path, string data)
        {
            _fileOperations.SaveCSV(path, data);
        }

        private List<ProductItem> ConvertOcrStringLineListToProductItemList(List<string> OcrStringLineList)
        {
            var productItemList = new List<ProductItem>();
            var itemListData = ConvertOcrStringLineListToItemListData(OcrStringLineList);
            int index = 1;
            foreach (var shortedString in itemListData)
            {
                var opc = string.Empty;
                var description = string.Empty;
                if (shortedString.Length == 0) 
                    continue;

                if (shortedString.Length > 6)
                {
                    var id = StringToIntConverter(shortedString[0]);
                    //var id = int.TryParse(shortedString[0], out var parsedId) ? parsedId : 0;
                    var lre = shortedString[shortedString.Length - 7];

                    var orderNumber = StringToIntConverter(shortedString[shortedString.Length - 6]);
                    var rcv = StringToIntConverter(shortedString[shortedString.Length - 5]);
                    var bo = StringToIntConverter(shortedString[shortedString.Length - 4]);
                    var unitCost = StringToConverterDouble(shortedString[shortedString.Length - 3].Trim(_trimCharArray), new CultureInfo("en-US"));
                    var orderCost = StringToConverterDouble(shortedString[shortedString.Length - 2].Trim(_trimCharArray), new CultureInfo("en-US"));
                    var rcvCost = StringToConverterDouble(shortedString[shortedString.Length - 1].Trim(_trimCharArray), new CultureInfo("en-US"));

                    if (shortedString[1].Contains("."))
                        shortedString[1] = shortedString[1].Replace(".", "");

                    shortedString[1] = shortedString[1].Replace(" ", "");
                    if (_validateService.ValidateItemCode(shortedString[1]))
                        opc = shortedString[1];

                    for (int i = 2; i < shortedString.Length - 7; i++)
                    {
                        description += shortedString[i] + " ";
                    }
                    var productItem = new ProductItem(id, opc, description, lre, orderNumber, rcv, bo, unitCost, orderCost, rcvCost);
                    index = id+1;
                    productItemList.Add(productItem);
                }
                else
                {
                    shortedString[0]= shortedString[0].Replace(" ", "");
                    if (_validateService.ValidateItemCode(shortedString[0]))
                    {
                        productItemList.Add(new ProductItem(index, shortedString[0], "", "", 0, 0, 0, 0, 0, 0));
                    }
                    else
                    {
                        productItemList.Add(new ProductItem(index, "", "", "", 0, 0, 0, 0, 0, 0));
                    }

                    index++;
                }
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
                        else if (_validateService.ValidateItemCode(shortedString))
                        {
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
                var stringArray = text.Split("-");
                if (stringArray.Length > 1)
                {
                    text = stringArray.ElementAt(1);
                }
            }
            return text;
        }
        public string SeparatedSpaces(string text)
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                var stringArray = text.Split(" ");
                if (stringArray.Length > 1)
                {
                    text = stringArray.ElementAt(1);
                }
            }
            return text;
        }
    }
}
