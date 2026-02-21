namespace ABC.BookStore.Depolar;
public class DepoHareketAppService : BookStoreAppService, IDepoHareketAppService
{
    private readonly IFaturaHareketRepository _faturaHareketRepository;

    public DepoHareketAppService(IFaturaHareketRepository faturaHareketRepository)
    {
        _faturaHareketRepository = faturaHareketRepository;
    }

    public virtual async Task<PagedResultDto<ListStokHareketDto>> GetListAsync(DepoHareketListParameterDto input)
    {
        var hareketler = await _faturaHareketRepository.GetPagedListAsync(input.SkipCount, input.MaxResultCount,
            x => x.DepoId == input.DepoId &&
                 x.Fatura.SubeId == input.SubeId &&
                 x.Fatura.DonemId == input.DonemId &&
                 x.Fatura.Durum,
            x => x.Fatura.Tarih,
            x => x.Fatura, x => x.Stok.Birim);

        var totalCount = await _faturaHareketRepository.CountAsync(x => x.DepoId == input.DepoId &&
                                                                        x.Fatura.SubeId == input.SubeId &&
                                                                        x.Fatura.DonemId == input.DonemId &&
                                                                        x.Fatura.Durum);

        var mappedDtos = ObjectMapper.Map<List<FaturaHareket>, List<ListStokHareketDto>>(hareketler);
        mappedDtos.ForEach(x =>
        {
            x.BelgeTuru = L[$"Enum:FaturaTuru:{(byte)x.FaturaTuru}"];
            x.HareketTuruAdi = L[$"Enum:FaturaHareketTuru:{(byte)x.HareketTuru}"];
        });

        return new PagedResultDto<ListStokHareketDto>(totalCount, mappedDtos);
    }

    public virtual Task<SelectFaturaHareketDto> GetAsync(Guid id) => throw new NotImplementedException();

    public virtual Task<SelectFaturaHareketDto> CreateAsync(FaturaHareketDto input) => throw new NotImplementedException();

    public virtual Task<SelectFaturaHareketDto> UpdateAsync(Guid id, FaturaHareketDto input) =>
        throw new NotImplementedException();

    public virtual Task DeleteAsync(Guid id) => throw new NotImplementedException();

    public virtual Task<string> GetCodeAsync(FaturaNoParameterDto input) => throw new NotImplementedException();
}