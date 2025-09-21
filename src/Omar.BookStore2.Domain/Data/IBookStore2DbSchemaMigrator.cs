using System.Threading.Tasks;

namespace Omar.BookStore2.Data;

public interface IBookStore2DbSchemaMigrator
{
    Task MigrateAsync();
}
