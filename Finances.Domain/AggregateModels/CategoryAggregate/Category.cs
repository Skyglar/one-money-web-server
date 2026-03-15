using OneMoney.Common.SeedWork;

namespace Finances.Domain.AggregateModels.CategoryAggregate;

public sealed class Category : Entity, IAggregateRoot{
    public required string Name { get; set; }

    public required string Image { get; set; }

    public required string Color { get; set; }
}