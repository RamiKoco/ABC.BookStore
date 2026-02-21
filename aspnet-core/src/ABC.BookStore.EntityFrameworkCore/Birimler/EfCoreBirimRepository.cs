namespace ABC.BookStore.Birimler;
public class EfCoreBirimRepository : EfCoreCommonRepository<Birim>, IBirimRepository
{
    public EfCoreBirimRepository(IDbContextProvider<BookStoreDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }
}