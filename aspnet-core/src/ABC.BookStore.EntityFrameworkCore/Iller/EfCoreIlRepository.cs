namespace ABC.BookStore.Iller;
public class EfCoreIlRepository : EfCoreCommonRepository<Il>, IIlRepository
{
    public EfCoreIlRepository(IDbContextProvider<BookStoreDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }
}