using Microsoft.AspNetCore.Mvc;
using Omar.BookStore2.Settings;
using Omar.BookStore2.Web.Pages;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.SettingManagement;

namespace Omar.BookStore2.Web.Pages.Settings
{
    public class CustomSettingsGroupModel:BookStore2PageModel
    {
        private readonly ISettingManager settingManager;

        [Microsoft.AspNetCore.Mvc.BindProperty]
        public CustomSettingsDto CustomSettings { get; set; }

        public CustomSettingsGroupModel(ISettingManager settingManager)
        {
            this.settingManager = settingManager;
        }
        public async Task OnGetAsync()
        {
            CustomSettings = new CustomSettingsDto();
            CustomSettings.TestSetting = await settingManager.GetOrNullGlobalAsync(BookStore2Settings.Test);
        }
        public async Task<IActionResult> OnPostAsync()
        {
            await settingManager.SetGlobalAsync(BookStore2Settings.Test, CustomSettings.TestSetting);
            return NoContent();
        }
    }
    
}

