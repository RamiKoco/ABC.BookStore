namespace ABC.BookStore.Kisiler;
public class EfCoreKisiRepository : EfCoreCommonRepository<Kisi>, IKisiRepository
{
    public EfCoreKisiRepository(IDbContextProvider<BookStoreDbContext> dbContextProvider) 
        : base(dbContextProvider)
    {        
    }
    public override async Task<IQueryable<Kisi>> WithDetailsAsync()
    {
        return (await GetQueryableAsync())
            .Include(x => x.OzelKod1)
            .Include(x => x.OzelKod2)
            .Include(x => x.Il)
            .Include(x => x.Ilce);
    }
}