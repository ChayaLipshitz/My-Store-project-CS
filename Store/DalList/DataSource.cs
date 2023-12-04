using Dal.DO;
using System.Net;
using System.Threading;

namespace Dal;
public class DataSource
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
        string[] productsName = { "Apples", "Doritos", "Potatos",
        "Grain", "Wheat", "Rye", "Oats", "Corn", "Rice", "Bakery goods", "Bread", "Rolls", "Cakes", "Oookies", "Pies",
         "White bread", "Whole-wheat bread", "Rye bread","buns","Sesame roll", "Cinnamon roll", "Hamburger bun", "Cookies and crackers" };

        for (int i = 0; i < 50; i++)
        {
            Product product = new Product();
            int nameIndex = (int)rand.NextInt64(productsName.Length);
            int price = (int)rand.NextInt64(1, 100);
            product.Name = productsName[nameIndex];
            product.Price = price;
            product.InStock = (int)rand.NextInt64(200);
            int x = (int)rand.NextInt64(3);
            product.Category = (eCategory)x;
            product.ID = Config.Product_ID;
            ProductsList.Add(product);
        }
    }
    static private void CreateOrdersList()
    {
        string[] customers_Name = { "Reuven", "Shimon", "Levi" };
        string[] Customer_Email = { "Reuven@Reuven", "Shimon@Shimon", "Levi@Levi" };
        string[] Customer_Address = { "Reuven st.", "Shimon st.", "Levi st." };

        for (int i = 0; i < 100; i++)
        {
            Order order = new Order();
            int nameIndex = (int)rand.NextInt64(customers_Name.Length);
            int emailIndex = (int)rand.NextInt64(Customer_Email.Length);
            int addressIndex = (int)rand.NextInt64(Customer_Address.Length);
            order.Customer_Name = customers_Name[nameIndex];
            order.Customer_Email = Customer_Email[emailIndex];
            order.Customer_Address = Customer_Address[addressIndex];
            order.ID = Config.Order_ID;
            order.Order_Date = DateTime.Now;
            order.Ship_Date = DateTime.MinValue;
            order.Delivery_Date = DateTime.MinValue;
            int randShipped = (int)rand.NextInt64(0, 4);
            if (randShipped > 0)
            {
                TimeSpan shipDelay = TimeSpan.FromDays(randShipped * 2);
                order.Ship_Date = order.Order_Date + shipDelay;
                int randDelivered = (int)rand.NextInt64(0, 3);
                if (randDelivered > 0)
                {
                    TimeSpan deliveryDelay = TimeSpan.FromDays(randDelivered * (randShipped * 2) * 4);
                    order.Delivery_Date = order.Order_Date + deliveryDelay;
                }
            }
            OrdersList.Add(order);
        }
    }

    static private void CreateOrderItemsList()
    {
        for (int i = 0; i < 200; i++)
        {
            OrderItem orderItem = new OrderItem();
            orderItem.Product_Price = (int)rand.NextInt64(1, 100);
            orderItem.Product_Amount = (int)rand.NextInt64(1, 20);
            orderItem.OrderItem_ID = Config.OrderItem_ID;
            int productIndex = (int)rand.NextInt64(ProductsList.Count());
            int orderIndex = (int)rand.NextInt64(OrdersList.Count());
            orderItem.Product_ID = ProductsList[productIndex].ID;
            orderItem.Order_ID = OrdersList[orderIndex].ID;
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

