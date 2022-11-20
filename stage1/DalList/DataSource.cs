using Dal.DO;
namespace Dal;
internal class DataSource
{

    static readonly Random rand = new Random();
    static internal List<Product> ProductsList = new List<Product>();
    static internal List<Order> OrdersList = new List<Order>();
    static internal List<OrderItem> OrderItemsList = new List<OrderItem>();
    static DataSource()
    {
        s_Initialize();
    }
    static public class Config
    {
        static private int order_ID = 111111;
        static private int product_ID = 111111;
        static private int orderItem_ID = 111111;
        static public int Order_ID { get { return order_ID++; } }
        static public int Product_ID { get { return product_ID++; } }
        static public int OrderItem_ID { get { return orderItem_ID++; } }


    }
    static private void CreateProductsList()
    {
        string[] productsName = { "apples", "doritos", "potatos" };
        for (int i = 0; i < 10;i++)
        {
            Product product = new Product();
            int nameIndex = (int)rand.NextInt64(productsName.Length);
            int price = (int)rand.NextInt64(1, 100);
            product.Product_Name = productsName[nameIndex];
            product.Product_Price = price;
            product.Product_Instock = (int)rand.NextInt64(200);
            int x = 1;
            product.Product_Category = (eCategory)x;
            product.Product_ID = Config.Product_ID;
            ProductsList.Add(product);
        }
    }
    static private void CreateOrdersList()
    {
        string[] customers_Name = { "aaa", "bbb", "ccc" };
        string[] Customer_Email = { "d", "e", "f" };
        string[] Customer_Address = { "h", "i", "j" };

        for (int i = 0; i < 10; i++)
        {
            Order order = new Order();
            int nameIndex = (int)rand.NextInt64(customers_Name.Length);
            int emailIndex = (int)rand.NextInt64(Customer_Email.Length);
            int addressIndex = (int)rand.NextInt64(Customer_Address.Length);

            order.Customer_Name = customers_Name[nameIndex];
            order.Customer_Email = Customer_Email[emailIndex];
            order.Customer_Address = Customer_Address[addressIndex];
            order.Order_ID = Config.Order_ID;
            order.Order_Date = DateTime.MinValue;
            TimeSpan shipDelay = TimeSpan.FromDays(2);
            TimeSpan deliveryDelay = TimeSpan.FromDays(2);
            order.Ship_Date = order.Order_Date + shipDelay;
            order.Delivery_Date = order.Ship_Date + deliveryDelay;
            OrdersList.Add(order);
        }
    }

    static private void CreateOrderItemsList()
    {
        for (int i = 0; i < 10; i++)
        {
            OrderItem orderItem = new OrderItem();
            orderItem.Product_Price = (int)rand.NextInt64(1, 100);
            orderItem.Product_Amount = (int)rand.NextInt64(1, 20);
            orderItem.OrderItem_ID = Config.OrderItem_ID;
            int productIndex = (int)rand.NextInt64(ProductsList.Count());
            int orderIndex = (int)rand.NextInt64(OrdersList.Count());
            orderItem.Product_ID = ProductsList[productIndex].Product_ID;
            orderItem.Order_ID = OrdersList[orderIndex].Order_ID;
            OrderItemsList.Add(orderItem);
        }
    }
    static private void s_Initialize()
    {
        CreateProductsList();
        CreateOrdersList();
        CreateOrderItemsList();
    }
}

