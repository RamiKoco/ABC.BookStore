using ABC.BookStore.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace ABC.BookStore.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class BookStoreController : AbpControllerBase
{
    protected BookStoreController()
    {
        LocalizationResource = typeof(BookStoreResource);
    }
}
