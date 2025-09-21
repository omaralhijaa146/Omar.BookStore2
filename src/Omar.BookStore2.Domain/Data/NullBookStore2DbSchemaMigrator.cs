using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Omar.BookStore2.Data;

/* This is used if database provider does't define
 * IBookStore2DbSchemaMigrator implementation.
 */
public class NullBookStore2DbSchemaMigrator : IBookStore2DbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
