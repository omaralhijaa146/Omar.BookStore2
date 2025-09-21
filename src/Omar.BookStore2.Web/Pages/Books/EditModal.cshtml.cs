using AutoMapper.Internal.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Omar.BookStore2.Books;
using System;
using System.Threading.Tasks;

namespace Omar.BookStore2.Web.Pages.Books
{
    public class EditModalModel : BookStore2PageModel
    {
        private readonly IBookAppService _bookAppService;

        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public UpdateBookDto Book { get; set; }

        public EditModalModel(IBookAppService bookAppService)
        {
            _bookAppService = bookAppService;
        }

        public async Task OnGetAsync()
        {
           var bookDto= await _bookAppService.GetAsync(Id);
            Book = ObjectMapper.Map<BookDto,UpdateBookDto>(bookDto);
        }

        public async Task<IActionResult> OnPostAsync() {

            await _bookAppService.UpdateAsync(Id,Book);
            return NoContent();
        }
    }
}
