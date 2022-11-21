using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class OrderForList
    {
        public int ID { get; set; }
        public string CustomerName { get; set; }
        public int AmountOfItems { get; set; }
        public eOrderStatus Status { get; set; }
        public double TotalPrice { get; set; }

    }
}
