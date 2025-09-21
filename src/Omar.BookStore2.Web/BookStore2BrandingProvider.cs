using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;
using Microsoft.Extensions.Localization;
using Omar.BookStore2.Localization;

namespace Omar.BookStore2.Web;

[Dependency(ReplaceServices = true)]
public class BookStore2BrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<BookStore2Resource> _localizer;

    public BookStore2BrandingProvider(IStringLocalizer<BookStore2Resource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
