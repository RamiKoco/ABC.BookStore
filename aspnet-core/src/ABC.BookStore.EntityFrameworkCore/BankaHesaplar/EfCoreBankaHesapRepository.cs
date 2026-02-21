namespace ABC.BookStore.BankaHesaplar;
public class EfCoreBankaHesapRepository : EfCoreCommonRepository<BankaHesap>, IBankaHesapRepository
{
    public EfCoreBankaHesapRepository(IDbContextProvider<BookStoreDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }
}