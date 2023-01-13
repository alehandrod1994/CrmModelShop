using System;

namespace CrmBl.Model
{
    public class Sell
    {
        public int ID { get; set; }
        public int CheckID { get; set; }
        public int ProductID { get; set; }
        public virtual Check Check { get; set; }
        public virtual Product Product { get; set; }
    }
}
