using Xunit;

namespace Omar.BookStore2.EntityFrameworkCore;

[CollectionDefinition(BookStore2TestConsts.CollectionDefinitionName)]
public class BookStore2EntityFrameworkCoreCollection : ICollectionFixture<BookStore2EntityFrameworkCoreFixture>
{

}
