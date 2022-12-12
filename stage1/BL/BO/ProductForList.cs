using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;
public class ProductForList
{
    public int ID { get; set; }
    public string? Name { get; set; }
    public double Price { get; set; }
    public eCategory? Category { get; set; }
    public override string ToString()
    {
        return $"ID: {ID}\n" +
            $"Name: {Name}\n" +
            $"Price: {Price}\n" +
            $"Category: {Category}\n\n";
    }

}
