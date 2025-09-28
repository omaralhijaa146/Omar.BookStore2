using Microsoft.AspNetCore.Mvc;
using Omar.BookStore2.Authors;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

public class EditAuthorViewModel
{
    [HiddenInput]
    [Required]
    public Guid Id { get; set; }

    [Required]
    [StringLength(AuthorConsts.MaxNameLength)]
    public string Name { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime BirthDate { get; set; }

    [TextArea]
    public string? ShortBio { get; set; }
}