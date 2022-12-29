

namespace domain.Entities
{
    public sealed class Category : EntityBase
    {
        public string Name { get; set; }

        public string Image { get; set; }

        public string Color { get; set; }

        public double Amount { get; set; }
    }
}
