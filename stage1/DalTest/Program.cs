﻿
using Dal.DO;

    int out_choice, inner_choice;
do
    {
    Console.WriteLine("enter 0 to stop the program\nenter 1 for product\nenter 2 for order\nenter 3 for order item\n");
        out_choice = Convert.ToInt32( Console.ReadLine());
    switch (out_choice)
    {
        case 0:
            break;
        case 1:
            Console.WriteLine("enter 0 to a new choice\nenter 1 for creating a new product\nenter 2 for presenting a certain product\nenter 3 for presenting all the products\nenter 4 for updating a product\nenter 5 for deleting a product\n ");
            break;
        case 2:
            break;
        case 3:
            break;
    }

    } while (out_choice != 0) ; 


















///////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////
///


//eOptions choice;
////int innerChoice;
//try
//{
//    do
//    {

//        Console.WriteLine("click 0 to exit, 1 for orders, 2 for orderitems and 3 for products");
//        choice = (eOptions)Convert.ToInt32(Console.ReadLine());
//        switch (choice)
//        {
//            case eOptions.Exit:
//                break;
//            case eOptions.Orders:
//                orderCrud();
//                break;
//            case eOptions.OrderItems:
//                orderItemCrud();
//                break;
//            case eOptions.Products:
//                productCrud();
//                break;
//            default:
//                throw new Exception("invalid choice");

//        }

//    } while (choice != 0);

//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex);
//}

//void productCrud()
//{
//    Random rand = new Random();
//    Product product;
//    bool idExists = false;
//    int productId;
//    int id;
//    Console.WriteLine("click 1 to add a product, 2 to see a specific product, 3 to see all products," +
//        " 4 to change an product's details or 5 to delete an product");
//    innerChoice = Convert.ToInt32(Console.ReadLine());
//    switch (innerChoice)
//    {
//        case (int)eCrudOptions.Create:
//            do
//            {
//                idExists = true;
//                id = (int)rand.NextInt64(100000, 1000000);
//                for (int j = 0; j < DataSource.Config.productIdx; j++)
//                    if (DataSource.productList[j].ID == id)
//                        idExists = false;
//            } while (!idExists);
//            product = ProductProperties(id);
//            DalProduct.CreateProduct(product);
//            break;
//        case (int)eCrudOptions.ReadSingle:
//            Console.WriteLine("enter the id of the product you would like to see");
//            productId = Convert.ToInt32(Console.ReadLine());
//            product = DalProduct.ReadProduct(productId);
//            Console.WriteLine(product);
//            break;
//        case (int)eCrudOptions.ReadAll:
//            Product[] allProducts = DalProduct.ReadProduct();
//            foreach (Product prod in allProducts)
//            { Console.WriteLine(prod); }
//            break;
//        case (int)eCrudOptions.Update:
//            Console.WriteLine("enter the id of the product item you would like to update");
//            productId = Convert.ToInt32(Console.ReadLine());
//            product = ProductProperties(productId);
//            DalProduct.UpdateProduct(product);
//            break;
//        case (int)eCrudOptions.Delete:
//            Console.WriteLine("enter the id of the product you would like to delete");
//            productId = Convert.ToInt32(Console.ReadLine());
//            DalProduct.DeleteProduct(productId);
//            break;
//        default:
//            throw new Exception("invalid choice");
//    }
//}

//void orderCrud()
//{
//    Order order;
//    int orderId;
//    Console.WriteLine("click 1 to add an order, 2 to see a specific order, 3 to see all orders," +
//        " 4 to change an order's details or 5 to delete an order");
//    innerChoice = Convert.ToInt32(Console.ReadLine());
//    switch (innerChoice)
//    {
//        case (int)eCrudOptions.Create:
//            orderId = DalList.DataSource.Config.OrderId;
//            order = OrderProperties(orderId);
//            DalOrder.CreateOrder(order);
//            break;
//        case (int)eCrudOptions.ReadSingle:
//            Console.WriteLine("enter the id of the order you would like to see");
//            orderId = Convert.ToInt32(Console.ReadLine());
//            order = DalOrder.ReadOrder(orderId);
//            Console.WriteLine(order);
//            break;
//        case (int)eCrudOptions.ReadAll:
//            Order[] allOrders = DalOrder.ReadOrder();
//            foreach (Order ord in allOrders)
//            { Console.WriteLine(ord); }
//            break;
//        case (int)eCrudOptions.Update:
//            Console.WriteLine("enter the id of the order you would like to update");
//            orderId = Convert.ToInt32(Console.ReadLine());
//            order = OrderProperties(orderId);
//            DalOrder.UpdateOrder(order);
//            break;
//        case (int)eCrudOptions.Delete:
//            Console.WriteLine("enter the id of the order you would like to delete");
//            orderId = Convert.ToInt32(Console.ReadLine());
//            DalOrder.DeleteOrder(orderId);
//            break;
//        default:
//            throw new Exception("invalid choice");
//    }
//}

