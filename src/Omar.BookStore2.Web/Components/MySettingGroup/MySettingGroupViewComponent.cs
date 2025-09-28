using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace Omar.BookStore2.Web.Components.MySettingGroup
{
    public class MySettingGroupViewComponent:AbpViewComponent
    {
        public virtual IViewComponentResult Invoke() {
            return View("~/Components/MySettingGroup/Default.cshtml");
        }
    }
}
