using BO;
using BlApi;
using BlImplementation;
public class Program
{
    public static  IBl bl = new Bl();
    public static  Cart cart = new Cart();
    public static void Main()
    {
        int out_choice;
        do
        {
            Console.WriteLine("enter 0 to stop the program\nenter 1 for product\nenter 2 for order\nenter 3 for cart\n");
            out_choice = Convert.ToInt32(Console.ReadLine());
            switch ((OUT_CHOICE)out_choice)
            {
                case OUT_CHOICE.EXIT:
                    break;
                case OUT_CHOICE.PRODUCT:
                    ProductMenu();
                    break;
                case OUT_CHOICE.ORDER:
                    OrderMenu();
                    break;
                case OUT_CHOICE.CART:
                    CartMenu();
                    break;
            }
        } while (out_choice != 0);

    }
    public static void ProductMenu()  
    {
        int inner_choice;
        do
        {
            Console.WriteLine("enter 0 to exit\n" +
                "enter 1 to ReadAll products\n" +
                "enter 2 for a certain Product details as a manager \n" +
                "enter 3 for a certain ProductDetails as a client\n" +
                "enter 4 for adding a new product\n");
            inner_choice = Convert.ToInt32(Console.ReadLine());
            try
            {
                switch (inner_choice)
                {
                    case 0:
                        break;
                    case 1:
                        foreach (BO.ProductForList product_for_list in bl.iProduct.ReadAll())
                        {
                            Console.WriteLine(product_for_list);
                            Console.WriteLine('\n');
                        }
                        break;
                    case 2:
                        Console.WriteLine("Enter the product id");
                        int id = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine(bl.iProduct.ProductDetails(id));
                        Console.WriteLine('\n');
                        break;
                    case 3:
                        Console.WriteLine("Enter the product id");
                        id = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine(bl.iProduct.ProductDetails(id,cart));
                        Console.WriteLine('\n');
                        break;
                    case 4:
                        Product newProdct = new Product();
                        Console.WriteLine("enter the product name:\n");
                        newProdct.Name = Console.ReadLine();
                        Console.WriteLine("enter the product price:\n");
                        newProdct.Price = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine($"choose category: enter {(int)eCategory.FRUITS} for {eCategory.FRUITS}, {(int)eCategory.SNACKS} for SNACKS,  {(int)eCategory.FROZEN} for FROZEN:\n");
                        newProdct.Category = (eCategory)Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("enter the amount of product in stock:\n");
                        newProdct.InStock = Convert.ToInt32(Console.ReadLine());
                        bl.iProduct.Add(newProdct);
                        break;
                }
            }
            catch (PropertyInValidException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch(ProductExistsInOrderException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ProductDoesNoExistInCartExceptions ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (CartISEmptyException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (DataError ex)
            {
                Console.WriteLine(ex.Message);
            }
        } while (inner_choice != 0);
    }
    public static void OrderMenu()
    {
        int inner_choice;
        do
        {
            Console.WriteLine("enter 0 o exit\n" +
                "enter 1 to Read All orders\n" +
                "enter 2 for  reading a certain order\n" +
                "enter 3 for a Updateing order shipped\n" +
                "enter 4 for Updateing order deivered\n" +
                "enter 5 for tracking an order\n" ) ;
            inner_choice = Convert.ToInt32(Console.ReadLine());
            try
            {

                switch (inner_choice)
                {
                    case 0:
                        break;
                    case 1:
                        foreach (OrderForList order in bl.iOrder.ReadAll())
                        {
                            Console.WriteLine(order);
                            Console.WriteLine("\n");
                        }
                        break;
                    case 2:
                        Console.WriteLine("enter the order id:\n");
                        int orderID = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine(bl.iOrder.Read(orderID));
                        Console.WriteLine("\n");
                        break;
                    case 3:
                        Console.WriteLine("enter the order id:\n");
                        orderID = Convert.ToInt32(Console.ReadLine());
                        Order updatedShippingOrder = bl.iOrder.UpdateOrderShipped(orderID);
                        break;
                    case 4:
                        Console.WriteLine("enter the order id:\n");
                        orderID = Convert.ToInt32(Console.ReadLine());
                        Order updatedDeliveredOrder = bl.iOrder.UpdateOrderDelivered(orderID);
                        break;
                    case 5:
                        Console.WriteLine("enter the order id:\n");
                        orderID = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine(bl.iOrder.Tracking(orderID));
                        break;

                }
            }
            catch (OrderAlreadyException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (PropertyInValidException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (DataError ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (OrderWasNotShippedException ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        } while (inner_choice != 0);
    }
    public static void CartMenu()
    {
        int inner_choice;
        do
        {
            Console.WriteLine("enter 0 o exit\n" +
                "enter 1 to add a item to cart\n" +
                "enter 2 to update quantity of item in cart\n" +
                "enter 3 for submitting the cart ");
            inner_choice = Convert.ToInt32(Console.ReadLine());
            try
            {
                switch (inner_choice)
                {
                    case 0:
                        break;
                    case 1:
                        Console.WriteLine("enter the product id:\n");
                        int id = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine(bl.iCart.addOrderItem(cart, id));
                        break;
                    case 2:
                        Console.WriteLine("enter the product id:\n");
                        id = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("enter the new quantity\n");
                        int quantity = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine(bl.iCart.UpdateOrderItem(cart, id, quantity));
                        break;
                    case 3:
                        Console.WriteLine("enter your name:");
                        cart.CustomerName = Console.ReadLine();
                        Console.WriteLine("enter your Email:");
                        cart.CustomerEmail = Console.ReadLine();
                        Console.WriteLine("enter your Address:");
                        cart.CustomerAddress = Console.ReadLine();
                        bl.iCart.SubmitOrder(cart, cart.CustomerName, cart.CustomerEmail, cart.CustomerAddress);
                        break;
                }
            }
            catch (NotInStockException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (DataError ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (PropertyInValidException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (NotExistExceptions ex)
            {
                Console.WriteLine(ex.Message);
            }
           
            
        } while (inner_choice != 0);

    }
   
}
