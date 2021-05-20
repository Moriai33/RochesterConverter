using System;
using System.Collections.Generic;

namespace RochesterConverter.Domain
{
    public class Order
    {
        public string NO { get; }
        public string PO { get; }
        public string CustomerAccount { get; }
        public DateTime OrderDate { get; }
        public IEnumerable<ProductItem> ItemList { get; }
        public double SubTotal { get; }
        public double ShippingCost { get; }
        public double TAX { get; }
        public double Total { get; }


        public Order(string no, string po, string customerAccount, DateTime orderDate, IEnumerable<ProductItem> itemList, double subTotal, double shippingCost, double tax, double total)
        {
            NO = no;
            PO = po;
            CustomerAccount = customerAccount;
            OrderDate = orderDate;
            ItemList = itemList;
            SubTotal = subTotal;
            ShippingCost = shippingCost;
            TAX = tax;
            Total = total;
        }

        public string GetMasPO()
        {
            return PO + " " + NO;
        }
    }
}
