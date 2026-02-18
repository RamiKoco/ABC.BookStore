using ABC.BookStore.Commons;
using ABC.BookStore.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace ABC.BookStore.OzelKodlar;
public class EfCoreOzelKodRepository : EfCoreCommonRepository<OzelKod>, IOzelKodRepository
{
    public EfCoreOzelKodRepository(IDbContextProvider<BookStoreDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }
}
