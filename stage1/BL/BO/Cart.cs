using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;
public class Cart
{
    public string? CustomerName { get; set; }
    public string? CustomerEmail { get; set; }
    public string? CustomerAddress { get; set; }
    public List<OrderItem?>? Items { get; set; } = new();
    public double? TotalPrice { get; set; }
    public override string ToString()
    {
        string items = "";
        foreach (OrderItem oi in Items)
        {
            items += $"{oi}\n";
        }
        return $"CustomerName: {CustomerName}\n" +
            $"CustomerEmail: {CustomerEmail}\n" +
            $"CustomerAddress: {CustomerAddress}\n" +
            $"Items:\n{items}\n" +
            $"TotalPrice: {TotalPrice}\n";
    }
}