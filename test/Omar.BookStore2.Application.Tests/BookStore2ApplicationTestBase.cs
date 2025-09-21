using Volo.Abp.Modularity;

namespace Omar.BookStore2;

public abstract class BookStore2ApplicationTestBase<TStartupModule> : BookStore2TestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
