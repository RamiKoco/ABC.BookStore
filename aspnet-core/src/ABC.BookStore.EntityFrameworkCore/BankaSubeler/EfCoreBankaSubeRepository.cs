namespace ABC.BookStore.BankaSubeler;
public class EfCoreBankaSubeRepository : EfCoreCommonRepository<BankaSube>, IBankaSubeRepository
{
    public EfCoreBankaSubeRepository(IDbContextProvider<BookStoreDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }
}