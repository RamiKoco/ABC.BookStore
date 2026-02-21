namespace ABC.BookStore.Permissions;

public static class BookStorePermissions
{
    public const string GroupName = "BookStore";

    public const string CreateConst = ".Create";
    public const string UpdateConst = ".Update";
    public const string DeleteConst = ".Delete";
    public const string TransactionConst = ".Transaction";

    public static class Banka_
    {
        public const string Default = $"{GroupName}.{nameof(Banka_)}";//BookStore.Banka
        public const string Create = Default + CreateConst;//BookStore.Banka.Create
        public const string Update = Default + UpdateConst;
        public const string Delete = Default + DeleteConst;
        public const string Transaction = Default + TransactionConst;
    }
    public static class BankaSube
    {
        public const string Default = $"{GroupName}.{nameof(BankaSube)}";
        public const string Create = Default + CreateConst;
        public const string Update = Default + UpdateConst;
        public const string Delete = Default + DeleteConst;
    }
    public static class BankaHesap
    {
        public const string Default = $"{GroupName}.{nameof(BankaHesap)}";
        public const string Create = Default + CreateConst;
        public const string Update = Default + UpdateConst;
        public const string Delete = Default + DeleteConst;
        public const string Transaction = Default + TransactionConst;
    }
    public static class Birim
    {
        public const string Default = $"{GroupName}.{nameof(Birim)}";
        public const string Create = Default + CreateConst;
        public const string Update = Default + UpdateConst;
        public const string Delete = Default + DeleteConst;
    }
    public static class Books
    {
        public const string Default = $"{GroupName}.{nameof(Books)}";
        public const string Create = Default + CreateConst;
        public const string Edit = Default + UpdateConst;
        public const string Delete = Default + DeleteConst;
    }
    public static class Cari
    {
        public const string Default = $"{GroupName}.{nameof(Cari)}";
        public const string Create = Default + CreateConst;
        public const string Update = Default + UpdateConst;
        public const string Delete = Default + DeleteConst;
        public const string Transaction = Default + TransactionConst;
    }
    public static class Depo
    {
        public const string Default = $"{GroupName}.{nameof(Depo)}";
        public const string Create = Default + CreateConst;
        public const string Update = Default + UpdateConst;
        public const string Delete = Default + DeleteConst;
        public const string Transaction = Default + TransactionConst;
    }
    public static class Donem
    {
        public const string Default = $"{GroupName}.{nameof(Donem)}";
        public const string Create = Default + CreateConst;
        public const string Update = Default + UpdateConst;
        public const string Delete = Default + DeleteConst;
    }
    public static class Il
    {
        public const string Default = $"{GroupName}.{nameof(Il)}";//BookStore.Il
        public const string Create = Default + CreateConst;//BookStore.Il.Create
        public const string Update = Default + UpdateConst;
        public const string Delete = Default + DeleteConst;
    }
    public static class Ilce
    {
        public const string Default = $"{GroupName}.{nameof(Ilce)}";
        public const string Create = Default + CreateConst;
        public const string Update = Default + UpdateConst;
        public const string Delete = Default + DeleteConst;
    }
    public static class Kisi
    {
        public const string Default = $"{GroupName}.{nameof(Kisi)}";
        public const string Create = Default + CreateConst;
        public const string Update = Default + UpdateConst;
        public const string Delete = Default + DeleteConst;
    }
    public static class OzelKod
    {
        public const string Default = $"{GroupName}.{nameof(OzelKod)}";
        public const string Create = Default + CreateConst;
        public const string Update = Default + UpdateConst;
        public const string Delete = Default + DeleteConst;
    }
    public static class Stok
    {
        public const string Default = $"{GroupName}.{nameof(Stok)}";
        public const string Create = Default + CreateConst;
        public const string Update = Default + UpdateConst;
        public const string Delete = Default + DeleteConst;
        public const string Transaction = Default + TransactionConst;
    }
    public static class Sube
    {
        public const string Default = $"{GroupName}.{nameof(Sube)}";
        public const string Create = Default + CreateConst;
        public const string Update = Default + UpdateConst;
        public const string Delete = Default + DeleteConst;
    }

}
