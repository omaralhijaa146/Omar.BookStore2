using Omar.BookStore2.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Omar.BookStore2.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(BookStore2EntityFrameworkCoreModule),
    typeof(BookStore2ApplicationContractsModule)
)]
public class BookStore2DbMigratorModule : AbpModule
{
}
