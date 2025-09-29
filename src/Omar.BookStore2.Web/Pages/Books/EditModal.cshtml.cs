using AutoMapper.Internal.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Omar.BookStore2.Books;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using Volo.Abp.ObjectMapping;

namespace Omar.BookStore2.Web.Pages.Books
{
    public class EditModalModel : BookStore2PageModel
    {
        private readonly IBookAppService _bookAppService;

        [BindProperty]
        public EditBookViewModel Book { get; set; }

        public List<SelectListItem> Authors { get; set; } 
        
        public EditModalModel(IBookAppService bookAppService)
        {
            _bookAppService = bookAppService;
        }

        public async Task OnGetAsync(Guid id)
        {
           var bookDto= await _bookAppService.GetAsync(id);
            Book = ObjectMapper.Map<BookDto,EditBookViewModel>(bookDto);
            var authorLookup = await _bookAppService.GetAuthorLookupAsync();
            Authors= authorLookup.Items.Select(x=>new SelectListItem(x.Name,x.Id.ToString())).ToList();
        }

        public async Task<IActionResult> OnPostAsync() {

            var bookDto = ObjectMapper.Map<EditBookViewModel, UpdateBookDto>(Book);
            await _bookAppService.UpdateAsync(Book.Id,bookDto);
            return NoContent();
        }
    }

    public class EditBookViewModel {

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
}
