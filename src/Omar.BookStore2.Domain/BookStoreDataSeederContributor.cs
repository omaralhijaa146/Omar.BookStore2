using Omar.BookStore2.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;

namespace Omar.BookStore2
{
    public class BookStoreDataSeederContributor : IDataSeedContributor,ITransientDependency
    {

        private readonly IRepository<Book, Guid> _bookRepository;
        private readonly IGuidGenerator _guidGenerator;

        public BookStoreDataSeederContributor(IRepository<Book,Guid> bookRepository,IGuidGenerator guidGenerator )
            
        {
            _bookRepository = bookRepository;
            _guidGenerator = guidGenerator;
        }

        public async Task SeedAsync(DataSeedContext context)
        {

            if (await _bookRepository.GetCountAsync() > 0)
                return;

            var booksToBeSeeded = new List<Book>
            {
                new Book(
                    _guidGenerator.Create(),
                    "1984",
                    BookType.Dystopia,
                    new DateTime(1949,6,8),
                    19.84f
                    ),
                new Book(
                    _guidGenerator.Create(),
                    "The Hitchhiker's Guide to the Galaxy",
                    BookType.ScienceFiction,
                    new DateTime(1995,9,27),
                    42.0f
                    ),
                new Book(
                    _guidGenerator.Create(),
                    "Omar Book",
                    BookType.Science,
                    new DateTime(2003,9,27),
                    145.5f
                    ),

            };


            await _bookRepository.InsertManyAsync(booksToBeSeeded,autoSave:true);
        }
    }
}
