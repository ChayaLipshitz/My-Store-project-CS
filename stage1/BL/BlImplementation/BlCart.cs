
using BlApi;
using Dal;
using DalApi;
namespace BlImplementation;
internal class BlCart:ICart
{
    IDal Dal = new DalList();

    public void addOrderItem(BO.Cart cart, int id)
    {
        foreach (BO.OrderItem item in cart.Items)
        {
            if (item.ID == id)
            {

            }
        }
    }
}