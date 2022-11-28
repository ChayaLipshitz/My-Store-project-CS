
using BlApi;
using Dal;
using DalApi;
namespace BlImplementation;
internal class BlCart:ICart
{
    IDal dal = new DalList();
    public BO.Cart addOrderItem(BO.Cart cart, int id)
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
                        cart.TotalPrice += p.Price;
                    }
                    return cart;
                }
                else
                    throw new BO.NotInStockException($"The product {p.Name} is not in stock");
            };
            //creating a new OrderItem:
            if (p.InStock > 0)
            {
                BO.OrderItem OItem = new BO.OrderItem();
                //OItem.ID = Dal.DO.data;
                OItem.ProductID = p.ID;
                OItem.Name = p.Name;
                OItem.Price = p.Price;
                OItem.Amount = 1;
                OItem.TotalPrice = p.Price;
                cart.TotalPrice += p.Price;
                cart.Items.Add(OItem);
            }
            else
                throw new BO.NotInStockException($"The product {p.Name} is not in stock");
            return cart;
        }
        catch (Dal.DO.NotExistExceptions e)
        {
            throw new BO.NotExistExceptions();////////
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
    public void SubmitOrder(BO.Cart cart, string CustomerName, string CustomerEmail, string CustomerAddress)
    {
        IsValidCart(cart, CustomerName, CustomerEmail, CustomerAddress); //check valiation of cart and customer details
        Dal.DO.Order newOrder;
        foreach(BO.OrderItem OItem in cart.Items)
        {
            ////////
        }
    }
    public void IsValidCart(BO.Cart cart, string CustomerName, string CustomerEmail, string CustomerAddress)
    {
        IDal dal = new DalList();
        foreach (BO.OrderItem OItem in cart.Items)
        {
            try
            {
                Dal.DO.Product product = dal.iproduct.Read(OItem.ID);
                if (OItem.Amount > 0)
                {
                    throw new BO.NotInStockException($"The quantity of the product {product.Name} ordered is 0");
                }
                if (OItem.Amount > product.InStock)
                {
                    throw new BO.NotInStockException($"The product {product.Name} is not in stock");
                }
            }
            catch (Dal.DO.NotExistExceptions)
            {
                throw new BO.NotExistExceptions();
            }
        }

        if (!IsValidEmail(CustomerEmail))
        {
            throw new BO.EmailIsNotValidException();
        }
        if (CustomerAddress == "") 
            throw new BO.CustomerDetailsAreUnknown("Customer address is unknown");
        if (CustomerName == "")
            throw new BO.CustomerDetailsAreUnknown("Customer name is unknown");
        if (CustomerEmail == "")
            throw new BO.CustomerDetailsAreUnknown("Customer Email is unknown");
    } 
    bool IsValidEmail(string email)
    {
        var trimmedEmail = email.Trim();

        if (trimmedEmail.EndsWith("."))
        {
            return false; // suggested by @TK-421
        }
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == trimmedEmail;
        }
        catch
        {
            return false;
        }

    }
}