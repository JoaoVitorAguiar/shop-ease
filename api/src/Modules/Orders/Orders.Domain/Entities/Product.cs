namespace Orders.Domain.Entities;

public class Product
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public string Sku { get; private set; }
    public int Stock { get; private set; }

    public Product() { }

    public Product(Guid id, string name, decimal price, string sku, int stock)
    {
        Id    = id;
        Name  = name;
        Price = price;
        Sku   = sku;
        Stock = stock;
    }
}
