using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Omar.BookStore2.Data;
using Volo.Abp.DependencyInjection;

namespace Omar.BookStore2.EntityFrameworkCore;

public class EntityFrameworkCoreBookStore2DbSchemaMigrator
    : IBookStore2DbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreBookStore2DbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the BookStore2DbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<BookStore2DbContext>()
            .Database
            .MigrateAsync();
    }
}
