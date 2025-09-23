using System;
using Volo.Abp.Application.Dtos;

namespace Omar.BookStore2.Authors
{
    public class AuthorDto:EntityDto<Guid>
    {
        public string Name { get; set; }
        public string ShortBio { get; set; }
        public DateTime BirthDate { get; set; }
    }

}
