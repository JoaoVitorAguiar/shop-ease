namespace Cart.Domain.Entities;

public class Product
{
    public Product() { }
    
    public Product(string name, decimal price, string sku, int stock)
    {
        Name = name;
        Price = price;
        Sku = sku;
        Stock = stock;
    }
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; } 
    public decimal Price { get; private set; } 
    public string Sku { get; private set; } 
    public int Stock { get; private set; } 
}