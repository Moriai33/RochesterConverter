using RochesterConverter.Application.Interface;
using RochesterConverter.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace RochesterConverter.Application
{
    internal class ValidateService : IValidateService
    {
        private readonly List<Error> errorList;
        public ValidateService()
        {
            errorList = new List<Error>();
          
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
                errorList.Add(new Error($"- MAS and UDF PO ID error",1,6));
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
    }
}

