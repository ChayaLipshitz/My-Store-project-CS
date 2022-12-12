


namespace Dal.DO;

public struct Order
{
    public int ID { get; set; }
    public string? Customer_Name { get; set; }
    public string? Customer_Email { get; set; }
    public string? Customer_Address { get; set; }
    public DateTime? Order_Date { get; set; }
    public DateTime? Ship_Date { get; set; }
    public DateTime? Delivery_Date { get; set; }
    public override string ToString()
    {
        return $"Order id: {ID}\n" +
            $"Customer name: {Customer_Name}\n" +
            $"Customer email: {Customer_Email}\n" +
            $"Customer address: {Customer_Address}\n" +
            $"Order date: {Order_Date}\n" +
            $"Ship date: {Ship_Date}\n" +
            $"Delivery date: {Delivery_Date}\n\n";
    }
    public Order(string name,string email,string address, DateTime Order_date, DateTime Ship_date, DateTime Delivery_date)
    {
        ID = -1;
        Customer_Name = name;
        Customer_Email = email;
        Customer_Address = address;
        Order_Date = Order_date;
        Ship_Date = Ship_date;
        Delivery_Date = Delivery_date;
    }



    





}

