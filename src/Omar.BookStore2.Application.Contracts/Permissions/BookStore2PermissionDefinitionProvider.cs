using Omar.BookStore2.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace Omar.BookStore2.Permissions;

public class BookStore2PermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(BookStore2Permissions.GroupName);

        //Define your own permissions here. Example:
        //myGroup.AddPermission(BookStore2Permissions.MyPermission1, L("Permission:MyPermission1"));
        var bookPermission = myGroup.AddPermission(BookStore2Permissions.Books.Default,L("Permission:Books"));
        bookPermission.AddChild(BookStore2Permissions.Books.Create,L("Permission:Books.Create"));
        bookPermission.AddChild(BookStore2Permissions.Books.Edit,L("Permission:Books.Edit"));
        bookPermission.AddChild(BookStore2Permissions.Books.Delete,L("Permission:Books.Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<BookStore2Resource>(name);
    }
}
