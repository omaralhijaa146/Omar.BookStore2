using Microsoft.AspNetCore.Mvc;
using Omar.BookStore2.Books;
using Omar.BookStore2.Web.Pages.Books;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

public class EditBookViewModel
{

    [Required]
    [HiddenInput]
    public Guid Id { get; set; }


    [SelectItems(nameof(EditModalModel.Authors))]
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