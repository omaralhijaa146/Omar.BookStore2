using Omar.BookStore2.Authors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Omar.BookStore2.EntityFrameworkCore.Applications.Authors
{
    [Collection(BookStore2TestConsts.CollectionDefinitionName)]
    public class EfCoreAuthorAppService_Tests:AuthorAppService_Tests<BookStore2EntityFrameworkCoreTestModule>
    {
    }
}
