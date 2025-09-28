using System.Threading.Tasks;
using Omar.BookStore2.Localization;
using Omar.BookStore2.Permissions;
using Omar.BookStore2.MultiTenancy;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.UI.Navigation;
using Volo.Abp.TenantManagement.Web.Navigation;

namespace Omar.BookStore2.Web.Menus;

public class BookStore2MenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private static Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<BookStore2Resource>();

        //Home
        context.Menu.AddItem(
            new ApplicationMenuItem(
                BookStore2Menus.Home,
                l["Menu:Home"],
                "~/",
                icon: "fa fa-home",
                order: 1
            )
        );


        //Administration
        var administration = context.Menu.GetAdministration();
        administration.Order = 6;

        //Administration->Identity
        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 1);
    
        if (MultiTenancyConsts.IsEnabled)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        else
        {
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }
        
        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 3);

        //Administration->Settings
        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 7);


        context.Menu.AddItem(
            
            new ApplicationMenuItem(
                "BooksStore",
                l["Menu:BookStore2"],
                icon: "fa fa-book"
                ).AddItem(
                
                new ApplicationMenuItem(
                    
                    "BooksStore.Books",
                    l["Menu:Books"],
                    url: "/Books").RequirePermissions(BookStore2Permissions.Books.Default)
                
                ).AddItem(
                
                new ApplicationMenuItem(
                    "BooksStore2.Auhtors",
                    l["Menu:Authors"],
                    url:"/Authors"
                    ).RequirePermissions(BookStore2Permissions.Authors.Default)
                )
            
            );

   

        return Task.CompletedTask;
    }
}
