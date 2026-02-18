using ABC.BookStore.Ilceler;
using ABC.BookStore.Iller;

namespace ABC.BookStore;
public class BookStoreApplicationAutoMapperProfile : Profile
{
    public BookStoreApplicationAutoMapperProfile()
    {
        //banka
        CreateMap<Book, SelectBookDto>()
            .ForMember(x => x.IlAdi, y => y.MapFrom(z => z.Il.Ad))
            .ForMember(x => x.IlceAdi, y => y.MapFrom(z => z.Ilce.Ad))
            .ForMember(x => x.OzelKod1Adi, y => y.MapFrom(z => z.OzelKod1.Ad))
            .ForMember(x => x.OzelKod2Adi, y => y.MapFrom(z => z.OzelKod2.Ad))
            .ForMember(x => x.OzelKod3Adi, y => y.MapFrom(z => z.OzelKod3.Ad))
            .ForMember(x => x.OzelKod4Adi, y => y.MapFrom(z => z.OzelKod4.Ad))
            .ForMember(x => x.OzelKod5Adi, y => y.MapFrom(z => z.OzelKod5.Ad));

        CreateMap<Book, ListBookDto>()
            .ForMember(x => x.IlAdi, y => y.MapFrom(z => z.Il.Ad))
            .ForMember(x => x.IlceAdi, y => y.MapFrom(z => z.Ilce.Ad))
            .ForMember(x => x.OzelKod1Adi, y => y.MapFrom(z => z.OzelKod1.Ad))
            .ForMember(x => x.OzelKod2Adi, y => y.MapFrom(z => z.OzelKod2.Ad))
            .ForMember(x => x.OzelKod3Adi, y => y.MapFrom(z => z.OzelKod3.Ad))
            .ForMember(x => x.OzelKod4Adi, y => y.MapFrom(z => z.OzelKod4.Ad))
            .ForMember(x => x.OzelKod5Adi, y => y.MapFrom(z => z.OzelKod5.Ad));

        CreateMap<CreateBookDto, Book>();
        CreateMap<UpdateBookDto, Book>();
        CreateMap<SelectBookDto, CreateBookDto>();
        CreateMap<SelectBookDto, UpdateBookDto>();
        //Il
        CreateMap<Il, SelectIlDto>();
        CreateMap<Il, ListIlDto>();

        CreateMap<CreateIlDto, Il>();
        CreateMap<UpdateIlDto, Il>();
        CreateMap<SelectIlDto, CreateIlDto>();
        CreateMap<SelectIlDto, UpdateIlDto>();

        //Ilce
        CreateMap<Ilce, SelectIlceDto>()
            .ForMember(x => x.IlAdi, y => y.MapFrom(z => z.Il.Ad));

        CreateMap<Ilce, ListIlceDto>()
            .ForMember(x => x.IlAdi, y => y.MapFrom(z => z.Il.Ad));

        CreateMap<CreateIlceDto, Ilce>();
        CreateMap<UpdateIlceDto, Ilce>();
        CreateMap<SelectIlceDto, CreateIlceDto>();
        CreateMap<SelectIlceDto, UpdateIlceDto>();

        //OzelKod
        CreateMap<OzelKod, SelectOzelKodDto>();
        CreateMap<OzelKod, ListOzelKodDto>();
        CreateMap<CreateOzelKodDto, OzelKod>();
        CreateMap<UpdateOzelKodDto, OzelKod>();
        CreateMap<SelectOzelKodDto, CreateOzelKodDto>();
        CreateMap<SelectOzelKodDto, UpdateOzelKodDto>();

    }
}
