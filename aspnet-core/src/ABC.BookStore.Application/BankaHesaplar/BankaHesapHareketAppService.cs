namespace ABC.BookStore.BankaHesaplar;
public class BankaHesapHareketAppService : BookStoreAppService, IBankaHesapHareketAppService
{
    private readonly IMakbuzHareketRepository _makbuzHareketRepository;

    public BankaHesapHareketAppService(IMakbuzHareketRepository makbuzHareketRepository)
    {
        _makbuzHareketRepository = makbuzHareketRepository;
    }

    public virtual async Task<PagedResultDto<ListOdemeBelgesiHareketDto>> GetListAsync(
        MakbuzHareketListParameterDto input)
    {
        var hareketler = await _makbuzHareketRepository.GetPagedListAsync(input.SkipCount, input.MaxResultCount,
            x => x.BankaHesapId == input.EntityId &&
                 x.Makbuz.SubeId == input.SubeId && x.Makbuz.DonemId == input.DonemId && x.Makbuz.Durum,
            x => x.Makbuz.Tarih,
            x => x.Makbuz);

        var totalCount = await _makbuzHareketRepository.CountAsync(x => x.BankaHesapId == input.EntityId &&
                                                                        x.Makbuz.SubeId == input.SubeId &&
                                                                        x.Makbuz.DonemId == input.DonemId &&
                                                                        x.Makbuz.Durum);

        var mappedDtos = ObjectMapper.Map<List<MakbuzHareket>, List<ListOdemeBelgesiHareketDto>>(hareketler);
        mappedDtos.ForEach(x =>
        {
            x.OdemeTuruAdi = L[$"Enum:OdemeTuru:{(byte)x.OdemeTuru}"];
            x.MakbuzTuruAdi = L[$"Enum:MakbuzTuru:{(byte)x.MakbuzTuru}"];
            x.BelgeDurumuAdi = L[$"Enum:BelgeDurumu:{(byte)x.BelgeDurumu}"];
        });

        return new PagedResultDto<ListOdemeBelgesiHareketDto>(totalCount, mappedDtos);
    }

    public virtual Task<SelectMakbuzHareketDto> GetAsync(Guid id) => throw new NotImplementedException();

    public virtual Task<SelectMakbuzHareketDto> CreateAsync(MakbuzHareketDto input) => throw new NotImplementedException();

    public virtual Task<SelectMakbuzHareketDto> UpdateAsync(Guid id, MakbuzHareketDto input) =>
        throw new NotImplementedException();

    public virtual Task DeleteAsync(Guid id) => throw new NotImplementedException();

    public virtual Task<string> GetCodeAsync(MakbuzNoParameterDto input) => throw new NotImplementedException();
}