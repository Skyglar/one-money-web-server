
using MongoDB.Driver;

namespace domain.Repositories.DbConnection.Contracts {
    public interface IDbConnectionFactory {
        IMongoDatabase NewDatabaseConnection();
    }
}
