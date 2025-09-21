using Volo.Abp.Modularity;

namespace Omar.BookStore2;

/* Inherit from this class for your domain layer tests. */
public abstract class BookStore2DomainTestBase<TStartupModule> : BookStore2TestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
