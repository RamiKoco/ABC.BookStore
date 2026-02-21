namespace ABC.BookStore.Hizmetler;
public class EfCoreHizmetRepository : EfCoreCommonRepository<Hizmet>, IHizmetRepository
{
    public EfCoreHizmetRepository(IDbContextProvider<BookStoreDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<Hizmet>> WithDetailsAsync()
    {
        return (await GetQueryableAsync())
            .Include(x => x.Birim)
            .Include(x => x.OzelKod1)
            .Include(x => x.OzelKod2)
            .Include(x => x.OzelKod3)
            .Include(x => x.OzelKod4)
            .Include(x => x.OzelKod5)
            .Include(x => x.FaturaHareketler).ThenInclude(x => x.Fatura);
    }
}