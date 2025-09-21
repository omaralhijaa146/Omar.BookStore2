using Volo.Abp.Modularity;

namespace Omar.BookStore2;

[DependsOn(
    typeof(BookStore2ApplicationModule),
    typeof(BookStore2DomainTestModule)
)]
public class BookStore2ApplicationTestModule : AbpModule
{

}
