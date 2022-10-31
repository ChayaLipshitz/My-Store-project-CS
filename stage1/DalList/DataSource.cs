using Dal.DO;
using System;

namespace DalList;

static internal class DataSource
{
    static readonly Random random = new Random();
    static internal Product[] ProductsArr = new Product[50];
    static internal Order[] OrdersArr = new Order[100];
    static internal OrderItem[] OrderItemsArr = new OrderItem[200];

    static int num_orders = 50;
    static DataSource()
    {
        s_Initialize();
    }
    static internal class Config
    {
        static internal int product_index = 0;
        static internal int order_index = 0;
        static internal int orderItem_index = 0;
        static private int order_ID = 1;
        static private int product_ID = 1;
        static private int orderItem_ID = 1;
        static public int Order_ID { get { return order_ID++; } }
        static public int Product_ID { get { return order_ID++; } }
        static public int OrderItem_ID { get { return order_ID++; } }

    }
    static private void CreateProductsArr()
    {
        string[] productsName = { "apples", "doritos", "potatos" };
        for (int i = 0; i < 10; Config.product_index++, i++)
        {
            ProductsArr[i] = new Product();
            int nameIndex = (int)rand.NextInt64(productsName.Length);
            int price = (int)rand.NextInt64(1, 100);
            ProductsArr[i].Product_Name = productsName[nameIndex];
            ProductsArr[i].Product_Price = price;
            ProductsArr[i].Product_Instock = (int)rand.NextInt64(200);
            int x = 1;
            ProductsArr[i].Product_Category = (eCategory)x;
            ProductsArr[i].Product_ID = Config.Product_ID;

        }
    }
    static private void CreateOrdersArr()
    {
        string[] customers_Name = { "aaa", "bbb", "ccc" };
        string[] Customer_Email = { "d", "e", "f" };
        string[] Customer_Address = { "h", "i", "j" };

        for (int i = 0; i < 10; Config.order_index++, i++)
        {
            OrdersArr[i] = new Order();
            int nameIndex = (int)rand.NextInt64(customers_Name.Length);
            int emailIndex = (int)rand.NextInt64(Customer_Email.Length);
            int addressIndex = (int)rand.NextInt64(Customer_Address.Length);

            OrdersArr[i].Customer_Name = customers_Name[nameIndex];
            OrdersArr[i].Customer_Email = Customer_Email[emailIndex];
            OrdersArr[i].Customer_Address = Customer_Address[addressIndex];
            OrdersArr[i].Order_ID = Config.Order_ID;
            OrdersArr[i].Order_Date = DateTime.MinValue;
            TimeSpan shipDelay = TimeSpan.FromDays(2);
            TimeSpan deliveryDelay = TimeSpan.FromDays(2);
            OrdersArr[i].Ship_Date = OrdersArr[i].Order_Date + shipDelay;
            OrdersArr[i].Delivery_Date = OrdersArr[i].Ship_Date + deliveryDelay;
        }
    }

    static private void CreateOrderItemsArr()
    {
        for (int i = 0; i < 10; Config.orderItem_index++, i++)
        {
            OrderItemsArr[i] = new OrderItem();
            int price = (int)rand.NextInt64(1, 100);
            int amount = (int)rand.NextInt64(1, 20);
            OrderItemsArr[i].Product_Price = price;
            OrderItemsArr[i].Product_Amount = amount;
            OrderItemsArr[i].OrderItem_ID = Config.OrderItem_ID;
            int productIndex = (int)rand.NextInt64(Config.product_index);
            int orderIndex = (int)rand.NextInt64(Config.order_index);
            OrderItemsArr[i].Product_ID = ProductsArr[productIndex].Product_ID;
            OrderItemsArr[i].Order_ID = OrdersArr[orderIndex].Order_ID;
        }
    }
    static private void s_Initialize()
    {
        CreateProductsArr();
        CreateOrdersArr();
        CreateOrderItemsArr();
    }
}

/////////////////////////////////////////////////////////////////////////////
/// <summary>
/// =======================================================================
/// </summary>
