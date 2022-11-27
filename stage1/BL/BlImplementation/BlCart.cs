
using BlApi;
using BO;
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

    public void SubmitOrder(Cart cart, string CustomerName, string CustomerEmail, string CustomerAddress)
    {
        throw new NotImplementedException();
    }

    public void UpdateOrderItem(Cart cart, int id, int quantity)
    {
        throw new NotImplementedException();
    }
}