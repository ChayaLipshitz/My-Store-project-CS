
namespace Dal.DO;

    public struct Product
    {
    public int ID { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public eCategory Category { get; set; }
    public int Instock { get; set; }

    public Product(string name, double price, eCategory category, int instock)
    {
       ID = -1;
        Category = category;
        Instock = instock;
        Name = name;
       Price = price;
    }
    public override string ToString()
    {
        return $"product id: {ID}\n" +
                    $"product name: {Name}\n" +
                    $"product price: {Price}\n" +
                    $"product category: {Category}\n" +
                    $"amount in stock: {Instock}\n\n";
    }
}

