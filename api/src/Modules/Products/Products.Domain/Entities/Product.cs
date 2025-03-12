using Shared.Entities;

namespace Products.Domain.Entities;

public class Product(string name, string description, decimal price) : Entity
{
    public string Name { get; private set; } = name;
    public string Description { get; private set; } = description;
    public decimal Price { get; private set; } = price;
}