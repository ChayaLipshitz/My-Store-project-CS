﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;
public class Cart
{
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerAddress { get; set; }
    public OrderItem Items { get; set; }
    public double TotalPrice { get; set; }
    public override string ToString()
    {
        return $"CustomerName: {CustomerName}\n" +
            $"CustomerEmail: {CustomerEmail}\n" +
            $"CustomerAddress: {CustomerAddress}\n" +
            $"Items: {Items}\n" +
            $"TotalPrice: {TotalPrice}\n\n";
    }
}