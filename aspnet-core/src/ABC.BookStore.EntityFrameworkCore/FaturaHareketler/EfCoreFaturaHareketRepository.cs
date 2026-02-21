using ABC.BookStore.Faturalar;
namespace ABC.BookStore.FaturaHareketler;
public class EfCoreFaturaHareketRepository : EfCoreCommonRepository<FaturaHareket>, 
    IFaturaHareketRepository
{
    public EfCoreFaturaHareketRepository(IDbContextProvider<BookStoreDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }
}