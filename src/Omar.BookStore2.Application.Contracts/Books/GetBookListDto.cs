using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Omar.BookStore2.Books
{
    public class GetBookListDto:PagedAndSortedResultRequestDto
    {
        public string? Filter { get; set; } 
        public string? FilterByAuthor { get; set; } 
    }
}
