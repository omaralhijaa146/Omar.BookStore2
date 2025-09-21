using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Omar.BookStore2.Authors
{
    public interface IAuthorRepository : IRepository<Author, Guid>
    {
        public Task<Author> FindByNameAsync(string name);

        public Task<List<Author>> GetListAsync(int skipCount,int maxResultCount,string sorting, string filter=null);
    }
}
