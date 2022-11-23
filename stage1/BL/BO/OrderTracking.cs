using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;
public class OrderTracking
{
    public int ID { get; set; }
    public eOrderStatus Status { get; set; }
    public List<(DateTime,eOrderStatus)> dateAndStatus { get; set; }
    public override string ToString()
    {
        return $"ID: {ID}\n" +
            $"Status: {Status}\n\n";
    }
}
