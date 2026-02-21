namespace ABC.BookStore.Cariler;
public class EfCoreCariRepository : EfCoreCommonRepository<Cari>, ICariRepository
{
    public EfCoreCariRepository(IDbContextProvider<BookStoreDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }
    public override async Task<IQueryable<Cari>> WithDetailsAsync()
    {
        return (await GetQueryableAsync())         
            .Include(x => x.OzelKod1)
            .Include(x => x.OzelKod2)
            .Include(x => x.OzelKod3)
            .Include(x => x.OzelKod4)
            .Include(x => x.OzelKod5)

            .Include(x => x.CariSubeler).ThenInclude(x => x.Aciklama);
    }
}