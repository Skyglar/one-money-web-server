using common.Helpers.Enums;

namespace domain.Entities
{
    public sealed class Account : EntityBase
    {
        public string Name { get; set; }

        public decimal Amount { get; set; }

        public AccountType AccountType { get; set; }
    }
}
