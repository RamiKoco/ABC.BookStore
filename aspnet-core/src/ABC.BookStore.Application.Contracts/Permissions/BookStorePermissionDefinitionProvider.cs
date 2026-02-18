using ABC.BookStore.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace ABC.BookStore.Permissions;

public class BookStorePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var localizePrefix = "Permission";
        var mainGroup = context.AddGroup(BookStorePermissions.GroupName,
            L($"{localizePrefix}:{BookStorePermissions.GroupName}"));

        // Books
        var books = mainGroup.AddPermission(BookStorePermissions.Books.Default,
            L($"{localizePrefix}:{nameof(BookStorePermissions.Books)}"));

        books.AddChild(BookStorePermissions.Books.Create,
            L($"{localizePrefix}:{nameof(BookStorePermissions.Books)}{BookStorePermissions.CreateConst}"));
        books.AddChild(BookStorePermissions.Books.Edit,
            L($"{localizePrefix}:{nameof(BookStorePermissions.Books)}{BookStorePermissions.UpdateConst}"));
        books.AddChild(BookStorePermissions.Books.Delete,
            L($"{localizePrefix}:{nameof(BookStorePermissions.Books)}{BookStorePermissions.DeleteConst}"));

        //OzelKod
        var ozelKod = mainGroup.AddPermission(BookStorePermissions.OzelKod.Default,
            L($"{localizePrefix}:{nameof(BookStorePermissions.OzelKod)}"));

        ozelKod.AddChild(BookStorePermissions.OzelKod.Create,
            L($"{localizePrefix}:{nameof(BookStorePermissions.OzelKod)}{BookStorePermissions.CreateConst}"));
        ozelKod.AddChild(BookStorePermissions.OzelKod.Update,
            L($"{localizePrefix}:{nameof(BookStorePermissions.OzelKod)}{BookStorePermissions.UpdateConst}"));
        ozelKod.AddChild(BookStorePermissions.OzelKod.Delete,
            L($"{localizePrefix}:{nameof(BookStorePermissions.OzelKod)}{BookStorePermissions.DeleteConst}"));

    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<BookStoreResource>(name);
    }
}
