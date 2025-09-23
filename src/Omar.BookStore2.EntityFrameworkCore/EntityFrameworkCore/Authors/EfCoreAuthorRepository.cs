using Microsoft.EntityFrameworkCore;
using Omar.BookStore2.Authors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Omar.BookStore2.EntityFrameworkCore.Authors
{
    public class EfCoreAuthorRepository : EfCoreRepository<BookStore2DbContext, Author, Guid>,IAuthorRepository
    {
        public EfCoreAuthorRepository(IDbContextProvider<BookStore2DbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<Author> FindByNameAsync(string name)
        {
            var dbSet = await GetDbSetAsync();
            var result = await dbSet.FirstOrDefaultAsync(x=>x.Name==name);
            return result;
        }

        public async Task<List<Author>> GetListAsync(int skipCount, int maxResultCount, string sorting, string filter = null)
        {
            var dbSet= await GetDbSetAsync();

            var pagedResult = await dbSet
                .WhereIf(
                !filter.IsNullOrWhiteSpace(),
                x => x.Name.Contains(filter))
                .OrderBy(sorting)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();

            return pagedResult;
        }
    }
}
