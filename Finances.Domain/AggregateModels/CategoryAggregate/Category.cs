using OneMoney.Common.SeedWork;

namespace Finances.Domain.AggregateModels.CategoryAggregate;

public sealed class Category : Entity, IAggregateRoot{
    public string Name { get; private set; }

    public string Image { get; private set; }

    public string Color { get; private set; }

    private Category() { }

    public Category(string name, string imageUrl, string color) {
        Id = Guid.NewGuid();
        Name = name;
        Image = imageUrl;
        Color = color;
    }
}