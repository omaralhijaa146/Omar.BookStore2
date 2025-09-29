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

namespace Omar.BookStore2.Web.Pages.Books
{
    public class CreateModalModel : BookStore2PageModel
    {
        private readonly IBookAppService _bookAppService;

        [BindProperty]
        public CreateBookViewModel Book { get; set; }

        public List<SelectListItem> Authors { get; set; }

        public CreateModalModel(IBookAppService bookAppService)
        {
            _bookAppService = bookAppService;
        }

        public async Task OnGetAsync()
        {
            Book = new CreateBookViewModel();
            var authorLookup= await _bookAppService.GetAuthorLookupAsync();
            Authors = authorLookup.Items.Select(x=>new SelectListItem(x.Name,x.Id.ToString())).ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var bookDto = ObjectMapper.Map<CreateBookViewModel,CreateBookDto>(Book);
            await _bookAppService.CreateAsync(bookDto);
            return NoContent();

        }

    }

    
}
