using Shared.Entities;

namespace Products.Domain.Entities;

public class Product(string name, string description, decimal price, int stock, string sku, Guid categoryId) : Entity
{
    public string Name { get; private set; } = name;
    public string Description { get; private set; } = description;
    public decimal Price { get; private set; } = price;
    public int Stock { get; private set; } = stock;
    public string Sku { get; private set; } = sku;

    public Guid CategoryId { get; private set; } = categoryId;
    public Category Category { get; private set; } 
}