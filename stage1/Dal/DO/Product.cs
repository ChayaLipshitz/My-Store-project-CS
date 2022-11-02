
namespace Dal.DO;

    public struct Product
    {
    public int Product_ID { get; set; }
    public string Product_Name { get; set; }
    public double Product_Price { get; set; }
    public eCategory Product_Category { get; set; }
    public int Product_Instock { get; set; }

    public Product(string name, double price, eCategory category, int instock)
    {
        Product_ID = -1;
        Product_Category = category;
        Product_Instock = instock;
        Product_Name = name;
        Product_Price = price;
    }
    public override string ToString()
    {
        return $"product id: {Product_ID}\n" +
                    $"product name: {Product_Name}\n" +
                    $"product price: {Product_Price}\n" +
                    $"product category: {Product_Category}\n" +
                    $"amount in stock: {Product_Instock}\n\n";
    }
}

