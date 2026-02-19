namespace ABC.BookStore.Bankalar;
public class EfCoreBankaRepository : EfCoreCommonRepository<Banka>, IBankaRepository
{
    public EfCoreBankaRepository(IDbContextProvider<BookStoreDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }
    public override async Task<IQueryable<Banka>> WithDetailsAsync()
    {
        return (await GetQueryableAsync())
            .Include(x => x.OzelKod1)
            .Include(x => x.OzelKod2)
            .Include(x => x.OzelKod3)
            .Include(x => x.OzelKod4)
            .Include(x => x.OzelKod5);
            //.Include(x => x.Il)
            //.Include(x => x.Ilce);
    }
}