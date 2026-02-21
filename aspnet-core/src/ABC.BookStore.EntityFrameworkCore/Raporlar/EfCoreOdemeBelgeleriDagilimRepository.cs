namespace ABC.BookStore.Raporlar;
public class EfCoreOdemeBelgeleriDagilimRepository : EfCoreCommonNoKeyRepository<OdemeBelgeleriDagilim>,
    IOdemeBelgeleriDagilimRepository
{
    public EfCoreOdemeBelgeleriDagilimRepository(IDbContextProvider<BookStoreDbContext> dbContextProvider) 
        : base(dbContextProvider)
    {
    }
}