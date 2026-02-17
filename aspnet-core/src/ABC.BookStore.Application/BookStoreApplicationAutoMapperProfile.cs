using ABC.BookStore.Books;
using AutoMapper;

namespace ABC.BookStore;

public class BookStoreApplicationAutoMapperProfile : Profile
{
    public BookStoreApplicationAutoMapperProfile()
    {
        CreateMap<Book, SelectBookDto>();
        CreateMap<Book, ListBookDto>();
        CreateMap<CreateBookDto, Book>();
        CreateMap<UpdateBookDto, Book>();
    }
}
