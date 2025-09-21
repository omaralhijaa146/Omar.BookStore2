using Omar.BookStore2.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Omar.BookStore2.EntityFrameworkCore.Applications.Books
{
    [Collection(BookStore2TestConsts.CollectionDefinitionName)]
    public class EfCoreBookAppService_Tests : BookAppService_Tests<BookStore2EntityFrameworkCoreTestModule>
    {
       
    }
}
