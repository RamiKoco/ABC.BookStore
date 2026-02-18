namespace ABC.BookStore;
public class BookStoreApplicationAutoMapperProfile : Profile
{
    public BookStoreApplicationAutoMapperProfile()
    {
        //banka
        CreateMap<Book, SelectBookDto>()
            .ForMember(x => x.OzelKod1Adi, y => y.MapFrom(z => z.OzelKod1.Ad))
            .ForMember(x => x.OzelKod2Adi, y => y.MapFrom(z => z.OzelKod2.Ad))
            .ForMember(x => x.OzelKod3Adi, y => y.MapFrom(z => z.OzelKod3.Ad))
            .ForMember(x => x.OzelKod4Adi, y => y.MapFrom(z => z.OzelKod4.Ad))
            .ForMember(x => x.OzelKod5Adi, y => y.MapFrom(z => z.OzelKod5.Ad));

        CreateMap<Book, ListBookDto>()
            .ForMember(x => x.OzelKod1Adi, y => y.MapFrom(z => z.OzelKod1.Ad))
            .ForMember(x => x.OzelKod2Adi, y => y.MapFrom(z => z.OzelKod2.Ad))
            .ForMember(x => x.OzelKod3Adi, y => y.MapFrom(z => z.OzelKod3.Ad))
            .ForMember(x => x.OzelKod4Adi, y => y.MapFrom(z => z.OzelKod4.Ad))
            .ForMember(x => x.OzelKod5Adi, y => y.MapFrom(z => z.OzelKod5.Ad));

        CreateMap<CreateBookDto, Book>();
        CreateMap<UpdateBookDto, Book>();
        CreateMap<SelectBookDto, CreateBookDto>();
        CreateMap<SelectBookDto, UpdateBookDto>();

        //OzelKod
        CreateMap<OzelKod, SelectOzelKodDto>();
        CreateMap<OzelKod, ListOzelKodDto>();
        CreateMap<CreateOzelKodDto, OzelKod>();
        CreateMap<UpdateOzelKodDto, OzelKod>();
        CreateMap<SelectOzelKodDto, CreateOzelKodDto>();
        CreateMap<SelectOzelKodDto, UpdateOzelKodDto>();

    }
}
