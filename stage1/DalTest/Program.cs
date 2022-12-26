using Dal.DO;
using DalApi;
namespace Dal;
class Program
{
    public static readonly DalList dalList = DalList.GetInstence();
    private static readonly Iproduct dalProduct = dalList.iproduct;
    private static readonly Iorder dalOrder = dalList.iorder;
    private static readonly IorderItem dalOrderItem = dalList.iorderItem;
    private static Product CreateProductFromUser()
    {
        Console.WriteLine("enter the product name:\n");
        string name = Console.ReadLine();
        Console.WriteLine("enter the product price:\n");
        double price = Convert.ToDouble(Console.ReadLine());
        Console.WriteLine($"choose category: enter {(int)eCategory.FRUITS} for FRUITS, {(int)eCategory.SNACKS} for SNACKS,  {(int)eCategory.FROZEN} for FROZEN:\n");
        eCategory category = (eCategory)Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("enter the amount you have from this product in stock\n");
        int instock = Convert.ToInt32(Console.ReadLine());
        Product product = new Product(name, price, category, instock);
        return product;
    }
    private static Product UpdateProductFromUser(Product p )
    {
        Console.WriteLine("enter the product name:\n");
        string newName = Console.ReadLine();
        string name = newName ==""? p.Name:newName;
        Console.WriteLine("enter the product price:\n");
        string priceStr = Console.ReadLine();
        double price = priceStr != "" ? Convert.ToDouble(priceStr) : p.Price;
        Console.WriteLine($"choose category: enter {(int)eCategory.FRUITS} for FRUITS, {(int)eCategory.SNACKS} for SNACKS,  {(int)eCategory.FROZEN} for FROZEN:\n");
        string categorySTR = Console.ReadLine();
        eCategory category = (eCategory)(categorySTR != "" ? (eCategory)Convert.ToInt32(categorySTR) : p.Category);
        Console.WriteLine("enter the amount you have from this product in stock\n");
        string inStockSTR = Console.ReadLine();
        int instock = inStockSTR != "" ? Convert.ToInt32(inStockSTR) : p.InStock;
        Product product = new Product(name, price, category, instock);
        return product;
    }
    private static void ProductMenu()
    {
        int inner_choice = -1;
        while (inner_choice != 0)
        {
            Console.WriteLine("enter 0 to a new choice\n" +
                "enter 1 for creating a new product\n" +
                "enter 2 for presenting a certain product\n" +
                "enter 3 for presenting all the products\n" +
                "enter 4 for updating a product\n" +
                "enter 5 for deleting a product\n ");
            inner_choice = Convert.ToInt32(Console.ReadLine());
            switch ((INNER_CHOICE)inner_choice)
            {
                case INNER_CHOICE.EXIT:
                    return;
                case INNER_CHOICE.CREATE:
                    try
                    {
                        dalProduct.Create(CreateProductFromUser());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    break;
                case INNER_CHOICE.READ:
                    Console.WriteLine("enter the product id:\n");
                    int id = Convert.ToInt32(Console.ReadLine());
                    try
                    {
                        Console.WriteLine((dalProduct.ReadSingle(p=>p.ID==id)));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    break;
                case INNER_CHOICE.READ_ALL:
                    IEnumerable<Product> products = dalProduct.ReadByFilter();
                    foreach (Product p in products)
                    {
                        Console.WriteLine(p.ToString() + "\n");
                    }
                    break;
                case INNER_CHOICE.UPDATE:

                    try
                    {
                        Console.WriteLine("enter the id of the product to update:\n");
                        id = Convert.ToInt32(Console.ReadLine());
                        Product old_p = dalProduct.ReadSingle(p => p.ID == id);
                        Console.WriteLine(old_p.ToString());
                        Product p = UpdateProductFromUser(old_p);
                        p.ID = id;
                        dalProduct.Update(p);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    break;
                case INNER_CHOICE.DELETE:
                    Console.WriteLine("enter the id of the product to delete:\n");
                    try
                    {
                        id = Convert.ToInt32(Console.ReadLine());
                        dalProduct.Delete(id);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    break;
            }
        }
    }
    private static Order CreateOrderFromUser()
    {
        Console.WriteLine("enter the customer name:\n");
        string name = Console.ReadLine();
        Console.WriteLine("enter the customer Email:\n");
        string Email = Console.ReadLine();
        Console.WriteLine($"choose customer address\n");
        string address = Console.ReadLine();
        Console.WriteLine("enter the order date");
        string order_dateSTR = Console.ReadLine();
        DateTime Order_date;
        if (!DateTime.TryParse(order_dateSTR, out Order_date)) throw new Exception("the date is not in the correct format");
        Console.WriteLine("enter the ship date");
        DateTime Ship_Date = Convert.ToDateTime(Console.ReadLine());
        Console.WriteLine("enter the delivery date");
        DateTime Delivery_Date = Convert.ToDateTime(Console.ReadLine());
        Order order = new Order(name, Email, address, Order_date, Ship_Date, Delivery_Date);
        return order;
    }
    private static Order UpdateOrderFromUser(Order o)
    {
        Console.WriteLine("enter the customer name:\n");
        string newName = Console.ReadLine();
        string name = newName == "" ? o.Customer_Name : newName;
        Console.WriteLine("enter the customer Email:\n");
        string EmailStr = Console.ReadLine();
        string Email=EmailStr == ""?o.Customer_Email : EmailStr;
        Console.WriteLine("enter the customer address\n");
        string addressStr = Console.ReadLine();
        string address = addressStr == "" ? o.Customer_Address : addressStr;
        Console.WriteLine("enter the order date");
        string OrderDateSTR = Console.ReadLine();
        DateTime Order_Date = (DateTime)(OrderDateSTR != "" ? Convert.ToDateTime(OrderDateSTR) : o.Order_Date);
        Console.WriteLine("enter the ship date");
        string OrderShipSTR = Console.ReadLine();
        DateTime Ship_Date = (DateTime)(OrderShipSTR != "" ? Convert.ToDateTime(OrderShipSTR): o.Ship_Date);
        Console.WriteLine("enter the delivery date");
        string Delivery_DateSTR = Console.ReadLine();
        DateTime Delivery_Date = (DateTime)(Delivery_DateSTR != "" ? Convert.ToDateTime(Delivery_DateSTR) : o.Delivery_Date);
        Order order = new Order(name, Email, address, Order_Date, Ship_Date, Delivery_Date);
        return order;
    }
    private static void OrderMenu()
    {
        int inner_choice = -1;
        while (inner_choice != 0)
        {
            Console.WriteLine("enter 0 to a new choice\nenter 1 for creating a new order\nenter 2 for presenting a certain order\nenter 3 for presenting all the orders\nenter 4 for updating a order\nenter 5 for deleting a order\nenter 6 for all the items in a certain order\n ");
            inner_choice = Convert.ToInt32(Console.ReadLine());
            switch ((INNER_CHOICE)inner_choice)
            {
                case INNER_CHOICE.EXIT:
                    return;
                case INNER_CHOICE.CREATE:
                    try
                    {
                        dalOrder.Create(CreateOrderFromUser());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    break;
                case INNER_CHOICE.READ:
                    Console.WriteLine("enter the order id:\n");
                    int id = Convert.ToInt32(Console.ReadLine());
                    try
                    {
                        Console.WriteLine((dalOrder.ReadSingle(o=>o.ID==id)));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    break;
                case INNER_CHOICE.READ_ALL:
                    IEnumerable<Order> orders = dalOrder.ReadByFilter();
                    foreach (Order o in orders)
                    {
                        Console.WriteLine(o + "\n");
                    }
                    break;
                case INNER_CHOICE.UPDATE:
                    try
                    {
                        Console.WriteLine("enter the id of the order to update:\n");
                        id = Convert.ToInt32(Console.ReadLine());
                        Order old_order = dalOrder.ReadSingle(o => o.ID == id);
                        Console.WriteLine(old_order);
                        Order o = UpdateOrderFromUser(old_order);
                        o.ID = id;
                        dalOrder.Update(o);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    break;
                case INNER_CHOICE.DELETE:
                    Console.WriteLine("enter the id of the order to delete:\n");
                    try
                    {
                        id = Convert.ToInt32(Console.ReadLine());
                        dalOrder.Delete(id);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    break;
                case INNER_CHOICE.ITEMS_IN_ORDER:
                    Console.WriteLine("enter an order ID:\n");
                    id = Convert.ToInt32(Console.ReadLine());
                    try
                    {
                        IEnumerable<OrderItem> orderItems = dalOrder.ProductsInOrder(id);
                        foreach (OrderItem oi in orderItems)
                        {
                            Console.WriteLine(oi + "\n");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    break;
            }
        }
    }
    private static OrderItem CreateOrderItemFromUser()
    {
        Console.WriteLine("enter the product id:\n");
        int product_id = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("enter the order id:\n");
        int order_id = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("enter the amount of items");
        int num_items = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("enter the price of item");
        double price = Convert.ToDouble(Console.ReadLine());
        OrderItem oi = new OrderItem(order_id, product_id, price, num_items);
        return oi;
    }
    private static OrderItem UpdateOrderItemFromUser(OrderItem oi)
    {
        Console.WriteLine("enter the product id:\n");
        string pID_STR = Console.ReadLine();
        int product_id = pID_STR!=""?Convert.ToInt32(pID_STR):oi.Product_ID;
        Console.WriteLine("enter the order id:\n");
        string oID_STR = Console.ReadLine();
        int order_id = oID_STR!=""?Convert.ToInt32(oID_STR):oi.Order_ID;
        Console.WriteLine("enter the amount of items");
        string num_itemsSTR = Console.ReadLine();
        int num_items = num_itemsSTR!=""? Convert.ToInt32(num_itemsSTR):oi.Product_Amount;
        Console.WriteLine("enter the price of item");
        string priceSTR = Console.ReadLine();
        double price = priceSTR!=""? Convert.ToDouble(priceSTR):oi.Product_Price;
        OrderItem New_OI = new OrderItem(order_id, product_id, price, num_items);
        return New_OI;
    }
    private static void OrderItemMenu()
    {
        int inner_choice = -1;
        while (inner_choice != 0)
        {
            Console.WriteLine("enter 0 to a new choice\nenter 1 for creating a new orderItem\nenter 2 for presenting an certain orderItem\nenter 3 for presenting all the orderItems\nenter 4 for updating an orderItem\nenter 5 for deleting an orderItem\nenter 6 for reading an item by order id and product id\n ");
            inner_choice = Convert.ToInt32(Console.ReadLine());
            switch ((INNER_CHOICE)inner_choice)
            {
                case INNER_CHOICE.EXIT:
                    return;
                case INNER_CHOICE.CREATE:
                    try
                    {
                        dalOrderItem.Create(CreateOrderItemFromUser());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    break;
                case INNER_CHOICE.READ:
                    Console.WriteLine("enter the orderItem id:\n");
                    int id = Convert.ToInt32(Console.ReadLine());
                    try
                    {
                        Console.WriteLine((dalOrderItem.ReadSingle(oi => oi.OrderItem_ID == id)));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    break;
                case INNER_CHOICE.READ_ALL:
                    IEnumerable<OrderItem> orderItems = dalOrderItem.ReadByFilter();
                    foreach (OrderItem oi in orderItems)
                    {
                        Console.WriteLine(oi + "\n");
                    }
                    break;
                case INNER_CHOICE.UPDATE:
                    try
                    {
                        Console.WriteLine("enter the id of the orderItem to update:\n");
                        id = Convert.ToInt32(Console.ReadLine());
                        OrderItem old_oi = dalOrderItem.ReadSingle(oi => oi.OrderItem_ID == id);
                        Console.WriteLine(old_oi);
                        OrderItem oi = UpdateOrderItemFromUser(old_oi);
                        oi.OrderItem_ID = id;
                        dalOrderItem.Update(oi);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    break;
                case INNER_CHOICE.DELETE:
                    Console.WriteLine("enter the id of the orderItem to delete:\n");
                    try
                    {
                        id = Convert.ToInt32(Console.ReadLine());
                        dalOrderItem.Delete(id);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    break;
                case INNER_CHOICE.ITEM_BY_ORDER_PRODUCT:
                    Console.WriteLine("enter an order ID:\n");
                    int order_id = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("enter a product ID:\n");
                    int product_id = Convert.ToInt32(Console.ReadLine());
                    try
                    {
                        OrderItem oi = dalOrderItem.Read_item_by_product_order(order_id, product_id);
                        Console.WriteLine(oi);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    break;
            }
        }
    }
    public static void Main()
    {
        int out_choice;
        do
        {
            Console.WriteLine("enter 0 to stop the program\nenter 1 for product\nenter 2 for order\nenter 3 for order item\n");
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
                case OUT_CHOICE.ORDER_ITEM:
                    OrderItemMenu();
                    break;
            }
        } while (out_choice != 0);
    }
}