
using BlApi;
using Dal;
using DalApi;
namespace BlImplementation;
internal class BlCart:ICart
{
    IDal dal = new DalList();
    public void addOrderItem(BO.Cart cart, int id)
    {
        try
        {
            Dal.DO.Product p = dal.iproduct.Read(id);
            foreach (BO.OrderItem item in cart.Items)
            {
                if (item.ID == id)
                {
                    //Updating the existing OrderItem
                    if (p.InStock > 0)
                    {
                        item.Amount++;
                        item.TotalPrice += p.Price;
                    }
                    cart.TotalPrice += p.Price;
                    return;
                }
            };
            //creating a new OrderItem:
            BO.OrderItem OItem = new BO.OrderItem();
           //////// OItem.ID = Dal.DO.data;
            OItem.ProductID = p.ID;
            OItem.Name = p.Name;
            OItem.Price = p.Price;
            OItem.Amount = 1;
            OItem.TotalPrice = p.Price;
            cart.TotalPrice += p.Price;
        }catch(Dal.DO.NotExistExceptions e)
        {
            throw new BO.NotExistExceptions();////////
        }
    }

    public void SubmitOrder(BO.Cart cart, string CustomerName, string CustomerEmail, string CustomerAddress)
    {
        foreach(BO.OrderItem OItem in cart.Items)
        {
            if (OItem.Price)
            {
                
            }
        }
    }

    public BO.Cart UpdateOrderItem(BO.Cart cart, int id, int quantity)
    {
        IDal dal = new DalList();
        BO.OrderItem OItem = cart.Items.Find(OItem => OItem.ID == id);
        if (quantity == 0)
        {
            double price = OItem.TotalPrice;
            cart.Items.Remove(OItem);
            cart.TotalPrice -= price;
        }
        else if (OItem.Amount > quantity || OItem.Amount < quantity)
        {
            double lastTotalPrice = OItem.TotalPrice;
            OItem.Amount = quantity;
            OItem.TotalPrice = quantity * OItem.Price;
            cart.TotalPrice = cart.TotalPrice - lastTotalPrice + OItem.TotalPrice;
        }
        return cart;
    }
}