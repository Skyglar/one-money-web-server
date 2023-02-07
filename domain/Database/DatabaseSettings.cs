
using domain.Database.Contracts;

namespace domain.Database {
    public sealed class DatabaseSettings : IDatabaseSettings {
        public string CategoriesCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
