using Microsoft.AspNetCore.Mvc;
using Omar.BookStore2.Settings;
using Omar.BookStore2.Web.Pages.Settings;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Settings;

namespace Omar.BookStore2.Web.Components.CustomSettingGroup
{
    public class CustomSettingGroupComponent:AbpViewComponent
    {
        private readonly ISettingsAppService _settingAppService;

        public CustomSettingGroupComponent(ISettingsAppService settingAppService)
        {
            _settingAppService = settingAppService;
        }

        public virtual async Task<IViewComponentResult> InvokeAsync() {
            var settings = await _settingAppService.GetSettingsAsync();
           
            return View("~/Components/CustomSettingGroup/Default.cshtml",settings);
        } 
    }
}
