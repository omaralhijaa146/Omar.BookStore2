using Volo.Abp.Application.Dtos;

namespace Omar.BookStore2.Authors
{
    public class GetAuthorListDto : PagedAndSortedResultRequestDto
    {
        public string? Filter { get; set; }
    }

}
