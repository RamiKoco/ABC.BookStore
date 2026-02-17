using System.Threading.Tasks;

namespace ABC.BookStore.Data;

public interface IBookStoreDbSchemaMigrator
{
    Task MigrateAsync();
}
