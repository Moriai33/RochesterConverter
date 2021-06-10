using RochesterConverter.Application.Interface;
using RochesterConverter.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace RochesterConverter.Application
{
    internal class ValidateService : IValidateService
    {
        private readonly List<Error> errorList;
        private readonly Dictionary<int, Func<string, bool>> _functionList;
        private Dictionary<int, string> _errorsText = new Dictionary<int, string>()
            {
                { 2, "Not a valid date" },
                { 3, "The Customer length must be 7" },
                { 5, "The UDF doc length must be 10" },
                { 6, "The MAS PO length must be 15" },
                { 7, "The UDF PO length must be 15" },
                { 8, "Item code length must be 10" },
                { 9, "Qty must be number except zero" }
            };
        public ValidateService()
        {
            errorList = new List<Error>();
            _functionList = new Dictionary<int, Func<string, bool>>()
            {
                { 2, ValidateOrderDate },
                { 3, ValidateCustomer },
                { 5, ValidateUDF },
                { 6, ValidateMassPO },
                { 7, ValidateUdfPO },
                { 8, ValidateItemCode },
                { 9, ValidateQty }
            };
        }
        public List<Error> GetErrors(IEnumerable<IEnumerable<string>> listViewStringList)
        {
            errorList.Clear();
            errorList.Add(new Error("", 0, 0));

            if (!ValidateCustomer(listViewStringList.ElementAt(1).ElementAt(3)))
                errorList.Add(new Error($"- Customer ID error",1,3));
            if (!ValidateOrderDate(listViewStringList.ElementAt(1).ElementAt(2)))
                errorList.Add(new Error($"- Order date error",1,2));
            if (!ValidateMassPO(listViewStringList.ElementAt(1).ElementAt(6)))
                errorList.Add(new Error($"- MAS PO ID error", 1, 6));
            if (!ValidateUdfPO(listViewStringList.ElementAt(1).ElementAt(7)))
                errorList.Add(new Error($"- UDF PO ID error",1,7));
            if (!ValidateUDF(listViewStringList.ElementAt(1).ElementAt(5)))
                errorList.Add(new Error($"- UDF ID error",1,5));

            for (int i = 2; i < listViewStringList.Count(); i++)
            {
                if (!ValidateItemCode(listViewStringList.ElementAt(i).ElementAt(8)))
                    errorList.Add(new Error($"- Item code error in row: {listViewStringList.ElementAt(i).ElementAt(0)}",i,8));
                if (!ValidateQty(listViewStringList.ElementAt(i).ElementAt(9)))
                    errorList.Add(new Error($"- Qty error in row: {listViewStringList.ElementAt(i).ElementAt(0)}",i,9));
            }
            return errorList;
        }
        public bool ValidateItemCode(string text)
        {
            return text.Length == 10;
        }
        public bool ValidateQty(string text)
        {
            return int.TryParse(text, out var number) && number != 0;
        }
        public bool ValidateMassPO(string text)
        {
            return text.Length == 15;
        }
        public bool ValidateUdfPO(string text)
        {
            return text.Length == 15;
        }
        public bool ValidateCustomer(string text)
        {
            return text.Length == 7;
        }
        public bool ValidateOrderDate(string text)
        {
            return (DateTime.TryParse(text, new CultureInfo("en-US"), 0, out var date) && date.Year > 2000);
        }
        public bool ValidateUDF(string text)
        {
            return text.Length == 10;
        }
        public bool ValidateByIndex(int index, string text)
        {
            return _functionList.FirstOrDefault(x => x.Key == index).Value(text);
        }
        public string GetErrorMessageTextByIndex(int index)
        {
            return _errorsText.FirstOrDefault(x => x.Key == index).Value;
        }
    }
}

