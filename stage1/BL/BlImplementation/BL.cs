using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
namespace BlImplementation;
public class Bl : IBl
{
    public IOrder iOrder => new BlOrder();

    public IProduct iProduct => new BlProduct();

    public ICart iCart => new BlCart();
}
