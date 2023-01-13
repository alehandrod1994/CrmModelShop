using System;
using System.Collections.Generic;

namespace CrmBl.Model
{
    public class Seller
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Check> Checks { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
