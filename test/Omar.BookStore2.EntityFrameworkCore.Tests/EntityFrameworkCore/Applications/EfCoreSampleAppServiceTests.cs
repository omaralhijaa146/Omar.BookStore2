using Omar.BookStore2.Samples;
using Xunit;

namespace Omar.BookStore2.EntityFrameworkCore.Applications;

[Collection(BookStore2TestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<BookStore2EntityFrameworkCoreTestModule>
{

}
