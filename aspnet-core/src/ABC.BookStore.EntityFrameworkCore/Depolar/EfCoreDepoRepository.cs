namespace ABC.BookStore.Depolar;

public class EfCoreDepoRepository : EfCoreCommonRepository<Depo>, IDepoRepository
{
    public EfCoreDepoRepository(IDbContextProvider<BookStoreDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<Depo>> WithDetailsAsync()
    {
        return (await GetQueryableAsync())
            .Include(x => x.OzelKod1)
            .Include(x => x.OzelKod2)
            .Include(x => x.OzelKod3)
            .Include(x => x.OzelKod4)
            .Include(x => x.OzelKod5)
            .Include(x => x.FaturaHareketler).ThenInclude(x => x.Fatura);
    }
}