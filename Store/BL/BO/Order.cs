using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;
public class Order
{
    public int OrderID { get; set; }
    public string? CustomerName { get; set; }
    public string? CustomerEmail { get; set; }
    public string? CustomerAddress { get; set; }
    public DateTime? Order_Date { get; set; }
    public eOrderStatus? Status { get; set; }
    //public DateTime PaymentDate { get; set; }
    public DateTime? Ship_Date { get; set; }
    public DateTime? Delivery_Date { get; set; }
    public List<OrderItem?>? Items { get; set; } = new();
    public double? TotalPrice { get; set; }

    public override string ToString()
    {
        string items="";
        foreach(OrderItem oi in Items)
        {
            items += $"{oi}\n";
        }
        return $"OrderID: {OrderID}\n" +
           $"CustomerName: {CustomerName}\n" +
           $"CustomerEmail: {CustomerEmail}\n" +
           $"CustomerAddress: {CustomerAddress}\n" +
           $"OrderDate: {Order_Date}\n" +
           $"Status: {Status}\n" +
           $"ShipDate: {Ship_Date}\n" +
           $"DeliveryDate: {Delivery_Date}\n" +
           $"Items:\n{items}\n" +
           $"TotalPrice: {TotalPrice}\n\n";
    }
}
