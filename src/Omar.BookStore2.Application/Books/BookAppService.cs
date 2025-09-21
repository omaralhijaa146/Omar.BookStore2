using Omar.BookStore2.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Omar.BookStore2.Books
{
    public class BookAppService : CrudAppService<Book,BookDto,Guid,PagedAndSortedResultRequestDto,CreateBookDto,UpdateBookDto>,IBookAppService
    {

        public BookAppService(IRepository<Book,Guid> repository):base(repository)
        {

            GetPolicyName = BookStore2Permissions.Books.Default;
            GetListPolicyName = BookStore2Permissions.Books.Default;
            CreatePolicyName = BookStore2Permissions.Books.Create;
            UpdatePolicyName = BookStore2Permissions.Books.Edit;
            DeletePolicyName = BookStore2Permissions.Books.Delete;
            
        }
    }
}
