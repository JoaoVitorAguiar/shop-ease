using Shared.Entities;

namespace Products.Domain.Entities;

public class Category(string name, string slug) : Entity
{
    public string Name { get; private set; } = name;
    public string Slug { get; private set; } = slug;
    public IList<Product> Products { get; set; } = [];
}