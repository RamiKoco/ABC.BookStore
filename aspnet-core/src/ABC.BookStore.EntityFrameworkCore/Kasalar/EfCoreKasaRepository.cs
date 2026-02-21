namespace ABC.BookStore.Kasalar;
public class EfCoreKasaRepository : EfCoreCommonRepository<Kasa>, IKasaRepository
{
    public EfCoreKasaRepository(IDbContextProvider<BookStoreDbContext> dbContextProvider) 
        : base(dbContextProvider)
    {
    }
}