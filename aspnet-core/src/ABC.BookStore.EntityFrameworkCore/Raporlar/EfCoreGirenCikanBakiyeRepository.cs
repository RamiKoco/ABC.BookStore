namespace ABC.BookStore.Raporlar;
public class EfCoreGirenCikanBakiyeRepository : EfCoreCommonNoKeyRepository<GirenCikanBakiye>,
    IGirenCikanBakiyeRepository
{
    public EfCoreGirenCikanBakiyeRepository(IDbContextProvider<BookStoreDbContext> dbContextProvider) 
        : base(dbContextProvider)
    {
    }
}