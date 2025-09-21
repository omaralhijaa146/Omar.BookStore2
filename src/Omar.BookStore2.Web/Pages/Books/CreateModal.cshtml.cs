using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Omar.BookStore2.Books;
using System.Threading.Tasks;

namespace Omar.BookStore2.Web.Pages.Books
{
    public class CreateModalModel : BookStore2PageModel
    {
        private readonly IBookAppService _bookAppService;

        [BindProperty]
        public CreateBookDto Book { get; set; }

        public CreateModalModel(IBookAppService bookAppService)
        {
            _bookAppService = bookAppService;
        }

        public void OnGet()
        {
            Book = new CreateBookDto();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _bookAppService.CreateAsync(Book);
            return NoContent();

        }

    }
}
