using ABC.BookStore.Bankalar;
using ABC.BookStore.BankaSubeler;
using ABC.BookStore.Donemler;
using ABC.BookStore.Ilceler;
using ABC.BookStore.Iller;
using ABC.BookStore.Kisiler;
using ABC.BookStore.Parametreler;
using ABC.BookStore.Subeler;

namespace ABC.BookStore;
public class BookStoreApplicationAutoMapperProfile : Profile
{
    public BookStoreApplicationAutoMapperProfile()
    {
        //Banka
        CreateMap<Banka, SelectBankaDto>()
            .ForMember(x => x.OzelKod1Adi, y => y.MapFrom(z => z.OzelKod1.Ad))
            .ForMember(x => x.OzelKod2Adi, y => y.MapFrom(z => z.OzelKod2.Ad))
            .ForMember(x => x.OzelKod3Adi, y => y.MapFrom(z => z.OzelKod3.Ad))
            .ForMember(x => x.OzelKod4Adi, y => y.MapFrom(z => z.OzelKod4.Ad))
            .ForMember(x => x.OzelKod5Adi, y => y.MapFrom(z => z.OzelKod5.Ad));

        CreateMap<Banka, ListBankaDto>()
            .ForMember(x => x.OzelKod1Adi, y => y.MapFrom(z => z.OzelKod1.Ad))
            .ForMember(x => x.OzelKod2Adi, y => y.MapFrom(z => z.OzelKod2.Ad))
            .ForMember(x => x.OzelKod3Adi, y => y.MapFrom(z => z.OzelKod3.Ad))
            .ForMember(x => x.OzelKod4Adi, y => y.MapFrom(z => z.OzelKod4.Ad))
            .ForMember(x => x.OzelKod5Adi, y => y.MapFrom(z => z.OzelKod5.Ad));

        CreateMap<CreateBankaDto, Banka>();
        CreateMap<UpdateBankaDto, Banka>();
        CreateMap<SelectBankaDto, CreateBankaDto>();
        CreateMap<SelectBankaDto, UpdateBankaDto>();

        //Bankasube
        CreateMap<BankaSube, SelectBankaSubeDto>()
            .ForMember(x => x.BankaAdi, y => y.MapFrom(z => z.Banka.Ad))
            .ForMember(x => x.OzelKod1Adi, y => y.MapFrom(z => z.OzelKod1.Ad))
            .ForMember(x => x.OzelKod2Adi, y => y.MapFrom(z => z.OzelKod2.Ad))
            .ForMember(x => x.OzelKod3Adi, y => y.MapFrom(z => z.OzelKod3.Ad))
            .ForMember(x => x.OzelKod4Adi, y => y.MapFrom(z => z.OzelKod4.Ad))
            .ForMember(x => x.OzelKod5Adi, y => y.MapFrom(z => z.OzelKod5.Ad));

        CreateMap<BankaSube, ListBankaSubeDto>()
            .ForMember(x => x.BankaAdi, y => y.MapFrom(z => z.Banka.Ad))
            .ForMember(x => x.OzelKod1Adi, y => y.MapFrom(z => z.OzelKod1.Ad))
            .ForMember(x => x.OzelKod2Adi, y => y.MapFrom(z => z.OzelKod2.Ad))
            .ForMember(x => x.OzelKod3Adi, y => y.MapFrom(z => z.OzelKod3.Ad))
            .ForMember(x => x.OzelKod4Adi, y => y.MapFrom(z => z.OzelKod4.Ad))
            .ForMember(x => x.OzelKod5Adi, y => y.MapFrom(z => z.OzelKod5.Ad));

        CreateMap<CreateBankaSubeDto, BankaSube>();
        CreateMap<UpdateBankaSubeDto, BankaSube>();
        CreateMap<SelectBankaSubeDto, CreateBankaSubeDto>();
        CreateMap<SelectBankaSubeDto, UpdateBankaSubeDto>();

        //Book
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

        //Donem
        CreateMap<Donem, SelectDonemDto>();
        CreateMap<Donem, ListDonemDto>();
        CreateMap<CreateDonemDto, Donem>();
        CreateMap<UpdateDonemDto, Donem>();
        CreateMap<SelectDonemDto, CreateDonemDto>();
        CreateMap<SelectDonemDto, UpdateDonemDto>();

        //Firma Parametre
        CreateMap<FirmaParametre, SelectFirmaParametreDto>()
            .ForMember(x => x.SubeAdi, y => y.MapFrom(z => z.Sube.Ad))
            .ForMember(x => x.DonemAdi, y => y.MapFrom(z => z.Donem.Ad));

        CreateMap<CreateFirmaParametreDto, FirmaParametre>();
        CreateMap<UpdateFirmaParametreDto, FirmaParametre>();

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

        //kisi
        CreateMap<Kisi, SelectKisiDto>()           
            .ForMember(x => x.IlAdi, y => y.MapFrom(z => z.Il.Ad))
            .ForMember(x => x.IlceAdi, y => y.MapFrom(z => z.Ilce.Ad))   
            .ForMember(x => x.OzelKod1Adi, y => y.MapFrom(z => z.OzelKod1.Ad))
            .ForMember(x => x.OzelKod2Adi, y => y.MapFrom(z => z.OzelKod2.Ad));

        CreateMap<Kisi, ListKisiDto>()            
            .ForMember(x => x.IlAdi, y => y.MapFrom(z => z.Il.Ad))
            .ForMember(x => x.IlceAdi, y => y.MapFrom(z => z.Ilce.Ad))
            .ForMember(x => x.OzelKod1Adi, y => y.MapFrom(z => z.OzelKod1.Ad))
            .ForMember(x => x.OzelKod2Adi, y => y.MapFrom(z => z.OzelKod2.Ad));

        CreateMap<CreateKisiDto, Kisi>();
        CreateMap<UpdateKisiDto, Kisi>();
        CreateMap<SelectKisiDto, CreateKisiDto>();
        CreateMap<SelectKisiDto, UpdateKisiDto>();

        //OzelKod
        CreateMap<OzelKod, SelectOzelKodDto>();
        CreateMap<OzelKod, ListOzelKodDto>();
        CreateMap<CreateOzelKodDto, OzelKod>();
        CreateMap<UpdateOzelKodDto, OzelKod>();
        CreateMap<SelectOzelKodDto, CreateOzelKodDto>();
        CreateMap<SelectOzelKodDto, UpdateOzelKodDto>();

        //Sube
        CreateMap<Sube, SelectSubeDto>();
        CreateMap<Sube, ListSubeDto>();
        CreateMap<CreateSubeDto, Sube>();
        CreateMap<UpdateSubeDto, Sube>();
        CreateMap<SelectSubeDto, CreateSubeDto>();
        CreateMap<SelectSubeDto, UpdateSubeDto>();
    }
}
