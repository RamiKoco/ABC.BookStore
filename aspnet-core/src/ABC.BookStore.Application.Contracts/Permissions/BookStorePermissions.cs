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
}
