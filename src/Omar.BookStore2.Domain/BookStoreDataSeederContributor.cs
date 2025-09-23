using Omar.BookStore2.Authors;
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
        private readonly IAuthorRepository _authorRepository;
        private readonly AuthorManager _authorManager;
        private readonly IGuidGenerator _guidGenerator;

        public BookStoreDataSeederContributor(IRepository<Book,Guid> bookRepository,IAuthorRepository authorRepository,AuthorManager authorManager,IGuidGenerator guidGenerator )
            
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _authorManager = authorManager;
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


            if(await _authorRepository.GetCountAsync() > 0)
                return;

            var authorsToBeSeeded = new List<Author> { 
            
                await _authorManager.CreateAsync(
                     "George Orwell",
                    new DateTime(1903,6,25),
                    "Orwell produced literary criticism and poetry, fiction and polemical journalism; and is best known for the allegorical novella Animal Farm (1945) and the dystopian novel Nineteen Eighty-Four (1949)."
                    )
               ,
                await _authorManager.CreateAsync(
                    "Douglas Adams",
                    new DateTime(1952,3,11),
                    "Douglas Adams was an English author, screenwriter, essayist, humorist, satirist and dramatist. Adams was an advocate for environmentalism and conservation, a lover of fast cars, technological innovation and the Apple Macintosh, and a self-proclaimed 'radical atheist'."
                    ),
                await _authorManager.CreateAsync(
                    "Omar Abu Al Hijaa",
                    new DateTime(1985,5,15),
                    "Software developer and tech enthusiast."
                    )

            };
            await _authorRepository.InsertManyAsync(authorsToBeSeeded, autoSave: true);

        }
    }
}
