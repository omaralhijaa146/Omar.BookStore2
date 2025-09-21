using Volo.Abp.Modularity;

namespace Omar.BookStore2;

[DependsOn(
    typeof(BookStore2DomainModule),
    typeof(BookStore2TestBaseModule)
)]
public class BookStore2DomainTestModule : AbpModule
{

}
