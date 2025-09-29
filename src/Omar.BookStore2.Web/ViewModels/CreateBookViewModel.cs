using Omar.BookStore2.Books;
using Omar.BookStore2.Web.Pages.Books;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

public class CreateBookViewModel
{

    [SelectItems(nameof(CreateModalModel.Authors))]
    [DisplayName("Author")]
    public Guid AuthorId { get; set; }

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
}