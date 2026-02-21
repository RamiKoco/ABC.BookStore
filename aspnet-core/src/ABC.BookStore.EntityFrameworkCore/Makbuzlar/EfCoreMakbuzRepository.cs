namespace ABC.BookStore.Makbuzlar;
public class EfCoreMakbuzRepository : EfCoreCommonRepository<Makbuz>, IMakbuzRepository
{
    public EfCoreMakbuzRepository(IDbContextProvider<BookStoreDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<Makbuz>> WithDetailsAsync()
    {
        return (await GetQueryableAsync())
            .Include(x => x.Cari)
            .Include(x => x.Kasa)
            .Include(x => x.BankaHesap)
            .Include(x => x.OzelKod1)
            .Include(x => x.OzelKod2)
            .Include(x => x.OzelKod3)
            .Include(x => x.OzelKod4)
            .Include(x => x.OzelKod5)
            .Include(x => x.MakbuzHareketler).ThenInclude(x => x.CekBanka)
            .Include(x => x.MakbuzHareketler).ThenInclude(x => x.CekBankaSube)
            .Include(x => x.MakbuzHareketler).ThenInclude(x => x.Kasa)
            .Include(x => x.MakbuzHareketler).ThenInclude(x => x.BankaHesap);
    }
}