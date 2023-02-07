
namespace domain.Database.Contracts
{
    public interface IDatabaseSettings
    {
        string CategoriesCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
