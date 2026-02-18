using ABC.BookStore.Commons;
using ABC.BookStore.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace ABC.BookStore.Books;

public class EfCoreBookRepository : EfCoreCommonRepository<Book>, IBookRepository
{
    public EfCoreBookRepository(IDbContextProvider<BookStoreDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<Book>> WithDetailsAsync()
    {
        return (await GetQueryableAsync())
            .Include(x => x.OzelKod1)
            .Include(x => x.OzelKod2)
            .Include(x => x.OzelKod3)
            .Include(x => x.OzelKod4)
            .Include(x => x.OzelKod5);
    }
}