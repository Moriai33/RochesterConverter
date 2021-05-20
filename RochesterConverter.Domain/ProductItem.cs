namespace RochesterConverter.Domain
{
    public class ProductItem
    {
        public int ID { get; }
        public string OPC { get; }
        public string Description { get; }
        public string LRE { get; }
        public int OrderNumber { get; }
        public int RCV { get; }
        public int BO { get; }
        public double UnitCost { get; }
        public double OrderCost { get; }
        public double RCVCost { get; }


        public ProductItem(int id, string opc, string description, string lre, int orderNumber, int rcv, int bo, double unitCost, double orderCost, double rcvCost)
        {
            ID = id;
            OPC = opc;
            Description = description;
            LRE = lre;
            OrderNumber = orderNumber;
            RCV = rcv;
            BO = bo;
            UnitCost = unitCost;
            OrderCost = orderCost;
            RCVCost = rcvCost;
        }
    }
}
