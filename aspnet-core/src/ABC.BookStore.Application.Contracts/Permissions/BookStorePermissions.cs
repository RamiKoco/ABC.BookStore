namespace ABC.BookStore.Permissions;

public static class BookStorePermissions
{
    public const string GroupName = "BookStore";

    public const string CreateConst = ".Create";
    public const string UpdateConst = ".Update";
    public const string DeleteConst = ".Delete";

    public static class Books
    {
        public const string Default = $"{GroupName}.{nameof(Books)}";
        public const string Create = Default + CreateConst;
        public const string Edit = Default + UpdateConst;
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
    public static class OzelKod
    {
        public const string Default = $"{GroupName}.{nameof(OzelKod)}";
        public const string Create = Default + CreateConst;
        public const string Update = Default + UpdateConst;
        public const string Delete = Default + DeleteConst;
    }

}
