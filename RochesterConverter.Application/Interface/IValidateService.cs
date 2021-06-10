using RochesterConverter.Domain;
using System.Collections.Generic;
using System.Reflection;

namespace RochesterConverter.Application.Interface
{
    public interface IValidateService
    {
        List<Error> GetErrors(IEnumerable<IEnumerable<string>> listViewStringList);
        bool ValidateItemCode(string text);
        bool ValidateQty(string text);
        bool ValidateMassPO(string text);
        bool ValidateUdfPO(string text);
        bool ValidateCustomer(string text);
        bool ValidateOrderDate(string text);
        bool ValidateUDF(string text);
        bool ValidateByIndex(int index, string text);
        string GetErrorMessageTextByIndex(int index);
    }
}
