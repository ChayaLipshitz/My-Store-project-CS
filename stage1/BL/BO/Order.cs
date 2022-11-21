using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;
public class Order
{
    public int OrderID { get; set; }
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerAddress { get; set; }
    public DateTime OrderDate { get; set; }
    public eOrderStatus Status { get; set; }
    public DateTime PaymentDate { get; set; }
    public DateTime ShipDate { get; set; }
    public DateTime DeliveryDate { get; set; }
    public OrderItem Items { get; set; }
    public double TotalPrice { get; set; }

    public override string ToString()
    {
        return $"OrderID: {OrderID}\n" +
            $"CustomerName: {CustomerName}\n" +
            $"CustomerEmail: {CustomerEmail}\n" +
            $"CustomerAddress: {CustomerAddress}\n" +
            $"OrderDate: {OrderDate}\n" +
            $"Status: {Status}\n" +
            $"PaymentDate: {PaymentDate}\n" +
            $"ShipDate: {ShipDate}\n" +
            $"DeliveryDate: {DeliveryDate}\n" +
            $"Items: {Items}\n" +
            $"TotalPrice: {TotalPrice}\n\n";
    }
}
