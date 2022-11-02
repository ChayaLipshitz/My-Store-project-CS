
namespace Dal.DO;

    public struct OrderItem
    {
    public int OrderItem_ID { get; set; }   
    public int Product_ID { get; set; }
    public int Order_ID { get; set; }
    public double Product_Price { get; set; }
    public int Product_Amount { get; set; }
    public override string ToString()
    {
        return $"OrderItem ID: {OrderItem_ID}\n" +
            $"Product ID: {Product_ID}\n" +
            $"Order ID: {Order_ID}\n" +
            $"Product price: {Product_Price}\n" +
            $"Product amount: {Product_Amount}\n\n";
    }
    public OrderItem( int OrderID,int ProductID, double ProductPrice,int ProductAmount)
    {
        OrderItem_ID = -1;
        Product_ID = ProductID;
        Order_ID = OrderID;
        Product_Price = ProductPrice;
        Product_Amount = ProductAmount;
    }

}

