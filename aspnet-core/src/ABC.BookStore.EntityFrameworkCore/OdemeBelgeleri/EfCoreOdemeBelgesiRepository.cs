namespace ABC.BookStore.OdemeBelgeleri;
public class EfCoreOdemeBelgesiRepository : EfCoreCommonRepository<OdemeBelgesi>, IOdemeBelgesiRepository
{
    public EfCoreOdemeBelgesiRepository(IDbContextProvider<BookStoreDbContext> dbContextProvider) 
        : base(dbContextProvider)
    {        
    }
}