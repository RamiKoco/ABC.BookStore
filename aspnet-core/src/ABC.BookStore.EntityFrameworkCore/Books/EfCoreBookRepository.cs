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
}