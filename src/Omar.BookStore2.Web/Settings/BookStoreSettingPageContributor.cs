using Microsoft.Extensions.Localization;
using Omar.BookStore2.Localization;
using Omar.BookStore2.Web.Components.CustomSettingGroup;
using System.Threading.Tasks;
using Volo.Abp.Localization;
using Volo.Abp.SettingManagement.Web.Pages.SettingManagement;

namespace Omar.BookStore2.Web.Settings
{
    public class BookStoreSettingPageContributor : ISettingPageContributor
    {
        private readonly IStringLocalizer<BookStore2Resource> settingLocalizer;

        public BookStoreSettingPageContributor(IStringLocalizer<BookStore2Resource> settingLocalizer)
        {
            this.settingLocalizer = settingLocalizer;
        }
        public Task<bool> CheckPermissionsAsync(SettingPageCreationContext context )
        {
            return Task.FromResult(true);
        }

        public Task ConfigureAsync(SettingPageCreationContext context)
        {
            context.Groups.Add(new SettingPageGroup(
                "Volo.Abp.CustomSettingGroup",
               settingLocalizer["CustomSettingGroup"],
                typeof(CustomSettingGroupComponent),
                order:1
                ));
            return Task.CompletedTask;
        }
    }
}
