using Castle.Core.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Omar.BookStore2.Authors;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Omar.BookStore2.Web.Pages.Authors
{
    public class EditModalModel : BookStore2PageModel
    {
        private readonly IAuthorAppService _authorAppService;
        private readonly ILogger<EditModalModel> logger;

        [BindProperty]
        public EditAuthorViewModel Author { get; set; }

        public EditModalModel(IAuthorAppService authorAppService,ILogger<EditModalModel> logger)
        {
            _authorAppService = authorAppService;
            this.logger = logger;
            
        }

        public async Task OnGetAsync(Guid id)
        {
            var authorDto = await _authorAppService.GetAsync(id);
            Author = ObjectMapper.Map<AuthorDto, EditAuthorViewModel>(authorDto);
        }

        public async Task<IActionResult> OnPostAsync() {

            var authorToBeUpdated = ObjectMapper.Map<EditAuthorViewModel, UpdateAuthorDto>(Author);
            logger.LogInformation($"Debug Val: {Author.Id}", DateTime.Now);
            await _authorAppService.UpdateAsync(Author.Id,authorToBeUpdated);
            return NoContent();
        
        }
    }
}

