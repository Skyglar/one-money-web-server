using common;
using domain.Repositories.DbConnection.Contracts;
using MongoDB.Driver;

namespace domain.Repositories.DbConnection {
    public sealed class DbConnectionFactory : IDbConnectionFactory {
        private readonly MongoClient _mongoClient;

        public DbConnectionFactory() {
            _mongoClient = new MongoClient(AppSettingsConfigurationManager.LocalDatabaseConnectionString);
        }

        public IMongoDatabase NewDatabaseConnection() {
            return _mongoClient.GetDatabase(AppSettingsConfigurationManager.DatabaseName);
            //_categories = database.GetCollection<Category>(settings.CategoriesCollectionName);
        }
    }
}
