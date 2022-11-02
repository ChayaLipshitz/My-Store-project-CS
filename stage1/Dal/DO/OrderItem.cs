
namespace Dal.DO;

    public struct OrderItem
    {
    public int OrderItem_ID { get; set; }   
    public int Product_ID { get; set; }
    public int Order_ID { get; set; }
    public double Product_Price { get; set; }
    public int Product_Amount { get; set; }
    public string ToString()
    {
        return $"OrderItem ID: {OrderItem_ID}\n" +
            $"Product ID: {Product_ID}\n" +
            $"Order ID: {Order_ID}\n" +
            $"Product price: {Product_Price}\n" +
            $"Product amount: {Product_Amount}\n\n";
    }


}

