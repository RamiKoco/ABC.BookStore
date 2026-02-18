namespace ABC.BookStore.Ilceler;
public class EFCoreIlceRepository : EfCoreCommonRepository<Ilce>, IIlceRepository
{
    public EFCoreIlceRepository(IDbContextProvider<BookStoreDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }
}