//void orderItemCrud()
//{
//    OrderItem orderItem;
//    int orderItemId;
//    int orderId;
//    //bool idExsist = false;
//    Console.WriteLine("click 1 to add an orderItem, 2 to see a specific orderItem by his Id, " +
//        "3 to see a specific orderItem by orderId and productId, 4 to see the orderItems of a specific order," +
//        "5 to see all orderItems, 6 to change an orderItem's details or 7 to delete an orderItem");
//    innerChoice = Convert.ToInt32(Console.ReadLine());
//    switch (innerChoice)
//    {
//        case (int)eOrderItems.Create:
//            orderItemId = DataSource.Config.OrderItemId;
//            orderItem = OrderItemProperties(orderItemId);
//            DalOrderItem.CreateOrderItem(orderItem);
//            break;
//        case (int)eOrderItems.ReadById:
//            Console.WriteLine("enter the id of the orderItem you would like to see");
//            orderItemId = Convert.ToInt32(Console.ReadLine());
//            orderItem = DalOrderItem.ReadOrderItem(orderItemId);
//            Console.WriteLine(orderItem);
//            break;
//        case (int)eOrderItems.ReadByOrderProductIds:
//            Console.WriteLine("enter the id of the order and the id of the product of the orderitem you would like to see");
//            orderId = Convert.ToInt32(Console.ReadLine());
//            int productId = Convert.ToInt32(Console.ReadLine());
//            orderItem = DalOrderItem.ReadOrderItem(productId, orderId);
//            Console.WriteLine(orderItem);
//            break;
//        case (int)eOrderItems.ReadByOrderId:
//            Console.WriteLine("enter the id of the orderItem you would like to see");
//            orderId = Convert.ToInt32(Console.ReadLine());
//            OrderItem[] orderItems = DalOrderItem.ReadOrderItemByOrder(orderId);
//            foreach (OrderItem oi in orderItems)
//            { Console.WriteLine(oi); }
//            break;
//        case (int)eOrderItems.ReadAll:
//            OrderItem[] allOrderItem = DalOrderItem.ReadOrderItems();
//            foreach (OrderItem oi in allOrderItem)
//            { Console.WriteLine(oi); }
//            break;
//        case (int)eOrderItems.Update:
//            Console.WriteLine("enter the id of the order item you would like to update");
//            orderItemId = Convert.ToInt32(Console.ReadLine());
//            orderItem = OrderItemProperties(orderItemId);
//            DalOrderItem.UpdateOrderItem(orderItem);
//            break;
//        case (int)eOrderItems.Delete:
//            Console.WriteLine("enter the id of the order item you would like to delete");
//            orderItemId = Convert.ToInt32(Console.ReadLine());
//            DalOrderItem.DeleteOrderItem(orderItemId);
//            break;
//        default:
//            throw new Exception("invalid choice");
//    }
//}



//Product ProductProperties(int productId)
//{
//    Product product = new Product();
//    product.ID = productId;
//    Console.WriteLine("Enter the product's name:");
//    product.Name = Console.ReadLine();
//    Console.WriteLine("enter 0 if it is a type of bread, 1 if it is a type of milk, 2 if it is a treat," +
//        "3 if it is a fruit and 4 if it is a vegetable");
//    product.Category = (eCategories)Convert.ToInt32(Console.ReadLine());
//    Console.WriteLine("enter the product's price");
//    product.Price = Convert.ToDouble(Console.ReadLine());
//    Console.WriteLine("enter the amount in stock");
//    product.InStock = Convert.ToInt32(Console.ReadLine());
//    return product;
//}

//Order OrderProperties(int orderId)
//{
//    Order order = new Order();
//    try
//    {
//        order.ID = orderId;
//        Console.WriteLine("Enter your name:");
//        order.CustomerName = Console.ReadLine();
//        Console.WriteLine("Enter your Email:");
//        order.CustomerEmail = Console.ReadLine();
//        Console.WriteLine("Enter your address:");
//        order.CustomerAddress = Console.ReadLine();
//        Console.WriteLine("Enter an order date: ");
//        DateTime userDateTime;
//        if (DateTime.TryParse(Console.ReadLine(), out userDateTime))
//            order.OrderDate = userDateTime;
//        else
//            throw new Exception("You have entered an incorrect value.");
//        Console.WriteLine("Enter a ship date: ");
//        if (DateTime.TryParse(Console.ReadLine(), out userDateTime))
//            order.ShipDate = userDateTime;
//        else
//            throw new Exception("You have entered an incorrect value.");
//        Console.WriteLine("Enter a delivery date: ");
//        if (DateTime.TryParse(Console.ReadLine(), out userDateTime))
//            order.DeliveryDate = userDateTime;
//        else
//            throw new Exception("You have entered an incorrect value.");
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine(ex);
//    }
//    return order;
//}

//OrderItem OrderItemProperties(int orderItemId)
//{
//    OrderItem orderItem = new OrderItem();
//    orderItem.ID = orderItemId;
//    int orderId;
//    Console.WriteLine("Enter product name:");
//    string prodName = Console.ReadLine();
//    int prodIdx = -1;
//    for (int i = 0; i < DataSource.Config.productIdx; i++)
//    {
//        if (prodName == DataSource.productList[i].Name)
//            prodIdx = DataSource.productList[i].ID;
//    }
//    if (prodIdx == -1)
//        throw new Exception("no such product in our store");
//    else
//        orderItem.ProductId = prodIdx;
//    Console.WriteLine("Enter order ID:");
//    orderId = Convert.ToInt32(Console.ReadLine());
//    bool orderExsist = false;
//    for (int i = 0; i < DataSource.Config.orderIdx; i++)
//        if (DataSource.orderList[i].ID == orderId)
//            orderExsist = true;
//    if (!orderExsist)
//        throw new Exception("no such order");
//    orderItem.OrderId = orderId;
//    Console.WriteLine("Enter amount:");
//    int amount = Convert.ToInt32(Console.ReadLine());
//    for (int i = 0; i < DataSource.Config.orderIdx; i++)
//    {
//        if (DataSource.orderList[i].ID == orderItem.OrderId)
//        {
//            if (DataSource.productList[i].InStock >= amount)
//                orderItem.Amount = amount;
//            else
//                orderItem.Amount = DataSource.productList[i].InStock;
//            DataSource.productList[i].InStock -= orderItem.Amount;
//            orderItem.Price = DataSource.productList[i].Price * orderItem.Amount;
//        }
//    }
//    return orderItem;
//}










