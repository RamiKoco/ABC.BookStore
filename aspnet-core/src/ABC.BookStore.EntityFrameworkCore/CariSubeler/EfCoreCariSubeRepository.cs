namespace ABC.BookStore.CariSubeler;
public class EfCoreCariSubeRepository : EfCoreCommonRepository<CariSube>,
    ICariSubeRepository
{
    public EfCoreCariSubeRepository(IDbContextProvider<BookStoreDbContext> dbContextProvider)
        : base(dbContextProvider)
    {        
    }
}
