using System;
using System.Threading.Tasks;
using ABC.BookStore.Extensions;
using Volo.Abp.Domain.Services;

namespace ABC.BookStore.Books;

public class BookManager : DomainService
{
    private readonly IBookRepository _bookRepository;

    public BookManager(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task CheckCreateAsync(string kod)
    {
        await _bookRepository.KodAnyAsync(kod, x => x.Kod == kod);
    }

    public async Task CheckUpdateAsync(Guid id, string kod, Book entity)
    {
        await _bookRepository.KodAnyAsync(kod, x => x.Id != id && x.Kod == kod,
            entity.Kod != kod);
    }

    public async Task CheckDeleteAsync(Guid id)
    {
        // İlişkili entity varsa burada kontrol edilir
    }
}