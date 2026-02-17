using ABC.BookStore.CommonDtos;
using ABC.BookStore.Services;

namespace ABC.BookStore.Books;

public interface IBookAppService : ICrudAppService<SelectBookDto, ListBookDto,
    BookListParameterDto, CreateBookDto, UpdateBookDto, CodeParameterDto>
{
}