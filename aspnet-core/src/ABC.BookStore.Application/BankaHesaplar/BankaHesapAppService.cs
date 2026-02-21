namespace ABC.BookStore.BankaHesaplar;

[Authorize(BookStorePermissions.BankaHesap.Default)]
public class BankaHesapAppService : BookStoreAppService, IBankaHesapAppService
{
    private readonly IBankaHesapRepository _bankaHesapRepository;
    private readonly BankaHesapManager _bankaHesapManager;
    public BankaHesapAppService(IBankaHesapRepository bankaHesapRepository,
        BankaHesapManager bankaHesapManager)
    {
        _bankaHesapRepository = bankaHesapRepository;
        _bankaHesapManager = bankaHesapManager;
    }

    public virtual async Task<SelectBankaHesapDto> GetAsync(Guid id)
    {
        var entity = await _bankaHesapRepository.GetAsync(id, x => x.Id == id,
            x => x.BankaSube, 
            x => x.BankaSube.Banka,           
            x => x.OzelKod1, 
            x => x.OzelKod2, 
            x => x.OzelKod3,
            x => x.OzelKod4,
            x => x.OzelKod5);

        var mappedDto = ObjectMapper.Map<BankaHesap, SelectBankaHesapDto>(entity);
        mappedDto.HesapTuruAdi = L[$"Enum:BankaHesapTuru:{(byte)mappedDto.HesapTuru}"];       

        return mappedDto;
    }
    public virtual async Task<PagedResultDto<ListBankaHesapDto>> GetListAsync(
        BankaHesapListParameterDto input)
    {
        var entities = await _bankaHesapRepository.GetPagedListAsync(input.SkipCount,
            input.MaxResultCount, 
            x => input.HesapTuru == null ? x.SubeId == input.SubeId && x.CariId == input.CariId &&
            x.Durum == input.Durum : x.HesapTuru == input.HesapTuru &&
            x.SubeId == input.SubeId && x.CariId == input.CariId && x.Durum == input.Durum,
            x => x.Kod,
            x => x.BankaSube, x => x.BankaSube.Banka, 
            x => x.OzelKod1, 
            x => x.OzelKod2,
            x => x.OzelKod3,
            x => x.OzelKod4,
            x => x.OzelKod5,
            x => x.MakbuzHareketler);

        var totalCount = await _bankaHesapRepository.CountAsync(x => input.HesapTuru == null ?
              x.SubeId == input.SubeId && x.CariId == input.CariId &&
              x.Durum == input.Durum : x.HesapTuru == input.HesapTuru && x.CariId == input.CariId &&
              x.SubeId == input.SubeId &&  x.Durum == input.Durum);

        var mappedDtos = ObjectMapper.Map<List<BankaHesap>, List<ListBankaHesapDto>>(entities);

        mappedDtos.ForEach(x =>
        {
            x.HesapTuruAdi = L[$"Enum:BankaHesapTuru:{(byte)x.HesapTuru}"];

            x.Borc = x.MakbuzHareketler.Where(y => y.BelgeDurumu == BelgeDurumu.TahsilEdildi ||
                     y.OdemeTuru == OdemeTuru.Pos && y.BelgeDurumu == BelgeDurumu.Portfoyde)
                      .Sum(y => y.Tutar);

            x.Alacak = x.MakbuzHareketler.Where(y => y.BelgeDurumu == BelgeDurumu.Odendi)
                        .Sum(y => y.Tutar);
        });

        return new PagedResultDto<ListBankaHesapDto>(totalCount, mappedDtos);
    }

    [Authorize(BookStorePermissions.BankaHesap.Create)]
    public virtual async Task<SelectBankaHesapDto> CreateAsync(CreateBankaHesapDto input)
    {
        await _bankaHesapManager.CheckCreateAsync(input.Kod, input.BankaSubeId, input.CariId,
            input.OzelKod1Id, 
            input.OzelKod2Id,
            input.OzelKod3Id,
            input.OzelKod4Id,
            input.OzelKod5Id,
            input.SubeId);

        var entity = ObjectMapper.Map<CreateBankaHesapDto, BankaHesap>(input);
        await _bankaHesapRepository.InsertAsync(entity);
        return ObjectMapper.Map<BankaHesap, SelectBankaHesapDto>(entity);
    }
    [Authorize(BookStorePermissions.BankaHesap.Update)]
    public virtual async Task<SelectBankaHesapDto> UpdateAsync(Guid id,
        UpdateBankaHesapDto input)
    {
        var entity = await _bankaHesapRepository.GetAsync(id, x => x.Id == id);

        await _bankaHesapManager.CheckUpdateAsync(id, input.Kod, entity, input.BankaSubeId,
            input.OzelKod1Id, 
            input.OzelKod2Id,
            input.OzelKod3Id,
            input.OzelKod4Id,
            input.OzelKod5Id);

        var mappedEntity = ObjectMapper.Map(input, entity);
        await _bankaHesapRepository.UpdateAsync(mappedEntity);
        return ObjectMapper.Map<BankaHesap, SelectBankaHesapDto>(mappedEntity);
    }
    [Authorize(BookStorePermissions.BankaHesap.Delete)]
    public virtual async Task DeleteAsync(Guid id)
    {
        await _bankaHesapManager.CheckDeleteAsync(id);
        await _bankaHesapRepository.DeleteAsync(id);
    }
    public virtual async Task<string> GetCodeAsync(BankaHesapCodeParameterDto input)
    {
        return await _bankaHesapRepository.GetCodeAsync(x => x.Kod,
            x => x.SubeId == input.SubeId && x.Durum == input.Durum);
    }
}