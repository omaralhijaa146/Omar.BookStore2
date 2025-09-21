using Omar.BookStore2.Localization;
using Volo.Abp.Application.Services;

namespace Omar.BookStore2;

/* Inherit your application services from this class.
 */
public abstract class BookStore2AppService : ApplicationService
{
    protected BookStore2AppService()
    {
        LocalizationResource = typeof(BookStore2Resource);
    }
}
