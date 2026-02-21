using ABC.BookStore.Makbuzlar;
namespace ABC.BookStore.MakbuzHareketler;
public class EfCoreMakbuzHareketRepository : EfCoreCommonRepository<MakbuzHareket>, IMakbuzHareketRepository
{
    public EfCoreMakbuzHareketRepository(IDbContextProvider<BookStoreDbContext> dbContextProvider) 
        : base(dbContextProvider)
    {
    }
}