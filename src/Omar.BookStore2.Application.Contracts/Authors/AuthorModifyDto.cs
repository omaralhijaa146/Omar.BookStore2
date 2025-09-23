using System;
using System.ComponentModel.DataAnnotations;

namespace Omar.BookStore2.Authors
{
    public abstract class AuthorModifyDto
    {
        [Required]
        [StringLength(AuthorConsts.MaxNameLength)]
        public string Name { get; set; }
        public string? ShortBio { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime BirthDate { get; set; }
    }

}
