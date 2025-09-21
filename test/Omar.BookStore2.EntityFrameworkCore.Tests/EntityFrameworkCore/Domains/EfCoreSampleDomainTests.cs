using Omar.BookStore2.Samples;
using Xunit;

namespace Omar.BookStore2.EntityFrameworkCore.Domains;

[Collection(BookStore2TestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<BookStore2EntityFrameworkCoreTestModule>
{

}
