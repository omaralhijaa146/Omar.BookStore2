using System;
using System.ComponentModel.DataAnnotations;

namespace Omar.BookStore2.Books
{
    public abstract class BookModificationDto
    {
        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [Required]
        public BookType Type { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; }

        [Required]
        public float Price { get; set; }

        public Guid AuthorId {  get; set; }
    }
}
