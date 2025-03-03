using common.Helpers.Enums;

namespace domain.Entities
{
    public sealed class Transaction : EntityBase
    {
        public decimal Amount { get; set; }

        public string Comment { get; set; }

        public long AccountId { get; set; }

        public long CategoryId { get; set; }

        public TransactionType TransactionType { get; set; }

        public Account Account { get; set; }

        public Category Category { get; set; }
    }
}
