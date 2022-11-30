using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
namespace BlImplementation;
public class BL : IBl
{
    public IOrder Order => new BlOrder();

    public IProduct Product => new BlProduct();

    public ICart Cart => new BlCart();
}
