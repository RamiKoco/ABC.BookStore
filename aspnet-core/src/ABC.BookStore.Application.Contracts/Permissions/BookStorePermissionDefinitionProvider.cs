namespace ABC.BookStore.Permissions;
public class BookStorePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var localizePrefix = "Permission";
        var mainGroup = context.AddGroup(BookStorePermissions.GroupName,
            L($"{localizePrefix}:{BookStorePermissions.GroupName}"));
        //Banka
        var banka = mainGroup.AddPermission(BookStorePermissions.Banka_.Default,
            L($"{localizePrefix}:{nameof(BookStorePermissions.Banka_)}"));

        banka.AddChild(BookStorePermissions.Banka_.Create,
            L($"{localizePrefix}:{nameof(BookStorePermissions.Banka_)}{BookStorePermissions.CreateConst}"));//Permission:Banka.Create
        banka.AddChild(BookStorePermissions.Banka_.Update,
            L($"{localizePrefix}:{nameof(BookStorePermissions.Banka_)}{BookStorePermissions.UpdateConst}"));//Permission:Banka.Update
        banka.AddChild(BookStorePermissions.Banka_.Delete,
            L($"{localizePrefix}:{nameof(BookStorePermissions.Banka_)}{BookStorePermissions.DeleteConst}"));
        banka.AddChild(BookStorePermissions.Banka_.Transaction,
            L($"{localizePrefix}:{nameof(BookStorePermissions.Banka_)}{BookStorePermissions.TransactionConst}"));

        //Banka Şube
        var bankaSube = mainGroup.AddPermission(BookStorePermissions.BankaSube.Default,
            L($"{localizePrefix}:{nameof(BookStorePermissions.BankaSube)}"));

        bankaSube.AddChild(BookStorePermissions.BankaSube.Create,
            L($"{localizePrefix}:{nameof(BookStorePermissions.BankaSube)}{BookStorePermissions.CreateConst}"));
        bankaSube.AddChild(BookStorePermissions.BankaSube.Update,
            L($"{localizePrefix}:{nameof(BookStorePermissions.BankaSube)}{BookStorePermissions.UpdateConst}"));
        bankaSube.AddChild(BookStorePermissions.BankaSube.Delete,
            L($"{localizePrefix}:{nameof(BookStorePermissions.BankaSube)}{BookStorePermissions.DeleteConst}"));

        // Books
        var books = mainGroup.AddPermission(BookStorePermissions.Books.Default,
            L($"{localizePrefix}:{nameof(BookStorePermissions.Books)}"));

        books.AddChild(BookStorePermissions.Books.Create,
            L($"{localizePrefix}:{nameof(BookStorePermissions.Books)}{BookStorePermissions.CreateConst}"));
        books.AddChild(BookStorePermissions.Books.Edit,
            L($"{localizePrefix}:{nameof(BookStorePermissions.Books)}{BookStorePermissions.UpdateConst}"));
        books.AddChild(BookStorePermissions.Books.Delete,
            L($"{localizePrefix}:{nameof(BookStorePermissions.Books)}{BookStorePermissions.DeleteConst}"));

        //Il
        var il = mainGroup.AddPermission(BookStorePermissions.Il.Default,
            L($"{localizePrefix}:{nameof(BookStorePermissions.Il)}"));

        il.AddChild(BookStorePermissions.Il.Create,
          L($"{localizePrefix}:{nameof(BookStorePermissions.Il)}{BookStorePermissions.CreateConst}"));//Permission:Il.Create
        il.AddChild(BookStorePermissions.Il.Update,
            L($"{localizePrefix}:{nameof(BookStorePermissions.Il)}{BookStorePermissions.UpdateConst}"));//Permission:Il.Update
        il.AddChild(BookStorePermissions.Il.Delete,
            L($"{localizePrefix}:{nameof(BookStorePermissions.Il)}{BookStorePermissions.DeleteConst}"));
        //IlCe
        var ilce = mainGroup.AddPermission(BookStorePermissions.Ilce.Default,
            L($"{localizePrefix}:{nameof(BookStorePermissions.Ilce)}"));

        ilce.AddChild(BookStorePermissions.Ilce.Create,
            L($"{localizePrefix}:{nameof(BookStorePermissions.Ilce)}{BookStorePermissions.CreateConst}"));
        ilce.AddChild(BookStorePermissions.Ilce.Update,
            L($"{localizePrefix}:{nameof(BookStorePermissions.Ilce)}{BookStorePermissions.UpdateConst}"));
        ilce.AddChild(BookStorePermissions.Ilce.Delete,
            L($"{localizePrefix}:{nameof(BookStorePermissions.Ilce)}{BookStorePermissions.DeleteConst}"));
        //kisi
        var kisi = mainGroup.AddPermission(BookStorePermissions.Kisi.Default,
            L($"{localizePrefix}:{nameof(BookStorePermissions.Kisi)}"));

        kisi.AddChild(BookStorePermissions.Kisi.Create,
            L($"{localizePrefix}:{nameof(BookStorePermissions.Kisi)}{BookStorePermissions.CreateConst}"));
        kisi.AddChild(BookStorePermissions.Kisi.Update,
            L($"{localizePrefix}:{nameof(BookStorePermissions.Kisi)}{BookStorePermissions.UpdateConst}"));
        kisi.AddChild(BookStorePermissions.Kisi.Delete,
            L($"{localizePrefix}:{nameof(BookStorePermissions.Kisi)}{BookStorePermissions.DeleteConst}"));

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
